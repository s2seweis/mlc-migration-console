using System;
using System.Collections.Generic;
using XmlToSqlApp.Helpers;

namespace XmlToSqlApp
{
    public class XmlProcessorService
    {
        private readonly XmlProcessorHelper _xmlHelper;

        public XmlProcessorService()
        {
            _xmlHelper = new XmlProcessorHelper();
        }

        public List<XMLData> ConvertXML(string file, string nodePrefix, int fileAmount, string parentFolder, string backupFolder)
        {
            return _xmlHelper.ConvertXML(file, nodePrefix, fileAmount, parentFolder, backupFolder);
        }
    }
}

/*handling XML data processing, including XML conversion or transformation*/
