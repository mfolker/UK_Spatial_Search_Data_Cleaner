using System.IO;
using UKSSDC.Models.Enums;

namespace UKSSDC
{
    public class RecordsCommon
    {
        public RecordsCommon()
        {
            
        }

        //TODO: Move run methods to here.

        protected internal string[] SplitCsvLine(string csvLine)
        {
            string[] attributes = csvLine.Split(',');
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
