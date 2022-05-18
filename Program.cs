using System.Xml;
using System.Xml.Linq;
using System.Linq;

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

            var doc = XDocument.Parse(diffgram.ToString());

            foreach (var element in doc.Descendants()) {
                if (element.Name.LocalName.Equals("node")) {
                    var index = element.Attribute("match").Value;
                    if (index != null) {
                        string soruceName = getNodeName();
                        element.Name = soruceName;
                    }
                }
            }

            Console.WriteLine(doc.ToString());
        }

        private static string getNodeName() {
            return "rename_me";
        }

    }
}