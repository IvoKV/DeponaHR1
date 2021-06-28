using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DeponaHR1.Batch;

namespace DeponaHR1
{
    class BatchStart
    {
        private Dictionary<string, string> _dictMapp;       //static values
        private Dictionary<string, string> _dictProcess;    //static values

        public BatchStart(Dictionary<string, string> dictMapp, Dictionary<string, string> dictProcess)
        {
            this._dictMapp = dictMapp;
            this._dictProcess = dictProcess;
        }

        [MTAThread]
        public async Task StartPocessAsync()
        {
            await Task.Run(() =>
            {
                var t = RunTaskAsync();
            });
        }

        private async static Task RunTaskAsync()
        {
            DeponaConfig.Configuration.WriteLogMessage($"Destination: {DeponaConfig.Configuration.GetMappSettingsInstance("Destination")}");
            DeponaConfig.Configuration.WriteLogMessage($"Source: {DeponaConfig.Configuration.GetMappSettingsInstance("Source")}");

            FileNameImport fileNameImport = new FileNameImport(DeponaConfig.Configuration.GetMappSettingsInstance("Source"));
            List<string> fileNamesCollectionPDF = new List<string>(fileNameImport.getFileNamesCollectionPDF());
            List<string> fileNamesCollectionDAT = new List<string>(fileNameImport.getFileNamesCollectionDAT());

            string DATfileName = fileNamesCollectionDAT[0];
            fileNamesCollectionDAT = null;      // no more needed, use filenameDAT in stead

            DeponaHR1.DeponaConfig.Configuration.SetProcessControlFlowInstance("NumPDFFilesInSourceDir", fileNamesCollectionPDF.Count());
            DeponaConfig.Configuration.WriteLogMessage($"Filer som skall bearbetas (.pdf): {DeponaConfig.Configuration.GetProcessControlFlowInstance("NumPDFFilesInSourceDir")}");

            int currBatchSuffix = 0;
            if (fileNamesCollectionPDF.Count > 1000)
            {
                int batchRemainder = 0;
                batchRemainder = (fileNamesCollectionPDF.Count % 1000) > 0 ? 1 : 0;
                currBatchSuffix = 1;
                int countSubbatches = DeponaConfig.Configuration.GetProcessControlFlowInstance("NumPDFFilesInSourceDir");
                string logMessage = $"Antal sub-batchar som ska köras: {countSubbatches / 1000 + batchRemainder}";
                DeponaConfig.Configuration.WriteLogMessage(logMessage);
            }
            else
            {
                currBatchSuffix = 0;
                DeponaConfig.Configuration.WriteLogMessage($"Endast grundbatchen {DeponaConfig.Configuration.GetProcessSettingsInstance("BatchNo")} körs.");
            }

            int lowPos = 0;

            // -- MAIN PROCESS LOOP --
            while (DeponaConfig.Configuration.GetProcessControlFlowInstance("BatchInProgress") > 0)
            {
                List<string> sliceListPDFFileNames = new List<string>(fileNamesCollectionPDF.Skip(lowPos).Take(1000).ToList<string>());

                if (sliceListPDFFileNames.Count == 0)
                {
                    currBatchSuffix--;
                    break;
                }

                DeponaConfig.Configuration.WriteLogMessage($"Nu körs sub batch nr: {currBatchSuffix}");

                var mappingStructure = new MappingStructure(sliceListPDFFileNames, currBatchSuffix, DATfileName);
                mappingStructure.CreateMappingStructure();

                lowPos += 1000;
                currBatchSuffix++;
            }

            if (DeponaConfig.Configuration.GetProcessControlFlowInstance("BatchInProgress") > 0)
            {
                // get instance of DirectoryOperation
                var doOp = new DirectoryOperations();

                // copy all files from Source to Klar
                doOp.CopyAll();
                Thread.Sleep(800);

                // delete all files in Sourcre
                doOp.DeleteFilesInKalla();
                Thread.Sleep(800);

                // unlock .locked folder
                var a = await doOp.UnlockWorkFolder(currBatchSuffix);
                if (a == 1)
                {
                    DeponaConfig.Configuration.WriteLogMessage("Upplåsningen av batchmappen(-arna) har gått bra.");
                }
                else
                {
                    DeponaConfig.Configuration.WriteLogMessage("Fel vid upplåsningen av batchmappen(-arna)!");
                }

                // write finish log
                doOp.WriteFinishedLog();


                // increment Batch number
                DeponaConfig.Configuration.IncrementBatchNo();
            }

            // Stop timer
            DeponaConfig.Configuration.SetProcessControlFlowInstance("BatchInProgress", 0);
        }
    }
}
