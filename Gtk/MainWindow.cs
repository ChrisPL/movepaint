using System;
using Gtk;
using Cairo;
using movepaint;
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

	private int x = 0;
	private int y = 0;

	private int radius = 20;

	private movepaint.MoveStart move;
	private Thread moveThread;
	private Thread paintThread;

	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		move = new MoveStart();
		moveThread = new Thread(move.MoveLoop);
		moveThread.Start();
		if(moveThread.IsAlive)
		{
			paintThread = new Thread(Paint);
			paintThread.Start();
		}
	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		moveThread.Abort();
		Application.Quit ();
		a.RetVal = true;
	}

	private ReaderWriterLock rwLock = new ReaderWriterLock();

	void Paint ()
	{
		DrawingArea area = drawingarea1;
		Cairo.Context cc = Gdk.CairoHelper.Create (area.GdkWindow);

		while (true) {
		
			int width, height;
			width = Allocation.Width;
			height = Allocation.Height;

			rwLock.AcquireReaderLock(1000);
			R = move.Lr;
			G = move.Lg;
			B = move.Lb;
			radius = move.Trigger;

			x = width / 2;
			y = height / 2;
			rwLock.ReleaseReaderLock();


			cc.LineWidth = 1;
			cc.SetSourceRGB (r, g, b);
			cc.Arc (x, y, radius, 0, 2 * Math.PI);
			
			cc.SetSourceRGB (r, g, b);
			cc.StrokePreserve ();
			cc.Fill ();
			if(!moveThread.IsAlive)
				break;
		}

		((IDisposable) cc.Target).Dispose();
		((IDisposable) cc).Dispose();
	}


}
