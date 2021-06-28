using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DeponaHR1.DeponaConfig;

namespace DeponaHR1.Batch
{
    public class ProcessFiles
    {
        private string _fileItemName;
        private string _mapX;
        private string _mapY;
        private string _rond;
        private string _currentBatchSuffix;
        private string _idxFileOutName;
        private string _pdfFileName;
        private string _pdfFilenameOut = "0000001.pdf";
        private string _fileCSVContent;
        private string DATIndicyAttributes;

        private bool _updateFileContent = false;     // if the .dat file content shoud be changed

        public ProcessFiles()
        {
            // OBS: Current directory = Source
            Directory.SetCurrentDirectory(DeponaConfig.Configuration.GetMappSettingsInstance("Source"));
        }
        public ProcessFiles(string PDFfileItemName, string DATIndicy, string mapX, string mapY, int rond, string currBatchSuff) : this()
        {
            this._fileItemName = PDFfileItemName;
            this._mapX = mapX;
            this._mapY = mapY;
            this._rond = rond.ToString();
            this._currentBatchSuffix = currBatchSuff;
            this.DATIndicyAttributes = DATIndicy;

            PrepareUpdateDatFile();
            WriteIdxPdfToDestination();
        }       

        private void PrepareUpdateDatFile()
        {
            if (DeponaConfig.Configuration.FirstProcessedFile == null)
            {
                DeponaConfig.Configuration.FirstProcessedFile = _fileItemName;
            }
            DeponaConfig.Configuration.LastProcessedFile = _fileItemName;

            string filename = Path.Combine(DeponaConfig.Configuration.GetMappSettingsInstance("Source"), _fileItemName);

            //skapande av idx, pdf file name
            _idxFileOutName = "0000idx.dat";
            _idxFileOutName = _idxFileOutName.Insert(4, _rond.ToString());
            _pdfFileName = _fileItemName.Replace("dat", "pdf");
            _fileCSVContent = DATIndicyAttributes;

            if (_updateFileContent)
            {
                _fileCSVContent = "0000" + _rond + _mapY + _mapX + ";" + _fileCSVContent;
            }
        }

        private void WriteIdxPdfToDestination()
        {
            string lockedBatchName = DeponaConfig.Configuration.GetProcessSettingsInstance("DeponaFakt") + DeponaConfig.Configuration.GetProcessSettingsInstance("BatchNo") + _currentBatchSuffix + ".lock";
            string DATfileNameOut = Path.Combine(DeponaConfig.Configuration.GetMappSettingsInstance("Destination"), lockedBatchName, _mapX, _mapY, _idxFileOutName);
            File.WriteAllText(DATfileNameOut, _fileCSVContent, Encoding.Default);
            
            if (File.Exists(_pdfFileName))  
            {
                string[] csv = _fileCSVContent.Split(";");

                string pdfFileName = csv[csv.Length - 1].Trim();
                string pdfFileNameOut = _pdfFilenameOut.Insert(4, _rond.ToString());
                
                File.Copy(pdfFileName,
                    Path.Combine(DeponaConfig.Configuration.GetMappSettingsInstance("Destination"), lockedBatchName, _mapX, _mapY, pdfFileNameOut));
            }
            else
            {
                string logMessage = "Pdf-file not found!";
                DeponaConfig.Configuration.WriteLogMessage(logMessage);
            }
        }
    }
}
