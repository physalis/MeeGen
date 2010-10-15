using System;
using System.Collections.Generic;
using System.Collections;

using Cairo;
using Rsvg;

namespace MeeGen
{
	public enum ExportFormat
	{
		PNG,
		SVG,
		JPG,
		PDF
	}
	
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
		
		public void MoveUp(Layer l)
		{
			int index = layers.IndexOf(l);
			if(index >= 0 && index < this.Count-1)
			{	
				this.Remove(l);
				this.Insert(++index, l);
			}
		}
		
		public void MoveDown(Layer l)
		{
			int index = layers.IndexOf(l);
			
			if(index > 0)
			{	
				this.Remove(l);
				this.Insert(--index, l);
			}
		}
		
		public void Remove(Layer l)
		{
			this.layers.Remove(l);
		}
		
		// Move Up/down
		
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
				selected.Select(new Point(x, y));
		}
		
		// not yet working for polygons, also error prone when rotated
		// -> needs some more math
		internal static bool PointInRectangle(Point p, Rectangle r)
		{
			return p.X >= r.X &&
				   p.X <= r.X + r.Width &&
				   p.Y <= r.Y + r.Height &&
				   p.Y >= r.Y;
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
		
		public Surface Export(string filename, Size size, ExportFormat format)
		{
			this.UnselectAll();
			
			/*double leftMost   = double.MaxValue, 
				   rightMost  = 0,
				   topMost 	  = 0,
				   bottomMost = 0;
			
			Layer leftLayer   = new Layer(),
				  rightLayer  = new Layer(),
				  topLayer    = new Layer(),
			      bottomLayer = new Layer();
			
			// 0. determine the dimensions of the final image
			// TODO: need to work with copies
			foreach(Layer l in this)
			{
				if(l.Position.X + l.Size.Width/2 > rightMost)
				{
					rightMost =  l.Position.X + l.Size.Width;
					rightLayer = l;
				}
				if(l.Position.X - l.Size.Width/2 < leftMost)
				{
					leftMost = l.Position.X - l.Size.Width/2;
					leftLayer = l;
				}
				if(l.Position.Y + l.Size.Height > topMost)
				{
					topMost = l.Position.Y + l.Size.Height;
					topLayer = l;
				}
				if(l.Position.Y - l.Size.Height > bottomMost)
				{
					bottomMost = l.Position.Y - l.Size.Height;
					bottomLayer = l;
				}
			}
			
			leftLayer.Position = new Point((int)(leftLayer.Size.Width/2), (int)(leftLayer.Size.Height/2));
			rightLayer.Position = new Point((int)(rightLayer.Position.X+rightLayer.Size.Width/2),rightLayer.Position.Y);
			
			Console.WriteLine ("top: "+topMost);
			Console.WriteLine ("bottom: "+bottomMost);
			Console.WriteLine ("right: "+rightMost);
			Console.WriteLine ("left: "+leftMost);
			SvgSurface surface = new SvgSurface(filename, (rightMost), topMost);
			*/
			Surface surface;
			
			switch(format)
			{
			case ExportFormat.SVG:
				surface = new SvgSurface(filename, size.Width, size.Height);
				break;
			case ExportFormat.PDF:
				surface = new PdfSurface(filename, size.Width, size.Height);
				break;
			case ExportFormat.PNG:
				surface = new ImageSurface(Format.ARGB32, (int)size.Width, (int)size.Height);
				break;
			default:
				surface = new SvgSurface(filename, size.Width, size.Height);
				break;
			}
			
		
			Cairo.Context c = new Context(surface);
			c.Translate(0, 0);
			this.Draw(c);
			surface.Finish();
			c.Target.Dispose();
			((IDisposable) c).Dispose ();
			
			return surface;			
		}
		
		public void Draw(Cairo.Context context)
		{
			foreach(Layer l in this)
				l.Draw(context);
		}
		
		private Layer GetSelectedLayer()
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
}

