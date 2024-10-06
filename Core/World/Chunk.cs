using Common;
using Raylib_cs;

namespace Core.World;

class Chunk
{
	private List<Tile?> Data;
	private int DataLength;
	public int Width;
	public int Height;

	public Chunk(int width, int height)
	{
		Data = new List<Tile?>();
		Width = width;
		Height = height;
		InitData();
	}

	// ==== Data ==== //

	private void InitData()
	{
		int area = Width * Height;

		DataLength = area;

		for (int i = 0; i < area; i++)
		{
			Data.Add(null);
		}
	}

	// ==== Converters ==== //

	public int[] ToPos(int index)
	{
		int x = index % Width;
		int y = (int)(index / Width);

		return [x, y];
	}

	public int ToIndex(int x, int y)
	{
		return y * Width + x;
	}

	// ==== Checkers ==== //

	public bool IsPosOut(int x, int y)
	{
		return x < 0 || x >= Width || y < 0 || y >= Height;
	}

	// ==== Tile ==== //

	public void EraseTile(int x, int y)
	{
		Data[ToIndex(x, y)] = null;
	}

	public void SetTile(int id, int x, int y)
	{
		Tile tile = new Tile();
		tile.Tilemap = Content.Tilemaps[TileID.Tilemap[id]];
		tile.TilemapPos = [1, 1];
		tile.ID = id;
		Data[ToIndex(x, y)] = tile;
	}

	public void SetTileBound(int id, int x, int y)
	{
		if (!IsPosOut(x, y)) return;
		SetTile(id, x, y);
		// Tile tile = new Tile();
		// tile.Tilemap = Game.Tilemap;
		// tile.TilemapPos = [1, 1];
		// tile.ID = id;
		// Data[ToIndex(x, y)] = tile;
	}

	public Tile? GetTile(int x, int y)
	{
		return Data[ToIndex(x, y)];
	}

	public Tile? GetTileBound(int x, int y)
	{
		return !IsPosOut(x, y) ? Data[ToIndex(x, y)] : null;
	}

	public void UpdateTilesAround(int x, int y)
	{
		GetTileBound(x + 1, y)?.UpdateSprite(this, [x + 1, y]);
		GetTileBound(x - 1, y)?.UpdateSprite(this, [x - 1, y]);
		GetTileBound(x, y - 1)?.UpdateSprite(this, [x, y - 1]);
		GetTileBound(x, y + 1)?.UpdateSprite(this, [x, y + 1]);
	}

	public void UpdateTiles()
	{
		for (int i = 0; i != DataLength; i++)
		{
			Tile? tile = Data[i];

			if (tile == null)
			{
				continue;
			}

			int[] pos = ToPos(i);

			tile.UpdateSprite(this, pos);
		}
	}

	// ==== Draw ==== //

	public void Draw()
	{
		for (int i = 0; i != DataLength; i++)
		{
			Tile? tile = Data[i];

			if (tile == null)
			{
				continue;
			}

			int[] pos = ToPos(i);

			var x = Tile.WIDTH * pos[0];
			var y = Tile.HEIGHT * pos[1];

			// Raylib.DrawRectangleLines(x, y, Tile.WIDTH, Tile.HEIGHT, Color.Blue);
			tile.Tilemap.Draw(x, y, tile.TilemapPos[0], tile.TilemapPos[1]);
			// Raylib.DrawTexture(tile.Texture, x, y, Color.White);
		}
	}

	public void DrawBorders()
	{
		Raylib.DrawRectangleLines(0, 0, Width * Tile.WIDTH, Height * Tile.HEIGHT, Color.Black);
	}
}