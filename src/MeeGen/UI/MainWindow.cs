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
		//TODO: PERF Do I need both? 
		Dictionary<string, ListStore> iconDict; // scaled images
		Dictionary<string, string[]> imageDict; // contains image location,
												// might be better if I directly load Rsvg.Handles
		ShapeManager shapeManager;
		
		MoreMenu moreMenu;
		
		string preferencesFile;
		
		public MainWindow(string databaseFile, string preferencesFile) : base(Gtk.WindowType.Toplevel)
		{
			Build();
			
			this.drawingarea.DoubleBuffered = true;
			this.iconview.PixbufColumn = 0;
			
			this.moreMenu = new MoreMenu(FillIconView);
			this.moreMenu.ShowAll();
			
			//this.GtkScrolledWindow.VScrollbar.ModifyBg(Gtk.StateType.Normal, new Gdk.Color(255, 255, 255));
			//this.GtkScrolledWindow.VScrollbar.ModifyFg(Gtk.StateType.Normal, new Gdk.Color(255, 255, 255));
			//this.GtkScrolledWindow.VScrollbar.ModifyBase(Gtk.StateType.Normal, new Gdk.Color(255, 255, 255));
					
			this.iconDict = new Dictionary<string, ListStore>();
			this.imageDict = new Dictionary<string, string[]>();
			
			this.shapeManager = new ShapeManager();
			
			try
			{
				LoadImages(databaseFile);
				FillIconView("heads");
				
			}catch(Exception e)
			{
				MessageBox.ShowError(e.Message);
				//Application.Quit() fails for some reason
				System.Diagnostics.Process.GetCurrentProcess().Kill();  
			}
			
			// the drag and drop table that contains destination and source targets
			Gtk.TargetEntry[] dd_table = new Gtk.TargetEntry[]{new Gtk.TargetEntry("text/plain", 0, 0)};
			
			// Set up the drawingarea as a drop destination
			Gtk.Drag.DestSet(drawingarea, DestDefaults.All, dd_table, Gdk.DragAction.Copy);
			
			// Set up the iconview as the drag source
			Gtk.Drag.SourceSet(iconview, Gdk.ModifierType.Button1Mask, dd_table, Gdk.DragAction.Copy);
			
			this.preferencesFile = preferencesFile;
			this.LoadPreferences();
		}
		
#region 8< ------------ Properties ---------------- 
		
		private string Category
		{
			get;
			set;
		}
		
#endregion
		
#region 8< ------------ Methods ---------------- 
		
		private void LoadImages(string xmlFile)
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
					
					Gdk.Pixbuf pic = new Gdk.Pixbuf(builder.ToString(), 80, 80);
					//store.AppendValues(pic.ScaleSimple(80, 80, Gdk.InterpType.Tiles));
					store.AppendValues(pic);
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
		
		private void FillIconView(string category)
		{
			this.iconview.Model = this.iconDict[category];
			this.iconview.ScrollToPath(new TreePath("0"));
			this.Category = category;
		}
		
		private void LoadPreferences()
		{
			string daclr = "";
			string taclr = "";
			string selclr = "";
			
			try
			{
				XmlTextReader xmlReader = new XmlTextReader(this.preferencesFile);
				xmlReader.MoveToContent();
				xmlReader.MoveToFirstAttribute();
				
				xmlReader.Read();
				xmlReader.Read();
				
				daclr = xmlReader.GetAttribute("drawing-area");
				taclr = xmlReader.GetAttribute("tool-area");
				selclr = xmlReader.GetAttribute("selection");
				
				xmlReader.Close();
				
			}catch(IOException ioex)
			{
				MessageBox.ShowError("There was an error loading the preferences file.\n" + ioex.Message);
			}catch(Exception ex)
			{
				MessageBox.ShowError("There was an error reading the preferences file.\n" + ex.Message);
			}
			
			// drawing area
			Gdk.Color c = new Gdk.Color(0, 0, 0);
			Gdk.Color.Parse(daclr, ref c);
			this.drawingarea.ModifyBg(StateType.Normal, c);
			
			// tool area
			Gdk.Color.Parse(taclr, ref c);
			this.ModifyBg(StateType.Normal, c);
			this.iconview.ModifyBase(StateType.Normal, c);
			
			// selection rectangle (no effect)
			Gdk.Color.Parse(selclr, ref c);
			Shape.SelectionColor = new Cairo.Color((double)c.Red/65025,
			                                       (double)c.Green/65025,
			                                       (double)c.Blue/65025,
			                                       0.5);
		}
		
#endregion
		
#region 8< ------------ Widget Events ---------------- 
		
		protected virtual void CategoryButtonClicked (object sender, System.EventArgs e)
		{
			FillIconView(((Button)sender).Label.ToLower());
		}
		
		protected virtual void MoreButtonClicked (object sender, System.EventArgs e)
		{
			moreMenu.Popup();
		}
		
		protected virtual void DrawingAreaExpose (object o, Gtk.ExposeEventArgs args)
		{			
			this.drawingarea.GrabFocus();
			Widget w = o as Widget;
			
			int width, height;
			w.GdkWindow.GetSize(out width, out height);
			
		    Cairo.Context g = Gdk.CairoHelper.Create(w.GdkWindow);
		    g.Antialias = Antialias.Subpixel;
			
			g.Save();
			g.LineWidth = 2;
			g.Color = new Color(0, 0, 0);
			g.Rectangle(0, 0, width, height);
			g.Stroke();
			g.Restore();
			
			this.shapeManager.Draw(g);

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
			}
		}
		
		protected virtual void Drag_Begin (object o, Gtk.DragBeginArgs args)
		{
			TreeIter iter;
			Gdk.Pixbuf image;
			
			if(this.iconview.SelectedItems.Length <= 0)
			{
				// 'notify' the user about the fact that he is dragging an invalid object by 
				// setting the dragged image to the DialogError icon.
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
			Shape l = new Shape(args.SelectionData.Text, new Point(args.X, args.Y));
			
			this.shapeManager.Add(l);
			this.shapeManager.UnselectAll();
			this.shapeManager.Select(l);
			Gtk.Drag.Finish(args.Context, true, false, args.Time);
		}

		protected virtual void ResetButtonClicked (object sender, System.EventArgs e)
		{
			if(this.shapeManager.Selected.Size.Width != 0) //TODO: replace with != null
				this.shapeManager.Remove(this.shapeManager.Selected);
			else
				this.shapeManager.Clear(); // clear the list of shapes, so when ...
			
			this.drawingarea.QueueDraw(); // ... causing an expose here, nothing will be drawn
		}

		protected virtual void SaveButtonClicked (object sender, System.EventArgs e)
		{			
			ExportIntroWizard w = new ExportIntroWizard(this.shapeManager);
			w.Modal = true;
			w.ShowAll();
		}
		
		protected virtual void OpenButtonClicked (object sender, System.EventArgs e)
		{
			MessageBox.ShowInfo("Sorry, but this feature isn't implemented yet!");
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
			// and a Gtk-CRITICAL message on the console.
			next = next < 0 ? 0 : next; 
				
			this.iconview.ScrollToPath(new TreePath(next.ToString()));
		}
		
		protected virtual void ZoomInButtonClicked (object sender, System.EventArgs e)
		{
			this.shapeManager.Selected.Zoom(2);
			this.drawingarea.QueueDraw();
		}
		
		protected virtual void ZoomOutButtonClicked (object sender, System.EventArgs e)
		{
			this.shapeManager.Selected.Zoom(-2);
			this.drawingarea.QueueDraw();
		}
		
		protected virtual void RotateLeftButtonClicked (object sender, System.EventArgs e)
		{
			this.shapeManager.Selected.Rotation -= 0.1;
			this.drawingarea.QueueDraw();
		}
		
		protected virtual void RotateRightButtonClicked (object sender, System.EventArgs e)
		{
			this.shapeManager.Selected.Rotation += 0.1;
			this.drawingarea.QueueDraw();
		}
		
		protected virtual void ColorSelectionButtonClicked (object sender, System.EventArgs e)
		{
			ColorSelectDialog colorDialog = new ColorSelectDialog();
			
			int res = colorDialog.Run();
			
			if(res == (int)ResponseType.Ok)
				this.shapeManager.Selected.Colorize(colorDialog.ColorSelection.CurrentColor,
				                                    ((double)colorDialog.ColorSelection.CurrentAlpha)/65025);
			
			colorDialog.Destroy();
			this.drawingarea.QueueDraw();
		}
		
		protected virtual void DrawingAreaPress (object o, Gtk.ButtonPressEventArgs args)
		{
			//this.drawingarea.GdkWindow.Cursor = new Gdk.Cursor(Gdk.CursorType.Hand1);
			this.shapeManager.UnselectAll();
			this.shapeManager.Select((int)args.Event.X, (int)args.Event.Y);
			this.drawingarea.QueueDraw();
		}
				
		protected virtual void DrawingAreaMotionNotify (object o, Gtk.MotionNotifyEventArgs args)
		{
			if((args.Event.State & Gdk.ModifierType.Button1Mask) == Gdk.ModifierType.Button1Mask)
			{
				this.shapeManager.Selected.Drag(DragLocation.Inside,
				                                (int)args.Event.X,
				                                (int)args.Event.Y);
				this.drawingarea.QueueDraw();
			}
			if((args.Event.State & Gdk.ModifierType.Button3Mask) == Gdk.ModifierType.Button3Mask)
			{
				this.shapeManager.Selected.Drag(DragLocation.Resize,
				                                (int)args.Event.X,
				                                (int)args.Event.Y);
				this.drawingarea.QueueDraw();
			}
		}
		
		protected virtual void DownButtonClicked (object sender, System.EventArgs e)
		{
			this.shapeManager.MoveDown(this.shapeManager.Selected);
			this.drawingarea.QueueDraw();
		}
		
		protected virtual void UpButtonClicked (object sender, System.EventArgs e)
		{
			this.shapeManager.MoveUp(this.shapeManager.Selected);
			this.drawingarea.QueueDraw();
		}
		
		protected virtual void FlipVButtonClicked (object sender, System.EventArgs e)
		{
			this.shapeManager.Selected.FlipVertically();
			this.drawingarea.QueueDraw();
		}
		
		protected virtual void FlipHButtonClicked (object sender, System.EventArgs e)
		{
			this.shapeManager.Selected.FlipHorizontally();
			this.drawingarea.QueueDraw();
		}
		
		protected virtual void OnDrawingareaKeyPressEvent (object o, Gtk.KeyPressEventArgs args)
		{
			switch(args.Event.Key)
			{
				case Gdk.Key.Delete:
					this.shapeManager.Remove(this.shapeManager.Selected);
					this.drawingarea.QueueDraw();
				break;
				
				case Gdk.Key.Up:
					this.shapeManager.Selected.Move(0, -1);	
					this.drawingarea.QueueDraw();
				break;
				case Gdk.Key.Down:
					this.shapeManager.Selected.Move(0, 1);	
					this.drawingarea.QueueDraw();
				break;
				case Gdk.Key.Left:
					this.shapeManager.Selected.Move(-1, 0);	
					this.drawingarea.QueueDraw();
				break;
				case Gdk.Key.Right:
					this.shapeManager.Selected.Move(1, 0);	
					this.drawingarea.QueueDraw();
				break;
			}
		}
		
		protected void OnDeleteEvent (object sender, DeleteEventArgs a)
		{
			Application.Quit ();
			a.RetVal = true;
		}		
		
#endregion			
	}
}