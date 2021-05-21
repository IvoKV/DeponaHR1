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

            List<string> fileNamesCollection = new List<string>(fileNameImport.getFileNamesCollection());
            DeponaHR1.DeponaConfig.Configuration.SetProcessControlFlowInstance("NumFilesInSourceDir", fileNamesCollection.Count());
            DeponaConfig.Configuration.WriteLogMessage($"Filer som skall bearbetas (.dat): {DeponaConfig.Configuration.GetProcessControlFlowInstance("NumFilesInSourceDir")}");

            int currBatchSuffix = 0;
            if (fileNamesCollection.Count > 1000)
            {
                int batchRemainder = 0;
                batchRemainder = (fileNamesCollection.Count % 1000) > 0 ? 1 : 0;
                currBatchSuffix = 1;
                int countSubbatches = DeponaConfig.Configuration.GetProcessControlFlowInstance("NumFilesInSourceDir");
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
            while (true)
            {
                List<string> sliceList = new List<string>(fileNamesCollection.Skip(lowPos).Take(1000).ToList<string>());

                if (sliceList.Count == 0)
                {
                    currBatchSuffix--;
                    break;
                }

                DeponaConfig.Configuration.WriteLogMessage($"Nu körs sub batch nr: {currBatchSuffix}");

                var mappingStructure = new MappingStructure(sliceList, currBatchSuffix);
                mappingStructure.CreateMappingStructure();

                lowPos += 1000;
                currBatchSuffix++;
            }

            // get instance of DirectoryOperation
            var doOp = new DirectoryOperations();

            // copy all files from Source to Klar
            doOp.CopyAll();
            Thread.Sleep(800);

            // delete all files in Sourcre
            doOp.DeleteFilesInKalla();
            Thread.Sleep(800);

            // unlock .locked folder
            if (doOp.UnlockWorkFolder(currBatchSuffix) == 1)
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

            // Stop timer
            DeponaConfig.Configuration.SetProcessControlFlowInstance("BatchInProgress", 0);
        }
    }
}
