using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DeponaHR1.Batch
{
    class DATFileContentConverter
    {
        private Dictionary<int, string> datFileAttributesRows;

        public DATFileContentConverter()
        {
            datFileAttributesRows = new Dictionary<int, string>();
        }

        public void DATFileAttributesReader(String datFileName)
        {
            Directory.SetCurrentDirectory(DeponaConfig.Configuration.GetMappSettingsInstance("Source"));
            int indexCounter = -1;

            using (StreamReader streamReader = new StreamReader(datFileName, Encoding.GetEncoding(1252)))
            {
                while (streamReader.Peek() >= 0)
                {
                    indexCounter++;
                    datFileAttributesRows.Add(indexCounter, streamReader.ReadLine());
                }
            }
        }

        public int GetDATFileAttributeRowsCount()
        {
            return datFileAttributesRows.Count();
        }

        public string ExtractDATIndicyRow(string pdfFileName)
        {
            int myKey = -1;

            foreach (var datItemRow in datFileAttributesRows)
            {
                string examinRow = datItemRow.Value;
                if(examinRow.Contains(pdfFileName))
                {
                    myKey = datItemRow.Key;
                    return datFileAttributesRows[myKey];
                }
            }
            return String.Empty;
        }
    }
}
