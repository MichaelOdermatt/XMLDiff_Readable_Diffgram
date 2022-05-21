using System.Xml;
using System.Xml.Linq;
using System.Linq;
using System.Diagnostics;

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

            var sourceDoc = XDocument.Parse(source.ToString());
            var diffgramDoc = XDocument.Parse(diffgram.ToString());

            var sourceDocWithPadding = new XDocument(new XElement("Padding", source));
            traverseDiffgram(diffgramDoc.Root, sourceDocWithPadding.Root);

            Console.WriteLine(diffgramDoc);
        }

        private static string getNodeName(XElement element, int index, XDocument sourceDoc) {
            Console.WriteLine(element.Ancestors().Count());
            return "rename_me";
        }

        private static void traverseDiffgram(XElement diffgramNode, XElement sourceNode) {

            // iterate through all child elements 
            foreach (var node in diffgramNode.Elements()) 
            {

                if (node.Attribute("match") == null)
                    continue;

                string matchAttr = node.Attribute( "match" ).Value;

                // if the element has a match attribute
                if ( !string.IsNullOrEmpty(matchAttr) ) {
                    
                    switch ( node.Name.LocalName) {
                        
                        // if the element is named 'node'
                        case "node":
                            // from the source, get the element that
                            // corresponds with the value of the match attribute 
                            int sourceIndex = int.Parse(matchAttr);
                            var matchElement = sourceNode.Elements().ToList()[sourceIndex - 1];

                            node.Name = matchElement.Name;

                            // if the element has children use recursion
                            if ( diffgramNode.Elements().Count() > 0 )
                                traverseDiffgram( node, matchElement);

                            break;
                    }
                }
            }
        }

    }
}

// source on stack overflow https://stackoverflow.com/questions/4684241/help-with-xmldiffpatch-navigating-xml-doc-by-number-of-nodes
// source on microsoft docs https://docs.microsoft.com/en-us/previous-versions/dotnet/articles/aa302295(v=msdn.10)?redirectedfrom=MSDN#xmldif_topic4