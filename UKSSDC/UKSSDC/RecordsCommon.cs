using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UKSSDC.Models.Enums;

namespace UKSSDC
{
    public class RecordsCommon
    {
        public RecordsCommon()
        {
            
        }

        //TODO: Move run methods to here.

        //TODO: Modify breaks in files to |
        protected internal string[] SplitCsvLineComma(string csvLine)
        {
            string[] attributes = csvLine.Split(',');
            attributes[0] = StripEscapeCharacters(attributes[0]);
            return attributes;
        }

        protected internal string[] SplitCsvLinePipe(string csvLine)
        {
            string[] attributes = csvLine.Split('|');
            attributes[0] = StripEscapeCharacters(attributes[0]);
            return attributes;
        }

        protected internal string StripEscapeCharacters(string wktEscaped)
        {
            string result = wktEscaped.Replace("\"", "");
            return result;
        }

        protected internal Country DetermineCountry(string filePath)
        {
            string fileName = Path.GetFileNameWithoutExtension(filePath);

            if (fileName.Contains("England"))
            {
                return Country.England;
            }
            else if (fileName.Contains("Scotland"))
            {
                return Country.Scotland;
            }
            else
            {
                return Country.Wales;
            }
        }
    }
}
