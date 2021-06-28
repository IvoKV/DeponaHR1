using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DeponaHR1.Batch
{
    public class DirectoryOperations
    {
        private string _msgFirstFile = "Första filen som processades till destinationskatalogen: ";
        private string _msgLastFile = "Sista filen som processades till destinationskatalogen: ";

        internal void WriteFinishedLog()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(_msgFirstFile);
            sb.AppendLine(DeponaConfig.Configuration.FirstProcessedFile);
            DeponaConfig.Configuration.WriteLogMessage(sb.ToString());

            sb.Clear();
            sb.Append(_msgLastFile);
            sb.AppendLine(DeponaConfig.Configuration.LastProcessedFile);
            DeponaConfig.Configuration.WriteLogMessage(sb.ToString());

            sb.Clear();
            sb.Append("Avslutar batch ");
            sb.Append(DeponaConfig.Configuration.GetProcessSettingsInstance("DeponaFakt"));
            sb.AppendLine(DeponaConfig.Configuration.GetProcessSettingsInstance("BatchNo"));
            DeponaConfig.Configuration.WriteLogMessage(sb.ToString());
        }

        internal void CopyAll()
        {
            int copyCounter = 0;
            if (!Directory.Exists(Path.Combine(DeponaConfig.Configuration.GetMappSettingsInstance("Done"),
                 DeponaConfig.Configuration.GetProcessSettingsInstance("DeponaFakt") +
                 DeponaConfig.Configuration.GetProcessSettingsInstance("BatchNo"))))
            {
                Directory.CreateDirectory(Path.Combine(DeponaConfig.Configuration.GetMappSettingsInstance("Done"),
                    DeponaConfig.Configuration.GetProcessSettingsInstance("DeponaFakt") +
                    DeponaConfig.Configuration.GetProcessSettingsInstance("BatchNo")));
            }

            //Sets the Done Directory as the 'Working' Directory
            Directory.SetCurrentDirectory(Path.Combine(DeponaConfig.Configuration.GetMappSettingsInstance("Done"),
                 DeponaConfig.Configuration.GetProcessSettingsInstance("DeponaFakt") +
                 DeponaConfig.Configuration.GetProcessSettingsInstance("BatchNo")));

            DirectoryInfo diSource = new DirectoryInfo(DeponaConfig.Configuration.GetMappSettingsInstance("Source"));
            string diTarget = Directory.GetCurrentDirectory();

            // Copy each file into the new directory.
            foreach (FileInfo fi in diSource.GetFiles())
            {
                fi.CopyTo(Path.Combine(diTarget, fi.Name), true);
                copyCounter++;
                DeponaConfig.Configuration.IncrementFileManipCount();
            }
            string copyMess = $"Kopierade {copyCounter} filer från 'Source' till 'Done'.\n";
            DeponaConfig.Configuration.WriteLogMessage(copyMess);
        }

        internal void DeleteFilesInKalla()
        {
            int deleteCounter = 0;
            string[] fileNames = Directory.GetFiles(DeponaConfig.Configuration.GetMappSettingsInstance("Source"));

            foreach (string fileName in fileNames)
            {
                File.Delete(fileName);
                deleteCounter++;
                DeponaConfig.Configuration.IncrementFileManipCount();
            }
            string deleteMess = $"Raderade {deleteCounter} filer från 'Source' mappen.";
            DeponaConfig.Configuration.WriteLogMessage(deleteMess);
        }

        internal async Task<int> UnlockWorkFolder(int suff)
        {
            string workFolder = Path.Combine(DeponaConfig.Configuration.GetMappSettingsInstance("Destination"),
                                    DeponaConfig.Configuration.GetProcessSettingsInstance("DeponaFakt") +
                                    DeponaConfig.Configuration.GetProcessSettingsInstance("BatchNo"));
                                    
            string lockedFolder = workFolder + ".lock";

            var mapCheck = new AsyncAwaitMapControl();
            await mapCheck.ControlMap();

            int retval = -1;

            if (suff == 0)  // färre än 1000 filer från källan
            {
                DirectoryInfo dinf = new DirectoryInfo(lockedFolder);

                int iteration = 0;
                do
                {
                    iteration++;
                    try
                    {
                        dinf.MoveTo(workFolder);
                        DeponaConfig.Configuration.WriteLogMessage($"Upplåsningen lyckades vid försök nr {iteration}");
                        retval = 1;   //all ok.
                        break;
                    }
                    catch (Exception e)
                    {
                        string errMess = $"Upplåsningen misslyckades vid försök nr: {iteration}. Sys-message: {e.Message}. Försöker igen om 3 sekunder";
                        DeponaConfig.Configuration.WriteLogMessage(errMess);
                        retval = 0;
                        Thread.Sleep(3000);
                    }
                } while (iteration < 6);
            }
            else
            {
                for (int i = suff; i > 0; i--)
                {
                    string suffix = "-" + i.ToString();
                    DirectoryInfo dinf = new DirectoryInfo(lockedFolder);

                    int iteration = 0;
                    do
                    {
                        iteration++;
                        try
                        {
                            dinf.MoveTo(workFolder);
                            DeponaConfig.Configuration.WriteLogMessage($"Upplåsningen av batch-suffix {i} lyckades vid försök nr {iteration}");

                            if (retval != 0)
                            {
                                retval = 1;
                            }
                            break;
                        }
                        catch (Exception e)
                        {
                            string error = $"Upplåsningen av batch-suffix {i} misslyckades vid försök nr: {iteration}. Sys-message: {e.Message}. Försöker igen om 3 sekunder";
                            DeponaConfig.Configuration.WriteLogMessage($"ERROR: {error}");
                            retval = 0;
                            Thread.Sleep(3000);
                        }
                    } while (iteration < 6);
                }
            }
            return retval;
        }

        public class AsyncAwaitMapControl
        {
            public async Task ControlMap()
            {
                int a = await ControlAllFilesMovedToDestination();

                return;
            }

            private async Task<int> ControlAllFilesMovedToDestination()
            {
                bool goOn = true;
                int tryCases = 5;

                while (goOn)
                {
                    if(DeponaConfig.Configuration.GetProcessControlFlowInstance("NumProcessedFiles") < 
                        DeponaConfig.Configuration.GetProcessControlFlowInstance("NumPDFFilesInSourceDir") && tryCases > 0)
                    {
                        Thread.Sleep(2000);
                        tryCases--;
                    }
                    else if (tryCases == 0)
                    {
                        goOn = false;
                        string message = $"\n\rERROR: Alla filer verkar inte ha blivit överförda till servern ('Destination') = {tryCases} ";
                        DeponaConfig.Configuration.WriteLogMessage(message);
                    }
                    else if (DeponaConfig.Configuration.GetProcessControlFlowInstance("NumProcessedFiles") ==
                            DeponaConfig.Configuration.GetProcessControlFlowInstance("NumPDFFilesInSourceDir"))        //(ConfigData._bearbetadeFiler == ConfigData._numberOfFiles)
                    {
                        goOn = false;
                    }
                }
                return tryCases;
            }
        }
    }
}
