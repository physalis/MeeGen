using System;
using Gtk;

namespace MeeGen
{
	public class ExportIntroWizard : ExportWizard
	{
		public ExportIntroWizard(LayerManager layman) : base(layman)
		{
			this.SetSizeRequest(400, 300);
			
			ExportIntroPage exip = new ExportIntroPage(this);
			AppendPage(exip);
			SetPageTitle(exip, "");
			SetPageType(exip, AssistantPageType.Intro);
			SetPageHeaderImage(exip, Gdk.Pixbuf.LoadFromResource("MeeGen.Resources.Wizard.meegons-intro.png"));
			SetPageComplete(exip, false);
		}
	}
}

