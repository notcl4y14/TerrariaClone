using Raylib_cs;

namespace Common;

class Content
{
	public static string ContentPath = "./Content/";

	private static Dictionary<string, Texture2D> _Textures = [];
	private static Dictionary<string, Tilemap> _Tilemaps = [];

	public static Dictionary<string, Texture2D> Textures { get { return _Textures; } }
	public static Dictionary<string, Tilemap> Tilemaps { get { return _Tilemaps; } }

	public static bool LoadTexture(string name, string path)
	{
		Texture2D texture = Raylib.LoadTexture(ContentPath + path);

		if (_Textures.ContainsKey(name))
		{
			return false;
		}

		_Textures.Add(name, texture);
		return true;
	}

	public static bool LoadTilemap(string name, string textureName, int[] tileDimensions, int[] interval)
	{
		Tilemap tilemap = new Tilemap(tileDimensions[0], tileDimensions[1], interval[0], interval[1]);
		tilemap.Texture = _Textures[textureName];

		if (_Tilemaps.ContainsKey(name))
		{
			return false;
		}

		_Tilemaps.Add(name, tilemap);
		return true;
	}
}