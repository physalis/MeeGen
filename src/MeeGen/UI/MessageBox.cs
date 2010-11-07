using System;
using Gtk;

namespace MeeGen
{
	public class MessageBox
	{
		public MessageBox ()
		{
			
		}
		
		public static int ShowError(string message)
		{
			MessageDialog msg = new MessageDialog(null,
			                                      DialogFlags.Modal,
			                                      MessageType.Error,
			                                      ButtonsType.Ok,
			                                      message);
			
			msg.ModifyBg(StateType.Normal, new Gdk.Color(0xeb, 0x5f, 0x54));
			msg.SetPosition(WindowPosition.CenterOnParent);
			msg.Modal = true;
			msg.Title = "Error";
			msg.Decorated = false;
			msg.GrabFocus();
			SetButtonRelief(msg);
			int result = msg.Run();
			msg.Destroy();
			return result;
		}
		
		public static int ShowInfo(string message)
		{
			MessageDialog msg = new MessageDialog(null,
			                                      DialogFlags.Modal,
			                                      MessageType.Info,
			                                      ButtonsType.Ok,
			                                      message);
			
			msg.ModifyBg(StateType.Normal, new Gdk.Color(0x54, 0xb8, 0x7b));
			msg.SetPosition(WindowPosition.CenterOnParent);
			msg.Modal = true;
			msg.Title = "Info";
			msg.Decorated = false;
			msg.GrabFocus();
			SetButtonRelief(msg);
			int result = msg.Run();
			msg.Destroy();
			return result;
		}
		
		static void SetButtonRelief(Container c)
		{
			foreach(Widget w in c.AllChildren)
			{
				Button b;
				if((b = w as Button) != null)
					b.Relief = ReliefStyle.None;
				if((w as Container) != null)
					SetButtonRelief((Container)w);
			}
		}
	}
}

