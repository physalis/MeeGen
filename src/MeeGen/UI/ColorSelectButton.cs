using System;
using Gtk;
using Gdk;

namespace MeeGen
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class ColorSelectButton : Gtk.Bin
	{	
		private Color color;
		
		public ColorSelectButton ()
		{
			this.Build();
			this.Color = Colors.White;
		}
		
		public Color Color
		{
			get
			{
				return this.color;
			}
			set
			{
				this.DrawingArea.ModifyBg(StateType.Normal, value);
				this.color = value;
			}
		}
		
		protected virtual void OnButtonClicked (object sender, System.EventArgs e)
		{
			ColorSelectionDialog colorDialog = new ColorSelectionDialog("Select a color");
			colorDialog.ModifyBg(StateType.Normal, Colors.White);
			colorDialog.Decorated = false;
			
			colorDialog.ColorSelection.CurrentColor = this.color;
			
			if(colorDialog.Run() == (int)ResponseType.Ok)
				this.Color = colorDialog.ColorSelection.CurrentColor;
			
			colorDialog.Destroy();
		}
		
		protected virtual void OnDrawingareaExposeEvent (object o, Gtk.ExposeEventArgs args)
		{
			int width, height;
			this.DrawingArea.GdkWindow.GetSize(out width, out height);
			
			using(Gdk.GC g = new Gdk.GC(DrawingArea.GdkWindow))
			{
				g.SetLineAttributes(2, LineStyle.Solid, CapStyle.Round, JoinStyle.Miter);
				DrawingArea.GdkWindow.DrawRectangle(g, false, new Rectangle(0, 0, width, height));
			}
		}
	}
}

