using Core.World;
using Raylib_cs;

namespace Common;

class Cursor
{
	public int X;
	public int Y;

	public int TileID;

	public void SetTile(Chunk chunk)
	{
		Brush.TileID = TileID;
		Brush.SetTile(chunk, X, Y);
	}

	public void EraseTile(Chunk chunk)
	{
		Brush.EraseTile(chunk, X, Y);
	}

	public void DrawRect()
	{
		Raylib.DrawRectangleLines(X * Tile.WIDTH, Y * Tile.HEIGHT, Tile.WIDTH, Tile.HEIGHT, Color.Blue);
	}
}