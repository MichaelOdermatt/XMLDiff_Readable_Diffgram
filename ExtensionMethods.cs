using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace XMLDiff_Readable_Diffgram
{
    internal static class ExtensionMethods
    {
        /// <summary>
        /// Returns -1 if element does not have a attribute with the given name
        /// or if the attributes value cannot be parsed as an int.
        /// </summary>
        public static int GetAttributeIntValue(this XElement element, string attributeName)
        {
            var matchAttribute = element.Attribute(attributeName);

            if (matchAttribute == null)
                return -1;

            string matchValue = matchAttribute.Value;

            int matchIndex;
            bool isInt = int.TryParse(matchValue, out matchIndex);

            return isInt ? matchIndex : -1;
        }
    }
}
