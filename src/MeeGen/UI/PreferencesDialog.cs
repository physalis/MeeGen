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
		
		public Color DAColor
		{
			get{return this.DAColorSelectButton.Color;}
		}
		
		public Color TAColor
		{
			get{return this.TAColorSelectButton.Color;}
		}
	}
	
	public class MeeGenSettings
	{
		//TODO: make serializeable, make struct.
		
		private Color DrawingAreaColor;
		private Color ToolAreaColor;
		
		public MeeGenSettings(Color daColor, Color taColor)
		{
			this.DrawingAreaColor = daColor;
			this.ToolAreaColor = taColor;
		}
	}
}

