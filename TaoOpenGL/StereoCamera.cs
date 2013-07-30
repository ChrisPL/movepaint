using System;
using Tao.OpenGl;
using Tao.FreeGlut;

namespace TaoOpenGL
{
	public class StereoCamera
	{
		double mConvergence;
		double mEyeSeparation;
		double mAspectRatio;
		double mFOV;
		double mNearClippingDistance;
        double mFarClippingDistance;

		public StereoCamera (
			double convergence,
			double eyeSeparation,
			double aspectRatio,
			double fov,
			double nearClippingDistance,
			double farClippingDistance
			)
		{
			mConvergence            = convergence;
	        mEyeSeparation          = eyeSeparation;
	        mAspectRatio            = aspectRatio;
	        mFOV                    = fov * Math.PI / 180.0f;
	        mNearClippingDistance   = nearClippingDistance;
	        mFarClippingDistance    = farClippingDistance;
		}

		public void ApplyLeftFrustum()
	    {
	        double top, bottom, left, right;

	        top     = mNearClippingDistance * Math.Tan(mFOV/2);
	        bottom  = -top;

	        double a = mAspectRatio * Math.Tan(mFOV/2) * mConvergence;

	        double b = a - mEyeSeparation/2;
	        double c = a + mEyeSeparation/2;

	        left    = -b * mNearClippingDistance/mConvergence;
	        right   =  c * mNearClippingDistance/mConvergence;

	        // Set the Projection Matrix
			Gl.glMatrixMode(Gl.GL_PROJECTION);
	        Gl.glLoadIdentity();   
	        Gl.glFrustum(left, right, bottom, top, 
	                  mNearClippingDistance, mFarClippingDistance);

	        // Displace the world to right
	        Gl.glMatrixMode(Gl.GL_MODELVIEW);                     
	        Gl.glLoadIdentity();   
	        Gl.glTranslatef((float)mEyeSeparation/2, 0.0f, 0.0f);
	    }

	    public void ApplyRightFrustum()
	    {
	        double top, bottom, left, right;

	        top     = mNearClippingDistance * Math.Tan(mFOV/2);
	        bottom  = -top;

	        double a = mAspectRatio * Math.Tan(mFOV/2) * mConvergence;

	        double b = a - mEyeSeparation/2;
	        double c = a + mEyeSeparation/2;

	        left    =  -c * mNearClippingDistance/mConvergence;
	        right   =   b * mNearClippingDistance/mConvergence;

	        // Set the Projection Matrix
	        Gl.glMatrixMode(Gl.GL_PROJECTION);                        
	        Gl.glLoadIdentity();   
	        Gl.glFrustum(left, right, bottom, top, 
	                  mNearClippingDistance, mFarClippingDistance);

	        // Displace the world to left
	        Gl.glMatrixMode(Gl.GL_MODELVIEW);                     
	        Gl.glLoadIdentity();   
	        Gl.glTranslatef((float)-mEyeSeparation/2, 0.0f, 0.0f);
	    }

	}
}

