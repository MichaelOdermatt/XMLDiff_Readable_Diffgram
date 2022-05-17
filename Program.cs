using System.Xml;
using System.Xml.Linq;

namespace XMLDiff_Readable_Diffgram 
{
    class Program
    {
        static void Main(string[] args)
        {
            string sourcePath = String.Format(@"{0}/example_source.xml", Directory.GetCurrentDirectory());
            string diffgramPath = String.Format(@"{0}/example_diffgram.xml", Directory.GetCurrentDirectory());

            XElement source = XElement.Load(File.OpenText(sourcePath));
            XElement diffgram = XElement.Load(File.OpenText(diffgramPath));

            XmlDocument doc = new XmlDocument();
            doc.Load(sourcePath);
            XmlElement? root = doc.DocumentElement; 
            XmlNodeList? nodes = root.ChildNodes;

            foreach (XmlNode node in nodes)
            {
                Console.WriteLine(node.OuterXml);
            }
        }
    }
}