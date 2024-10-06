using Common;
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

	public void Move(double X, double Y)
	{
		Position[0] += X;
		Position[1] += Y;
	}

	public void ApplyVelocity()
	{
		Move(Velocity[0], Velocity[1]);
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

	public void ApplyPhysics()
	{
		Velocity[1] += Physics.Gravity;
		ApplyVelocity();
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