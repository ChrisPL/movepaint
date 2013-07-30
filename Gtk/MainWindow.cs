using System;
using Gtk;
using Cairo;
using Gdk;
using System.Threading;


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

	private int radius = 20;

	//public Camera cameraWindow = new Camera();
	//public ColorPicker colorWindow = new ColorPicker();

	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		this.DefaultSize = new Size(640,480);
		Build ();
		bkg = drawingarea1.GdkWindow;
	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}


	public void SetColor(int r, int g, int b)
	{
		colorbutton1.Color = new Gdk.Color((byte)r,(byte)g,(byte)b);
	}


	public void Paint (int trigger, int mx, int my, byte[] image)
	{
		DrawingArea area = drawingarea1;
		Cairo.Context cc = Gdk.CairoHelper.Create (area.GdkWindow);

		//cameraWindow.UpdateImage((byte[])image);

//		int width, height;
//		width = Allocation.Width;
//		height = Allocation.Height;

		radius = trigger/2;

		r = (double)colorbutton1.Color.Red / 65535;
		g = (double)colorbutton1.Color.Green / 65535;
		b = (double)colorbutton1.Color.Blue / 65535;

		cc.LineWidth = 1;
		cc.SetSourceRGB (r, g, b);
		cc.Arc (mx, my, radius, 0, 2 * Math.PI);
		
		cc.SetSourceRGB (r, g, b);
		cc.StrokePreserve ();
		cc.Fill ();

		//bkg = area.GdkWindow;

		((IDisposable) cc.Target).Dispose();
		((IDisposable) cc).Dispose();
	}

	Gdk.Drawable bkg;

	public void DrawCursor (int mx, int my)
	{
		Cairo.Context cc = Gdk.CairoHelper.Create(bkg);

		cc.LineWidth = 1;
		cc.SetSourceRGB (0,0,0);
		cc.Rectangle(mx-1,my-1,2,2);
		cc.Fill ();

		((IDisposable) cc.Target).Dispose();
		((IDisposable) cc).Dispose();
	}
}
