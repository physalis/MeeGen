using System;
using Gtk;
using Gdk;

namespace MeeGen
{
	public class ColorSelectDialog : Gtk.ColorSelectionDialog
	{
		public ColorSelectDialog () : base("Select a color")
		{
			this.Decorated = false;
			this.ColorSelection.HasOpacityControl = true;
			WidgetHelper.SetButtonRelief(this, ReliefStyle.None);
			this.ModifyBg(StateType.Normal, Colors.White);
			
			#region The MeeGo palette
			/*
			Color[] meegoPalette = new Color[8];
			Color.Parse("#57585b", ref meegoPalette[0]); // dark grey
			Color.Parse("#552987", ref meegoPalette[1]); // purple
			Color.Parse("#b5b7b4", ref meegoPalette[2]); // light grey
			Color.Parse("#eb5f54", ref meegoPalette[3]); // red
			Color.Parse("#eb2a8a", ref meegoPalette[4]); // magenta
			Color.Parse("#4fc3e6", ref meegoPalette[5]); // blue
			Color.Parse("#ffc501", ref meegoPalette[6]); // yellow
			Color.Parse("#54b87b", ref meegoPalette[7]); // green
			*/
			#endregion
			//string colors = ColorSelection.PaletteToString(meegoPalette);

			string colors = "#57585B:#552987:#B5B7B4:#EB5F54:#EB2A8A:#4FC3E6:#FFC501:#54B87B";

			this.ColorSelection.HasPalette = true;

			this.ColorSelection.Settings.SetStringProperty("gtk-color-palette",
			                                               colors+":#FFFFFF:#FFFFFF:"+colors+":#FFFFFF:#FFFFFF",
			                                               null);
		}
	}
}

