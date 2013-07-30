using System;

namespace Gtk
{
	public partial class ColorPicker : Gtk.Window
	{
		public ColorPicker () : 
				base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
		}

		public Gdk.Color CurrentColor{
			get{
				return colorselection1.CurrentColor;
			}
		}
	}
}

