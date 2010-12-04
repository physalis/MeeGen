using System;
using Gtk;

namespace MeeGen
{
	public delegate void FillIconViewDelegate(string arg);
	
	public class MoreMenu : Gtk.Menu
	{
		public MoreMenu (FillIconViewDelegate fillIconView)
		{
			// Move all this to a new class, derived from Gtk.Menu
			
			this.ModifyBg(StateType.Normal, new Gdk.Color(255, 255, 255));
			
			
			Pango.FontDescription font = new Pango.FontDescription();
			//font.Family = "Arial";
			font.AbsoluteSize = 15 * Pango.Scale.PangoScale;
			
			MenuItem im = new MenuItem("Items");
			foreach(Widget w in im.AllChildren)
			{
				w.ModifyFg(StateType.Normal, new Gdk.Color(0, 0, 0));
				w.ModifyFont(font);
			}
			im.Activated += delegate(object sender, EventArgs e) 
			{
				fillIconView("items");
			};
			this.Append(im);
					
			im = new MenuItem("Basic shapes");
			foreach(Widget w in im.AllChildren)
			{
				w.ModifyFg(StateType.Normal, new Gdk.Color(0, 0, 0));
				w.ModifyFont(font);
			}
			im.Activated += delegate(object sender, EventArgs e) 
			{
				fillIconView("basic-shapes");
			};
			this.Append(im);
			
			im = new MenuItem("Custom");
			foreach(Widget w in im.AllChildren)
			{
				w.ModifyFg(StateType.Normal, new Gdk.Color(0, 0, 0));
				w.ModifyFont(font);
			}
			im.Activated += delegate(object sender, EventArgs e) 
			{
				fillIconView("custom");
			};
			this.Append(im);
		}
	}
}

