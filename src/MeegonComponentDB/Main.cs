using System;
using System.IO;
using System.Xml;
using System.Text;

namespace MeegonComponentDB
{
	public class Program
	{
		static XmlWriter writer;
		
		public static int Main(string[] args)
		{
			if(args.Length < 1)
			{
				Usage();
				return -1;
			}
			
			DirectoryInfo parent = new DirectoryInfo(args[0]);
			
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
				writer.WriteAttributeString("directory", parent.Name);

			
				foreach(DirectoryInfo dir in parent.GetDirectories())
				{	
					writer.WriteStartElement("category"); 
				
					writer.WriteAttributeString("name", dir.Name);
				
					CrawlDirectories(dir);
				
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
			return 0;
		}
	
		private static void CrawlDirectories(DirectoryInfo parent)
		{
			foreach(FileInfo file in parent.GetFiles())
			{
				writer.WriteStartElement("entry");
				writer.WriteAttributeString("image", file.Name);
				writer.WriteEndElement();
			}
		}
		
		private static void Usage()
		{
			string filename = Path.GetFileName(System.Reflection.Assembly.GetExecutingAssembly().Location);
			Console.WriteLine("Usage:\nmono " 
			                   + filename + " <path to parent folder>\n"+
			                  "Example: mono " + filename + " ./extracted/");
		}
	}
}