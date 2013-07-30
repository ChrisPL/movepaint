using System;
using Tao.FreeGlut;
using Tao.OpenGl;
using movepaint;
using io.thp.psmove;

namespace TaoOpenGL
{
	class MainClass
	{
		static double cX = 0;
		static double cY = 0;
		static double cZ = 0;
		static MoveStart move;

		static Drawing drawing = new Drawing();
		static bool flagDraw = false;


		static void renderScene ()
		{
			// translate to appropriate depth along -Z
		    Gl.glTranslatef(0.0f, 0.0f, -1800.0f);

		    // rotate the scene for viewing
		    Gl.glRotatef(-60.0f, 1.0f, 0.0f, 0.0f);
		    Gl.glRotatef(-45.0f, 0.0f, 0.0f, 1.0f);


			Gl.glClear (Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);

			Gl.glLoadIdentity ();
			Glu.gluLookAt (0, 0, 0, cX, cY, cZ, 0, 1, 0);
			Gl.glRotated (floorAnlgle * 50, 1, 0, 0);
			Gl.glRotated (skyAngle * 50, 0, 1, 0);
			Gl.glRotated (cZ, 0, 0, 1);
			Gl.glPushMatrix ();
//			Gl.glBegin (Gl.GL_TRIANGLES);
//			Gl.glColor3f (1, 0, 0);
//			Gl.glVertex3d (-0.5, -0.5, 0.0);
//			Gl.glColor3f (0, 1, 0);
//			Gl.glVertex3d (0.5, 0.0, 0.0);
//			Gl.glColor3f (0, 0, 1);
//			Gl.glVertex3d (0.0, 0.5, 0.0);
//			Gl.glEnd ();
			Glut.glutWireCube (1);			
			Gl.glPopMatrix ();
			foreach (Shape s in drawing.History) {
				Gl.glPushMatrix();
				Gl.glTranslated( s.x, s.y, s.z);
				drawing.drawSolidShape(s.radius, s.shape, s.color);
				Gl.glPopMatrix();
			}
			if (!move.IsMoveButton) {
				Gl.glPushMatrix ();
				Gl.glTranslated (
					((move.Radius / 50.0) - 1.0) * -1.0, 
					((move.Y / 240.0) - 1.0) * -1.0, 
					(move.X / 240.0) - 1.0
				);
				if(move.StartDrawing)
				{
					drawing.drawWireShape(move.Trigger/255.0);
					flagDraw = true;
				}
				else
					if(flagDraw)
					{
						drawing.drawSolidShape(move.Trigger/255.0);
						flagDraw = false;	
					}
					else
						drawing.drawWireShape(0.1);


				Gl.glPopMatrix ();
			}
			Glut.glutSwapBuffers ();
		}

		static double? prevMx, prevMy, prevMz;
		static double floorAnlgle = 0, skyAngle = 0; 

		static void resetAngle ()
		{
			floorAnlgle = 0;
			skyAngle = 0;
			
			cX = 100 * Math.Cos(skyAngle) * Math.Cos(floorAnlgle);
			cY = 100 * Math.Sin(skyAngle);
			cZ = 100 * Math.Cos (skyAngle) * Math.Sin (floorAnlgle);
		}

		static void update (int value)
		{

			if (move.IsShouldRender) {
				if (move.IsMoveButton)
				{
					if (prevMx.HasValue) {
//						cX += (move.Mz - prevMz.Value) * 500;
//						cY += (move.My - prevMy.Value) * 500;
//						cZ += (move.Mx - prevMx.Value) * 500;	

						if(move.Mx - prevMx > 0)
							floorAnlgle += 0.1;
						if(move.Mx - prevMx < 0)
							floorAnlgle -= 0.1;
						if(move.My - prevMy > 0)
							skyAngle += 0.1;
						if(move.My - prevMy < 0)
							skyAngle -= 0.1;

						cX = 100 * Math.Cos (skyAngle) * Math.Cos(floorAnlgle);
						cY = 100 * Math.Sin(skyAngle);
						cZ = 100 * Math.Cos (skyAngle) * Math.Sin (floorAnlgle);

					}
					prevMx = move.Mx;
					prevMy = move.My;
					prevMz = move.Mz;
				}
				else
				{
					prevMx = null;
					prevMy = null;
					prevMz = null;
				}
				//delete !move.StartDrawing to obtain painting effect
				if(flagDraw)// && !move.StartDrawing)
					drawing.History.Add(
						new Shape(
						((move.Radius / 50.0) - 1.0) * -1.0, 
						((move.Y / 240.0) - 1.0) * -1.0, 
						(move.X / 240.0) - 1.0,
						drawing.ShapeNumber,
						move.Trigger/255.0,
						drawing.ColorNumber));
				if(move.IsCircleBtnReleased)
					drawing.ShapeNumber++;
				if(move.IsCrossBtnReleased)
					drawing.ShapeNumber--;
				if(move.IsSelectBtnReleased)
					resetAngle();
				if(move.IsSquareBtnReleased)
					drawing.ColorNumber--;
				if(move.IsTriangleBtnReleased)
					drawing.ColorNumber++;
				if(move.IsStartBtnReleased)
					drawing.History.Clear();
				//Console.WriteLine (_cameraX);
				Glut.glutPostRedisplay ();

//				StereoCamera cam = new StereoCamera(
//			        2000.0,     // Convergence 
//			        35.0,       // Eye Separation
//			        1.3333,     // Aspect Ratio
//			        45.0,       // FOV along Y in degrees
//			        10.0,       // Near Clipping Distance
//			        20000.0);   // Far Clipping Distance
//
//				cam.ApplyLeftFrustum();
//				Gl.glColorMask(true, false, false, false);
//
//				Glut.glutDisplayFunc (renderScene);
//
//				Gl.glClear(Gl.GL_DEPTH_BUFFER_BIT);
//
//				cam.ApplyRightFrustum();
//
//				Gl.glColorMask(false, true, true, false);
//				Glut.glutDisplayFunc (renderScene);
//
//				Gl.glColorMask(true, true, true, true);
			}
			Glut.glutTimerFunc (25, update, 0);
		}

		static void setLighting (bool on)
		{
			if (on) {
				Gl.glEnable (Gl.GL_LIGHTING);
				Gl.glEnable (Gl.GL_LIGHT0);
				float[] light_pos0 = {0f,1f,0f,0f};
				Gl.glLightfv (Gl.GL_LIGHT0, Gl.GL_POSITION, light_pos0);
			
				Gl.glEnable (Gl.GL_LIGHT1);
				float[] light_pos2 = {0f,-1f,0f,0f};
				Gl.glLightfv (Gl.GL_LIGHT1, Gl.GL_POSITION, light_pos2);
			}
			else
				Gl.glDisable(Gl.GL_LIGHTING);
		}

		static bool isLight = true;
		static void pressKey(int key, int x, int y){
			switch(key){
			case Glut.GLUT_KEY_F1:
				isLight = !isLight;
				setLighting(isLight);
				break;
			}
		}

		public static void Main (string[] args)
		{
			move = new MoveStart();
			Glut.glutInit ();
			Glut.glutInitDisplayMode (Glut.GLUT_DEPTH | Glut.GLUT_DOUBLE | Glut.GLUT_RGBA);
			Glut.glutInitWindowSize (640, 480);
			Glut.glutCreateWindow ("PSMove Paint 3D");
			Gl.glEnable (Gl.GL_DEPTH_TEST);
			setLighting(true);
			Gl.glShadeModel(Gl.GL_FLAT);
			Gl.glTexEnvf(Gl.GL_TEXTURE_ENV,Gl.GL_TEXTURE_ENV_MODE,Gl.GL_MODULATE);

//			StereoCamera cam = new StereoCamera(
//		        2000.0,     // Convergence 
//		        35.0,       // Eye Separation
//		        1.3333,     // Aspect Ratio
//		        45.0,       // FOV along Y in degrees
//		        10.0,       // Near Clipping Distance
//		        20000.0);   // Far Clipping Distance
//
//			cam.ApplyLeftFrustum();
//			Gl.glColorMask(true, false, false, false);
//
//			Glut.glutDisplayFunc (renderScene);
//
//			Gl.glClear(Gl.GL_DEPTH_BUFFER_BIT);
//
//			cam.ApplyRightFrustum();
//
//			Gl.glColorMask(false, true, true, false);
			Glut.glutDisplayFunc (renderScene);
//
//			Gl.glColorMask(true, true, true, true);

			Glut.glutIgnoreKeyRepeat(1);
			Glut.glutSpecialFunc(pressKey);

			Glut.glutTimerFunc (25, update, 0);

			Glut.glutMainLoop ();
		}
	}
}
