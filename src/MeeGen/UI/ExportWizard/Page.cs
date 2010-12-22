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
			w3.Pixbuf = Gdk.Pixbuf.LoadFromResource("MeeGen.Resources.Wizard.save.png");
			
			w2.Add (w3);
			
			Gtk.Label w5 = new Gtk.Label ();
			w5.LabelProp = "Save as local file";
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
			w3.Pixbuf = Gdk.Pixbuf.LoadFromResource("MeeGen.Resources.Wizard.web.png");
			
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
			this.exportWeb.Clicked += ExportWebClicked;
			
			WidgetHelper.SetButtonRelief(this, ReliefStyle.None);
		}

		void ExportLocalClicked (object sender, EventArgs e)
		{
			this.a.Destroy();
			LocalExportWizard local = new LocalExportWizard(this.a.ShapeManager);
			local.Modal = true;
			local.ShowAll();
		}
		
		void ExportWebClicked (object sender, EventArgs e)
		{
			this.a.Destroy();
			WebExportWizard web = new WebExportWizard(this.a.ShapeManager);
			web.Modal = true;
			web.ShowAll();
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
			Build();
			
			wiz.Close += delegate(object sender, EventArgs e) 
			{
				ExportFormat format = ExportFormat.SVG; 
				
				string filename = this.entry1.Text;
				
				format = (ExportFormat)Enum.Parse(typeof(ExportFormat), combobox1.ActiveText.Substring(0,3));

				wiz.ShapeManager.Export(this.entry1.Text, format);
				
				//TODO: Add credits to PNG and PDF as well
				if(format == ExportFormat.SVG)
				{
					System.IO.StreamWriter writer = new System.IO.StreamWriter(filename, true);
					writer.Write("<!-- created with the MeeGen avatar-designer (http://meego.com) -->");
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
	
		private MeeGen.ColorSelectButton colorbutton1;
		
		private global::Gtk.HBox hbox1;
		
		private global::Gtk.CheckButton checkbutton2;
		
		
		//TODO: make neater
		private void Build()
		{
			this.vbox1 = new global::Gtk.VBox ();
			this.vbox1.Name = "vbox1";
			this.vbox1.Spacing = 20;
			this.vbox1.Homogeneous = true;
			// Container child vbox1.Gtk.Box+BoxChild
			this.combobox1 = global::Gtk.ComboBox.NewText ();
			this.combobox1.AppendText (global::Mono.Unix.Catalog.GetString ("SVG - Scalable Vector Graphic"));
			this.combobox1.AppendText (global::Mono.Unix.Catalog.GetString ("PNG - Portable Network Graphic"));
			this.combobox1.AppendText (global::Mono.Unix.Catalog.GetString ("PDF - Portable Document File"));
			this.combobox1.AppendText (global::Mono.Unix.Catalog.GetString ("MIF - MeeGen Image File"));
			this.combobox1.Name = "combobox1";
			this.combobox1.Active = 0;
			this.vbox1.Add (this.combobox1);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.combobox1]));
			w1.Position = 0;
			w1.Expand = true;
			w1.Fill = true;
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
			
			this.hbox1 = new global::Gtk.HBox ();
			this.hbox1.Name = "hbox1";
			this.hbox1.Spacing = 6;
						
			// Container child hbox1.Gtk.Box+BoxChild
			this.colorbutton1 = new MeeGen.ColorSelectButton();
			this.colorbutton1.CanFocus = true;
			this.colorbutton1.Events = ((global::Gdk.EventMask)(784));
			this.colorbutton1.Name = "colorbutton1";
			this.colorbutton1.SetSizeRequest(80, 15); 
			
			//this.colorbutton1.Relief = ReliefStyle.None;
			
			this.hbox1.Add (this.colorbutton1);
			
			global::Gtk.Box.BoxChild w20 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.colorbutton1]));
			w20.Position = 0;
			w20.Expand = false;
			w20.Fill = true;
			
			
			// Container child hbox1.Gtk.Box+BoxChild
			this.checkbutton2 = new global::Gtk.CheckButton ();
			this.checkbutton2.CanFocus = true;
			this.checkbutton2.Name = "checkbutton2";
			this.checkbutton2.Label = global::Mono.Unix.Catalog.GetString ("Transparent");
			this.checkbutton2.Active = true;
			this.checkbutton2.DrawIndicator = true;
			this.checkbutton2.UseUnderline = true;
			
			this.colorbutton1.Sensitive = false;
			
			this.checkbutton2.Toggled += delegate(object sender, EventArgs e) 
			{
				this.colorbutton1.Sensitive = !this.checkbutton2.Active;
			};
			
			this.hbox1.Add (this.checkbutton2);
			
			this.hbox7.Add(this.hbox1);
			
			this.vbox1.Add (this.hbox7);
			
			global::Gtk.Box.BoxChild w22 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.hbox7]));
			w22.PackType = ((global::Gtk.PackType)(1));
			w22.Position = 3;
			w22.Fill = true;
				
			this.vbox1.BorderWidth = 3;
			
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
				
				dia.CurrentName = "meegon." + this.combobox1.ActiveText.Substring(0, 3).ToLower();
				
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
				dia.SetCurrentFolder(this.entry1.Text);
				
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
			
			this.combobox1.Changed += delegate(object sender, EventArgs e) 
			{
				if(combobox1.ActiveText == "MIF - MeeGen Image File")
				{
					//TODO implement
					MessageBox.ShowInfo("Sorry, but this feature isn't implemented yet");
					this.combobox1.Active = 0;
				}else
				{
					string entry = this.entry1.Text;
					entry = entry.Substring(0, this.entry1.Text.Length-3) +
						    this.combobox1.ActiveText.Substring(0, 3).ToLower();
					this.entry1.Text = entry;
				}
			};
			
			this.entry1.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "/meegon.svg";

			this.Add (this.vbox1);
			this.ShowAll();
			
			this.ToggleSizeSettings(true);
		}	
		
		private void ToggleSizeSettings(bool hide)
		{
			this.togglebutton4.Sensitive = !hide;
			this.vbox3.Sensitive = !hide;
		}
	}
	
	public class ExportWebServicePage : Gtk.VBox
	{
		private ExportWizard wiz;
		
		private global::Gtk.Table table1;

		private global::Gtk.Button button1;
	
		private global::Gtk.Button button2;
	
		private global::Gtk.Button button3;
	
		private global::Gtk.Button button4;
		
		public ExportWebServicePage(ExportWizard wiz)
		{
			this.wiz = wiz;
			this.Build();
		}
		
		private void Build()
		{
			this.table1 = new global::Gtk.Table (((uint)(2)), ((uint)(2)), true);
			this.table1.Name = "table1";
			this.table1.RowSpacing = ((uint)(6));
			this.table1.ColumnSpacing = ((uint)(6));
			
			// Container child table1.Gtk.Table+TableChild
			this.button1 = new global::Gtk.Button ();
			this.button1.CanFocus = true;
			this.button1.Name = "button1";
			this.button1.UseUnderline = true;
			this.button1.Relief = ReliefStyle.None;
			this.button1.CanFocus = false;
			// Container child button1.Gtk.Container+ContainerChild
			global::Gtk.Alignment w1 = new global::Gtk.Alignment (0.5f, 0.5f, 0f, 0f);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w2 = new global::Gtk.HBox ();
			w2.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w3 = new global::Gtk.Image ();
			w3.Pixbuf = global::Gdk.Pixbuf.LoadFromResource ("MeeGen.Resources.Wizard.meego-forum-logo.png");
			w2.Add (w3);
			
			//
			// button1 =  Meego forum button
			//
			this.button1.Clicked += delegate(object sender, EventArgs e) 
			{
				ExportMeeGoForumPage emfp = new ExportMeeGoForumPage(this.wiz);
				wiz.AppendPage(emfp);
				wiz.SetPageType(emfp, AssistantPageType.Confirm);
				
				wiz.SetPageHeaderImage(emfp, Gdk.Pixbuf.LoadFromResource("MeeGen.Resources.Wizard.meego-forum-logo.png"));
				wiz.SetPageComplete(emfp, true);
	
				//wiz.InsertPage(wiz.GetNthPage(--wiz.CurrentPage), wiz.CurrentPage);
				wiz.CurrentPage++;
			};
			
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w5 = new global::Gtk.Label ();
			w2.Add (w5);
			w1.Add (w2);
			this.button1.Add (w1);
			this.table1.Add (this.button1);
			// Container child table1.Gtk.Table+TableChild
			this.button2 = new global::Gtk.Button ();
			this.button2.CanFocus = true;
			this.button2.Name = "button2";
			this.button2.UseUnderline = true;
			this.button2.Relief = ReliefStyle.None;
			this.button2.CanFocus = false;
			// Container child button2.Gtk.Container+ContainerChild
			global::Gtk.Alignment w10 = new global::Gtk.Alignment (0.5f, 0.5f, 0f, 0f);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w11 = new global::Gtk.HBox ();
			w11.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w12 = new global::Gtk.Image ();
			w12.Pixbuf = global::Gdk.Pixbuf.LoadFromResource ("MeeGen.Resources.Wizard.cafepress-logo.png");
			w11.Add (w12);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w14 = new global::Gtk.Label ();
			w11.Add (w14);
			w10.Add (w11);
			this.button2.Add (w10);
			this.table1.Add (this.button2);
			global::Gtk.Table.TableChild w18 = ((global::Gtk.Table.TableChild)(this.table1[this.button2]));
			w18.LeftAttach = ((uint)(1));
			w18.RightAttach = ((uint)(2));
			w18.XOptions = ((global::Gtk.AttachOptions)(4));
			w18.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.button3 = new global::Gtk.Button ();
			this.button3.CanFocus = true;
			this.button3.Name = "button3";
			this.button3.UseUnderline = true;
			this.button3.Relief = ReliefStyle.None;
			this.button3.CanFocus = false;
			// Container child button3.Gtk.Container+ContainerChild
			global::Gtk.Alignment w19 = new global::Gtk.Alignment (0.5f, 0.5f, 0f, 0f);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w20 = new global::Gtk.HBox ();
			w20.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w21 = new global::Gtk.Image ();
			w21.Pixbuf = global::Gdk.Pixbuf.LoadFromResource ("MeeGen.Resources.Wizard.gravatar-logo.png");
			w20.Add (w21);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w23 = new global::Gtk.Label ();
			w20.Add (w23);
			w19.Add (w20);
			this.button3.Add (w19);
			this.table1.Add (this.button3);
			global::Gtk.Table.TableChild w27 = ((global::Gtk.Table.TableChild)(this.table1[this.button3]));
			w27.TopAttach = ((uint)(1));
			w27.BottomAttach = ((uint)(2));
			w27.XOptions = ((global::Gtk.AttachOptions)(4));
			w27.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.button4 = new global::Gtk.Button ();
			this.button4.CanFocus = true;
			this.button4.Name = "button4";
			this.button4.UseUnderline = true;
			this.button4.Relief = ReliefStyle.None;
			this.button4.CanFocus = false;
			// Container child button4.Gtk.Container+ContainerChild
			global::Gtk.Alignment w28 = new global::Gtk.Alignment (0.5f, 0.5f, 0f, 0f);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w29 = new global::Gtk.HBox ();
			w29.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w30 = new global::Gtk.Image ();
			w30.Pixbuf = global::Gdk.Pixbuf.LoadFromResource ("MeeGen.Resources.Wizard.zazzle-logo.png");
			w29.Add (w30);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w32 = new global::Gtk.Label ();
			w29.Add (w32);
			w28.Add (w29);
			this.button4.Add (w28);
			this.table1.Add (this.button4);
			global::Gtk.Table.TableChild w36 = ((global::Gtk.Table.TableChild)(this.table1[this.button4]));
			w36.TopAttach = ((uint)(1));
			w36.BottomAttach = ((uint)(2));
			w36.LeftAttach = ((uint)(1));
			w36.RightAttach = ((uint)(2));
			w36.XOptions = ((global::Gtk.AttachOptions)(4));
			w36.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			/*this.button5 = new global::Gtk.Button ();
			this.button5.CanFocus = true;
			this.button5.Name = "button5";
			this.button5.UseUnderline = true;
			this.button5.Label = global::Mono.Unix.Catalog.GetString ("GtkButton");
			this.button5.Relief = ReliefStyle.None;
			this.table1.Add (this.button5);
			global::Gtk.Table.TableChild w37 = ((global::Gtk.Table.TableChild)(this.table1[this.button5]));
			w37.TopAttach = ((uint)(2));
			w37.BottomAttach = ((uint)(3));
			w37.XOptions = ((global::Gtk.AttachOptions)(4));
			w37.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.button6 = new global::Gtk.Button ();
			this.button6.CanFocus = true;
			this.button6.Name = "button6";
			this.button6.UseUnderline = true;
			this.button6.Label = global::Mono.Unix.Catalog.GetString ("GtkButton");
			this.button6.Relief = ReliefStyle.None;
			this.table1.Add (this.button6);
			global::Gtk.Table.TableChild w38 = ((global::Gtk.Table.TableChild)(this.table1[this.button6]));
			w38.TopAttach = ((uint)(2));
			w38.BottomAttach = ((uint)(3));
			w38.LeftAttach = ((uint)(1));
			w38.RightAttach = ((uint)(2));
			w38.XOptions = ((global::Gtk.AttachOptions)(4));
			w38.YOptions = ((global::Gtk.AttachOptions)(4));*/
			this.Add (this.table1);
		}
	}
	
	public class ExportMeeGoForumPage : Gtk.VBox
	{
		private global::Gtk.VBox vbox1;

		private global::Gtk.HBox hbox2;
	
		private global::Gtk.Label label1;
	
		private global::Gtk.Entry entry1;
	
		private global::Gtk.HBox hbox3;
	
		private global::Gtk.Label label2;
	
		private global::Gtk.Entry entry2;
	
		private global::Gtk.HBox hbox4;
	
		private global::Gtk.Label label3;
	
		private global::Gtk.HBox hbox5;
	
		private MeeGen.ColorSelectButton button1;
	
		private global::Gtk.CheckButton checkbutton1;
		
		public ExportMeeGoForumPage(ExportWizard wiz)
		{
			this.Build();
			this.ShowAll();
			
			wiz.Close += delegate(object sender, EventArgs e)
			{
				//TODO implement
				MessageBox.ShowInfo("Sorry, but this feature isn't implemeted yet.");
				wiz.Destroy();
			};
		}
		
		private void Build()
		{
			this.vbox1 = new global::Gtk.VBox ();
			this.vbox1.Name = "vbox1";
			this.vbox1.Spacing = 2;
			this.vbox1.BorderWidth = 10;
			// Container child vbox1.Gtk.Box+BoxChild
			this.hbox2 = new global::Gtk.HBox ();
			this.hbox2.Name = "hbox2";
			this.hbox2.Spacing = 6;
			// Container child hbox2.Gtk.Box+BoxChild
			this.label1 = new global::Gtk.Label ();
			this.label1.Name = "label1";
			this.label1.LabelProp = global::Mono.Unix.Catalog.GetString ("<b>Username:</b>");
			this.label1.UseMarkup = true;
			this.hbox2.Add (this.label1);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.label1]));
			w1.Position = 0;
			// Container child hbox2.Gtk.Box+BoxChild
			this.entry1 = new global::Gtk.Entry ();
			this.entry1.CanFocus = true;
			this.entry1.Name = "entry1";
			this.entry1.IsEditable = true;
			this.entry1.InvisibleChar = '●';
			this.hbox2.Add (this.entry1);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.entry1]));
			w2.Position = 1;
			this.vbox1.Add (this.hbox2);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.hbox2]));
			w3.Position = 0;
			w3.Expand = false;
			w3.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.hbox3 = new global::Gtk.HBox ();
			this.hbox3.Name = "hbox3";
			this.hbox3.Spacing = 6;
			// Container child hbox3.Gtk.Box+BoxChild
			this.label2 = new global::Gtk.Label ();
			this.label2.Name = "label2";
			this.label2.LabelProp = global::Mono.Unix.Catalog.GetString("<b>Password:</b>");
			this.label2.UseMarkup = true;
			this.hbox3.Add (this.label2);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hbox3[this.label2]));
			w4.Position = 0;
			// Container child hbox3.Gtk.Box+BoxChild
			this.entry2 = new global::Gtk.Entry ();
			this.entry2.CanFocus = true;
			this.entry2.Name = "entry2";
			this.entry2.IsEditable = true;
			this.entry2.Visibility = false;
			this.entry2.InvisibleChar = '●';
			this.hbox3.Add (this.entry2);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.hbox3[this.entry2]));
			w5.Position = 1;
			this.vbox1.Add (this.hbox3);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.hbox3]));
			w6.Position = 1;
			w6.Expand = false;
			w6.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.hbox4 = new global::Gtk.HBox ();
			this.hbox4.Name = "hbox4";
			this.hbox4.Spacing = 6;
			// Container child hbox4.Gtk.Box+BoxChild
			this.label3 = new global::Gtk.Label ();
			this.label3.Name = "label3";
			this.label3.LabelProp = global::Mono.Unix.Catalog.GetString ("<b>Background color:</b>");
			this.label3.UseMarkup = true;
			this.hbox4.Add (this.label3);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.hbox4[this.label3]));
			w7.Position = 0;
			// Container child hbox4.Gtk.Box+BoxChild
			this.hbox5 = new global::Gtk.HBox ();
			this.hbox5.Name = "hbox5";
			this.hbox5.Spacing = 6;
			this.hbox5.Homogeneous = true;
			// Container child hbox5.Gtk.Box+BoxChild
			this.button1 = new ColorSelectButton ();
			this.button1.CanFocus = true;
			this.button1.Name = "button1";
			
			//this.button1.UseUnderline = true;
			//this.button1.Label = global::Mono.Unix.Catalog.GetString ("GtkButton");
			this.hbox5.Add (this.button1);
			global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.hbox5[this.button1]));
			w8.Position = 0;
			w8.Expand = true;
			w8.Fill = true;
			
			// Container child hbox5.Gtk.Box+BoxChild
			this.checkbutton1 = new global::Gtk.CheckButton ();
			this.checkbutton1.CanFocus = true;
			this.checkbutton1.Name = "checkbutton1";
			this.checkbutton1.Label = global::Mono.Unix.Catalog.GetString ("Transparent");
			this.checkbutton1.Active = true;
			this.checkbutton1.DrawIndicator = true;
			this.checkbutton1.UseUnderline = true;
			this.hbox5.Add (this.checkbutton1);
			global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(this.hbox5[this.checkbutton1]));
			w9.Position = 1;
			this.hbox4.Add (this.hbox5);
			global::Gtk.Box.BoxChild w10 = ((global::Gtk.Box.BoxChild)(this.hbox4[this.hbox5]));
			w10.Position = 1;
			this.vbox1.Add (this.hbox4);
			global::Gtk.Box.BoxChild w11 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.hbox4]));
			w11.Position = 2;
			w11.Expand = false;
			w11.Fill = false;
			this.Add (this.vbox1);
		}
	}
}