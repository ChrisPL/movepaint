using System;
using io.thp.psmove;

namespace movepaint
{
	public class MoveTracker
	{
		private PSMoveTracker tracker;
		PSMoveTrackerRGBImage image;

		public MoveTracker (PSMove move)
		{
			tracker = new PSMoveTracker();
			while(tracker.enable(move) != Status.Tracker_CALIBRATED);
			tracker.set_auto_update_leds(move,1);
			image = tracker.get_image();
		}

		public PSMoveTrackerRGBImage GetImage ()
		{
			image = tracker.get_image();
			return image;
		}

		public void Update()
		{
			tracker.update_image();
			tracker.update();
		}

		public void SetAutoUpdateLEDs (PSMove move, int binary)
		{
			tracker.set_auto_update_leds(move, 1);
		}

		public void GetPosition(PSMove move, out float x, out float y, out float radius)
		{
			x =0; y=0; radius =0;
			tracker.get_position(move,out x,out y,out radius);
		}

		public float CalculateDistance (float radius)
		{
			return tracker.distance_from_radius(radius);
		}

		public Status GetStatus(PSMove move)
		{
			return tracker.get_status(move);
		}
	}
}

