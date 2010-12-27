using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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
	
	public struct ColorizeItem
	{
		public int index;
		public bool fill;
		public bool stroke;
	}

	public class Shape
	{
		protected Point position;
		protected Handle svgHandle;
		protected Size size;
		
		protected double zoom;
		protected double rotation; // as rad
		protected bool selected;
		
		protected string svgContent;
		
		protected Point offset; // the point where the user has clicked to select this shape
		
		protected FlipMode flipMode;
		
		protected XmlDocument svgDoc;
		
		protected ColorizeItem[] colorizeItems;
		
		/// <summary>
		/// Creates a new instance of the MeeGen.Shape class
		/// </summary>
		/// <param name="filename">
		/// The name of the SVG-file from which to create this instance
		/// </param>
		/// <param name="pos">
		/// The starting position of the new shape
		/// </param> 
		public Shape(string filename, Point pos) 
					: this(new Handle(filename), pos)
		{

		}
		
		public Shape(byte[] data, Point pos)
					: this(new Handle(data), pos)
		{
			this.svgContent = System.Text.Encoding.Default.GetString(data);
		}
		
		/// <summary>
		/// Creates a new instance of the MeeGen.Shape class
		/// </summary>
		/// <param name="handle">
		/// The Rsvg.Handle that contains the image data for this instance
		/// A <see cref="Handle"/>
		/// </param>
		/// <param name="pos">
		/// The starting position of the new shape
		/// A <see cref="Point"/>
		/// </param>
		public Shape(Handle handle, Point pos)
		{
			this.position = pos;
			
			this.svgHandle = handle;
			
			this.size = new Size(this.svgHandle.Dimensions.Width,
			                     this.svgHandle.Dimensions.Height);
			
			this.zoom = 1d;
			this.rotation = 0d;
			this.selected = false;
			
			this.flipMode = FlipMode.None;
			
			if(handle.BaseUri.Length > 0)
			{
				StreamReader reader = new StreamReader(handle.BaseUri.Substring(7));
				svgContent = reader.ReadToEnd();
				reader.Close();
			}
			
			this.svgDoc = new XmlDocument();
			svgDoc.LoadXml(svgContent);
			
			this.colorizeItems = this.ParseMetadata();
		}
		
		internal Shape()
		{
			this.position = new Point(0, 0);
			this.svgHandle = new Handle();
			this.size = new Size();
			this.size.Width = 0;
			this.size.Height = 0;
			this.zoom = 0;
			this.rotation = 0;
			this.selected = false;
			this.flipMode = FlipMode.None;
			this.svgContent = null;
			this.svgDoc = new XmlDocument();
		}
		
		static Shape()
		{
			Shape.SelectionColor = new Color(0.7, 0.71, 0.7, 0.5);
		}
		
		#region properties
		public static Color SelectionColor 
		{
			get;
			set;
		}
		
		public FlipMode FlipMode
		{
			get {return this.flipMode;}
		}
		
		/// <summary>
		/// Gets the 2D space this shape requires in a simplified rectangular format
		/// </summary>
		public Rectangle Boundaries
		{
			get{return this.GetBounds();}
		}
		
		/// <summary>
		/// Get the Rsvg.Handle associated with this shape
		/// </summary>
		public Handle SvgHandle
		{
			get {return this.svgHandle;}
		}
		
		/// <summary>
		/// Get or set the location of the center of this shape
		/// </summary>
		public Point Position
		{
			get {return this.position;}
			set {this.position = value;}
		}
		
		/// <summary>
		/// Get or set the size of this shape
		/// </summary>
		public Size Size
		{
			get{return this.size;}
			set{this.size = value;}
		}
		
		/// <summary>
		/// Get or set the rotation (in radian) of this shape
		/// </summary>
		public double Rotation
		{
			get{return this.rotation;}
			set{this.rotation = value;}
		}
		
		/// <summary>
		/// Select this shape (creates a box around this shape)
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
		/// Get or set whether this shape is selected or not
		/// </summary>
		public bool Selected
		{
			get{return this.selected;}
			set{this.selected = value;}
		}
		#endregion
		
		/// <summary>
		/// Rotates the shape by the specified angle
		/// </summary>
		/// <param name="degree">
		/// The angle to rotate this shape by (in degree)
		/// A <see cref="System.Double"/>
		/// </param>
		public void Rotate(double degree)
		{
			this.rotation = this.DegToRad(degree);
		}
		
		/// <summary>
		/// Stretches this shape's width by the specified value
		/// </summary>
		/// <param name="val">
		/// Increases/Decreases the width by that value
		/// A <see cref="System.Int32"/>
		/// </param>
		public void ScaleWidth(int val)
		{
			// make sure that the width stays at least +5.
			// If it gets negative, the algorithm that checks,
			// whether the user has clicked inside a shape won't work anymore
			this.size.Width = this.size.Width + val <= 5 ?
							  this.size.Width : this.size.Width + val;
		}
		
		/// <summary>
		/// Stretches this shape's height by the specified value
		/// </summary>
		/// <param name="val">
		/// Increases/Decreases the height by that value
		/// A <see cref="System.Int32"/>
		/// </param>
		public void ScaleHeight(int val)
		{
			// make sure that the height stays at least +5.
			// If it gets negative, the algorithm that checks,
			// whether the user has clicked inside a shape won't work anymore
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
			if(this.colorizeItems == null)
				return;

            // make sure the color-values stay <= 255
			string hexcolor = String.Format("#{0:x2}{1:x2}{2:x2}",
			                                color.Red    / 255 > 255 ? 255 : color.Red   / 255,
			                                color.Green / 255 > 255 ? 255 : color.Green / 255,
			                                color.Blue / 255 > 255 ? 255 : color.Blue  / 255);

			opacity = Math.Round(opacity, 1);
			
			// circle, ellipse, rect, line, polyline, polygon, text(X)
			foreach(ColorizeItem clrItem in this.colorizeItems)
			{
				XmlNode gNode = this.svgDoc.DocumentElement.GetElementsByTagName("g")[0];

				// if the geometric primitives are grouped in a g-tag
				if(gNode != null)
				{
					try
					{
						string style = gNode.ChildNodes[clrItem.index].Attributes["style"].Value;
						
						if(clrItem.stroke && clrItem.fill)
						{						
							// replace the current fill color value
							style = Regex.Replace(style, "fill:#[0-9a-fA-F]{6}", "fill:" + hexcolor);
							// replace the fill opacity
							style = Regex.Replace(style,
							                      "fill-opacity:[1]|fill-opacity:0.[0-9]+",
							                      "fill-opacity:" + opacity);
							// replace the stroke color
							style = Regex.Replace(style, "stroke:#[0-9a-fA-F]{6}", "stroke:" + hexcolor);
							// replace the stroke opacity
							style = Regex.Replace(style,
							                      "stroke-opacity:[1]|fill-opacity:0.[0-9]+",
							                      "stroke-opacity:" + opacity);
							// apply the changes
							gNode.ChildNodes[clrItem.index].Attributes["style"].Value = style;
							
						}else if(clrItem.fill && !clrItem.stroke)
						{
							style = Regex.Replace(style, "fill:#[0-9a-fA-F]{6}", "fill:" + hexcolor);
							style = Regex.Replace(style,
							                      "fill-opacity:[1]|fill-opacity:0.[0-9]+",
							                      "fill-opacity:" + opacity);
							
							gNode.ChildNodes[clrItem.index].Attributes["style"].Value = style;
							
						}else if(!clrItem.fill && clrItem.stroke)
						{
							style = Regex.Replace(style, "stroke:#[0-9a-fA-F]{6}", "stroke:" + hexcolor);
							style = Regex.Replace(style,
							                      "stroke-opacity:[1]|fill-opacity:0.[0-9]+",
							                      "stroke-opacity:" + opacity);
							
							gNode.ChildNodes[clrItem.index].Attributes["style"].Value = style;
						}
					}catch(Exception e)
					{
						MessageBox.ShowError("There was an error when trying to change the shape's color:\n"
						                     + e.Message);
						break;
					}
				}
			}
			this.svgContent = this.svgDoc.OuterXml;

			// recreate this Rsvg.Handle with the changed svg data
			this.svgHandle = new Handle(System.Text.Encoding.UTF8.GetBytes(svgDoc.OuterXml));
		}
		
		private void MoveDrag(int dx, int dy)
		{
			this.position.X += dx - this.offset.X;
			this.position.Y += dy - this.offset.Y;
			
			this.offset = new Point(dx, dy);
		}
		
		public void Move(int dx, int dy)
		{
			this.position.X += dx;
			this.position.Y += dy;
			
			this.offset = new Point(dx, dy);
		}
		
		public void Drag(DragLocation dl, int dx, int dy)
		{
			switch(dl)
			{
				case DragLocation.None:
					break;
				case DragLocation.Inside:
					this.MoveDrag(dx, dy);
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
		/// Draws this shape using the specified Cairo.Context
		/// </summary>
		/// <param name="cx">
		/// The context to use for drawing
		/// A <see cref="Cairo.Context"/>
		/// </param>
		public virtual void Draw(Cairo.Context cx)
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
				//cx.Color = new Color(0.7, 0.71, 0.7, 0.5);	
				cx.Color = Shape.SelectionColor;
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
		
		private ColorizeItem[] ParseMetadata()
		{
			List<ColorizeItem> itemList = new List<ColorizeItem>();
			
			// the first occurrance of the metadata tag
			
			XmlNode metadata = this.svgDoc.GetElementsByTagName("metadata")[0];
			
			//no metadata specified, changing the color of this shape is prohibited
			if(metadata == null)
				return null;
				
				
			// foreach 'colorize'-node
			foreach (XmlNode sub in metadata.ChildNodes)
			{
				// only if it is meegen-specific metadata
				if(sub.Name == "colorize")
				{
					// first, define the indices used for this colorize instruction
					string index = sub.Attributes["index"].Value;
					index = index.Trim();
					string[] indices = index.Split(',');
								
					// these are the default values
					bool fill = true;
					bool stroke = false;
					
					// find out if the stroke or fille attribute is present,
					// and if so, adapt the fill and stroke variable
					if(sub.Attributes["stroke"] != null)
					{
						stroke = bool.Parse(sub.Attributes["stroke"].Value);
					}
					if(sub.Attributes["fill"] != null)
					{
						fill = bool.Parse(sub.Attributes["fill"].Value);
					}
							
					foreach(string s in indices)
					{
						ColorizeItem item;
						item.index = int.Parse(s);
						item.stroke = stroke;
						item.fill = fill;
						
						itemList.Add(item);
					}	
				}
			}
			return itemList.ToArray();
		}
		
		// TODO: this method is being called quite often,
		//		 performance... -> adding a Changed-property gets more likely
		protected Rectangle GetBounds()
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
			
			// TODO: if rotation > Math.PI...
			// also it isn't very precise.
			//Console.WriteLine (rotation);

			this.rotation = Math.Abs(this.rotation) > 2*Math.PI ? 0 : this.rotation; 
			double rot = -this.rotation < 0 ? -this.rotation : this.rotation;
				   rot = -rot > Math.PI ? -2 * Math.PI - rot : rot;
			       rot = -rot > Math.PI / 2 ? -Math.PI - rot : rot;
			
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
		
		protected double DegToRad(double degree)
		{
			return (degree/180) * Math.PI;
		}
		
		protected void DrawRoundedRectangle(Cairo.Context gr,
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
		
		protected void DrawRoundedRectangle(Cairo.Context c, Rectangle r, double rad)
		{
			DrawRoundedRectangle(c, r.X, r.Y, r.Width, r.Height, rad);
		}
	}
	
	public class BackgroundShape : Shape
	{
		public BackgroundShape() : base()
		{

		}
		
		public override void Draw (Context cx)
		{
			
		}
	}
}

