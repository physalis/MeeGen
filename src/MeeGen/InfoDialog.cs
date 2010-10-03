using System;
using System.Reflection;

namespace MeeGen
{
	public partial class InfoDialog : Gtk.Dialog
	{
		public InfoDialog ()
		{
			this.Build ();
			this.ModifyBg(Gtk.StateType.Normal, new Gdk.Color(255, 255, 255));
			
			
			Pango.FontDescription font = new Pango.FontDescription();
			font.Family = "Arial";
			font.AbsoluteSize = 25 * Pango.Scale.PangoScale;

			this.label3.ModifyFont(font);
			
			this.label1.LabelProp = "<b>Version</b>\n"+
									"\t"+Assembly.GetExecutingAssembly().GetName().Version.ToString()+"\n"+
									"<b>License</b>\n"+
									"\tReleased under the GNU General Public License.\n"+
									"<b>Copyright</b>\n"+
									"\tCopyright © Christian Gulden 2010, 2010\n"+
									"<b>Additional</b>\n"+
									"\tThe characters used for the export-wizard, \n" +
									"\tthe logo and the default components \n" +
								    "\tare from the MeeGo Project - http://meego.com";
		}
		
		protected virtual void CloseClicked (object sender, System.EventArgs e)
		{
			this.Destroy();
		}
	}
}

