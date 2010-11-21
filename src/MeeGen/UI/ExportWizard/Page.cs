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
			w5.LabelProp = "Save as local image";
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
			//slp.ShowAll();
			
			
			a.AppendPage(slp);
			a.SetPageTitle(slp, "Save locally");
			a.SetPageType(slp, AssistantPageType.Confirm);
			 
			a.SetPageComplete(this, true);
			
			a.CurrentPage++; // Move to the SaveLocalPage
			
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
		ExportWizard wiz;
		
		public SaveLocalPage(ExportWizard wiz) : base()
		{
			this.wiz = wiz;			
			//wiz.SetPageComplete(this, true);
			
			// choose filetype & size & location
			// the actual saving takes places in the 
			// ExportWizard.AssistantClose()-method
			// button to select an export-background-color

			/*FileChooserDialog dia = new FileChooserDialog("Save (Format depends on file extension (*.svg, *.pdf or *.png))",
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

				wiz.LayerManager.Export(dia.Filename, format);
				
				//TODO: Add credits to PNG and PDF as well
				if(format == ExportFormat.SVG)
				{
					System.IO.StreamWriter writer = new System.IO.StreamWriter(dia.Filename, true);
					writer.Write("<!-- created with the MeeGen avatar-generator (http://meego.com) -->");
					writer.Close();
				}
				
				wiz.Destroy();
				dia.Destroy();
				
			}else
			{
				wiz.Destroy();
				dia.Destroy();
			}	*/
			
			Build();
			
			wiz.Close += delegate(object sender, EventArgs e) 
			{
				ExportFormat format = ExportFormat.SVG; // SVG is the default saving format
				
				string filename = this.entry1.Text;
				
				format = (ExportFormat)Enum.Parse(typeof(ExportFormat), combobox1.ActiveText);

				wiz.LayerManager.Export(this.entry1.Text, format);
				
				//TODO: Add credits to PNG and PDF as well
				if(format == ExportFormat.SVG)
				{
					System.IO.StreamWriter writer = new System.IO.StreamWriter(filename, true);
					writer.Write("<!-- created with the MeeGen avatar-generator (http://meego.com) -->");
					writer.Close();
				}
			
				wiz.Destroy();
			};	
		}
		
		private global::Gtk.VBox vbox1;

		private global::Gtk.ComboBox combobox1;
	
		private global::Gtk.HBox hbox8;
	
		private global::Gtk.Label label7;
	
		private global::Gtk.Entry entry1;
	
		private global::Gtk.Button button1;
	
		private global::Gtk.Frame frame3;
	
		private global::Gtk.Alignment GtkAlignment2;
	
		private global::Gtk.HBox hbox4;
	
		private global::Gtk.VBox vbox3;
	
		private global::Gtk.HBox hbox5;
	
		private global::Gtk.Label label4;
	
		private global::Gtk.SpinButton spinbutton4;
	
		private global::Gtk.HBox hbox6;
	
		private global::Gtk.Label label5;
	
		private global::Gtk.SpinButton spinbutton5;
	
		private global::Gtk.HBox hbox9;
	
		private global::Gtk.ToggleButton togglebutton4;
	
		private global::Gtk.CheckButton checkbutton1;
	
		private global::Gtk.Label GtkLabel2;
	
		private global::Gtk.HBox hbox7;
	
		private global::Gtk.Label label6;
	
		private global::Gtk.ColorButton colorbutton1;
		
		
		//TODO: make neater
		private void Build()
		{
			this.vbox1 = new global::Gtk.VBox ();
			this.vbox1.Name = "vbox1";
			this.vbox1.Spacing = 6;
			// Container child vbox1.Gtk.Box+BoxChild
			this.combobox1 = global::Gtk.ComboBox.NewText ();
			this.combobox1.AppendText (global::Mono.Unix.Catalog.GetString ("SVG"));
			this.combobox1.AppendText (global::Mono.Unix.Catalog.GetString ("PNG"));
			this.combobox1.AppendText (global::Mono.Unix.Catalog.GetString ("PDF"));
			this.combobox1.Name = "combobox1";
			this.combobox1.Active = 0;
			this.vbox1.Add (this.combobox1);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.combobox1]));
			w1.Position = 0;
			w1.Expand = false;
			w1.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.hbox8 = new global::Gtk.HBox ();
			this.hbox8.Name = "hbox8";
			this.hbox8.Spacing = 6;
			// Container child hbox8.Gtk.Box+BoxChild
			this.label7 = new global::Gtk.Label ();
			this.label7.Name = "label7";
			this.label7.LabelProp = global::Mono.Unix.Catalog.GetString ("<b>Location: </b>");
			this.label7.UseMarkup = true;
			this.hbox8.Add (this.label7);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.hbox8[this.label7]));
			w2.Position = 0;
			w2.Expand = false;
			w2.Fill = false;
			// Container child hbox8.Gtk.Box+BoxChild
			this.entry1 = new global::Gtk.Entry ();
			this.entry1.CanFocus = true;
			this.entry1.Name = "entry1";
			this.entry1.IsEditable = true;
			this.entry1.InvisibleChar = '●';
			this.hbox8.Add (this.entry1);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.hbox8[this.entry1]));
			w3.Position = 1;
			// Container child hbox8.Gtk.Box+BoxChild
			this.button1 = new global::Gtk.Button ();
			this.button1.CanFocus = true;
			this.button1.Name = "button1";
			this.button1.UseUnderline = true;
			this.button1.Label = global::Mono.Unix.Catalog.GetString (" ... ");
			this.hbox8.Add (this.button1);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hbox8[this.button1]));
			w4.Position = 2;
			w4.Expand = false;
			w4.Fill = false;
			this.vbox1.Add (this.hbox8);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.hbox8]));
			w5.Position = 1;
			w5.Expand = false;
			w5.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.frame3 = new global::Gtk.Frame ();
			this.frame3.Name = "frame3";
			this.frame3.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child frame3.Gtk.Container+ContainerChild
			this.GtkAlignment2 = new global::Gtk.Alignment (0f, 0f, 1f, 1f);
			this.GtkAlignment2.Name = "GtkAlignment2";
			this.GtkAlignment2.LeftPadding = ((uint)(12));
			// Container child GtkAlignment2.Gtk.Container+ContainerChild
			this.hbox4 = new global::Gtk.HBox ();
			this.hbox4.Name = "hbox4";
			this.hbox4.Spacing = 6;
			// Container child hbox4.Gtk.Box+BoxChild
			this.vbox3 = new global::Gtk.VBox ();
			this.vbox3.Name = "vbox3";
			this.vbox3.Spacing = 6;
			// Container child vbox3.Gtk.Box+BoxChild
			this.hbox5 = new global::Gtk.HBox ();
			this.hbox5.Name = "hbox5";
			this.hbox5.Spacing = 6;
			// Container child hbox5.Gtk.Box+BoxChild
			this.label4 = new global::Gtk.Label ();
			this.label4.Name = "label4";
			this.label4.LabelProp = global::Mono.Unix.Catalog.GetString ("Width:");
			this.label4.UseMarkup = true;
			this.hbox5.Add (this.label4);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.hbox5[this.label4]));
			w6.Position = 0;
			w6.Expand = false;
			w6.Fill = false;
			// Container child hbox5.Gtk.Box+BoxChild
			this.spinbutton4 = new global::Gtk.SpinButton (0, 100, 1);
			this.spinbutton4.CanFocus = true;
			this.spinbutton4.Name = "spinbutton4";
			this.spinbutton4.Adjustment.PageIncrement = 10;
			this.spinbutton4.ClimbRate = 1;
			this.spinbutton4.Numeric = true;
			this.hbox5.Add (this.spinbutton4);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.hbox5[this.spinbutton4]));
			w7.PackType = ((global::Gtk.PackType)(1));
			w7.Position = 1;
			w7.Expand = false;
			w7.Fill = false;
			this.vbox3.Add (this.hbox5);
			global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.vbox3[this.hbox5]));
			w8.Position = 0;
			w8.Expand = false;
			w8.Fill = false;
			// Container child vbox3.Gtk.Box+BoxChild
			this.hbox6 = new global::Gtk.HBox ();
			this.hbox6.Name = "hbox6";
			this.hbox6.Spacing = 6;
			// Container child hbox6.Gtk.Box+BoxChild
			this.label5 = new global::Gtk.Label ();
			this.label5.Name = "label5";
			this.label5.LabelProp = global::Mono.Unix.Catalog.GetString ("Height:");
			this.label5.UseMarkup = true;
			this.hbox6.Add (this.label5);
			global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(this.hbox6[this.label5]));
			w9.Position = 0;
			w9.Expand = false;
			w9.Fill = false;
			// Container child hbox6.Gtk.Box+BoxChild
			this.spinbutton5 = new global::Gtk.SpinButton (0, 100, 1);
			this.spinbutton5.CanFocus = true;
			this.spinbutton5.Name = "spinbutton5";
			this.spinbutton5.Adjustment.PageIncrement = 10;
			this.spinbutton5.ClimbRate = 1;
			this.spinbutton5.Numeric = true;
			this.hbox6.Add (this.spinbutton5);
			global::Gtk.Box.BoxChild w10 = ((global::Gtk.Box.BoxChild)(this.hbox6[this.spinbutton5]));
			w10.PackType = ((global::Gtk.PackType)(1));
			w10.Position = 1;
			w10.Expand = false;
			w10.Fill = false;
			this.vbox3.Add (this.hbox6);
			global::Gtk.Box.BoxChild w11 = ((global::Gtk.Box.BoxChild)(this.vbox3[this.hbox6]));
			w11.PackType = ((global::Gtk.PackType)(1));
			w11.Position = 1;
			w11.Expand = false;
			w11.Fill = false;
			this.hbox4.Add (this.vbox3);
			global::Gtk.Box.BoxChild w12 = ((global::Gtk.Box.BoxChild)(this.hbox4[this.vbox3]));
			w12.Position = 0;
			w12.Expand = false;
			w12.Fill = false;
			// Container child hbox4.Gtk.Box+BoxChild
			this.hbox9 = new global::Gtk.HBox ();
			this.hbox9.Name = "hbox9";
			this.hbox9.Spacing = 6;
			// Container child hbox9.Gtk.Box+BoxChild
			this.togglebutton4 = new global::Gtk.ToggleButton ();
			this.togglebutton4.CanFocus = true;
			this.togglebutton4.Name = "togglebutton4";
			this.togglebutton4.UseUnderline = true;
			this.togglebutton4.Active = true;
			this.togglebutton4.Relief = ReliefStyle.None;
			this.togglebutton4.CanFocus = false;
			
			
			// Container child togglebutton4.Gtk.Container+ContainerChild
			global::Gtk.Alignment w33 = new global::Gtk.Alignment (0.5f, 0.5f, 0f, 0f);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w34 = new global::Gtk.HBox ();
			w34.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w35 = new global::Gtk.Image ();
			w35.Pixbuf = Gdk.Pixbuf.LoadFromResource("MeeGen.Resources.ratio-preserve.png");
			w34.Add (w35);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w37 = new global::Gtk.Label ();
			w34.Add (w37);
			w33.Add (w34);
			this.togglebutton4.Add (w33);
			
			this.hbox9.Add (this.togglebutton4);
			
			
			global::Gtk.Box.BoxChild w13 = ((global::Gtk.Box.BoxChild)(this.hbox9[this.togglebutton4]));
			w13.Position = 0;
			w13.Expand = false;
			w13.Fill = false;
			// Container child hbox9.Gtk.Box+BoxChild
			this.checkbutton1 = new global::Gtk.CheckButton ();
			this.checkbutton1.CanFocus = true;
			this.checkbutton1.Name = "checkbutton1";
			this.checkbutton1.Label = global::Mono.Unix.Catalog.GetString ("Automatic ");
			this.checkbutton1.DrawIndicator = true;
			this.checkbutton1.UseUnderline = true;
			this.checkbutton1.Active = true;

			this.hbox9.Add (this.checkbutton1);
			global::Gtk.Box.BoxChild w14 = ((global::Gtk.Box.BoxChild)(this.hbox9[this.checkbutton1]));
			w14.Position = 1;
			this.hbox4.Add (this.hbox9);
			global::Gtk.Box.BoxChild w15 = ((global::Gtk.Box.BoxChild)(this.hbox4[this.hbox9]));
			w15.Position = 1;
			this.GtkAlignment2.Add (this.hbox4);
			this.frame3.Add (this.GtkAlignment2);
			this.GtkLabel2 = new global::Gtk.Label ();
			this.GtkLabel2.Name = "GtkLabel2";
			this.GtkLabel2.LabelProp = global::Mono.Unix.Catalog.GetString ("<b>Size</b>");
			this.GtkLabel2.UseMarkup = true;
			this.GtkLabel2.SetPadding(4, 0);
			this.frame3.LabelWidget = this.GtkLabel2;
			this.vbox1.Add (this.frame3);
			global::Gtk.Box.BoxChild w18 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.frame3]));
			w18.Position = 2;
			w18.Expand = false;
			w18.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.hbox7 = new global::Gtk.HBox ();
			this.hbox7.Name = "hbox7";
			this.hbox7.Spacing = 0;
			// Container child hbox7.Gtk.Box+BoxChild
			this.label6 = new global::Gtk.Label ();
			this.label6.Name = "label6";
			this.label6.LabelProp = global::Mono.Unix.Catalog.GetString ("<b>Background color:</b>");
			this.label6.UseMarkup = true;
			this.hbox7.Add (this.label6);
			global::Gtk.Box.BoxChild w19 = ((global::Gtk.Box.BoxChild)(this.hbox7[this.label6]));
			w19.Position = 0;
			w19.Expand = false;
			w19.Fill = false;
			// Container child hbox7.Gtk.Box+BoxChild
			this.colorbutton1 = new global::Gtk.ColorButton ();
			this.colorbutton1.CanFocus = true;
			this.colorbutton1.Events = ((global::Gdk.EventMask)(784));
			this.colorbutton1.Name = "colorbutton1";
			this.colorbutton1.Relief = ReliefStyle.None;
			this.hbox7.Add (this.colorbutton1);
			global::Gtk.Box.BoxChild w20 = ((global::Gtk.Box.BoxChild)(this.hbox7[this.colorbutton1]));
			w20.Position = 1;
			this.vbox1.Add (this.hbox7);
			global::Gtk.Box.BoxChild w21 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.hbox7]));
			w21.PackType = ((global::Gtk.PackType)(1));
			w21.Position = 3;
			w21.Expand = false;
			w21.Fill = false;
				
			this.vbox1.BorderWidth = 3;
			
			this.entry1.Changed += delegate(object sender, EventArgs e) 
			{
				wiz.SetPageComplete(this, true);
			};
			
			this.button1.Clicked += delegate(object sender, EventArgs e)
			{
				FileChooserDialog dia = new FileChooserDialog("Select location",
				                                              null,
				                                              FileChooserAction.Save,
				                                              "Close", ResponseType.Close,
				                                              "Ok", ResponseType.Ok);
				
				dia.ModifyFg(StateType.Normal, new Gdk.Color(255, 255, 255));
				dia.ModifyBg(StateType.Normal, new Gdk.Color(255, 255, 255));
				
				dia.Icon = Gdk.Pixbuf.LoadFromResource("MeeGen.Resources.document-save.png");
				dia.DoOverwriteConfirmation = true;
				
				dia.CurrentName = "Untitled." + this.combobox1.ActiveText.ToLower();
				
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
				
				dia.SkipTaskbarHint = true;
				if(dia.Run() == (int)ResponseType.Ok)
				{
					this.entry1.Text = dia.Filename;
				}
				dia.Destroy();
			};
			
			this.checkbutton1.Toggled += delegate(object sender, EventArgs e) 
			{
				ToggleSizeSettings(checkbutton1.Active);
			};
			
			this.togglebutton4.Toggled += delegate(object sender, EventArgs e) 
			{
				if(togglebutton4.Active)
				{
					w35.Pixbuf = Gdk.Pixbuf.LoadFromResource("MeeGen.Resources.ratio-preserve.png");
				}else
				{
					w35.Pixbuf = Gdk.Pixbuf.LoadFromResource("MeeGen.Resources.ratio-custom.png");
				}
			};

			this.Add (this.vbox1);
			this.ShowAll();
			
			this.ToggleSizeSettings(true);
		}	
		
		private void ToggleSizeSettings(bool hide)
		{
			if(hide)
			{
				//this.vbox3.HideAll();
				this.togglebutton4.Sensitive = false;
				this.vbox3.Sensitive = false;
				//this.vbox3.Visible = false;
				//this.togglebutton4.Visible = false;
			}else
			{
				this.togglebutton4.Sensitive = true;
				this.vbox3.Sensitive = true;
				//this.vbox3.ShowAll();
				//this.togglebutton4.Show();
				//this.vbox3.Visible = true;
				//this.togglebutton4.Visible = true;
			}
		}
	}
}