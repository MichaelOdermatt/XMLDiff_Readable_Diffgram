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

            var sourceDoc = XDocument.Parse(source.ToString());
            var diffgramDoc = XDocument.Parse(diffgram.ToString());

            foreach (var element in diffgramDoc.Descendants()) {
                if (element.Name.LocalName.Equals("node")) {
                    var index = element.Attribute("match").Value;
                    if (index != null) {
                        string soruceName = getNodeName(element, Int32.Parse(index), sourceDoc);
                        element.Name = soruceName;
                    }
                }
            }

            Console.WriteLine(diffgramDoc.ToString());
        }

        private static string getNodeName(XElement element, int index, XDocument sourceDoc) {
            Console.WriteLine(element.Ancestors().Count());
            return "rename_me";
        }

        private static void traverseDiffgram(XElement diffgramNode) {

            // iterate through all child nodes
            foreach (var node in diffgramNode.Elements()) 
            {
                string matchAttr = diffgramNode.Attribute( "match" ).Value;
                XmlNodeList matchNodes = null;

                // if the node has a match attribute
                if ( !string.IsNullOrEmpty(matchAttr) ) {
                    
                    switch ( diffgramNode.Name.LocalName) {
                        
                        // if the node is named 'node'
                        case "node":
                            
                            if ( matchNodes.Count != 1 )
                                throw new Exception( "The 'match' attribute of 'node' element must select a single node." );
                            //matchNodes.MoveNext();
                            
                            // if the node has children use recursion
                            if ( diffgramNode.Elements().Count() > 0 )
                                traverseDiffgram( diffgramElement, (XmlDiffViewParentNode)matchNodes.Current );
                            

                            currentPosition = matchNodes.Current;
                            break;
                    }
                }
            }
        }

    }
}

// source on stack overflow https://stackoverflow.com/questions/4684241/help-with-xmldiffpatch-navigating-xml-doc-by-number-of-nodes
// source on microsoft docs https://docs.microsoft.com/en-us/previous-versions/dotnet/articles/aa302295(v=msdn.10)?redirectedfrom=MSDN#xmldif_topic4