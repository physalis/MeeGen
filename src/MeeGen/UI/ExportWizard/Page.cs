using System;
using Gtk;

namespace MeeGen
{
	public class ExportIntroPage : Gtk.VBox
	{
		Button exportLocal, exportWeb;
		ExportWizard a;
		
		public ExportIntroPage(ExportWizard a) : base()
		{
			this.Homogeneous = true;
			this.Name = "Intro_VBox";
					
			this.a = a;
			
			exportLocal = new Button();
			//exportLocal.Label = "Save as image";
			exportLocal.CanFocus = false;
			
			Alignment w1 = new Alignment (0.5f, 0.5f, 0f, 0f);
		
			HBox w2 = new HBox ();
			w2.Spacing = 2;
			
			Image w3 = new Gtk.Image ();
			w3.Pixbuf = this.RenderIcon(Stock.File, IconSize.Dialog, "");
			
			w2.Add (w3);
			
			Gtk.Label w5 = new Gtk.Label ();
			w5.LabelProp = "Save as PDF, SVG or PNG";
			w5.UseUnderline = true;
			w2.Add (w5);
			w1.Add (w2);
			this.exportLocal.Add (w1);
			
			this.Add(exportLocal);
			BoxChild w = (BoxChild)this[exportLocal];
			w.Position = 0;
			w.Expand = false;
			
			exportWeb = new Button();
			exportWeb.CanFocus = false;
			
			w1 = new Alignment (0.5f, 0.5f, 0f, 0f);
		
			w2 = new HBox ();
			w2.Spacing = 2;
			
			w3 = new Gtk.Image ();
			w3.Pixbuf = Gdk.Pixbuf.LoadFromResource("MeeGen.Resources.network.png");
			
			w2.Add (w3);
			
			w5 = new Gtk.Label ();
			w5.LabelProp = "Export to a webservice";
			w5.UseUnderline = true;
			w2.Add (w5);
			w1.Add (w2);
			this.exportWeb.Add(w1);
			
			this.Add(exportWeb);
			w = (BoxChild)this[exportWeb];
			w.Position = 1;
			w.Expand = false;	
			
			this.exportLocal.Clicked += ExportLocalClicked;
		}

		void ExportLocalClicked (object sender, EventArgs e)
		{
			SaveLocalPage slp = new SaveLocalPage(a);
			slp.ShowAll();
			
			/*
			a.AppendPage(slp);
			a.SetPageTitle(slp, "Save locally");
			a.SetPageType(slp, AssistantPageType.Summary);
			 
			a.SetPageComplete(this, true);
			
			a.CurrentPage++; // Move to the SaveLocalPage
			*/
		}
		
//		void walk(Container c, int l)
//		{
//			foreach(Widget w in c.AllChildren)
//			{
//				for(int i = 0; i < l; i++)
//				{
//					Console.Write("\t");
//				}
//				Console.WriteLine(w.Name);
//				if((w as Container) != null)
//					walk((Container)w, ++l);
//			}
//		}
	}
	
	public class SaveLocalPage : Gtk.VBox
	{
		public SaveLocalPage(ExportWizard wiz) : base()
		{
						
			//wiz.SetPageComplete(this, true);
			
			// choose filetype & size & location
			// the actual saving takes places in the 
			// ExportWizard.AssistantClose()-method
			// button to select an export-background-color

			FileChooserDialog dia = new FileChooserDialog("Save (Format depends on file extension (*.svg, *.pdf or *.png))",
			                                              wiz,
			                                              FileChooserAction.Save,
			                                              "Cancel", ResponseType.Cancel,
			    										  "Save", ResponseType.Accept);
			
			
			dia.ModifyFg(StateType.Normal, new Gdk.Color(255, 255, 255));
			dia.ModifyBg(StateType.Normal, new Gdk.Color(255, 255, 255));
			
			dia.Icon = Gdk.Pixbuf.LoadFromResource("MeeGen.Resources.document-save.png");
			dia.DoOverwriteConfirmation = true;
			dia.CurrentName = "Untitled.svg";
			
			FileFilter f = new FileFilter();
			f.Name = "SVG";
			f.AddMimeType("image/svg+xml");
			dia.AddFilter(f);
			
			f = new FileFilter();
			f.Name = "PNG";
			f.AddMimeType("image/png");
			dia.AddFilter(f);
			
			f = new FileFilter();
			f.Name = "PDF";
			f.AddMimeType("application/pdf");
			dia.AddFilter(f);
						
			WidgetHelper.SetButtonRelief(dia, ReliefStyle.None);
			
			int response = dia.Run();
			
			if(response == (int)ResponseType.Accept)
			{
				ExportFormat format = ExportFormat.SVG; // SVG is the default saving format
				
				//if(dia.Filename.ToLower().EndsWith(".svg"))
				//	format = ExportFormat.SVG;
				if(dia.Filename.ToLower().EndsWith(".pdf"))
					format = ExportFormat.PDF;
				if(dia.Filename.ToLower().EndsWith(".png"))
				    format = ExportFormat.PNG;

				wiz.LayerManager.Export(dia.Filename, new Size(500, 500), format);
								
				wiz.Destroy();
				dia.Destroy();
				
			}else
			{
				wiz.Destroy();
				dia.Destroy();
			}
			
		}
		
	}
}