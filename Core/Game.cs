using Raylib_cs;
using Core.World;
using Common;

namespace Core;

class Game
{
	public static Chunk Chunk;
	public static Cursor Cursor;

	public static Entity entity;
	
	public static void Initialize()
	{
		InitializeWindow();
		InitializeContent();
		TileID.Load();
		InitializeWorld();

		entity = new Entity();
		entity.Position = [0, 0];
		entity.Size = [16 * 2, 16 * 3];

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
		Content.LoadTexture("Tiles_2", "Tiles/Tiles_2.png");

		Content.LoadTilemap("Tiles_0", "Tiles_0", [16, 16], [2, 2]);
		Content.LoadTilemap("Tiles_1", "Tiles_1", [16, 16], [2, 2]);
		Content.LoadTilemap("Tiles_2", "Tiles_2", [16, 16], [2, 2]);
	}

	public static void InitializeWorld()
	{
		Chunk = new Chunk(25, 25);

		for (int x = 0; x < 25; x++)
		{
			for (int y = 15; y < 25; y++)
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
		else if (Raylib.IsKeyPressed(KeyboardKey.Two))
		{
			Cursor.TileID = 2;
		}
		else if (Raylib.IsKeyPressed(KeyboardKey.Zero))
		{
			Cursor.TileID = 0;
		}

		int playerMoveX = Raylib.IsKeyDown(KeyboardKey.D) - Raylib.IsKeyDown(KeyboardKey.A);

		entity.Position[0] += playerMoveX * 2;

		if (Raylib.IsKeyPressed(KeyboardKey.W))
		{
			entity.Velocity[1] = -8;
		}
	}

	public static void Draw()
	{
		Raylib.BeginDrawing();

		Raylib.ClearBackground(Color.White);

		Chunk.Draw();
		entity.ApplyPhysics(Chunk);
		entity.Draw();
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