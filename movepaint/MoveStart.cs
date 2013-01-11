using System;
using io.thp.psmove;
using System.Threading;

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

		public MoveStart ()
		{

		}

		public static void Main ()
		{
		}

		public void MoveLoop ()
		{
			while (true) {
				UpdateMove ();
				if (brk)
					break;
			}
		}

		private ReaderWriterLock rwLock = new ReaderWriterLock();

		public void UpdateMove ()
		{
			while (move.poll() != 0) {
				move.get_magnetometer_vector (out mx, out my, out mz);

				rwLock.AcquireWriterLock(1000);
				trigger = move.get_trigger ();
				lr = (int)((mx + 1) * 127.5);
				lg = (int)((my + 1) * 127.5);
				lb = (int)((mz + 1) * 127.5);
				rwLock.ReleaseWriterLock();

				move.set_leds (lr, lg, lb);
				move.set_rumble (trigger);
				move.update_leds ();					
				Console.WriteLine (lr + " " + lg + " " + lb);

						

				brk = move.get_buttons () == (int)Button.Btn_MOVE;
				if (brk)
					break;
			}

		}
	}
}
