using System;
using Gtk;
using Cairo;


public partial class MainWindow: Gtk.Window
{	
	private double r = 0;
	private double g = 0;
	private double b = 0;

	public int R {
		get {
			return (int)(r * 255);
		}
		set { 
			r = (double)value / 255;
		}
	}

	public int G {
		get {
			return (int)(g * 255);
		}
		set {
			g = (double)value / 255;
		}
	}

	public int B {
		get {
			return (int)(b * 255);
		}
		set {
			b = (double)value / 255;
		}
	}

	private int x = 0;
	private int y = 0;

	private int radius = 20;


	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}


	public void Paint (int lr, int lg, int lb, int trigger, int mx, int my)
	{
		DrawingArea area = drawingarea1;
		Cairo.Context cc = Gdk.CairoHelper.Create (area.GdkWindow);

		
		int width, height;
		width = Allocation.Width;
		height = Allocation.Height;

		R = lr;
		G = lg;
		B = lb;
		radius = trigger;

		x = width / 2;
		y = height / 2;


		cc.LineWidth = 1;
		cc.SetSourceRGB (r, g, b);
		cc.Arc (x, y, radius, 0, 2 * Math.PI);
		
		cc.SetSourceRGB (r, g, b);
		cc.StrokePreserve ();
		cc.Fill ();

		((IDisposable) cc.Target).Dispose();
		((IDisposable) cc).Dispose();
	}


}
