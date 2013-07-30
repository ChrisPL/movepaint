using System;
using Tao.FreeGlut;
using Tao.OpenGl;
using System.Collections.Generic;

namespace TaoOpenGL
{
	public class Drawing
	{
		int shapeNumber = 0;
		const int numOfShapes = 2;

		public int ShapeNumber {
			get {
				return shapeNumber;
			}
			set {
				shapeNumber = value % numOfShapes;
			}
		}
		int colorNumber = 0;
		const int numOfColors = 5;

		public int ColorNumber {
			get {
				return colorNumber;
			}
			set {
				colorNumber = value % numOfColors;
			}
		}

		int materialNumber = 0;
		const int numOfMaterials = 4;

		public int MaterialNumber {
			get {
				return materialNumber;
			}
			set {
				materialNumber = value % numOfMaterials;
			}
		}

		List<Shape> history;

		public List<Shape> History {
			get {
				return history;
			}
			set {
				history = value;
			}
		}

		public Drawing ()
		{
			history = new List<Shape>();
		}

		public void drawWireShape (double radius)
		{
			color();
			switch (shapeNumber) {
			case 0:
				wireSphere(radius);
				break;
			case 1:
				wireCube(radius);
				break;
			default:
				wireSphere(radius);
				break;
			}
		}

		void wireSphere(double radius)
		{
			Glut.glutWireSphere(radius, 25, 25);
		}

		void wireCube(double radius)
		{
			Glut.glutWireCube(radius/2.0);
		}

		public void drawSolidShape (double radius, int? shapeNo = null, int? colorNo = null)
		{
			if (!shapeNo.HasValue) shapeNo = shapeNumber;
			color(colorNo);
			shapeNo %= numOfShapes;
			switch (shapeNo) {
			case 0:
				solidSphere(radius);
				break;
			case 1:
				solidCube(radius);
				break;
			default:
				solidSphere(radius);
				break;
			}
		}

		void solidSphere(double r)
		{
			Glut.glutSolidSphere(r, 25, 25);
		}

		void solidCube (double r)
		{
			Glut.glutSolidCube(r/2.0);
		}

		void color (int? colNo = null)
		{
			if (!colNo.HasValue) colNo = colorNumber;
			switch (colNo) {
			case 0:
				Gl.glColor3d (0.9, 0, 0.5);
				break;
			case 1:
				Gl.glColor3d (0.7, 0.7, 0.7);
				break;
			case 2:
				Gl.glColor3d(1,0,0);
				break;
			case 3:
				Gl.glColor3d(0,1,0);
				break;
			case 4:
				Gl.glColor3d(0,0,1);
				break;
			default:
				Gl.glColor3d (0.9, 0, 0.5);
				break;
			}
			material(colNo);
		}

		void material (int? matNo = null)
		{
			float[] no_mat = {0.0f, 0.0f, 0.0f, 1.0f};
			float[] mat_ambient = {0.7f,0.7f,0.7f,1.0f};
			float[] mat_ambient_color = {0.0f,0.5f,0.0f,1.0f};
			float[] mat_diffuse = {0.1f,0.5f,0.8f,1.0f};

			float[] brass_ambient = {0.329412f, 0.223529f, 0.027451f, 1.0f};
			float[] brass_diffuse = {0.780392f, 0.568627f, 0.113725f, 1.0f};
			float[] brass_specular = {0.992157f, 0.941176f, 0.807843f, 1.0f};
			float[] brass_shininess = {27.8974f};

			float[] pearl_ambient = {0.25f, 0.20725f, 0.20725f, 0.922f};
			float[] pearl_diffuse = {1.0f, 0.829f, 0.829f, 0.922f};
			float[] pearl_specular = {0.296648f, 0.296648f, 0.296648f, 0.922f};
			float[] pearl_shininess = {11.264f};

			float[] pgold_ambient = {0.24725f, 0.2245f, 0.0645f, 1.0f};
			float[] pgold_diffuse = {0.34615f, 0.3143f, 0.0903f, 1.0f};
			float[] pgold_specular = {0.797357f, 0.723991f, 0.208006f, 1.0f};
			float[] pgold_shininess = {89.2f};

			float[] mat_specular = {1.0f,1.0f,1.0f,1.0f};
			float[] no_shininess = {0.0f};
			float[] low_shininess = {5.0f};
			float[] high_shininess = {100.0f};
			float[] mat_emission = {0.3f,0.2f,0.2f,0.0f};

			if (!matNo.HasValue)
				matNo = materialNumber;
			switch (matNo) {
			case 0:
				Gl.glMaterialfv(Gl.GL_FRONT_AND_BACK,Gl.GL_AMBIENT, mat_ambient_color);
				Gl.glMaterialfv(Gl.GL_FRONT_AND_BACK,Gl.GL_DIFFUSE, mat_diffuse);
				Gl.glMaterialfv(Gl.GL_FRONT_AND_BACK,Gl.GL_SPECULAR,no_mat);
				Gl.glMaterialfv(Gl.GL_FRONT_AND_BACK,Gl.GL_SHININESS, no_shininess);
				Gl.glMaterialfv(Gl.GL_FRONT_AND_BACK,Gl.GL_EMISSION, no_mat);
				break;
			case 1:
				Gl.glMaterialfv(Gl.GL_FRONT_AND_BACK,Gl.GL_AMBIENT, pgold_ambient);
				Gl.glMaterialfv(Gl.GL_FRONT_AND_BACK,Gl.GL_DIFFUSE, pgold_diffuse);
				Gl.glMaterialfv(Gl.GL_FRONT_AND_BACK,Gl.GL_SPECULAR,pgold_specular);
				Gl.glMaterialfv(Gl.GL_FRONT_AND_BACK,Gl.GL_SHININESS, pgold_shininess);
				Gl.glMaterialfv(Gl.GL_FRONT_AND_BACK,Gl.GL_EMISSION, no_mat);
				break;
			case 2:
				Gl.glMaterialfv(Gl.GL_FRONT_AND_BACK,Gl.GL_AMBIENT, pearl_ambient);
				Gl.glMaterialfv(Gl.GL_FRONT_AND_BACK,Gl.GL_DIFFUSE, pearl_diffuse);
				Gl.glMaterialfv(Gl.GL_FRONT_AND_BACK,Gl.GL_SPECULAR,pearl_specular);
				Gl.glMaterialfv(Gl.GL_FRONT_AND_BACK,Gl.GL_SHININESS, pearl_shininess);
				Gl.glMaterialfv(Gl.GL_FRONT_AND_BACK,Gl.GL_EMISSION, no_mat);
				break;
			case 3:
				Gl.glMaterialfv(Gl.GL_FRONT_AND_BACK,Gl.GL_AMBIENT, brass_ambient);
				Gl.glMaterialfv(Gl.GL_FRONT_AND_BACK,Gl.GL_DIFFUSE, brass_diffuse);
				Gl.glMaterialfv(Gl.GL_FRONT_AND_BACK,Gl.GL_SPECULAR,brass_specular);
				Gl.glMaterialfv(Gl.GL_FRONT_AND_BACK,Gl.GL_SHININESS, brass_shininess);
				Gl.glMaterialfv(Gl.GL_FRONT_AND_BACK,Gl.GL_EMISSION, no_mat);
				break;
			}
		}
	}
}

