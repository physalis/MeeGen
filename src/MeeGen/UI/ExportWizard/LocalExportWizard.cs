using System;
using Gtk;

namespace MeeGen
{
	
	public class LocalExportWizard : ExportWizard
	{	
		public LocalExportWizard(ShapeManager manager) : base(manager)
		{			
			SetSizeRequest(400, 300);
			
			SaveLocalPage local = new SaveLocalPage(this);
			AppendPage(local);
			SetPageTitle(local, "Save locally");
			SetPageType(local, AssistantPageType.Confirm);
			SetPageHeaderImage(local, Gdk.Pixbuf.LoadFromResource("MeeGen.Resources.document-save.png"));
			SetPageComplete(local, true);
		}
	}
}

