using System;
using Gdk;
using Gtk;

namespace MeeGen
{
	public partial class PreferencesDialog : Gtk.Dialog
	{
		public PreferencesDialog ()
		{
			this.Build ();
			this.ModifyBg(StateType.Normal, new Color(255, 255, 255));
			this.notebook1.ModifyBg(StateType.Normal, new Color(255, 255, 255));
			this.notebook1.CurrentPage = 0;
			
			WidgetHelper.SetButtonRelief(this, ReliefStyle.None);
		}
	}
}

