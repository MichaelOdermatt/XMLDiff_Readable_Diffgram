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

            //Console.WriteLine(diffgramDoc.ToString());
        }

        private static string getNodeName(XElement element, int index, XDocument sourceDoc) {
            Console.WriteLine(element.Ancestors().Count());
            return "rename_me";
        }

        private static void traverseDiffgram(XElement diffgramNode, XElement sourceNode) {

            // iterate through all child nodes
            foreach (var node in diffgramNode.Elements()) 
            {

                if (node.Attribute("match") == null)
                {
                    continue;
                }

                string matchAttr = node.Attribute( "match" ).Value;
                XElement matchNodes = null;

                // if the node has a match attribute
                if ( !string.IsNullOrEmpty(matchAttr) ) {
                    
                    switch ( node.Name.LocalName) {
                        
                        // if the node is named 'node'
                        case "node":
                            int sourceIndex = int.Parse(matchAttr);

                            Debug.WriteLine(sourceIndex);
                            matchNodes = sourceNode.Elements().ToList()[sourceIndex - 1];
                            //if ( matchNodes.Count != 1 )
                            //throw new Exception( "The 'match' attribute of 'node' element must select a single node." );
                            //matchNodes.MoveNext();

                            node.Name = matchNodes.Name;

                            // if the node has children use recursion
                            if ( diffgramNode.Elements().Count() > 0 )
                            {
                                traverseDiffgram( node, matchNodes);
                            }

                            //currentPosition = matchNodes.Current;
                            break;
                    }
                }
            }

            Console.WriteLine(diffgramNode);
        }

    }
}

// source on stack overflow https://stackoverflow.com/questions/4684241/help-with-xmldiffpatch-navigating-xml-doc-by-number-of-nodes
// source on microsoft docs https://docs.microsoft.com/en-us/previous-versions/dotnet/articles/aa302295(v=msdn.10)?redirectedfrom=MSDN#xmldif_topic4