using Core.World;

namespace Common;

class Brush
{
	public static int TileID = 0;
	public static bool UpdateSprites = true;

	public static void SetTile(Chunk chunk, int x, int y)
	{
		if (chunk.IsPosOut(x, y)) return;
		chunk.SetTile(TileID, x, y);
		chunk.GetTile(x, y)?.UpdateSprite(chunk, [x, y]);
		if (UpdateSprites) chunk.UpdateTilesAround(x, y);
	}

	public static void EraseTile(Chunk chunk, int x, int y)
	{
		if (chunk.IsPosOut(x, y)) return;
		chunk.EraseTile(x, y);
		if (UpdateSprites) chunk.UpdateTilesAround(x, y);
	}
}