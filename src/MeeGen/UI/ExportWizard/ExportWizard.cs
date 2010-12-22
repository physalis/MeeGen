using System;
using Gtk;
using Gdk;

namespace MeeGen
{
	public enum ExportFormat
	{
		PNG,
		SVG,
		PDF
	}
	
	public class ExportWizard : Gtk.Assistant
	{
		ShapeManager manager;
		
		public ExportWizard(ShapeManager manager) : base()
		{
			this.SkipTaskbarHint = true;
			this.Resizable = false;
			
			Title = "ExportWizard";
			
			this.SetPosition(WindowPosition.CenterOnParent);
			this.Decorated = false;
			this.ModifyBg(StateType.Normal, new Gdk.Color(255, 255, 255));
			
			this.manager = manager;
			
			Cancel += new EventHandler (AssistantCancel);
			
			WidgetHelper.SetButtonRelief(this, ReliefStyle.None);
		}
		
		public ShapeManager ShapeManager
		{
			get{return this.manager;}
		}
		
		protected override bool OnDeleteEvent (Gdk.Event ev)
		{
			//Console.WriteLine ("Assistant Destroyed prematurely");
			Application.Quit ();
			return true;
		}
		
		void AssistantCancel (object o, EventArgs args)
		{
			//Console.WriteLine ("Assistant cancelled.");
			Destroy ();
			//Application.Quit ();
		}

		void AssistantClose (object o, EventArgs args)
		{
			//Console.WriteLine ("Assistant ran to completion.");
			//Destroy (); //Destroy after the exporting has happened in SaveLocalPage and ExportWebPage
			//Application.Quit ();
		}
	}
}

