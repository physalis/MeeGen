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
		
		public void Export(string filename, Format format)
		{
			this.UnselectAll(); // else there may still be the selection rectangles
			
			SvgSurface surface = new SvgSurface(filename, 1000, 1000);
			//PdfSurface surface = new PdfSurface(filename, 1000, 1000);
			
			Cairo.Context c = new Context(surface);
			this.Draw(c);
			surface.Finish();
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

