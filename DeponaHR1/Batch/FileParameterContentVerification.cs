using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DeponaHR1.Batch
{
    class FileParameterContentVerification
    {
        private List<string> pdfFileNames;
        private List<string> datFileNames;

        public FileParameterContentVerification(List<string> pdfFileNames, List<string> datFileName)
        {
            Directory.SetCurrentDirectory(DeponaConfig.Configuration.GetMappSettingsInstance("Source"));
            this.pdfFileNames = pdfFileNames;
            this.datFileNames = datFileName;        // must contain only 1 item!
        }

        public bool verifyContents()
        {
            DATFileContentConverter datFileContentConverter = new DATFileContentConverter();
            datFileContentConverter.DATFileAttributesReader(datFileNames[0]);

            if ( datFileNames.Count > 1)
            {
                return false;
            }

            if (pdfFileNames.Count == datFileContentConverter.GetDATFileAttributeRowsCount())
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        internal bool verifyDATattributeCorrespondsForPDFFileName()
        {
            List<string> DATIndicies = new List<string>();
            string currentDirPath = Directory.GetCurrentDirectory();
            string filePathName = Path.Combine(Directory.GetCurrentDirectory(), datFileNames[0]);

            using (StreamReader streamReader = new StreamReader(filePathName, Encoding.GetEncoding(1252)))
            {
                while (streamReader.Peek() >= 0)
                {

                    DATIndicies.Add(streamReader.ReadLine());
                }
            }

            foreach (var DATRow in DATIndicies)
            {
                string[] DATRowcontents = DATRow.Split(';');
                string searchValue = DATRowcontents[DATRowcontents.Length - 1];

                var result = Array.Find(pdfFileNames.ToArray(), element => element == searchValue);
                if (result == null)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
