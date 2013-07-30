using System;
using io.thp.psmove;
using System.Threading;
using GtkWin;
using Gtk;

namespace movepaint
{
	public class MoveStart
	{
		PSMove move = new PSMove ();
		MoveTracker tracker;

		#region Public variables

		public int Trigger {
			get {
				return trigger;
			}
			set {
				trigger = value;
			}
		}

		int trigger = 0;
		float mx = 0, my = 0, mz = 0;

		public float Mx {
			get {
				return mx;
			}
		}

		public float My {
			get {
				return my;
			}
		}

		public float Mz {
			get {
				return mz;
			}
		}

		float x, y, radius;

		public int Radius {
			get {
				return (int)radius;
			}
			set {
				radius = value;
			}
		}

		public int Y {
			get {
				return (int)y;
			}
			set {
				y = value;
			}
		}

		public int X {
			get {
				return 640 - (int)x;
			}
			set {
				x = value;
			}
		}

		public int Lr {
			get {
				return lr;
			}
			set {
				lr = value;
			}
		}

		public int Lg {
			get {
				return lg;
			}
			set {
				lg = value;
			}
		}

		public int Lb {
			get {
				return lb;
			}
			set {
				lb = value;
			}
		}

		int lr, lg, lb;
		private bool isShouldRender;

		public bool IsShouldRender {
			get {
				return isShouldRender;
			}
			set {
				isShouldRender = value;
			}
		}

		public bool IsTracking {
			get {
				return tracker.GetStatus (move) == Status.Tracker_TRACKING;
			}
		}

		private bool isMoveButton = false;

		public bool IsMoveButton {
			get { 
				return isMoveButton; 
			}
		}

		private bool startDrawing = false;

		public bool StartDrawing {
			get {
				return startDrawing;
			}
		}

		private bool isCrossBtnReleased = false;

		public bool IsCrossBtnReleased {
			get {
				return isCrossBtnReleased;
			}
		}

		private bool isCircleBtnReleased = false;

		public bool IsCircleBtnReleased {
			get {
				return isCircleBtnReleased;
			}
		}

		private bool isSelectBtnReleased = false;

		public bool IsSelectBtnReleased {
			get {
				return isSelectBtnReleased;
			}
		}

		private bool isSquareBtnReleased = false;

		public bool IsSquareBtnReleased {
			get {
				return isSquareBtnReleased;
			}
		}

		private bool isTriangleBtnReleased = false;

		public bool IsTriangleBtnReleased {
			get {
				return isTriangleBtnReleased;
			}
		}

		private bool isStartBtnReleased = false;

		public bool IsStartBtnReleased {
			get {
				return isStartBtnReleased;
			}
		}

		#endregion

		public MoveStart ()
		{
			isShouldRender = true;
			//Application.Init();
			//win = new MainWindow();
			moveThread = new Thread (MoveLoop);
			moveThread.Start ();
			//Application.Run();
		}

		//MainWindow win;
		Thread moveThread;

		public static void Main ()
		{
			new MoveStart ();
		}

		public void MoveLoop ()
		{
			tracker = new MoveTracker (move);
//			Random rnd = new Random();
			while (true) {
				tracker.Update ();
				UpdateMove ();
				if (tracker.GetStatus (move) == Status.Tracker_TRACKING) {
					isShouldRender = true;
				} else
					isShouldRender = false;

				//Thread.Sleep (5);
			}
		}

		private ReaderWriterLock rwLock = new ReaderWriterLock ();


		public void UpdateMove ()
		{
			while (move.poll() != 0) {


				rwLock.AcquireWriterLock (1000);
				
				trigger = move.get_trigger ();				
				move.set_rumble (trigger);

				move.get_magnetometer_vector (out mx, out my, out mz);
				if (tracker.GetStatus (move) == Status.Tracker_TRACKING) {
					tracker.GetPosition (move, out x, out y, out radius);
					//Console.WriteLine (x + " " + y + " " + radius);
				} else
					Console.WriteLine ("Not tracking");

				uint pressed = 0, released = 0;
				move.get_button_events(out pressed, out released);
//				int button = move.get_buttons ();
//				if (button == (int)io.thp.psmove.Button.Btn_CIRCLE)
//					lr = (int)((mx + 1) * 127.5);
//				else if (button == (int)io.thp.psmove.Button.Btn_TRIANGLE)
//					lg = (int)((mx + 1) * 127.5);
//				else if (button == (int)io.thp.psmove.Button.Btn_CROSS)
//					lb = (int)((mx + 1) * 127.5);
				
//				if(button != 0)
//					win.SetColor(lr,lg,lb);
				if (pressed == (int)io.thp.psmove.Button.Btn_MOVE)
					isMoveButton = true;
				if (released == (int)io.thp.psmove.Button.Btn_MOVE)
					isMoveButton = false;
				if (pressed == (int)io.thp.psmove.Button.Btn_T)
					startDrawing = true;
				if (released == (int)io.thp.psmove.Button.Btn_T)
					startDrawing = false;
				isCrossBtnReleased = pressed == (int)io.thp.psmove.Button.Btn_CROSS;
				isCircleBtnReleased = pressed == (int)io.thp.psmove.Button.Btn_CIRCLE;
				isSelectBtnReleased = pressed == (int)io.thp.psmove.Button.Btn_SELECT;
				isSquareBtnReleased = pressed == (int)io.thp.psmove.Button.Btn_SQUARE;
				isTriangleBtnReleased = pressed == (int)io.thp.psmove.Button.Btn_TRIANGLE;
				isStartBtnReleased = pressed == (int)io.thp.psmove.Button.Btn_START;
				rwLock.ReleaseWriterLock ();

				move.update_leds ();
//				Console.WriteLine (lr + " " + lg + " " + lb);
			}

		}
	}
}
