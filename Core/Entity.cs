using Common;
using Core.World;
using Raylib_cs;

namespace Core;

class Entity
{
	public int ID = 0;
	public double[] Position = [0, 0];
	public double[] Size = [0, 0];
	
	public double[] Velocity = [0, 0];
	public double Accel = 0;
	public double Frict = 0;

	public bool Overlaps(Entity other)
	{
		return Position[0] < other.Position[0] + other.Size[0]
		    && other.Position[0] < Position[0] + Size[0]
			&& Position[1] < other.Position[1] + other.Size[1]
		    && other.Position[1] < Position[1] + Size[1];
	}

	public int Separate(Entity other)
	{
		double centerX = other.Position[0] + other.Size[0] / 2;
		double centerY = other.Position[1] + other.Size[1] / 2;

		double dx = Position[0] - centerX;
		double dy = Position[1] - centerY;

		double x1 = Math.Max(Position[0], other.Position[0]);
		double y1 = Math.Max(Position[1], other.Position[1]);
		double x2 = Math.Min(Position[0] + Size[0], other.Position[0] + other.Size[0]);
		double y2 = Math.Min(Position[1] + Size[1], other.Position[1] + other.Size[1]);

		double interWidth = x2 - x1;
		double interHeight = y2 - y1;

		double vx = interWidth * Math.Sign(dx);
		double vy = interHeight * Math.Sign(dy);

		if (interWidth < interHeight)
		{
			Position[0] += vx;
			return 0;
		}
		else
		{
			Position[1] += vy;
			return 1;
		}
	}

	public void Move(double X, double Y)
	{
		Position[0] += X;
		Position[1] += Y;
	}

	public void Move(double[] pos)
	{
		Position[0] += pos[0];
		Position[1] += pos[1];
	}

	public void ApplyVelocity()
	{
		Move(Velocity);
	}

	public void ApplyAccel(double multX, double multY)
	{
		Velocity[0] += Accel * multX;
		Velocity[1] += Accel * multY;
	}

	public void ApplyFrict(double multX, double multY)
	{
		Velocity[0] -= Frict * multX;
		Velocity[1] -= Frict * multY;
	}

	public void ApplyPhysics(Chunk chunk)
	{
		Velocity[1] += Physics.Gravity;
		ApplyVelocity();

		int[] tilePos = [(int)Math.Round(Position[0] / Tile.WIDTH), (int)Math.Round(Position[1] / Tile.HEIGHT)];
		int[] tileSize = [(int)Math.Round(Size[0] / Tile.WIDTH), (int)Math.Round(Size[1] / Tile.HEIGHT)];

		Entity[] entTiles = chunk.GetTileHitboxes(
			tilePos[0] - 1,
			tilePos[1] - 1,
			tilePos[0] + tileSize[0] + 1,
			tilePos[1] + tileSize[1] + 1
		);

		foreach (Entity entTile in entTiles)
		{
			entTile.DrawHitbox();
			
			if (!Overlaps(entTile))
			{
				continue;
			}

			// TODO: Check collision after velocity is applied
			int xy = Separate(entTile);

			switch (xy)
			{
				case 0:
					Velocity[0] = 0;
					break;
				
				case 1:
					Velocity[1] = 0;
					break;
			}
		}
	}

	public virtual void Draw()
	{
		DrawHitbox();
	}

	public void DrawHitbox()
	{
		Raylib.DrawRectangleLines((int)Position[0], (int)Position[1], (int)Size[0], (int)Size[1], Color.Blue);
	}
}