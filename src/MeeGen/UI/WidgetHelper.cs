using System;
using Gtk;
using Gdk;

namespace MeeGen
{
	public class WidgetHelper
	{
		// prevents the instantiation of this static class
		private WidgetHelper()
		{
			
		}
		
		/// <summary>
		/// Recursively sets the ReliefStyle of every button in the specified container
		/// </summary>
		/// <param name="c">
		/// The parent container
		/// A <see cref="Container"/>
		/// </param>
		/// <param name="style">
		/// The relief style to use
		/// A <see cref="Gtk.ReliefStyle"/>
		/// </param>
		public static void SetButtonRelief(Container c, ReliefStyle style)
		{
			foreach(Widget w in c.AllChildren)
			{
				if((w as Button) != null)
					((Button)w).Relief = style;
				if((w as Container) != null)
					SetButtonRelief((Container)w, style);
			}
		}
	}
	
	public class Colors
	{
		private static Color black;
		private static Color white;
		
		static Colors()
		{
			Colors.black = new Color(0x0, 0x0, 0x0);	
			Colors.white = new Color(0xff, 0xff, 0xff);
		}
		
		public static Color Black
		{
			get{return black;}
		}
		
		public static Color White
		{
			get{return white;}
		}
		
		public static Cairo.Color GdkToCairoColor(Gdk.Color c, double alpha)
		{
			return new Cairo.Color((double)c.Red/65025, (double)c.Green/65025, (double)c.Blue/65025, alpha);	
		}
	}	
}

