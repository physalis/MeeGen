
// This file has been generated by the GUI designer. Do not modify.
namespace MeeGen
{
	public partial class ColorSelectButton
	{
		private global::Gtk.Button Button;

		private global::Gtk.DrawingArea DrawingArea;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget MeeGen.ColorSelectButton
			global::Stetic.BinContainer.Attach (this);
			this.Name = "MeeGen.ColorSelectButton";
			// Container child MeeGen.ColorSelectButton.Gtk.Container+ContainerChild
			this.Button = new global::Gtk.Button ();
			this.Button.CanFocus = true;
			this.Button.Name = "Button";
			this.Button.FocusOnClick = false;
			this.Button.Relief = ((global::Gtk.ReliefStyle)(2));
			// Container child Button.Gtk.Container+ContainerChild
			this.DrawingArea = new global::Gtk.DrawingArea ();
			this.DrawingArea.Name = "DrawingArea";
			this.Button.Add (this.DrawingArea);
			this.Button.Label = null;
			this.Add (this.Button);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.Hide ();
			this.Button.Clicked += new global::System.EventHandler (this.OnButtonClicked);
			this.DrawingArea.ExposeEvent += new global::Gtk.ExposeEventHandler (this.OnDrawingareaExposeEvent);
		}
	}
}
