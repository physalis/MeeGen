using System;
using System.IO;
using System.Xml;
using Gtk;

namespace MeeGen
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			//TODO: Make neater
			if(args.Length >= 1)
			{
				if(args[0] == "--create-db" || args[0] == "-c")
				{
					if(args.Length > 1)
						CreateDatabase(args[1]);
					else
						Usage();
				}
				else if(args[0] == "--benchmark" || args[0] == "-b")
				{
					if(args.Length > 1)
						Benchmark(Convert.ToInt32(args[1]));
					else
						Usage();
				}
				else if(args[0] == "--help" || args[0] == "-h")
				{
						Usage();
				}
				else
				{
					Application.Init ();
					MainWindow win = new MainWindow(args[0]);
					win.Show ();
					Application.Run ();
				}
			}
			else
			{
				Application.Init ();
				MainWindow win = new MainWindow("./ComponentDB.xml");
				win.Show ();
				Application.Run ();
			}
			
		}
		
		private static void CreateDatabase(string folder)
		{
			XmlWriter writer;
			DirectoryInfo parent = new DirectoryInfo(folder);
			
			XmlWriterSettings settings = new XmlWriterSettings();
			settings.Indent = true;
			settings.IndentChars = "\t";
			settings.NewLineChars = "\r\n";
			
			try
			{
		    	Stream stream = new FileStream("./ComponentDB.xml", FileMode.Create);
				writer = XmlWriter.Create(stream, settings);
			
				writer.WriteStartDocument();
			
				writer.WriteStartElement("component-database"); // root element
				writer.WriteAttributeString("directory", parent.FullName);

			
				foreach(DirectoryInfo dir in parent.GetDirectories())
				{	
					writer.WriteStartElement("category"); 
				
					writer.WriteAttributeString("name", dir.Name);
				
					CrawlDirectories(dir, writer);
				
					writer.WriteEndElement(); // closes category
				}	
			
				writer.WriteEndElement(); // close root element
				writer.WriteEndDocument(); // not necessary...
			
				writer.Close();
				stream.Close();
				
			}catch(Exception e)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("[ERROR]: " + e.Message);
				File.Delete("./ComponentDB.xml");
			}
		}
	
		private static void CrawlDirectories(DirectoryInfo parent, XmlWriter writer)
		{
			foreach(FileInfo file in parent.GetFiles())
			{
				writer.WriteStartElement("entry");
				writer.WriteAttributeString("image", file.Name);
				writer.WriteEndElement();
			}
		}
		
		private static void Benchmark(int count)
		{
			Application.Init();
			MainWindow win = new MainWindow("./ComponentDB.xml");
			
			System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
			watch.Reset();
			
			for(int i = 0; i < count; i++)
			{
				watch.Start();
				win.Benchmark();
				watch.Stop();
				Console.WriteLine("{0}\t{1}", i, watch.ElapsedTicks);
				watch.Reset();
			}
			
			System.Diagnostics.Process.GetCurrentProcess().Kill();  
		}
		
		private static void Usage()
		{
			string filename = Path.GetFileName(System.Reflection.Assembly.GetExecutingAssembly().Location);
			Console.WriteLine("Usage:\n" +
							  "\tmono "+filename+" [OPTION] [FOLDER] | [FILE]\n" +
							  "or\n" +
							  "\t./"+filename+" [OPTION] [FOLDER] | [FILE]\n\n" +
							  "-h, --help \t\t\t display this message\n" +
							  "-c, --create-db [FOLDER] \t creates a ComponentDB.xml file from [FOLDER]\n" +
							  "-b, --benchmark [COUNT] \t\t\t test the performance of this application and write" +
							  "the results to stdout.\n\n" +
							  "When run with only [FILE] specified, "+filename+" handles [FILE] as\n" +
							  "a ComponentDB.xml.\nWhen run with no arguments, it uses ./ComponentDB.xml.");
		}
	} 
}
