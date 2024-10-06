using Raylib_cs;

namespace Core;

class Window
{
	private static string _Title = "";
	public static int Width = 0;
	public static int Height = 0;

	public static string Title
	{
		get {
			return _Title;
		}

		set {
			_Title = value;
			Raylib.SetWindowTitle(value);
		}
	}
}