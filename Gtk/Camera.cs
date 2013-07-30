using System;

namespace Gtk
{
	public partial class Camera : Gtk.Window
	{
		public Camera () : 
				base(Gtk.WindowType.Toplevel)
		{
			this.DefaultHeight = 480;
			this.DefaultWidth = 640;
			this.Build ();

			this.Show();
		}

		public void UpdateImage(byte[] data)
		{
			cameraImage.Pixbuf = new Gdk.Pixbuf(data);
		}
	}
}

