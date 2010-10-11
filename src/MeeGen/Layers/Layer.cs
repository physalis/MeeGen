using System;
using Rsvg;
using Cairo;

namespace MeeGen
{
	public class Layer
	{
		protected Point position;
		protected Handle svgHandle;
		protected Size size;
		
		protected double zoom;
		protected double rotation; // in rad
		protected bool selected;
		
		protected Point offset; // the point where the user has clicked to select this shape
		
		public Layer(string filename, Point pos)
		{
			this.position = pos;
			
			this.svgHandle = new Handle(filename);
			
			this.size = new Size(this.svgHandle.Dimensions.Width,
			                     this.svgHandle.Dimensions.Height);
			
			this.zoom = 1d;
			this.rotation = 0d;
			
			this.selected = false;
		}
			                                                      
		public Layer()
		{
			this.position = new Point(0,0);
			this.svgHandle = new Handle();
			this.size = new Size();
			this.size.Width = 0;
			this.size.Height = 0;
			this.zoom = 0;
			this.rotation = 0;
			this.selected = false;
		}
		
		public Handle SvgHandle
		{
			get {return this.svgHandle;}
		}
		
		/// <summary>
		/// The position of the upper left corner of the image
		/// </summary>
		public Point Position
		{
			get {return this.position;}
		}
		
		public Size Size
		{
			get{return this.size;}
			set{this.size = value;}
		}
		
		public double Rotation
		{
			get{return this.rotation;}
			set{this.rotation = value;}
		}
		
		public void Select(Point offset)
		{
			this.offset = offset;
			this.Selected = true;
		}
		
		public bool Selected
		{
			get{return this.selected;}
			set{this.selected = value;}
		}
		
		public void Rotate(double degree)
		{
			this.rotation = this.DegToRad(degree);
		}
		
		public void ScaleWidth(int val)
		{
			this.size.Width += val;
		}
		
		public void ScaleHeight(int val)
		{
			this.size.Height += val;
		}
		
		public void ZoomIn(double val)
		{
			this.zoom += val;
			this.size.Width += val;
			this.size.Height += val;
		}
		
		public void ZoomOut(double val)
		{
			this.zoom -= val;
			this.size.Width -= val;
			this.size.Height -= val;
		}
		
		public void Colorify(Gdk.Color color)
		{
			// open svghandle.baseuri in RAM (!!!), change color (style attribute)
			// and reset svghandle Handle(byte[] data) from stream. (bytestream, memorystream);
			
			// Because images like Pets consist of several layers themselves, colorifying them
			// is difficult. A solution might be to add a Colorable bool-member to every Layer, if
			// it consists of several layers itself (pet & item category) do not colorify it.
		}
		
		public void Move(int dx, int dy)
		{
			//Console.WriteLine((dx - this.offset.X) + "  " + (dy - this.offset.Y));
			
			this.position.X += dx - this.offset.X;
			this.position.Y += dy - this.offset.Y;
			
			this.offset = new Point(dx, dy);
		}
		
		public void Drag(DragLocation dl, int dx, int dy)
		{
			switch(dl)
			{
				case DragLocation.None:
					break;
				case DragLocation.Inside:
					this.Move(dx, dy);
					break;
				case DragLocation.Resize:
					this.size.Width += dx - this.offset.X;
					this.size.Height -= dy - this.offset.Y;
					this.offset = new Point(dx, dy);
					break;
				default:
					break;
			}
		}
		
		public void Draw(Cairo.Context cx)
		{
			//TODO: dont scale the selection rectangle!
			
			cx.Save();
			
			cx.Translate(this.Position.X, this.Position.Y);
			
		//	this.size.Width = 200;
			
			cx.Scale(this.Size.Width / this.svgHandle.Dimensions.Width,
			         this.Size.Height / this.svgHandle.Dimensions.Height);
			
			
			cx.Rotate(this.Rotation);
			cx.Translate(-this.SvgHandle.Dimensions.Width/2, -this.SvgHandle.Dimensions.Height/2);
			this.svgHandle.RenderCairo(cx);
			
			if(this.Selected)
			{
				//cx.Color = new Color(0.58, 0.65, 0.19);
				cx.Save();
				cx.Color = new Color(0, 0, 0);
				cx.LineWidth = 1;
				
				cx.Rectangle(4, 4, 7, 7); //upper right
				cx.Rectangle(this.SvgHandle.Dimensions.Width-10, 4, 7, 7); // upper left
				cx.Rectangle(4, this.SvgHandle.Dimensions.Height-10, 7, 7);
				cx.Rectangle(this.SvgHandle.Dimensions.Width-10, this.SvgHandle.Dimensions.Height-10, 7, 7);
			
				cx.Fill();
				cx.Restore();
				
				//DrawRoundedRectangle(cx, 0, 0, this.size.Width, this.size.Height, 5);
				DrawRoundedRectangle(cx, -3, -3, this.svgHandle.Dimensions.Width+6, this.svgHandle.Dimensions.Height+6, 5);

				cx.SetDash(new double[]{7, 4}, 0);
				cx.LineWidth = 1.2;
				cx.Stroke();
			}
						
			cx.Restore();
		}
		
		private double DegToRad(double degree)
		{
			return (degree/180) * Math.PI;
		}
		
		static void DrawRoundedRectangle (Cairo.Context gr, double x, double y, double width, double height, double radius)
    	{
	        gr.Save ();
	        
	        if ((radius > height / 2) || (radius > width / 2))
	            radius = Math.Min (height / 2, width / 2);
	            
	        gr.MoveTo (x, y + radius);
	        gr.Arc (x + radius, y + radius, radius, Math.PI, -Math.PI / 2);
	        gr.LineTo (x + width - radius, y);
	        gr.Arc (x + width - radius, y + radius, radius, -Math.PI / 2, 0);
	        gr.LineTo (x + width, y + height - radius);
	        gr.Arc (x + width - radius, y + height - radius, radius, 0, Math.PI / 2);
	        gr.LineTo (x + radius, y + height);
	        gr.Arc (x + radius, y + height - radius, radius, Math.PI / 2, Math.PI);
	        
	        gr.ClosePath ();
	        gr.Restore ();
		}
		
		static void DrawTriangle(Cairo.Context c, double x, double y, double width)
		{
			c.Save();
			c.MoveTo(x, y);
			c.LineTo(x, y+width);
			c.MoveTo(x, y+width);
			c.LineTo(x+width, y);
			c.MoveTo(x+width, y);
			c.LineTo(x, y);
			c.ClosePath();
			c.Restore();
		}
	}
	
	public struct Size
	{
		public Size(double Width, double Height)
		{
			this.Width = Width;
			this.Height = Height;
		}
		
		public double Width;
		public double Height;
	}
	
	public enum DragLocation
	{
		UpperLeft,
		UpperRight,
		BottomLeft,
		BottomRight,
		Inside,
		Resize,
		None
	}
}

