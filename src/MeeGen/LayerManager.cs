using System;
using System.Collections.Generic;
using System.Collections;

using Cairo;
using Rsvg;

namespace MeeGen
{
	public class LayerManager : IEnumerable
	{
		List<Layer> layers;
		
		public LayerManager ()
		{
			layers = new List<Layer>();
		}
		
		public Layer this[int index]
		{
			get {return this.layers[index];}
		}
		
		public int Count
		{
			get {return layers.Count;}
		}
		
		public Layer Selected
		{
			get{return this.GetSelectedLayer();}
		}
		
		// adds a new layer 
		public void Add(Layer l)
		{
			this.layers.Add(l);
		}
		
		public void Insert(int index, Layer l)
		{
			this.layers.Insert(index, l);
		}
		
		public void Select(int index)
		{
			if(index < this.Count && index >= 0)
				this[index].Selected = true;
		}
		
		public void Select(int x, int y)
		{
			Layer selected = null;
			
			foreach(Layer l in this)
			{
				//x, y on field?
				//yep: -> selected = l;
				// rectangular selection.
				if(PointInRectangle(new Point(x,y),
				                    new Rectangle(new Point((int)(l.Position.X-l.Size.Width/2), (int)(l.Position.Y-l.Size.Height/2)), l.Size.Width, l.Size.Height)))
				{
					selected = l;
				}
			}
			if(selected != null)
				selected.Selected = true;
		}
		
		// not yet working for polygons, also error prone when rotated
		// -> needs some more math
		private bool PointInRectangle(Point p, Rectangle r)
		{
			if(p.X >= r.X && p.X <= r.X+r.Width && p.Y <= r.Y +r.Height && p.Y >= r.Y)
				return true;
			return false;
		}
		
		public void Unselect(int index)
		{
			this[index].Selected = false;
		}
		
		public void UnselectAll()
		{
			foreach(Layer l in this)
				l.Selected = false;
			
		}
		
		public void Clear()
		{
			this.layers.Clear();
		}
		
		public void Export(string filename, Format format)
		{
			SvgSurface surface = new SvgSurface(filename, 1000, 1000);
			
			Cairo.Context c = new Context(surface);
			this.Draw(c);
			surface.Finish();
		}
		
		public void Draw(Cairo.Context context)
		{
			foreach(Layer l in this)
				l.Draw(context);
		}
		
		public Layer GetSelectedLayer()
		{
			foreach(Layer l in this)
			{
				if(l.Selected)
					return l;
			}
			// return a stub layer, easier than always checking whether 
			// the returned Layer is null
			return new Layer();
			//return null
		}
		
		
		public IEnumerator GetEnumerator()
		{
			foreach(Layer layer in this.layers)
				yield return layer;
		}
	}
	
	public class Layer
	{
		protected Point position;
		protected Handle svgHandle;
		protected Size size;
		
		protected double zoom;
		protected double rotation; // in rad
		protected bool selected;
		
		public Layer(string filename, Point pos)
		{
			this.position = pos;
			
			this.svgHandle = new Handle(filename);
			
			this.size = new Size();
			this.size.Width = this.svgHandle.Dimensions.Width;
			this.size.Height = this.svgHandle.Dimensions.Height;
			
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
		}
		
		public double Rotation
		{
			get{return this.rotation;}
			set{this.rotation = value;}
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
		
		public void ZoomIn(double val)
		{
			this.zoom += val;
			this.size.Width *= zoom;
			this.size.Height *= zoom;
		}
		
		public void ZoomOut(double val)
		{
			this.zoom -= val;
			this.size.Width *= zoom;
			this.size.Height *= zoom;
		}
		
		public void Colorify(Gdk.Color color)
		{
			// open svghandle.baseuri in RAM (!!!), change color (style attribute)
			// and reset svghandle Handle(byte[] data) from stream. (bytestream, memorystream);
			
			// Because images like Pets consist of several layers themselves, colorifying them
			// is difficult. A solution might be to add a Colorable bool-member to every layer, and check there
			// and if adequate do some notifications (stylish boxes popping up, 
			// informing the user about his mistake and then fade away...
		}
		
		public void Move(int x, int y)
		{
			this.position.X = x;
			this.position.Y = y;
		}
		
		public void MoveRelative(int dx, int dy)
		{
			this.position.X += dx;
			this.position.Y += dy;
		}
		
		public void Draw(Cairo.Context cx)
		{
			//cx.Save();
			//cx.Color = new Color(255, 0, 0);
			//DrawRoundedRectangle(cx, this.Center.X, this.Center.Y, 10, 10, 5);
			//cx.Color = new Color(0, 255, 0);
			//DrawRoundedRectangle(cx, this.Location.X, this.Location.Y, 10, 10, 5);
			//cx.MoveTo(0, 0);
			//cx.SetDash(new double[]{6}, 0);
			//cx.LineTo((double)this.Position.X, (double)this.Position.Y);
			//cx.Stroke();
			//cx.Restore();
			
			cx.Save();
			
			cx.Translate(this.Position.X, this.Position.Y);
			cx.Scale(zoom, zoom);
			cx.Rotate(this.Rotation);
			cx.Translate(-this.size.Width/2, -this.size.Height/2);
			
			if(this.Selected)
			{
				DrawRoundedRectangle(cx, 0, 0, this.size.Width, this.size.Height, 5);
				//cx.Color = new Color(0.58, 0.65, 0.19);
				cx.Color = new Color(0, 0, 0);
				//Console.WriteLine("selc");
				cx.SetDash(new double[]{1, 0, 0,0,0,0, 1}, 0);
				cx.LineWidth = 2;
				cx.Stroke();
			}
			//DrawRoundedRectangle(cx, 0, 0, this.size.Width, this.size.Height, 5);
			//cx.Stroke();
			this.SvgHandle.RenderCairo(cx);
			
			cx.Restore();
		}
		
		private double DegToRad(double degree)
		{
			return (degree/360) * (2 * Math.PI);
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

	}
	
	public struct Size
	{
		public double Width;
		public double Height;
	}
}

