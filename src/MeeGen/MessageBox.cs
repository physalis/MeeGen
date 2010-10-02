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
			                                      //"hello world",
			                                      message);
			
			msg.ModifyBg(StateType.Normal, new Gdk.Color(0xeb, 0x5f, 0x54));
			
			// not necessary for the default MeeGo UX but makes it look better on every other distro
			msg.Icon = Gtk.IconTheme.Default.LoadIcon (Stock.DialogError, 48, (IconLookupFlags) 0);
			//msg.ModifyText(StateType.Normal, new Gdk.Color(255, 0, 0));
			msg.SetPosition(WindowPosition.CenterOnParent);
			msg.Title = "Error";
			msg.Decorated = false;
			msg.GrabFocus();
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
			msg.Icon = Gtk.IconTheme.Default.LoadIcon (Stock.DialogInfo, 48, (IconLookupFlags) 0);
			msg.SetPosition(WindowPosition.CenterOnParent);
			msg.Title = "Info";
			msg.Decorated = false;
			msg.GrabFocus();
			    
			int result = msg.Run();
			msg.Destroy();
			return result;
		}
	}
}

