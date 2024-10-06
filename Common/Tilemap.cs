using System.Numerics;
using Raylib_cs;

namespace Common;

class Tilemap
{
	public Texture2D Texture;
	public int TileWidth;
	public int TileHeight;
	public int IntervalX;
	public int IntervalY;

	public Tilemap(int tileWidth, int tileHeight, int intervalX, int intervalY)
	{
		TileWidth = tileWidth;
		TileHeight = tileHeight;
		IntervalX = intervalX;
		IntervalY = intervalY;
	}

	public void LoadTexture(Texture2D texture)
	{
		Texture = texture;
	}

	public void LoadTexture(string textureName)
	{
		Texture = Raylib.LoadTexture(textureName);
	}

	public Texture2D TileAsTexture(int x, int y)
	{
		var sourceRect = new Rectangle(x * (TileWidth + IntervalX), y * (TileHeight + IntervalY), TileWidth, TileHeight);
		return Raylib.LoadTextureFromImage(Raylib.ImageFromImage(Raylib.LoadImageFromTexture(Texture), sourceRect));
	}

	public void Draw(int x, int y, int tx, int ty)
	{
		var sourceRect = new Rectangle(tx * (TileWidth + IntervalX), ty * (TileHeight + IntervalY), TileWidth, TileHeight);
		var destRect = new Rectangle(x, y, TileWidth, TileHeight);
		Raylib.DrawTexturePro(Texture, sourceRect, destRect, new Vector2(0, 0), 0, Color.White);
	}
}