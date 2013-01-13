using System;
using Gtk;

namespace GtkWin
{
	public class GtkMainClass
	{
		public static void Main()
		{
		}


		public static void Start (System.Object win)
		{
			Application.Init ();
			((MainWindow)win).Show ();
			Application.Run ();
		}


	}
}
