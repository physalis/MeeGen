using System;
using Gtk;

namespace MeeGen
{
	public class MoreMenu : Gtk.Menu
	{
		public MoreMenu ()
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
			this.Append(im);
			
			im = new MenuItem("Basic shapes");
			foreach(Widget w in im.AllChildren)
			{
				w.ModifyFg(StateType.Normal, new Gdk.Color(0, 0, 0));
				w.ModifyFont(font);

			}
			this.Append(im);
					
			im = new MenuItem("Eyes");
			foreach(Widget w in im.AllChildren)
			{
				w.ModifyFg(StateType.Normal, new Gdk.Color(0, 0, 0));
				w.ModifyFont(font);
			}
			this.Append(im);
			
			im = new MenuItem("Other");
			foreach(Widget w in im.AllChildren)
			{
				w.ModifyFg(StateType.Normal, new Gdk.Color(0, 0, 0));
				w.ModifyFont(font);
			}
			this.Append(im);
			
			this.ShowAll();
			this.Popup();
		}
	}
}

