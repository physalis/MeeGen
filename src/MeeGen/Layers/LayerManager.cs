using System;
using System.Collections.Generic;
using System.Collections;
using Cairo;

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
		
		public void Select(Layer l)
		{
			this.Select(this.layers.IndexOf(l));
		}
		
		public void Select(int x, int y)
		{
			//TODO: PERF
			Layer selected = null;
			
			foreach(Layer l in this)
			{
				//TODO: Check for rotated shapes and test accordingly
				if(PointInRectangle(new Point(x,y),
				                    new Rectangle(new Point((int)(l.Position.X-l.Boundaries.Width/2),
				                                            (int)(l.Position.Y-l.Boundaries.Height/2)),
				                                 		    l.Boundaries.Width,
				                                  			l.Boundaries.Height)))
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
		
		public void Export(string filename, ExportFormat format)
		{
			this.UnselectAll();
			
			double leftMost   = double.MaxValue, 
				   rightMost  = 0,
				   topMost 	  = double.MaxValue,
				   bottomMost = 0;
			
			Layer leftLayer   = new Layer(),
				  rightLayer  = new Layer(),
				  topLayer    = new Layer(),
			      bottomLayer = new Layer();
			
			foreach(Layer l in this)
			{
				if(l.Position.X + l.Boundaries.Width / 2 > rightMost)
				{
					rightMost =  l.Position.X + l.Boundaries.Width / 2;
					rightLayer = l;
				}
				if(l.Position.X - l.Boundaries.Width / 2 < leftMost)
				{
					leftMost = l.Position.X - l.Boundaries.Width / 2;
					leftLayer = l;
				}
				if((l.Position.Y - l.Boundaries.Height / 2) < topMost)
				{
					topMost = l.Position.Y - l.Boundaries.Height / 2;
					topLayer = l;
				}
				if(l.Position.Y + l.Boundaries.Height > bottomMost)
				{
					bottomMost = l.Position.Y + l.Boundaries.Height;
					bottomLayer = l;
				}
			}

			Size size;
			
			//TODO: to keep or not to keep the 1px border, that is the question.
			size.Width = (rightLayer.Position.X + rightLayer.Boundaries.Width / 2) -
						 (leftLayer.Position.X - leftLayer.Boundaries.Width / 2) + 1;
			size.Height = (bottomLayer.Position.Y + bottomLayer.Boundaries.Height / 2) -
						  (topLayer.Position.Y - topLayer.Boundaries.Height / 2) + 1;
			
			Surface surface;

			//TODO: each surface is handling the size differently... BUT WHY?
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

			c.Translate(-leftLayer.Position.X + leftLayer.Boundaries.Width / 2,
			            -(topLayer.Position.Y - topLayer.Boundaries.Height / 2));

			this.Draw(c);
		
			if(format == ExportFormat.PNG)
				surface.WriteToPng(filename);
			surface.Finish();
			
			c.Target.Dispose();
	
			((IDisposable) c).Dispose ();		
		}
		
		public void Draw(Cairo.Context context)
		{
			foreach(Layer l in this)
			{
				l.Draw(context);
				
				//TODO: PERF Layer.Changed
				// it shouldn't be called for each item 
				// whenever the drawingarea is invalidated, instead
				// I should keep track on changed items (pos/zoom/...)
				// and redraw those.
				// if(l.Changed)
				// l.Draw(context);
			}
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

