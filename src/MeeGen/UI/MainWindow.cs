using System;
using System.IO;
using System.Xml;
using System.Text;
using Gtk;
using System.Collections.Generic;
using Cairo;

namespace MeeGen
{
	public partial class MainWindow : Gtk.Window
	{
		// I really don't know if I need both... 
		// Also Pixbuf may not be the right choice for SVG's
		Dictionary<string, ListStore> iconDict; // scaled images
		Dictionary<string, string[]> imageDict; // contains image location,
												// might be better if I directly load Rsvg.Handles
		
		LayerManager layerManager;
		
		//Point last;
		
		public MainWindow(string databaseFile) : base(Gtk.WindowType.Toplevel)
		{
			Build();
			
			this.drawingarea.ModifyBg(StateType.Normal, new Gdk.Color(105, 89, 205));
			//this.drawingarea.ModifyBg(StateType.Normal, new Gdk.Color(255, 255, 255));
			this.drawingarea.DoubleBuffered = true;
			this.ModifyBg(StateType.Normal, new Gdk.Color(0xb5, 0xb7, 0xb4));

			// doens't look bad in light grey, might add a settings xml file for such options
			//this.iconview.ModifyBase(StateType.Normal, new Gdk.Color(0xb5, 0xb7, 0xb4));
			
			this.ModifyBg(StateType.Normal, new Gdk.Color(255, 255, 255));
			
			/*
			this.GtkScrolledWindow.VScrollbar.ModifyBg(Gtk.StateType.Normal, new Gdk.Color(0xb5, 0xb7, 0xb4));
			this.GtkScrolledWindow.VScrollbar.ModifyFg(Gtk.StateType.Normal, new Gdk.Color(0xb5, 0xb7, 0xb4));
			this.GtkScrolledWindow.VScrollbar.ModifyBase(Gtk.StateType.Normal, new Gdk.Color(0xb5, 0xb7, 0xb4));
			this.GtkScrolledWindow.ModifyBase(Gtk.StateType.Normal, new Gdk.Color(0xb5, 0xb7, 0xb4));
			this.GtkScrolledWindow.ModifyFg(Gtk.StateType.Normal, new Gdk.Color(0xb5, 0xb7, 0xb4));
			this.GtkScrolledWindow.ModifyBg(Gtk.StateType.Normal, new Gdk.Color(0xb5, 0xb7, 0xb4));
			*/
			
			this.GtkScrolledWindow.VScrollbar.ModifyBg(Gtk.StateType.Normal, new Gdk.Color(255, 255, 255));
			this.GtkScrolledWindow.VScrollbar.ModifyFg(Gtk.StateType.Normal, new Gdk.Color(255, 255, 255));
			this.GtkScrolledWindow.VScrollbar.ModifyBase(Gtk.StateType.Normal, new Gdk.Color(255, 255, 255));
					
			this.iconDict = new Dictionary<string, ListStore>();
			this.imageDict = new Dictionary<string, string[]>();
			
			this.layerManager = new LayerManager();
			
			// scroll to the beginning of the iconview list 
			// required for the buttons, because on MeeGo they don't work
			// unless the scrollbar is on the very beginning.
			// It causes a Gtk-CRITICAL but it works...
			//this.iconview.ScrollToPath(new TreePath("0"));
			
			this.iconview.PixbufColumn = 0;
			
			try
			{
				LoadImages(databaseFile);
				this.iconview.Model = iconDict["heads"];
				this.Category = "heads";
			}catch(Exception e)
			{
				MessageBox.ShowError(e.Message);
				//Application.Quit() fails for some reason
				System.Diagnostics.Process.GetCurrentProcess().Kill();  
			}
			
			// the drag and drop table, that contains destination and source targets
			Gtk.TargetEntry[] dd_table = new Gtk.TargetEntry[]{new Gtk.TargetEntry("text/plain", 0, 0)};
			
			// Set up the drawingarea as a drop destination
			Gtk.Drag.DestSet(drawingarea, DestDefaults.All, dd_table,Gdk.DragAction.Copy);
			
			// Set up the iconview as the drag source
			Gtk.Drag.SourceSet(iconview, Gdk.ModifierType.Button1Mask, dd_table, Gdk.DragAction.Copy);
		}
		
#region 8< ------------ Properties ---------------- 
		
		private string Category
		{
			get;
			set;
		}
		
#endregion
		
#region 8< ------------ Methods ---------------- 
		
		protected virtual void CategoryButtonClicked (object sender, System.EventArgs e)
		{
			FillIconView(((Button)sender).Label.ToLower());
		}
		
		protected virtual void LoadImages(string xmlFile)
		{
			string category = "";
			string parentDirectory = "";
			
			ListStore store = new ListStore(typeof(Gdk.Pixbuf));
			List<string> list = new List<string>();
	
			XmlReader reader = new XmlTextReader(xmlFile);
		
			reader.MoveToContent();
			parentDirectory = reader.GetAttribute("directory");
			parentDirectory += parentDirectory.EndsWith("/") ? "" : "/";
		
			reader.Read(); // it requires two reader.Reads  
			reader.Read(); // to advance to the next element
		
			while(!reader.EOF)
			{
				category = reader.GetAttribute("name");
			
				reader.Read(); // Move from category
				reader.Read(); // to the first entry
	
				while(reader.Name == "entry")
				{
					string image = reader.GetAttribute("image");
				
					StringBuilder builder = new StringBuilder(parentDirectory);
					builder.Append(category); // parentDirectory has a trailing "/"
					builder.Append("/" + image);
				
					store.AppendValues(new Gdk.Pixbuf(builder.ToString(), 80, 80));
					list.Add(builder.ToString());
					
					reader.Read(); // moves to next entry
					reader.Read(); // or category
				}
				// Only add a new store if we are in category end context
				if(reader.Name == "category")
				{
					this.iconDict.Add(category, store);
					this.imageDict.Add(category, list.ToArray());
					
					list = new List<string>();
					store = new ListStore(typeof(Gdk.Pixbuf));
				}
			
				reader.Read();
				reader.Read();
			}
			reader.Close();
		}
		
		protected virtual void FillIconView(string category)
		{
			this.iconview.Model = this.iconDict[category];
			this.iconview.ScrollToPath(new TreePath("0"));
			this.Category = category;
		}
#endregion
		
#region 8< ------------ Widget Events ---------------- 
		
		protected virtual void MoreButtonClicked (object sender, System.EventArgs e)
		{
			// Move all this to a new class, derived from Gtk.Menu
			//MessageBox.ShowInfo("Sorry, but this feature isn't implemented yet.");
			Menu m = new Menu();
			m.ModifyBg(StateType.Normal, new Gdk.Color(255, 255, 255));
			
			
			Pango.FontDescription font = new Pango.FontDescription();
			//font.Family = "Arial";
			font.AbsoluteSize = 15 * Pango.Scale.PangoScale;
			
			MenuItem im = new MenuItem("Items");
			foreach(Widget w in im.AllChildren)
			{
				w.ModifyFg(StateType.Normal, new Gdk.Color(0, 0, 0));
				w.ModifyFont(font);
			}
			m.Append(im);
			
			im = new MenuItem("Basic shapes");
			foreach(Widget w in im.AllChildren)
			{
				w.ModifyFg(StateType.Normal, new Gdk.Color(0, 0, 0));
				w.ModifyFont(font);

			}
			m.Append(im);
					
			im = new MenuItem("Eyes");
			foreach(Widget w in im.AllChildren)
			{
				w.ModifyFg(StateType.Normal, new Gdk.Color(0, 0, 0));
				w.ModifyFont(font);
			}
			m.Append(im);
			
			im = new MenuItem("Other");
			foreach(Widget w in im.AllChildren)
			{
				w.ModifyFg(StateType.Normal, new Gdk.Color(0, 0, 0));
				w.ModifyFont(font);
			}
			m.Append(im);
			
			m.ShowAll();
			m.Popup();
		}
		
		protected virtual void DrawingAreaExpose (object o, Gtk.ExposeEventArgs args)
		{			
			Widget w = o as Widget;
			
		    Cairo.Context g = Gdk.CairoHelper.Create(w.GdkWindow);
		    g.Antialias = Antialias.Subpixel; 
			
			this.layerManager.Draw(g);
			
			((IDisposable) g.Target).Dispose ();                                      
		    ((IDisposable) g).Dispose ();
		}
		
		protected virtual void DragData_Get (object o, Gtk.DragDataGetArgs args)
		{
			// make sure, that dragging empty space (which is possible)
			// won't result in an exception
			if(!(this.iconview.SelectedItems.Length <= 0)) 
			{
				args.SelectionData.Set(args.Context.Targets[0],
				                       8,
				                       System.Text.Encoding.UTF8.GetBytes(this.imageDict[this.Category][Convert.ToInt32(this.iconview.SelectedItems[0].ToString())]));
				//args.SelectionData.SetPixbuf(image);
			}
		}
		
		protected virtual void Drag_Begin (object o, Gtk.DragBeginArgs args)
		{
			TreeIter iter;
			Gdk.Pixbuf image;
			
			if(this.iconview.SelectedItems.Length <= 0)
			{
				// 'notify' the user about the fact that he is dragging an invalid object
				Gtk.Drag.SetIconStock(args.Context, Stock.DialogError, 0, 0);
			}
			else
			{
				this.iconview.Model.GetIterFromString(out iter,
				                                      this.iconview.SelectedItems[0].ToString());
				
				image = Rsvg.Tool.PixbufFromFile(this.imageDict[this.Category][Convert.ToInt32(this.iconview.SelectedItems[0].ToString())]);
				
				Gtk.Drag.SetIconPixbuf(args.Context,
			    	                   image,
			        	               image.Width/2, 
			            	           image.Height/2);
			}
		}
		
		protected virtual void DragData_Received (object o, Gtk.DragDataReceivedArgs args)
		{			
			this.layerManager.Add(new Layer(args.SelectionData.Text, new Point(args.X, args.Y) ));
			this.layerManager.UnselectAll();
			Gtk.Drag.Finish(args.Context, true, false, args.Time);
		}

		protected virtual void ResetButtonClicked (object sender, System.EventArgs e)
		{
			if(this.layerManager.Selected.Size.Width != 0) //TODO: replace with != null
				this.layerManager.Remove(this.layerManager.Selected);
			else
				this.layerManager.Clear(); // clear the list of layers, so when
			
			this.drawingarea.QueueDraw(); // causing an expose here, nothing will be drawn
		}

		protected virtual void ExportButtonClicked (object sender, System.EventArgs e)
		{
			//MessageBox.ShowInfo("Sorry, but this feature isn't implemented yet.");
			this.layerManager.Export("./fooo.svg", Format.ARGB32);
		}

		protected virtual void AboutButtonClicked (object sender, System.EventArgs e)
		{
			InfoDialog info = new InfoDialog();
			info.Run();
		}
		
		protected virtual void ScrollRightButtonClicked (object sender, System.EventArgs e)
		{
			TreePath start, end;
			this.iconview.GetVisibleRange(out start, out end);
			
			int next = Convert.ToInt32(end.ToString()) + 1;
			
			this.iconview.ScrollToPath(new TreePath(next.ToString()));
		}
		
		protected virtual void ScrollLeftButtonClicked (object sender, System.EventArgs e)
		{
			TreePath start, end;
			this.iconview.GetVisibleRange(out start, out end);
			
			int next = Convert.ToInt32(start.ToString()) - 1;
			
			// it won't crash if it's a negative value but it results in a Gtk-WARNING 
			// and a Gtk-CRITICAL message on stdout.
			next = next < 0 ? 0 : next; 
				
			this.iconview.ScrollToPath(new TreePath(next.ToString()));
		}
		
		protected virtual void ZoomInButtonClicked (object sender, System.EventArgs e)
		{
			foreach(Layer l in layerManager)
				l.ZoomIn(2);
			this.drawingarea.QueueDraw();
		}
		
		protected virtual void ZoomOutButtonClicked (object sender, System.EventArgs e)
		{
			foreach(Layer l in layerManager)
				l.ZoomOut(2);
			this.drawingarea.QueueDraw();
		}
		
		protected virtual void RotateLeftButtonClicked (object sender, System.EventArgs e)
		{
			this.layerManager.Selected.Rotation -= 0.1;
			this.drawingarea.QueueDraw();
		}
		
		protected virtual void RotateRightButtonClicked (object sender, System.EventArgs e)
		{
			this.layerManager.Selected.Rotation += 0.1;
			this.drawingarea.QueueDraw();
		}
		
		protected virtual void FlipButtonClicked (object sender, System.EventArgs e)
		{
			MessageBox.ShowInfo("Sorry, but this feature isn't implemented yet.");
		}
		
		protected virtual void SettingsButtonClicked (object sender, System.EventArgs e)
		{
			MessageBox.ShowInfo("Sorry, but this feature isn't implemented yet.");
			//SettingsDialog dia  = new SettingsDialog();
			//dia.Run();
		}
		
		protected virtual void ColorSelectionButtonClicked (object sender, System.EventArgs e)
		{
			MessageBox.ShowInfo("Sorry, but this feature isn't implemented yet.");
		}
		
		protected virtual void DrawingAreaClickReleased (object o, Gtk.ButtonReleaseEventArgs args)
		{
			//this.drawingarea.GdkWindow.Cursor = new Gdk.Cursor(Gdk.CursorType.Arrow);
			//Console.WriteLine(args.Event.X + " " + args.Event.Y);
		}
		
		protected virtual void DrawingAreaPress (object o, Gtk.ButtonPressEventArgs args)
		{
			//this.drawingarea.GdkWindow.Cursor = new Gdk.Cursor(Gdk.CursorType.Hand1);
			this.layerManager.UnselectAll();
			this.layerManager.Select((int)args.Event.X, (int)args.Event.Y);
			this.drawingarea.QueueDraw();
		}
				
		protected virtual void DrawingAreaMotionNotify (object o, Gtk.MotionNotifyEventArgs args)
		{
			if((args.Event.State & Gdk.ModifierType.Button1Mask) == Gdk.ModifierType.Button1Mask)
			{
				this.layerManager.Selected.Drag(DragLocation.Inside,
				                                (int)args.Event.X,
				                                (int)args.Event.Y);
				this.drawingarea.QueueDraw();
			}
			if((args.Event.State & Gdk.ModifierType.Button3Mask) == Gdk.ModifierType.Button3Mask)
			{
				this.layerManager.Selected.Drag(DragLocation.Resize,
				                                (int)args.Event.X,
				                                (int)args.Event.Y);
				this.drawingarea.QueueDraw();
			}
		}
		
		protected virtual void LayerDownButtonClicked (object sender, System.EventArgs e)
		{
			this.layerManager.MoveDown(this.layerManager.Selected);
			this.drawingarea.QueueDraw();
		}
		
		protected virtual void LayerUpButtonClicked (object sender, System.EventArgs e)
		{
			this.layerManager.MoveUp(this.layerManager.Selected);
			this.drawingarea.QueueDraw();
		}
		
#endregion	
		
		protected void OnDeleteEvent (object sender, DeleteEventArgs a)
		{
			Application.Quit ();
			a.RetVal = true;
		}		
	}
}