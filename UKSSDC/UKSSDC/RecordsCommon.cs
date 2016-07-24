using System.IO;
using UKSSDC.Models.Enums;

namespace UKSSDC
{
    internal class RecordsCommon
    {
        //internal static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

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
