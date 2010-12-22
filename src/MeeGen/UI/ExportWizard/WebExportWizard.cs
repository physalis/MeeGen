using System;
using Gtk;

namespace MeeGen
{	
	public class WebExportWizard : ExportWizard 
	{
		public WebExportWizard(ShapeManager manager) : base(manager)
		{
			SetSizeRequest(450, 300);
			
			ExportWebServicePage web = new ExportWebServicePage(this);
			AppendPage(web);
			SetPageTitle(web, "Export to a webservice");
			SetPageType(web, AssistantPageType.Intro);
			SetPageHeaderImage(web, Gdk.Pixbuf.LoadFromResource("MeeGen.Resources.Wizard.web.png"));
			SetPageComplete(web, false);
		}
	}
}
