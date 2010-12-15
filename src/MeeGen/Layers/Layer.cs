using System;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using Rsvg;
using Cairo;

namespace MeeGen
{
	[FlagsAttribute]
	public enum FlipMode
	{
		Horizontal = 0x01,
		Vertical = 0x02,
		Both = Horizontal | Vertical,
		None = 0x00
	}
	
	public struct Size
	{
		public double Width;
		public double Height;
		
		public Size(double Width, double Height)
		{
			this.Width = Width;
			this.Height = Height;
		}
	}
	
	public enum DragLocation
	{
		/*UpperLeft,
		UpperRight,
		BottomLeft,
		BottomRight,*/
		Inside,
		Resize,
		None
	}
		
	public class Layer
	{
		protected Point position;
		protected Handle svgHandle;
		protected Size size;
		
		protected double zoom;
		protected double rotation; // in rad
		protected bool selected;
		
		protected string svgContent;
		
		protected Cairo.Color color;
		
		protected Point offset; // the point where the user has clicked to select this shape
		
		protected FlipMode flipMode;
		
		/// <summary>
		/// Creates a new instance of the MeeGen.Layer class
		/// </summary>
		/// <param name="filename">
		/// The name of the SVG-file from which to create this instance
		/// </param>
		/// <param name="pos">
		/// The starting position of the new layer
		/// </param> 
		public Layer(string filename, Point pos) 
					: this(new Handle(filename), pos)
		{

		}
		
		/// <summary>
		/// Creates a new instance of the MeeGen.Layer class
		/// </summary>
		/// <param name="handle">
		/// The Rsvg.Handle that contains the image data for this instance
		/// A <see cref="Handle"/>
		/// </param>
		/// <param name="pos">
		/// The starting position of the new layer
		/// A <see cref="Point"/>
		/// </param>
		public Layer(Handle handle, Point pos)
		{
			this.position = pos;
			
			this.svgHandle = handle;
			
			this.size = new Size(this.svgHandle.Dimensions.Width,
			                     this.svgHandle.Dimensions.Height);
			
			this.zoom = 1d;
			this.rotation = 0d;
			this.selected = false;
			
			this.flipMode = FlipMode.None;
			
			this.color = new Color(1, 1, 1, 1);
			
			StreamReader reader = new StreamReader(handle.BaseUri.Substring(7));
			svgContent = reader.ReadToEnd();
			reader.Close();
		}
		
		internal Layer()
		{
			this.position = new Point(0,0);
			this.svgHandle = new Handle();
			this.size = new Size();
			this.size.Width = 0;
			this.size.Height = 0;
			this.zoom = 0;
			this.rotation = 0;
			this.selected = false;
			this.flipMode = FlipMode.None;
			this.svgContent = null;
		}
		
		public Color Color
		{
			get {return this.color;}
		}
		
		public FlipMode FlipMode
		{
			get {return this.flipMode;}
		}
		
		/// <summary>
		/// Gets the 2D space this layer requires in a simplified rectangular format
		/// </summary>
		public Rectangle Boundaries
		{
			get{return this.GetBounds();}
		}
		
		/// <summary>
		/// Get the Rsvg.Handle associated with this layer
		/// </summary>
		public Handle SvgHandle
		{
			get {return this.svgHandle;}
		}
		
		/// <summary>
		/// Get or set the location of the center of this layer
		/// </summary>
		public Point Position
		{
			// TODO: should become the position of the top-left corner
			get {return this.position;}
			set {this.position = value;}
		}
		
		/// <summary>
		/// Get or set the size of this layer
		/// </summary>
		public Size Size
		{
			get{return this.size;}
			set{this.size = value;}
		}
		
		/// <summary>
		/// Get or set the rotation (in radian) of this layer
		/// </summary>
		public double Rotation
		{
			get{return this.rotation;}
			set{this.rotation = value;}
		}
		
		/// <summary>
		/// Select this layer (creates a box around this layer)
		/// </summary>
		/// <param name="offset">
		/// The position where the user has clicked to select this image
		/// A <see cref="Point"/>
		/// </param>
		public void Select(Point offset)
		{
			this.offset = offset;
			this.Selected = true;
		}
		
		/// <summary>
		/// Get or set whether this layer is selected or not
		/// </summary>
		public bool Selected
		{
			get{return this.selected;}
			set{this.selected = value;}
		}
		
		/// <summary>
		/// Rotates the layer by the specified angle
		/// </summary>
		/// <param name="degree">
		/// The angle to rotate this layer by (in degree)
		/// A <see cref="System.Double"/>
		/// </param>
		public void Rotate(double degree)
		{
			this.rotation = this.DegToRad(degree);
		}
		
		/// <summary>
		/// Stretches this layer's width by the specified value
		/// </summary>
		/// <param name="val">
		/// Increases/Decreases the width by that value
		/// A <see cref="System.Int32"/>
		/// </param>
		public void ScaleWidth(int val)
		{
			// make sure that the width stays at least +5.
			// If it gets negative, the algorithm that checks,
			// whether the user has clicked inside a layer won't work anymore
			this.size.Width = this.size.Width + val <= 5 ?
							  this.size.Width : this.size.Width + val;
		}
		
		/// <summary>
		/// Stretches this layer's height by the specified value
		/// </summary>
		/// <param name="val">
		/// Increases/Decreases the height by that value
		/// A <see cref="System.Int32"/>
		/// </param>
		public void ScaleHeight(int val)
		{
			// make sure that the height stays at least +5.
			// If it gets negative, the algorithm that checks,
			// whether the user has clicked inside a layer won't work anymore
			this.size.Height = this.size.Height - val <= 5 ?
							   this.size.Height : this.size.Height - val;
		}
		
		/// <summary>
		/// Scales the image and preserves the aspect ratio
		/// </summary>
		/// <param name="val">
		/// A positive or negative value
		/// A <see cref="System.Double"/>
		/// </param>
		public void Zoom(int val)
		{
			this.zoom += val;
			this.ScaleWidth(val);
			this.ScaleHeight(-val);
		}
		
		/// <summary>
		/// Changes the color of the first occuring style attribute in the SVG-image
		/// </summary>
		/// <param name="color">
		/// A <see cref="Gdk.Color"/>
		/// </param>
		public void Colorize(Gdk.Color color, double opacity)
		{		
			if(svgContent == null)
				return;
            // make sure the color-values stay <= 255
			string hexcolor = String.Format("#{0:x2}{1:x2}{2:x2}",
			                                color.Red    / 255 > 255 ? 255 : color.Red   / 255,
			                                color.Green / 255 > 255 ? 255 : color.Green / 255,
			                                color.Blue / 255 > 255 ? 255 : color.Blue  / 255);

			opacity = Math.Round(opacity, 1);
			
			this.color.R = color.Red/255;
			this.color.G = color.Green/255;
			this.color.B = color.Blue/255;
			this.color.A = opacity;

			
			//TODO: PERF maybe use using(..)
									
			XmlDocument doc = new XmlDocument();	
			doc.PreserveWhitespace = true;
			doc.LoadXml(svgContent);	
		
			//Console.WriteLine(doc.DocumentType.ToString());
			
			// TODO: add metadata tags to the images, that specify which paths color to change
			// 		 if none available, change the 0. (or none?).
			
			// this will set the first occurance of path-element to hexcolor.
			// TODO: use regular expressions to replace the fill-color and the fill-opacity 
			
			Console.WriteLine();
			
			// circle, ellipse, rect, line, polyline, polygon, text(?)
			if(doc.GetElementsByTagName("path").Count > 0)
			{
				doc.GetElementsByTagName("path")[0].Attributes["style"].Value = 
				"fill:"+hexcolor+";fill-opacity:"+opacity+";fill-rule:nonzero;stroke:none";
			}else if(doc.GetElementsByTagName("rect") != null)
			{
				doc.GetElementsByTagName("rect")[0].Attributes["style"].Value = 
				"fill:"+hexcolor+";fill-opacity:"+opacity+";fill-rule:nonzero;stroke:none";
			}else
				return;
			
			this.svgContent = doc.OuterXml;

			this.svgHandle = new Handle(System.Text.Encoding.UTF8.GetBytes(doc.OuterXml));
			
		}
		
		public void Move(int dx, int dy)
		{
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
					this.ScaleWidth(dx - this.offset.X);
					this.ScaleHeight(dy - this.offset.Y);
					this.offset = new Point(dx, dy);
					break;
				default:
					break;
			}
		}
		
		public void FlipHorizontally()
		{
			if((this.FlipMode & FlipMode.Horizontal) == FlipMode.Horizontal)
				this.flipMode ^= FlipMode.Horizontal;
			else
				this.flipMode |= FlipMode.Horizontal;
		}
		
		public void FlipVertically()
		{
			if((this.FlipMode & FlipMode.Vertical) == FlipMode.Vertical)
				this.flipMode ^= FlipMode.Vertical;
			else
				this.flipMode |= FlipMode.Vertical;
		}
		
		/// <summary>
		/// Draws this layer using the specified Cairo.Context
		/// </summary>
		/// <param name="cx">
		/// The context to use for drawing
		/// A <see cref="Cairo.Context"/>
		/// </param>
		public void Draw(Cairo.Context cx)
		{			
			// start by drawing the unrotated selection-rectangle
			if(this.Selected)
			{			
				cx.Save();
			
				cx.Translate(this.Position.X, this.Position.Y);
						
				/*cx.Scale(this.Size.Width / this.svgHandle.Dimensions.Width,
			         this.Size.Height / this.svgHandle.Dimensions.Height);*/
			
				/*cx.Translate(-this.svgHandle.Dimensions.Width / 2,
			             -this.svgHandle.Dimensions.Height / 2);*/
		
				// leave a small offset of 3 on every side of the rectangle
				// simply looks neater 
				DrawRoundedRectangle(cx, 
				                     this.Boundaries.X - 3,
				                     this.Boundaries.Y - 3,
				                     this.Boundaries.Width + 6,
				                     this.Boundaries.Height + 6,
				                     7);
				             
				// #b5b7b4 - light grey (meego palette)
				//cx.Color = new Color(0.7, 0.71, 0.7, 0.3);	
				cx.Color = new Color(0.7, 0.71, 0.7, 0.5);	
				cx.Fill();

				cx.Restore();
			}
			
			cx.Save();
			
			cx.Translate(this.Position.X, this.Position.Y);
			
			// not well thought, but it works, needs refinements.
			switch(this.flipMode)
			{
				case FlipMode.Horizontal:
					cx.Scale(-this.Size.Width / this.svgHandle.Dimensions.Width,
				         	 this.Size.Height / this.svgHandle.Dimensions.Height);
					break;
				case FlipMode.Vertical:
					cx.Scale(this.Size.Width / this.svgHandle.Dimensions.Width,
				         	 -this.Size.Height / this.svgHandle.Dimensions.Height);
					break;
				case FlipMode.None:
					cx.Scale(this.Size.Width / this.svgHandle.Dimensions.Width,
				         	 this.Size.Height / this.svgHandle.Dimensions.Height);
					break;
				default:
					cx.Scale(-this.Size.Width / this.svgHandle.Dimensions.Width,
					         -this.Size.Height / this.svgHandle.Dimensions.Height);
					break;
			}
			
			//TODO: MAYBE make the rotation depend on the FlipMode
			cx.Rotate(this.Rotation);
			cx.Translate(-this.svgHandle.Dimensions.Width / 2,
			             -this.svgHandle.Dimensions.Height / 2);
			
			// for debugging purposes
			/*cx.Rectangle(0, 0, this.svgHandle.Dimensions.Width, this.svgHandle.Dimensions.Height);
			cx.Color = new Color(200, 100, 100, 100);
			cx.Fill();*/
			
			this.svgHandle.RenderCairo(cx);

			cx.Restore();
		}
		
		// TODO: this method is being called quite often,
		//		 performance... -> adding a Changed-property gets more likely
		private Rectangle GetBounds()
		{
			Rectangle b = new Rectangle(-this.size.Width/2,
			                            -this.size.Height/2,
			                            this.size.Width,
			                            this.size.Height);
			
			
			PointD[] untransformed = {new PointD(b.X, b.Y),
								  	  new PointD(b.X + b.Width, b.Y),
								      new PointD(b.X + b.Width, b.Y + b.Height),
								  	  new PointD(b.X, b.Y + b.Height)};
			
			PointD[] transformed = new PointD[4];
	
			double x0 = (b.X + b.Width/2);
			double y0 = (b.Y + b.Height/2);
			
			// TODO: if rotation > Math.PI ...
			// also it isn't very precise.
			//Console.WriteLine (rotation);

			double rot = this.rotation < 0 ? -this.rotation : this.rotation;
				   rot = rot > Math.PI/2 ? Math.PI/2 - rot : -rot;
			
			//double rotation = this.rotation > 0 ? -this.rotation : this.rotation;

			for(int i = 0; i < 4; i++)
			{
				double x = (x0 + (untransformed[i].X - x0) * Math.Cos(rot)
				            + (untransformed[i].Y - y0) * Math.Sin(rot));
			
				double y = (y0 - (untransformed[i].X - x0) * Math.Sin(rot)
				            + (untransformed[i].Y - y0) * Math.Cos(rot));
				
				transformed[i] = new PointD(x, y);
			}
			
			return new Rectangle(transformed[3].X,
			                     transformed[0].Y,
			                     transformed[1].X - transformed[3].X,
			                     transformed[2].Y - transformed[0].Y);
		}
		
		private double DegToRad(double degree)
		{
			return (degree/180) * Math.PI;
		}
		
		private void DrawRoundedRectangle(Cairo.Context gr,
		                                  double x,
		                                  double y,
		                                  double width,
		                                  double height,
		                                  double radius)
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
		
		private void DrawRoundedRectangle(Cairo.Context c, Rectangle r, double rad)
		{
			DrawRoundedRectangle(c, r.X, r.Y, r.Width, r.Height, rad);
		}
	}
}

