using System;

namespace XmlToSqlApp
{
    /// <summary>
    /// Represents data parsed from an XML file and later stored in an SQL database.
    /// </summary>

    // Defines a class called XMLData, which represents the data from an XML file and later stored in an SQl database. 
    public class XMLData
    {
        // getter and setter methods, allows to read and set values
        public string Id { get; set; }
        public string App_id { get; set; }
        public string Tooltip { get; set; }
        public string Type { get; set; }
        public string Verweis_Id { get; set; }
        public string Language_Id { get; set; }
        public string Laufnummer { get; set; }
        public string Startwert { get; set; }
        public string Textabweichung { get; set; }
        public string Bearbeitung { get; set; }

        private string _caption;
        public string Caption
        {
            get => _caption;
            set => _caption = TruncateOrNull(value, 150);
        }

        private string _title;
        public string Title
        {
            get => _title;
            set => _title = TruncateOrNull(value, 150);
        }

        private string _text;
        public string Text
        {
            get => _text;
            set => _text = TruncateOrNull(value, 150);
        }

        /// <summary>
        /// Truncates a string to the specified length or returns null if the input is null or empty.
        /// </summary>
        /// <param name="value">The input string.</param>
        /// <param name="maxLength">The maximum allowed length.</param>
        /// <returns>The truncated string or null.</returns>
        private static string TruncateOrNull(string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value))
                return null;

            return value.Length > maxLength ? value.Substring(0, maxLength) : value;
        }
    }
}

/*
Explanation for summaries:
The <summary> tag in C# is used to provide a brief description of a method, class, or property, 
improving code readability and understanding. It also enables automatic documentation generation and 
shows helpful tooltips in IDEs like Visual Studio through IntelliSense.
 */