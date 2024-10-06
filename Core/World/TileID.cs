namespace Core.World;

class TileID
{
	public static int Dirt = 0;
	public static int Stone = 1;
	public static int Grass = 3;

	public static List<string> Tilemap = [];

	public static void Load()
	{
		Tilemap.Add("Tiles_0");
		Tilemap.Add("Tiles_1");
	}
}