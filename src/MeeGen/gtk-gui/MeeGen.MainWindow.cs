
// This file has been generated by the GUI designer. Do not modify.
namespace MeeGen
{
	public partial class MainWindow
	{
		private global::Gtk.VBox vbox1;

		private global::Gtk.VBox vbox2;

		private global::Gtk.HBox hbox1;

		private global::Gtk.VBox vbox3;

		private global::Gtk.VButtonBox vbuttonbox1;

		private global::Gtk.Button BtnHeads;

		private global::Gtk.Button BtnHair;

		private global::Gtk.Button BtnArms;

		private global::Gtk.Button BtnBodies;

		private global::Gtk.Button BtnLegs;

		private global::Gtk.Button BtnEyes;

		private global::Gtk.Button BtnPets;

		private global::Gtk.Button BtnMore;

		private global::Gtk.HBox hbox2;

		private global::Gtk.Label label1;

		private global::Gtk.Image image8;

		private global::Gtk.Table table1;

		private global::Gtk.Button BtnAbout;

		private global::Gtk.Button BtnReset;

		private global::Gtk.Button BtnSettings;

		private global::Gtk.Button button14;

		private global::Gtk.VBox vbox6;

		private global::Gtk.HButtonBox hbuttonbox15;

		private global::Gtk.Button BtnZoomIn;

		private global::Gtk.Button BtnZoomOut;

		private global::Gtk.Button BtnRotateLeft;

		private global::Gtk.Button BtnRotateRight;

		private global::Gtk.Button BtnLayerUp;

		private global::Gtk.Button BtnLayerDown;

		private global::Gtk.Button BtnFlipH;

		private global::Gtk.Button BtnFlipV;

		private global::Gtk.Button BtnColorSelection;

		private global::Gtk.DrawingArea drawingarea;

		private global::Gtk.HBox hbox4;

		private global::Gtk.Button BtnScrollLeft;

		private global::Gtk.ScrolledWindow GtkScrolledWindow;

		private global::Gtk.IconView iconview;

		private global::Gtk.Button BtnScrollRight;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget MeeGen.MainWindow
			this.Name = "MeeGen.MainWindow";
			this.Title = global::Mono.Unix.Catalog.GetString ("MeeGen");
			this.Icon = global::Gdk.Pixbuf.LoadFromResource ("MeeGen.Resources.ico-meegen.png");
			this.WindowPosition = ((global::Gtk.WindowPosition)(1));
			this.AllowShrink = true;
			// Container child MeeGen.MainWindow.Gtk.Container+ContainerChild
			this.vbox1 = new global::Gtk.VBox ();
			this.vbox1.Name = "vbox1";
			this.vbox1.Spacing = 6;
			// Container child vbox1.Gtk.Box+BoxChild
			this.vbox2 = new global::Gtk.VBox ();
			this.vbox2.Name = "vbox2";
			this.vbox2.Spacing = 6;
			// Container child vbox2.Gtk.Box+BoxChild
			this.hbox1 = new global::Gtk.HBox ();
			this.hbox1.Name = "hbox1";
			this.hbox1.Spacing = 6;
			// Container child hbox1.Gtk.Box+BoxChild
			this.vbox3 = new global::Gtk.VBox ();
			this.vbox3.Name = "vbox3";
			this.vbox3.Spacing = 6;
			// Container child vbox3.Gtk.Box+BoxChild
			this.vbuttonbox1 = new global::Gtk.VButtonBox ();
			this.vbuttonbox1.Name = "vbuttonbox1";
			this.vbuttonbox1.Homogeneous = true;
			this.vbuttonbox1.Spacing = 2;
			this.vbuttonbox1.LayoutStyle = ((global::Gtk.ButtonBoxStyle)(1));
			// Container child vbuttonbox1.Gtk.ButtonBox+ButtonBoxChild
			this.BtnHeads = new global::Gtk.Button ();
			this.BtnHeads.CanFocus = true;
			this.BtnHeads.Name = "BtnHeads";
			this.BtnHeads.UseUnderline = true;
			this.BtnHeads.Relief = ((global::Gtk.ReliefStyle)(2));
			this.BtnHeads.Label = global::Mono.Unix.Catalog.GetString ("Heads");
			this.vbuttonbox1.Add (this.BtnHeads);
			global::Gtk.ButtonBox.ButtonBoxChild w1 = ((global::Gtk.ButtonBox.ButtonBoxChild)(this.vbuttonbox1[this.BtnHeads]));
			w1.Expand = false;
			w1.Fill = false;
			// Container child vbuttonbox1.Gtk.ButtonBox+ButtonBoxChild
			this.BtnHair = new global::Gtk.Button ();
			this.BtnHair.CanFocus = true;
			this.BtnHair.Name = "BtnHair";
			this.BtnHair.UseUnderline = true;
			this.BtnHair.Relief = ((global::Gtk.ReliefStyle)(2));
			this.BtnHair.Label = global::Mono.Unix.Catalog.GetString ("Hair");
			this.vbuttonbox1.Add (this.BtnHair);
			global::Gtk.ButtonBox.ButtonBoxChild w2 = ((global::Gtk.ButtonBox.ButtonBoxChild)(this.vbuttonbox1[this.BtnHair]));
			w2.Position = 1;
			w2.Expand = false;
			w2.Fill = false;
			// Container child vbuttonbox1.Gtk.ButtonBox+ButtonBoxChild
			this.BtnArms = new global::Gtk.Button ();
			this.BtnArms.CanFocus = true;
			this.BtnArms.Name = "BtnArms";
			this.BtnArms.UseUnderline = true;
			this.BtnArms.Relief = ((global::Gtk.ReliefStyle)(2));
			this.BtnArms.Label = global::Mono.Unix.Catalog.GetString ("Arms");
			this.vbuttonbox1.Add (this.BtnArms);
			global::Gtk.ButtonBox.ButtonBoxChild w3 = ((global::Gtk.ButtonBox.ButtonBoxChild)(this.vbuttonbox1[this.BtnArms]));
			w3.Position = 2;
			w3.Expand = false;
			w3.Fill = false;
			// Container child vbuttonbox1.Gtk.ButtonBox+ButtonBoxChild
			this.BtnBodies = new global::Gtk.Button ();
			this.BtnBodies.CanFocus = true;
			this.BtnBodies.Name = "BtnBodies";
			this.BtnBodies.UseUnderline = true;
			this.BtnBodies.Relief = ((global::Gtk.ReliefStyle)(2));
			this.BtnBodies.Label = global::Mono.Unix.Catalog.GetString ("Bodies");
			this.vbuttonbox1.Add (this.BtnBodies);
			global::Gtk.ButtonBox.ButtonBoxChild w4 = ((global::Gtk.ButtonBox.ButtonBoxChild)(this.vbuttonbox1[this.BtnBodies]));
			w4.Position = 3;
			w4.Expand = false;
			w4.Fill = false;
			// Container child vbuttonbox1.Gtk.ButtonBox+ButtonBoxChild
			this.BtnLegs = new global::Gtk.Button ();
			this.BtnLegs.CanFocus = true;
			this.BtnLegs.Name = "BtnLegs";
			this.BtnLegs.UseUnderline = true;
			this.BtnLegs.Relief = ((global::Gtk.ReliefStyle)(2));
			this.BtnLegs.Label = global::Mono.Unix.Catalog.GetString ("Legs");
			this.vbuttonbox1.Add (this.BtnLegs);
			global::Gtk.ButtonBox.ButtonBoxChild w5 = ((global::Gtk.ButtonBox.ButtonBoxChild)(this.vbuttonbox1[this.BtnLegs]));
			w5.Position = 4;
			w5.Expand = false;
			w5.Fill = false;
			// Container child vbuttonbox1.Gtk.ButtonBox+ButtonBoxChild
			this.BtnEyes = new global::Gtk.Button ();
			this.BtnEyes.CanFocus = true;
			this.BtnEyes.Name = "BtnEyes";
			this.BtnEyes.UseUnderline = true;
			this.BtnEyes.Relief = ((global::Gtk.ReliefStyle)(2));
			this.BtnEyes.Label = global::Mono.Unix.Catalog.GetString ("Eyes");
			this.vbuttonbox1.Add (this.BtnEyes);
			global::Gtk.ButtonBox.ButtonBoxChild w6 = ((global::Gtk.ButtonBox.ButtonBoxChild)(this.vbuttonbox1[this.BtnEyes]));
			w6.Position = 5;
			w6.Expand = false;
			w6.Fill = false;
			// Container child vbuttonbox1.Gtk.ButtonBox+ButtonBoxChild
			this.BtnPets = new global::Gtk.Button ();
			this.BtnPets.CanFocus = true;
			this.BtnPets.Name = "BtnPets";
			this.BtnPets.UseUnderline = true;
			this.BtnPets.Relief = ((global::Gtk.ReliefStyle)(2));
			this.BtnPets.Label = global::Mono.Unix.Catalog.GetString ("Pets");
			this.vbuttonbox1.Add (this.BtnPets);
			global::Gtk.ButtonBox.ButtonBoxChild w7 = ((global::Gtk.ButtonBox.ButtonBoxChild)(this.vbuttonbox1[this.BtnPets]));
			w7.Position = 6;
			w7.Expand = false;
			w7.Fill = false;
			// Container child vbuttonbox1.Gtk.ButtonBox+ButtonBoxChild
			this.BtnMore = new global::Gtk.Button ();
			this.BtnMore.CanFocus = true;
			this.BtnMore.Name = "BtnMore";
			this.BtnMore.Relief = ((global::Gtk.ReliefStyle)(2));
			this.BtnMore.Xalign = 0f;
			this.BtnMore.Yalign = 0f;
			this.BtnMore.BorderWidth = ((uint)(6));
			// Container child BtnMore.Gtk.Container+ContainerChild
			this.hbox2 = new global::Gtk.HBox ();
			this.hbox2.Name = "hbox2";
			this.hbox2.Spacing = 6;
			this.hbox2.BorderWidth = ((uint)(5));
			// Container child hbox2.Gtk.Box+BoxChild
			this.label1 = new global::Gtk.Label ();
			this.label1.Name = "label1";
			this.label1.Yalign = 0.8f;
			this.label1.LabelProp = global::Mono.Unix.Catalog.GetString ("More");
			this.label1.Justify = ((global::Gtk.Justification)(2));
			this.label1.WidthChars = 8;
			this.label1.SingleLineMode = true;
			this.hbox2.Add (this.label1);
			global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.label1]));
			w8.Position = 0;
			w8.Expand = false;
			w8.Fill = false;
			// Container child hbox2.Gtk.Box+BoxChild
			this.image8 = new global::Gtk.Image ();
			this.image8.Name = "image8";
			this.image8.Pixbuf = global::Gdk.Pixbuf.LoadFromResource ("MeeGen.Resources.small-arrow-right.png");
			this.hbox2.Add (this.image8);
			global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.image8]));
			w9.Position = 1;
			w9.Expand = false;
			w9.Fill = false;
			this.BtnMore.Add (this.hbox2);
			this.BtnMore.Label = null;
			this.vbuttonbox1.Add (this.BtnMore);
			global::Gtk.ButtonBox.ButtonBoxChild w11 = ((global::Gtk.ButtonBox.ButtonBoxChild)(this.vbuttonbox1[this.BtnMore]));
			w11.Position = 7;
			w11.Expand = false;
			w11.Fill = false;
			this.vbox3.Add (this.vbuttonbox1);
			global::Gtk.Box.BoxChild w12 = ((global::Gtk.Box.BoxChild)(this.vbox3[this.vbuttonbox1]));
			w12.Position = 0;
			// Container child vbox3.Gtk.Box+BoxChild
			this.table1 = new global::Gtk.Table (((uint)(2)), ((uint)(2)), true);
			this.table1.Name = "table1";
			this.table1.RowSpacing = ((uint)(5));
			this.table1.ColumnSpacing = ((uint)(20));
			this.table1.BorderWidth = ((uint)(5));
			// Container child table1.Gtk.Table+TableChild
			this.BtnAbout = new global::Gtk.Button ();
			this.BtnAbout.TooltipMarkup = "About";
			this.BtnAbout.CanFocus = true;
			this.BtnAbout.Name = "BtnAbout";
			this.BtnAbout.UseUnderline = true;
			this.BtnAbout.FocusOnClick = false;
			this.BtnAbout.Relief = ((global::Gtk.ReliefStyle)(2));
			// Container child BtnAbout.Gtk.Container+ContainerChild
			global::Gtk.Alignment w13 = new global::Gtk.Alignment (0.5f, 0.5f, 0f, 0f);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w14 = new global::Gtk.HBox ();
			w14.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w15 = new global::Gtk.Image ();
			w15.Pixbuf = global::Gdk.Pixbuf.LoadFromResource ("MeeGen.Resources.dialog-information.png");
			w14.Add (w15);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w17 = new global::Gtk.Label ();
			w14.Add (w17);
			w13.Add (w14);
			this.BtnAbout.Add (w13);
			this.table1.Add (this.BtnAbout);
			global::Gtk.Table.TableChild w21 = ((global::Gtk.Table.TableChild)(this.table1[this.BtnAbout]));
			w21.TopAttach = ((uint)(1));
			w21.BottomAttach = ((uint)(2));
			w21.LeftAttach = ((uint)(1));
			w21.RightAttach = ((uint)(2));
			w21.XOptions = ((global::Gtk.AttachOptions)(4));
			w21.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.BtnReset = new global::Gtk.Button ();
			this.BtnReset.TooltipMarkup = "Reset";
			this.BtnReset.CanFocus = true;
			this.BtnReset.Name = "BtnReset";
			this.BtnReset.UseUnderline = true;
			this.BtnReset.FocusOnClick = false;
			this.BtnReset.Relief = ((global::Gtk.ReliefStyle)(2));
			// Container child BtnReset.Gtk.Container+ContainerChild
			global::Gtk.Alignment w22 = new global::Gtk.Alignment (0.5f, 0.5f, 0f, 0f);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w23 = new global::Gtk.HBox ();
			w23.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w24 = new global::Gtk.Image ();
			w24.Pixbuf = global::Gdk.Pixbuf.LoadFromResource ("MeeGen.Resources.user-trash.png");
			w23.Add (w24);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w26 = new global::Gtk.Label ();
			w23.Add (w26);
			w22.Add (w23);
			this.BtnReset.Add (w22);
			this.table1.Add (this.BtnReset);
			global::Gtk.Table.TableChild w30 = ((global::Gtk.Table.TableChild)(this.table1[this.BtnReset]));
			w30.TopAttach = ((uint)(1));
			w30.BottomAttach = ((uint)(2));
			w30.XOptions = ((global::Gtk.AttachOptions)(4));
			w30.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.BtnSettings = new global::Gtk.Button ();
			this.BtnSettings.TooltipMarkup = "Preferences";
			this.BtnSettings.CanFocus = true;
			this.BtnSettings.Name = "BtnSettings";
			this.BtnSettings.UseUnderline = true;
			this.BtnSettings.FocusOnClick = false;
			this.BtnSettings.Relief = ((global::Gtk.ReliefStyle)(2));
			// Container child BtnSettings.Gtk.Container+ContainerChild
			global::Gtk.Alignment w31 = new global::Gtk.Alignment (0.5f, 0.5f, 0f, 0f);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w32 = new global::Gtk.HBox ();
			w32.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w33 = new global::Gtk.Image ();
			w33.Pixbuf = global::Gdk.Pixbuf.LoadFromResource ("MeeGen.Resources.edit-preferences.png");
			w32.Add (w33);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w35 = new global::Gtk.Label ();
			w32.Add (w35);
			w31.Add (w32);
			this.BtnSettings.Add (w31);
			this.table1.Add (this.BtnSettings);
			global::Gtk.Table.TableChild w39 = ((global::Gtk.Table.TableChild)(this.table1[this.BtnSettings]));
			w39.LeftAttach = ((uint)(1));
			w39.RightAttach = ((uint)(2));
			w39.XOptions = ((global::Gtk.AttachOptions)(4));
			w39.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.button14 = new global::Gtk.Button ();
			this.button14.TooltipMarkup = "Export";
			this.button14.CanFocus = true;
			this.button14.Name = "button14";
			this.button14.UseUnderline = true;
			this.button14.FocusOnClick = false;
			this.button14.Relief = ((global::Gtk.ReliefStyle)(2));
			// Container child button14.Gtk.Container+ContainerChild
			global::Gtk.Alignment w40 = new global::Gtk.Alignment (0.5f, 0.5f, 0f, 0f);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w41 = new global::Gtk.HBox ();
			w41.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w42 = new global::Gtk.Image ();
			w42.Pixbuf = global::Gdk.Pixbuf.LoadFromResource ("MeeGen.Resources.document-save.png");
			w41.Add (w42);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w44 = new global::Gtk.Label ();
			w41.Add (w44);
			w40.Add (w41);
			this.button14.Add (w40);
			this.table1.Add (this.button14);
			global::Gtk.Table.TableChild w48 = ((global::Gtk.Table.TableChild)(this.table1[this.button14]));
			w48.XOptions = ((global::Gtk.AttachOptions)(4));
			w48.YOptions = ((global::Gtk.AttachOptions)(4));
			this.vbox3.Add (this.table1);
			global::Gtk.Box.BoxChild w49 = ((global::Gtk.Box.BoxChild)(this.vbox3[this.table1]));
			w49.Position = 1;
			w49.Expand = false;
			w49.Fill = false;
			this.hbox1.Add (this.vbox3);
			global::Gtk.Box.BoxChild w50 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.vbox3]));
			w50.Position = 0;
			w50.Expand = false;
			w50.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.vbox6 = new global::Gtk.VBox ();
			this.vbox6.Name = "vbox6";
			// Container child vbox6.Gtk.Box+BoxChild
			this.hbuttonbox15 = new global::Gtk.HButtonBox ();
			this.hbuttonbox15.Name = "hbuttonbox15";
			this.hbuttonbox15.LayoutStyle = ((global::Gtk.ButtonBoxStyle)(2));
			// Container child hbuttonbox15.Gtk.ButtonBox+ButtonBoxChild
			this.BtnZoomIn = new global::Gtk.Button ();
			this.BtnZoomIn.CanFocus = true;
			this.BtnZoomIn.Name = "BtnZoomIn";
			this.BtnZoomIn.UseUnderline = true;
			this.BtnZoomIn.FocusOnClick = false;
			this.BtnZoomIn.Relief = ((global::Gtk.ReliefStyle)(2));
			// Container child BtnZoomIn.Gtk.Container+ContainerChild
			global::Gtk.Alignment w51 = new global::Gtk.Alignment (0.5f, 0.5f, 0f, 0f);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w52 = new global::Gtk.HBox ();
			w52.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w53 = new global::Gtk.Image ();
			w53.Pixbuf = global::Gdk.Pixbuf.LoadFromResource ("MeeGen.Resources.zoom-in.png");
			w52.Add (w53);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w55 = new global::Gtk.Label ();
			w52.Add (w55);
			w51.Add (w52);
			this.BtnZoomIn.Add (w51);
			this.hbuttonbox15.Add (this.BtnZoomIn);
			global::Gtk.ButtonBox.ButtonBoxChild w59 = ((global::Gtk.ButtonBox.ButtonBoxChild)(this.hbuttonbox15[this.BtnZoomIn]));
			w59.Expand = false;
			w59.Fill = false;
			// Container child hbuttonbox15.Gtk.ButtonBox+ButtonBoxChild
			this.BtnZoomOut = new global::Gtk.Button ();
			this.BtnZoomOut.CanFocus = true;
			this.BtnZoomOut.Name = "BtnZoomOut";
			this.BtnZoomOut.UseUnderline = true;
			this.BtnZoomOut.FocusOnClick = false;
			this.BtnZoomOut.Relief = ((global::Gtk.ReliefStyle)(2));
			// Container child BtnZoomOut.Gtk.Container+ContainerChild
			global::Gtk.Alignment w60 = new global::Gtk.Alignment (0.5f, 0.5f, 0f, 0f);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w61 = new global::Gtk.HBox ();
			w61.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w62 = new global::Gtk.Image ();
			w62.Pixbuf = global::Gdk.Pixbuf.LoadFromResource ("MeeGen.Resources.zoom-out.png");
			w61.Add (w62);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w64 = new global::Gtk.Label ();
			w61.Add (w64);
			w60.Add (w61);
			this.BtnZoomOut.Add (w60);
			this.hbuttonbox15.Add (this.BtnZoomOut);
			global::Gtk.ButtonBox.ButtonBoxChild w68 = ((global::Gtk.ButtonBox.ButtonBoxChild)(this.hbuttonbox15[this.BtnZoomOut]));
			w68.Position = 1;
			w68.Expand = false;
			w68.Fill = false;
			// Container child hbuttonbox15.Gtk.ButtonBox+ButtonBoxChild
			this.BtnRotateLeft = new global::Gtk.Button ();
			this.BtnRotateLeft.CanFocus = true;
			this.BtnRotateLeft.Name = "BtnRotateLeft";
			this.BtnRotateLeft.UseUnderline = true;
			this.BtnRotateLeft.FocusOnClick = false;
			this.BtnRotateLeft.Relief = ((global::Gtk.ReliefStyle)(2));
			// Container child BtnRotateLeft.Gtk.Container+ContainerChild
			global::Gtk.Alignment w69 = new global::Gtk.Alignment (0.5f, 0.5f, 0f, 0f);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w70 = new global::Gtk.HBox ();
			w70.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w71 = new global::Gtk.Image ();
			w71.Pixbuf = global::Gdk.Pixbuf.LoadFromResource ("MeeGen.Resources.object-rotate-left.png");
			w70.Add (w71);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w73 = new global::Gtk.Label ();
			w70.Add (w73);
			w69.Add (w70);
			this.BtnRotateLeft.Add (w69);
			this.hbuttonbox15.Add (this.BtnRotateLeft);
			global::Gtk.ButtonBox.ButtonBoxChild w77 = ((global::Gtk.ButtonBox.ButtonBoxChild)(this.hbuttonbox15[this.BtnRotateLeft]));
			w77.Position = 2;
			w77.Expand = false;
			w77.Fill = false;
			// Container child hbuttonbox15.Gtk.ButtonBox+ButtonBoxChild
			this.BtnRotateRight = new global::Gtk.Button ();
			this.BtnRotateRight.CanFocus = true;
			this.BtnRotateRight.Name = "BtnRotateRight";
			this.BtnRotateRight.UseUnderline = true;
			this.BtnRotateRight.FocusOnClick = false;
			this.BtnRotateRight.Relief = ((global::Gtk.ReliefStyle)(2));
			// Container child BtnRotateRight.Gtk.Container+ContainerChild
			global::Gtk.Alignment w78 = new global::Gtk.Alignment (0.5f, 0.5f, 0f, 0f);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w79 = new global::Gtk.HBox ();
			w79.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w80 = new global::Gtk.Image ();
			w80.Pixbuf = global::Gdk.Pixbuf.LoadFromResource ("MeeGen.Resources.object-rotate-right.png");
			w79.Add (w80);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w82 = new global::Gtk.Label ();
			w79.Add (w82);
			w78.Add (w79);
			this.BtnRotateRight.Add (w78);
			this.hbuttonbox15.Add (this.BtnRotateRight);
			global::Gtk.ButtonBox.ButtonBoxChild w86 = ((global::Gtk.ButtonBox.ButtonBoxChild)(this.hbuttonbox15[this.BtnRotateRight]));
			w86.Position = 3;
			w86.Expand = false;
			w86.Fill = false;
			// Container child hbuttonbox15.Gtk.ButtonBox+ButtonBoxChild
			this.BtnLayerUp = new global::Gtk.Button ();
			this.BtnLayerUp.CanFocus = true;
			this.BtnLayerUp.Name = "BtnLayerUp";
			this.BtnLayerUp.UseUnderline = true;
			this.BtnLayerUp.FocusOnClick = false;
			this.BtnLayerUp.Relief = ((global::Gtk.ReliefStyle)(2));
			// Container child BtnLayerUp.Gtk.Container+ContainerChild
			global::Gtk.Alignment w87 = new global::Gtk.Alignment (0.5f, 0.5f, 0f, 0f);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w88 = new global::Gtk.HBox ();
			w88.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w89 = new global::Gtk.Image ();
			w89.Pixbuf = global::Gdk.Pixbuf.LoadFromResource ("MeeGen.Resources.go-up.png");
			w88.Add (w89);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w91 = new global::Gtk.Label ();
			w88.Add (w91);
			w87.Add (w88);
			this.BtnLayerUp.Add (w87);
			this.hbuttonbox15.Add (this.BtnLayerUp);
			global::Gtk.ButtonBox.ButtonBoxChild w95 = ((global::Gtk.ButtonBox.ButtonBoxChild)(this.hbuttonbox15[this.BtnLayerUp]));
			w95.Position = 4;
			w95.Expand = false;
			w95.Fill = false;
			// Container child hbuttonbox15.Gtk.ButtonBox+ButtonBoxChild
			this.BtnLayerDown = new global::Gtk.Button ();
			this.BtnLayerDown.CanFocus = true;
			this.BtnLayerDown.Name = "BtnLayerDown";
			this.BtnLayerDown.UseUnderline = true;
			this.BtnLayerDown.FocusOnClick = false;
			this.BtnLayerDown.Relief = ((global::Gtk.ReliefStyle)(2));
			// Container child BtnLayerDown.Gtk.Container+ContainerChild
			global::Gtk.Alignment w96 = new global::Gtk.Alignment (0.5f, 0.5f, 0f, 0f);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w97 = new global::Gtk.HBox ();
			w97.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w98 = new global::Gtk.Image ();
			w98.Pixbuf = global::Gdk.Pixbuf.LoadFromResource ("MeeGen.Resources.go-down.png");
			w97.Add (w98);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w100 = new global::Gtk.Label ();
			w97.Add (w100);
			w96.Add (w97);
			this.BtnLayerDown.Add (w96);
			this.hbuttonbox15.Add (this.BtnLayerDown);
			global::Gtk.ButtonBox.ButtonBoxChild w104 = ((global::Gtk.ButtonBox.ButtonBoxChild)(this.hbuttonbox15[this.BtnLayerDown]));
			w104.Position = 5;
			w104.Expand = false;
			w104.Fill = false;
			// Container child hbuttonbox15.Gtk.ButtonBox+ButtonBoxChild
			this.BtnFlipH = new global::Gtk.Button ();
			this.BtnFlipH.CanFocus = true;
			this.BtnFlipH.Name = "BtnFlipH";
			this.BtnFlipH.UseUnderline = true;
			this.BtnFlipH.FocusOnClick = false;
			this.BtnFlipH.Relief = ((global::Gtk.ReliefStyle)(2));
			// Container child BtnFlipH.Gtk.Container+ContainerChild
			global::Gtk.Alignment w105 = new global::Gtk.Alignment (0.5f, 0.5f, 0f, 0f);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w106 = new global::Gtk.HBox ();
			w106.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w107 = new global::Gtk.Image ();
			w107.Pixbuf = global::Gdk.Pixbuf.LoadFromResource ("MeeGen.Resources.object-flip-horizontal.png");
			w106.Add (w107);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w109 = new global::Gtk.Label ();
			w106.Add (w109);
			w105.Add (w106);
			this.BtnFlipH.Add (w105);
			this.hbuttonbox15.Add (this.BtnFlipH);
			global::Gtk.ButtonBox.ButtonBoxChild w113 = ((global::Gtk.ButtonBox.ButtonBoxChild)(this.hbuttonbox15[this.BtnFlipH]));
			w113.Position = 6;
			w113.Expand = false;
			w113.Fill = false;
			// Container child hbuttonbox15.Gtk.ButtonBox+ButtonBoxChild
			this.BtnFlipV = new global::Gtk.Button ();
			this.BtnFlipV.CanFocus = true;
			this.BtnFlipV.Name = "BtnFlipV";
			this.BtnFlipV.UseUnderline = true;
			this.BtnFlipV.FocusOnClick = false;
			this.BtnFlipV.Relief = ((global::Gtk.ReliefStyle)(2));
			// Container child BtnFlipV.Gtk.Container+ContainerChild
			global::Gtk.Alignment w114 = new global::Gtk.Alignment (0.5f, 0.5f, 0f, 0f);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w115 = new global::Gtk.HBox ();
			w115.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w116 = new global::Gtk.Image ();
			w116.Pixbuf = global::Gdk.Pixbuf.LoadFromResource ("MeeGen.Resources.object-flip-vertical.png");
			w115.Add (w116);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w118 = new global::Gtk.Label ();
			w115.Add (w118);
			w114.Add (w115);
			this.BtnFlipV.Add (w114);
			this.hbuttonbox15.Add (this.BtnFlipV);
			global::Gtk.ButtonBox.ButtonBoxChild w122 = ((global::Gtk.ButtonBox.ButtonBoxChild)(this.hbuttonbox15[this.BtnFlipV]));
			w122.Position = 7;
			w122.Expand = false;
			w122.Fill = false;
			// Container child hbuttonbox15.Gtk.ButtonBox+ButtonBoxChild
			this.BtnColorSelection = new global::Gtk.Button ();
			this.BtnColorSelection.CanFocus = true;
			this.BtnColorSelection.Name = "BtnColorSelection";
			this.BtnColorSelection.UseUnderline = true;
			this.BtnColorSelection.FocusOnClick = false;
			this.BtnColorSelection.Relief = ((global::Gtk.ReliefStyle)(2));
			// Container child BtnColorSelection.Gtk.Container+ContainerChild
			global::Gtk.Alignment w123 = new global::Gtk.Alignment (0.5f, 0.5f, 0f, 0f);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w124 = new global::Gtk.HBox ();
			w124.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w125 = new global::Gtk.Image ();
			w125.Pixbuf = global::Gdk.Pixbuf.LoadFromResource ("MeeGen.Resources.color-chooser.png");
			w124.Add (w125);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w127 = new global::Gtk.Label ();
			w124.Add (w127);
			w123.Add (w124);
			this.BtnColorSelection.Add (w123);
			this.hbuttonbox15.Add (this.BtnColorSelection);
			global::Gtk.ButtonBox.ButtonBoxChild w131 = ((global::Gtk.ButtonBox.ButtonBoxChild)(this.hbuttonbox15[this.BtnColorSelection]));
			w131.Position = 8;
			w131.Expand = false;
			w131.Fill = false;
			this.vbox6.Add (this.hbuttonbox15);
			global::Gtk.Box.BoxChild w132 = ((global::Gtk.Box.BoxChild)(this.vbox6[this.hbuttonbox15]));
			w132.Position = 0;
			w132.Expand = false;
			w132.Fill = false;
			// Container child vbox6.Gtk.Box+BoxChild
			this.drawingarea = new global::Gtk.DrawingArea ();
			this.drawingarea.CanDefault = true;
			this.drawingarea.CanFocus = true;
			this.drawingarea.Events = ((global::Gdk.EventMask)(804));
			this.drawingarea.Name = "drawingarea";
			this.vbox6.Add (this.drawingarea);
			global::Gtk.Box.BoxChild w133 = ((global::Gtk.Box.BoxChild)(this.vbox6[this.drawingarea]));
			w133.Position = 1;
			// Container child vbox6.Gtk.Box+BoxChild
			this.hbox4 = new global::Gtk.HBox ();
			this.hbox4.Name = "hbox4";
			this.hbox4.Spacing = 6;
			// Container child hbox4.Gtk.Box+BoxChild
			this.BtnScrollLeft = new global::Gtk.Button ();
			this.BtnScrollLeft.CanFocus = true;
			this.BtnScrollLeft.Name = "BtnScrollLeft";
			this.BtnScrollLeft.UseUnderline = true;
			this.BtnScrollLeft.FocusOnClick = false;
			this.BtnScrollLeft.Relief = ((global::Gtk.ReliefStyle)(2));
			// Container child BtnScrollLeft.Gtk.Container+ContainerChild
			global::Gtk.Alignment w134 = new global::Gtk.Alignment (0.5f, 0.5f, 0f, 0f);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w135 = new global::Gtk.HBox ();
			w135.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w136 = new global::Gtk.Image ();
			w136.Pixbuf = global::Gdk.Pixbuf.LoadFromResource ("MeeGen.Resources.arrow-left.png");
			w135.Add (w136);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w138 = new global::Gtk.Label ();
			w135.Add (w138);
			w134.Add (w135);
			this.BtnScrollLeft.Add (w134);
			this.hbox4.Add (this.BtnScrollLeft);
			global::Gtk.Box.BoxChild w142 = ((global::Gtk.Box.BoxChild)(this.hbox4[this.BtnScrollLeft]));
			w142.Position = 0;
			w142.Expand = false;
			w142.Fill = false;
			// Container child hbox4.Gtk.Box+BoxChild
			this.GtkScrolledWindow = new global::Gtk.ScrolledWindow ();
			this.GtkScrolledWindow.Name = "GtkScrolledWindow";
			this.GtkScrolledWindow.HscrollbarPolicy = ((global::Gtk.PolicyType)(2));
			// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
			this.iconview = new global::Gtk.IconView ();
			this.iconview.Name = "iconview";
			this.GtkScrolledWindow.Add (this.iconview);
			this.hbox4.Add (this.GtkScrolledWindow);
			global::Gtk.Box.BoxChild w144 = ((global::Gtk.Box.BoxChild)(this.hbox4[this.GtkScrolledWindow]));
			w144.Position = 1;
			// Container child hbox4.Gtk.Box+BoxChild
			this.BtnScrollRight = new global::Gtk.Button ();
			this.BtnScrollRight.CanFocus = true;
			this.BtnScrollRight.Name = "BtnScrollRight";
			this.BtnScrollRight.UseUnderline = true;
			this.BtnScrollRight.FocusOnClick = false;
			this.BtnScrollRight.Relief = ((global::Gtk.ReliefStyle)(2));
			// Container child BtnScrollRight.Gtk.Container+ContainerChild
			global::Gtk.Alignment w145 = new global::Gtk.Alignment (0.5f, 0.5f, 0f, 0f);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w146 = new global::Gtk.HBox ();
			w146.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w147 = new global::Gtk.Image ();
			w147.Pixbuf = global::Gdk.Pixbuf.LoadFromResource ("MeeGen.Resources.arrow-right.png");
			w146.Add (w147);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w149 = new global::Gtk.Label ();
			w146.Add (w149);
			w145.Add (w146);
			this.BtnScrollRight.Add (w145);
			this.hbox4.Add (this.BtnScrollRight);
			global::Gtk.Box.BoxChild w153 = ((global::Gtk.Box.BoxChild)(this.hbox4[this.BtnScrollRight]));
			w153.Position = 2;
			w153.Expand = false;
			w153.Fill = false;
			this.vbox6.Add (this.hbox4);
			global::Gtk.Box.BoxChild w154 = ((global::Gtk.Box.BoxChild)(this.vbox6[this.hbox4]));
			w154.Position = 2;
			w154.Expand = false;
			w154.Fill = false;
			this.hbox1.Add (this.vbox6);
			global::Gtk.Box.BoxChild w155 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.vbox6]));
			w155.Position = 1;
			this.vbox2.Add (this.hbox1);
			global::Gtk.Box.BoxChild w156 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.hbox1]));
			w156.Position = 0;
			this.vbox1.Add (this.vbox2);
			global::Gtk.Box.BoxChild w157 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.vbox2]));
			w157.Position = 0;
			this.Add (this.vbox1);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.DefaultWidth = 899;
			this.DefaultHeight = 562;
			this.drawingarea.HasDefault = true;
			this.Show ();
			this.DeleteEvent += new global::Gtk.DeleteEventHandler (this.OnDeleteEvent);
			this.BtnHeads.Clicked += new global::System.EventHandler (this.CategoryButtonClicked);
			this.BtnHair.Clicked += new global::System.EventHandler (this.CategoryButtonClicked);
			this.BtnArms.Clicked += new global::System.EventHandler (this.CategoryButtonClicked);
			this.BtnBodies.Clicked += new global::System.EventHandler (this.CategoryButtonClicked);
			this.BtnLegs.Clicked += new global::System.EventHandler (this.CategoryButtonClicked);
			this.BtnEyes.Clicked += new global::System.EventHandler (this.CategoryButtonClicked);
			this.BtnPets.Clicked += new global::System.EventHandler (this.CategoryButtonClicked);
			this.BtnMore.Clicked += new global::System.EventHandler (this.MoreButtonClicked);
			this.button14.Clicked += new global::System.EventHandler (this.ExportButtonClicked);
			this.BtnSettings.Clicked += new global::System.EventHandler (this.SettingsButtonClicked);
			this.BtnReset.Clicked += new global::System.EventHandler (this.ResetButtonClicked);
			this.BtnAbout.Clicked += new global::System.EventHandler (this.AboutButtonClicked);
			this.BtnZoomIn.Clicked += new global::System.EventHandler (this.ZoomInButtonClicked);
			this.BtnZoomOut.Clicked += new global::System.EventHandler (this.ZoomOutButtonClicked);
			this.BtnRotateLeft.Clicked += new global::System.EventHandler (this.RotateLeftButtonClicked);
			this.BtnRotateRight.Clicked += new global::System.EventHandler (this.RotateRightButtonClicked);
			this.BtnLayerUp.Clicked += new global::System.EventHandler (this.LayerUpButtonClicked);
			this.BtnLayerDown.Clicked += new global::System.EventHandler (this.LayerDownButtonClicked);
			this.BtnFlipH.Clicked += new global::System.EventHandler (this.FlipHButtonClicked);
			this.BtnFlipV.Clicked += new global::System.EventHandler (this.FlipVButtonClicked);
			this.BtnColorSelection.Clicked += new global::System.EventHandler (this.ColorSelectionButtonClicked);
			this.drawingarea.ExposeEvent += new global::Gtk.ExposeEventHandler (this.DrawingAreaExpose);
			this.drawingarea.DragDataReceived += new global::Gtk.DragDataReceivedHandler (this.DragData_Received);
			this.drawingarea.ButtonReleaseEvent += new global::Gtk.ButtonReleaseEventHandler (this.DrawingAreaClickReleased);
			this.drawingarea.ButtonPressEvent += new global::Gtk.ButtonPressEventHandler (this.DrawingAreaPress);
			this.drawingarea.MotionNotifyEvent += new global::Gtk.MotionNotifyEventHandler (this.DrawingAreaMotionNotify);
			this.drawingarea.KeyPressEvent += new global::Gtk.KeyPressEventHandler (this.OnDrawingareaKeyPressEvent);
			this.BtnScrollLeft.Clicked += new global::System.EventHandler (this.ScrollLeftButtonClicked);
			this.iconview.DragDataGet += new global::Gtk.DragDataGetHandler (this.DragData_Get);
			this.iconview.DragBegin += new global::Gtk.DragBeginHandler (this.Drag_Begin);
			this.BtnScrollRight.Clicked += new global::System.EventHandler (this.ScrollRightButtonClicked);
		}
	}
}
