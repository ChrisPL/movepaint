using System;
using Gtk;

namespace GtkWin
{
	public class GtkMainClass
	{
		public static MainWindow Win {
			get {
				return win;
			}
		}

		private static MainWindow win;

		public static void Main ()
		{
			Application.Init ();
			win = new MainWindow ();
			win.Show ();
			Application.Run ();
		}


	}
}
