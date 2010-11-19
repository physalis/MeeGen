using System;
using Gtk;

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
}
