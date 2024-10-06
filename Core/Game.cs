using Raylib_cs;
using Core.World;
using Common;

namespace Core;

class Game
{
	public static Chunk Chunk;
	public static Cursor Cursor;

	// public static Tilemap Tilemap = new Common.Tilemap(16, 16, 2, 2);
	
	public static void Initialize()
	{
		InitializeWindow();
        // Tilemap.LoadTexture("Content/Tiles/Tiles_1.png");
		InitializeContent();
		TileID.Load();
		InitializeWorld();

		Cursor = new Cursor();
		Cursor.X = 0;
		Cursor.Y = 0;
		Cursor.TileID = 1;
	}

	public static void InitializeWindow()
	{
		const string title = "Terraria Clone v0.1";
		const int width = 800;
		const int height = 480;

		Raylib.SetTargetFPS(60);
		Raylib.InitWindow(width, height, title);
	}

	public static void InitializeContent()
	{
		Content.LoadTexture("Tiles_0", "Tiles/Tiles_0.png");
		Content.LoadTexture("Tiles_1", "Tiles/Tiles_1.png");

		Content.LoadTilemap("Tiles_0", "Tiles_0", [16, 16], [2, 2]);
		Content.LoadTilemap("Tiles_1", "Tiles_1", [16, 16], [2, 2]);
	}

	public static void InitializeWorld()
	{
		Chunk = new Chunk(8, 8);

		for (int x = 0; x < 8; x++)
		{
			for (int y = 4; y < 8; y++)
			{
				Chunk.SetTile(TileID.Stone, x, y);
			}
		}

		Chunk.UpdateTiles();
	}

	public static void Update()
	{
		int cursorMoveX = Raylib.IsKeyPressed(KeyboardKey.Right) - Raylib.IsKeyPressed(KeyboardKey.Left);
		int cursorMoveY = Raylib.IsKeyPressed(KeyboardKey.Down) - Raylib.IsKeyPressed(KeyboardKey.Up);

		Cursor.X += cursorMoveX;
		Cursor.Y += cursorMoveY;

		// if (Raylib.IsKeyPressed(KeyboardKey.Left))
		// {
		// 	Cursor.X -= 1;
		// }
		// else if (Raylib.IsKeyPressed(KeyboardKey.Right))
		// {
		// 	Cursor.X += 1;
		// }
		
		// if (Raylib.IsKeyPressed(KeyboardKey.Up))
		// {
		// 	Cursor.Y -= 1;
		// }
		// else if (Raylib.IsKeyPressed(KeyboardKey.Down))
		// {
		// 	Cursor.Y += 1;
		// }
		
		if (Raylib.IsKeyPressed(KeyboardKey.Space))
		{
			Cursor.SetTile(Chunk);
		}
		else if (Raylib.IsKeyPressed(KeyboardKey.Backspace))
		{
			Cursor.EraseTile(Chunk);
		}
		
		if (Raylib.IsKeyPressed(KeyboardKey.U))
		{
			Chunk.UpdateTiles();
		}
		
		if (Raylib.IsKeyPressed(KeyboardKey.One))
		{
			Cursor.TileID = 1;
		}
		else if (Raylib.IsKeyPressed(KeyboardKey.Zero))
		{
			Cursor.TileID = 0;
		}
	}

	public static void Draw()
	{
		Raylib.BeginDrawing();

		Raylib.ClearBackground(Color.White);

		Chunk.Draw();
		Chunk.DrawBorders();

		Cursor.DrawRect();

		Raylib.EndDrawing();
	}

	public static void Run()
	{
		while (!Raylib.WindowShouldClose())
		{
			Update();
			Draw();
		}

		Raylib.CloseWindow();
	}
}