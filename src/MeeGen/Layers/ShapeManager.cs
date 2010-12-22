using System;
using System.Collections.Generic;
using System.Collections;
using Cairo;

namespace MeeGen
{
	//TODO: refactor selection stuff
	public class ShapeManager : IEnumerable
	{
		List<Shape> shapes;
		Shape selected;
		
		public ShapeManager ()
		{
			shapes = new List<Shape>();
			this.selected = new Shape();
		}
		
		public Shape this[int index]
		{
			get {return this.shapes[index];}
		}

		public IEnumerator GetEnumerator()
		{
			foreach(Shape shape in this.shapes)
				yield return shape;
		}
		
		public int Count
		{
			get {return shapes.Count;}
		}
		
		public Shape Selected
		{
			get{return this.selected;}
		}
		
		// adds a new shape 
		public void Add(Shape l)
		{
			this.shapes.Add(l);
		}
		
		public void Insert(int index, Shape l)
		{
			this.shapes.Insert(index, l);
		}
		
		public void MoveUp(Shape l)
		{
			int index = shapes.IndexOf(l);
			if(index >= 0 && index < this.Count-1)
			{	
				this.Remove(l);
				this.Insert(++index, l);
			}
		}
		
		public void MoveDown(Shape l)
		{
			int index = shapes.IndexOf(l);
			
			if(index > 0)
			{	
				this.Remove(l);
				this.Insert(--index, l);
			}
		}
		
		public void Remove(Shape l)
		{
			this.shapes.Remove(l);
		}
				
		public void Select(int index)
		{			
			if(index < this.Count && index >= 0)
			{
				this.selected.Selected = false;
				this[index].Selected = true;
				this.selected = this[index];
			}
		}
		
		public void Select(Shape l)
		{
			this.Select(this.shapes.IndexOf(l));
		}
		
		public void Select(int x, int y)
		{
			//TODO: PERF
			Shape sel = null;
			
			foreach(Shape l in this)
			{
				if(PointInRectangle(new Point(x,y),
				                    new Rectangle(new Point((int)(l.Position.X-l.Boundaries.Width/2),
				                                            (int)(l.Position.Y-l.Boundaries.Height/2)),
				                                 		    l.Boundaries.Width,
				                                  			l.Boundaries.Height)))
				{
					sel = l;
				}
			}
			if(sel != null)
			{
				this.selected.Selected = false;
				sel.Select(new Point(x, y));
				this.selected = sel;
			}
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
			this.selected = new Shape();
			this[index].Selected = false;
		}
		
		public void UnselectAll()
		{
			this.selected = new Shape();
			foreach(Shape l in this)
				l.Selected = false;
		}
		
		public void Clear()
		{
			this.shapes.Clear();
		}
		
		public void Export(string filename, ExportFormat format)
		{
			this.UnselectAll();
			
			double leftMost   = double.MaxValue, 
				   rightMost  = 0,
				   topMost 	  = double.MaxValue,
				   bottomMost = 0;
			
			Shape leftLayer   = new Shape(),
				  rightLayer  = new Shape(),
				  topLayer    = new Shape(),
			      bottomLayer = new Shape();
			
			foreach(Shape l in this)
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
					surface = new ImageSurface(Format.ARGB32,
				                           	   (int)size.Width,
				                               (int)size.Height);
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
		
		public void Export(string filename, ExportFormat format, Size size)
		{
			throw new NotImplementedException();
		}
		
		public void Draw(Cairo.Context context)
		{
			foreach(Shape l in this)
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
		
		[Obsolete]
		private Shape GetSelectedLayer()
		{
			foreach(Shape l in this)
			{
				if(l.Selected)
					return l;
			}
			// return a stub shape, easier than always checking whether 
			// the returned Shape is null
			return new Shape();
			//return null
		}
	}
}

