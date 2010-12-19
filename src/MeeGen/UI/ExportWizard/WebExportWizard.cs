using System;
using Gtk;

namespace MeeGen
{	
	public class WebExportWizard : ExportWizard 
	{
		public WebExportWizard(LayerManager manager) : base(manager)
		{
			SetSizeRequest(450, 300);
			
			ExportWebServicePage web = new ExportWebServicePage(this);
			AppendPage(web);
			SetPageTitle(web, "Export to a webservice");
			SetPageType(web, AssistantPageType.Intro);
			SetPageHeaderImage(web, Gdk.Pixbuf.LoadFromResource("MeeGen.Resources.web.png"));
			SetPageComplete(web, false);
		}
	}
}
