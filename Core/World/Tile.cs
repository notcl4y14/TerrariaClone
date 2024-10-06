using Common;
using Raylib_cs;

namespace Core.World;

class Tile
{
	public int ID;
	public Tilemap Tilemap;
	public int[] TilemapPos = [0, 0];
	public static int WIDTH = 16;
	public static int HEIGHT = 16;

	public void UpdateSprite(Chunk chunk, int[] pos)
	{
		var top = !chunk.IsPosOut(pos[0], pos[1] - 1) ? chunk.GetTile(pos[0], pos[1] - 1) : null;
		var bottom = !chunk.IsPosOut(pos[0], pos[1] + 1) ? chunk.GetTile(pos[0], pos[1] + 1) : null;
		var left = !chunk.IsPosOut(pos[0] - 1, pos[1]) ? chunk.GetTile(pos[0] - 1, pos[1]) : null;
		var right = !chunk.IsPosOut(pos[0] + 1, pos[1]) ? chunk.GetTile(pos[0] + 1, pos[1]) : null;

		int[] match = [
			left?.ID == ID ? 1 : 0,
			right?.ID == ID ? 1 : 0,
			top?.ID == ID ? 1 : 0,
			bottom?.ID == ID ? 1 : 0
		];

		switch (match)
		{
			// Single block
			// [1]
			case [0, 0, 0, 0]:
				// Console.WriteLine($"{match[0]} {match[1]} {match[2]} {match[3]}");
				TilemapPos = [9, 3];
				break;

			// Left Side
			// [1][ ][ ]
			// [2][ ][ ]
			// [3][ ][ ]

			case [0, 1, 0, 1]:
				TilemapPos = [0, 3];
				break;
			case [0, 1, 1, 1]:
				TilemapPos = [0, 0];
				break;
			case [0, 1, 1, 0]:
				TilemapPos = [0, 4];
				break;

			// Middle Side
			// [ ][1][ ]
			// [ ][2][ ]
			// [ ][3][ ]

			case [1, 1, 0, 1]:
				TilemapPos = [1, 0];
				break;
			case [1, 1, 1, 1]:
				TilemapPos = [1, 1];
				break;
			case [1, 1, 1, 0]:
				TilemapPos = [1, 2];
				break;

			// Right Side
			// [ ][ ][1]
			// [ ][ ][2]
			// [ ][ ][3]

			case [1, 0, 0, 1]:
				TilemapPos = [1, 3];
				break;
			case [1, 0, 1, 1]:
				TilemapPos = [4, 0];
				break;
			case [1, 0, 1, 0]:
				TilemapPos = [1, 4];
				break;

			// Horizontal Line
			// [1][2][3]

			case [0, 1, 0, 0]:
				TilemapPos = [9, 0];
				break;
			case [1, 1, 0, 0]:
				TilemapPos = [6, 4];
				break;
			case [1, 0, 0, 0]:
				TilemapPos = [12, 0];
				break;

			// Vertical Line
			//    [1]
			//    [2]
			//    [3]

			case [0, 0, 0, 1]:
				TilemapPos = [6, 0];
				break;
			case [0, 0, 1, 1]:
				TilemapPos = [5, 0];
				break;
			case [0, 0, 1, 0]:
				TilemapPos = [6, 3];
				break;
		}

		// // Single block
		// // [1]

		// if (left?.ID != ID && top?.ID != ID && right?.ID != ID && bottom?.ID != ID)
		// {
		//	 TilemapPos = [9, 3];
		// }

		// // Left Side
		// // [1][ ][ ]
		// // [2][ ][ ]
		// // [3][ ][ ]
		
		// if (left?.ID != ID && top?.ID != ID)
		// {
		//	 TilemapPos = [0, 3];
		// }
		// else if (left?.ID != ID)
		// {
		//	 TilemapPos = [0, 0];
		// }
		// else if (left?.ID != ID && bottom?.ID != ID)
		// {
		//	 TilemapPos = [0, 4];
		// }

		// // Middle Side
		// // [ ][1][ ]
		// // [ ][2][ ]
		// // [ ][3][ ]
		
		// if (left?.ID == ID && right?.ID == ID && top?.ID != ID && bottom?.ID == ID)
		// {
		//	 TilemapPos = [1, 0];
		// }
		// else if (left?.ID == ID && right?.ID == ID && top?.ID == ID && bottom?.ID == ID)
		// {
		//	 TilemapPos = [1, 1];
		// }
		// else if (left?.ID == ID && right?.ID == ID && top?.ID == ID && bottom?.ID != ID)
		// {
		//	 TilemapPos = [1, 2];
		// }

		// // Right Side
		// // [ ][ ][1]
		// // [ ][ ][2]
		// // [ ][ ][3]
		
		// if (right?.ID != ID && top?.ID != ID)
		// {
		//	 TilemapPos = [1, 3];
		// }
		// else if (right?.ID != ID)
		// {
		//	 TilemapPos = [4, 0];
		// }
		// else if (right?.ID != ID && bottom?.ID != ID)
		// {
		//	 TilemapPos = [1, 4];
		// }

		// // Sides
		// //	[3]
		// // [1]   [2]
		// //	[4]
		
		// if (left?.ID != ID)
		// {
		//	 TilemapPos = [0, 0];
		// }
		// else if (right?.ID != ID)
		// {
		//	 TilemapPos = [4, 0];
		// }
		// else if (top?.ID != ID)
		// {
		//	 TilemapPos = [1, 0];
		// }
		// else if (bottom?.ID != ID)
		// {
		//	 TilemapPos = [1, 2];
		// }

		// // Corners
		// // [1] [3]
		// // [2] [4]

		// if (left?.ID != ID && top?.ID != ID)
		// {
		//	 TilemapPos = [0, 3];
		// }
		// else if (left?.ID != ID && bottom?.ID != ID)
		// {
		//	 TilemapPos = [0, 4];
		// }
		// else if (right?.ID != ID && top?.ID != ID)
		// {
		//	 TilemapPos = [1, 3];
		// }
		// else if (right?.ID != ID && bottom?.ID != ID)
		// {
		//	 TilemapPos = [1, 4];
		// }
	}
}