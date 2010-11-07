using System;
using Gtk;

namespace MeeGen
{
	public class WidgetHelper
	{
		private WidgetHelper()
		{
			
		}
		
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

