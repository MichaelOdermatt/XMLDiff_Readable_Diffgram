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

            diffgramDoc.Root.RemoveAttributes();
            traverseDiffgram(diffgramDoc.Root, sourceDocWithPadding.Root);

            Console.WriteLine(diffgramDoc);
        }

        private static void traverseDiffgram(XElement diffgramNode, XElement? sourceNode) {

            foreach (var node in diffgramNode.Elements()) 
            {
                switch ( node.Name.LocalName) {
                        
                    case "node":
                        {
                            var matchElement = GetMatchElement(node, sourceNode);

                            node.Name = matchElement.Name;
                            node.RemoveAttributes();
                            foreach (var attribute in matchElement.Attributes())
                            {
                                node.SetAttributeValue(attribute.Name, attribute.Value);
                            }

                            if (diffgramNode.Elements().Count() > 0)
                                traverseDiffgram(node, matchElement);

                        }
                        break;

                    case "add":
                        {
                            var additionType = node.GetAttributeIntValue("type");
                            if (additionType == -1)
                                continue;

                            if (additionType == 1)
                                node.Name = "added-element";
                            else if (additionType == 2)
                                node.Name = "added-attribute";

                            node.SetAttributeValue("type", null);

                            traverseDiffgram(node, null);
                        }
                        break; 

                    case "remove":
                        {
                            var matchElement = GetMatchElement(node, sourceNode);

                            node.Name = "Removed";
                            node.Add(matchElement);
                            node.SetAttributeValue("match", null);

                            traverseDiffgram(node, null);
                        }
                        break; 
                }
            }
        }

        /// <summary>
        /// Returns the child element at the index represented by the match attribute.
        /// </summary>
        private static XElement? GetMatchElement(XElement node, XElement? sourceNode)
        {
            var sourceIndex = node.GetAttributeIntValue("match");
            if (sourceIndex == -1)
                return null;

            if (sourceNode == null)
                return null;

            return sourceNode.Elements().ToList()[sourceIndex - 1];
        }

    }
}

// source on stack overflow https://stackoverflow.com/questions/4684241/help-with-xmldiffpatch-navigating-xml-doc-by-number-of-nodes
// source on microsoft docs https://docs.microsoft.com/en-us/previous-versions/dotnet/articles/aa302295(v=msdn.10)?redirectedfrom=MSDN#xmldif_topic4