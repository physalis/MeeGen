using System;
using System.Reflection;
using System.Diagnostics;

namespace MeeGen
{
	public partial class InfoDialog : Gtk.Dialog
	{	
		// The Point where the user started pressing the left mouse button
		Gdk.Point offset = Gdk.Point.Zero;
		
		// the Gtk.MotionNotifyEventArgs does not contain Information about the
		// Button states, so I collect all necessary information using 
		// the MouseDown and MouseUp-events.
		bool mouseDown = false;
		
		public InfoDialog ()
		{
			this.Build ();
			this.ModifyBg(Gtk.StateType.Normal, new Gdk.Color(255, 255, 255));
			
			
			Pango.FontDescription font = new Pango.FontDescription();
			font.Family = "Arial";
			font.AbsoluteSize = 25 * Pango.Scale.PangoScale;

			this.label3.ModifyFont(font);
			
			System.Version ver = Assembly.GetExecutingAssembly().GetName().Version;
			
			this.label1.LabelProp = "<b>Version</b>\n"+
									"\t"+ver.Major.ToString()+"."+ver.Minor.ToString()+"\n"+
									"<b>License</b>\n"+
									"\tReleased under the GNU General Public License.\n"+
									"<b>Copyright</b>\n"+
									"\tCopyright © Christian Gulden 2010\n\n"+
									"The default components are from the MeeGo Project\n" +
								    "\t\t<big><u>http://meego.com</u></big> ";

		}
		
		protected virtual void MouseDown (object o, Gtk.ButtonPressEventArgs args)
		{
			if(args.Event.Button == 1)
			{
				this.offset = new Gdk.Point((int)args.Event.X, (int)args.Event.Y);
				mouseDown = true;
			}
		}
		
		
		// allows the user to drag the Info-Dialog, even if it is not
		// Decorated with a title bar
		protected virtual void MouseMove (object o, Gtk.MotionNotifyEventArgs args)
		{
			if (mouseDown)
            {
                Gdk.Point delta = new Gdk.Point((int)(args.Event.X - offset.X), (int)(args.Event.Y - offset.Y));
				
				int x, y;
				this.GetPosition(out x, out y);

				this.Move(x + delta.X, y + delta.Y);
			}
		}
		
		protected virtual void MouseUp (object o, Gtk.ButtonReleaseEventArgs args)
		{
			if(args.Event.Button == 1)
			{
				mouseDown = false;
			}
		}
		
		protected virtual void CloseClicked (object sender, System.EventArgs e)
		{
			this.Destroy();
		}
	}
}

