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

		public int Trigger {
			get {
				return trigger;
			}
			set {
				trigger = value;
			}
		}

		int trigger = 0;
		float mx, my, mz;

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
		bool brk = false;

		Thread windowThread;

		public MoveStart ()
		{
			Application.Init();
			win = new MainWindow();
			win.Show();
			moveThread = new Thread(MoveLoop);
			moveThread.Start();
			Application.Run();
//			windowThread = new Thread(Application.Run);
//			windowThread.Start();
			moveThread.Join();
		}

		MainWindow win;
		Thread moveThread;

		public static void Main ()
		{
			new MoveStart();
		}

		public void MoveLoop ()
		{
			
//			Random rnd = new Random();
			while (true) {
				UpdateMove ();
//				TestVariables(rnd);
				win.Paint(Lr,Lg,Lb,trigger,0,0);
				Thread.Sleep(1);
				if (brk)
					break;
			}
		}

		private ReaderWriterLock rwLock = new ReaderWriterLock();

		private void TestVariables(Random rnd)
		{
			lr = rnd.Next()%256;
			lg = rnd.Next()%256;
			lb = rnd.Next()%256;
			trigger = rnd.Next()%256;
		}

		public void UpdateMove ()
		{
			while (move.poll() != 0) {


				rwLock.AcquireWriterLock(1000);
				
				trigger = move.get_trigger ();				
				move.set_rumble (trigger);

				if(trigger == 0)
					return;

				move.get_magnetometer_vector (out mx, out my, out mz);

				lr = (int)((mx + 1) * 127.5);
				lg = (int)((my + 1) * 127.5);
				lb = (int)((mz + 1) * 127.5);
				rwLock.ReleaseWriterLock();

				move.set_leds (lr, lg, lb);
				move.update_leds ();					
//				Console.WriteLine (lr + " " + lg + " " + lb);


				brk = move.get_buttons () == (int)io.thp.psmove.Button.Btn_MOVE;
				if (brk)
					break;
			}

		}
	}
}
