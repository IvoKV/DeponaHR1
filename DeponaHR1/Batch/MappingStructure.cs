using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DeponaHR1.DeponaConfig;

namespace DeponaHR1.Batch
{
    public class MappingStructure
    {
        private int mapx = 9;
        private int mapy = -1;
        private bool lessThousandFiles;

        private List<string> _fileNameList;
        private int countOfRond;
        private string _currBatchSuffix;
        private string[,] _indexfil;

        public MappingStructure()
        {
            Directory.SetCurrentDirectory(DeponaConfig.Configuration.GetMappSettingsInstance("Destination"));
        }

        public MappingStructure(List<string> files, int suffix) : this()
        {
            mapx = 9;
            mapy = -1;
            countOfRond = 0;

            if (suffix == 0)
                _currBatchSuffix = "";
            else
                _currBatchSuffix = "-" + suffix.ToString();

            _fileNameList = files;
            _indexfil = new string[10, 100];

            if(DeponaConfig.Configuration.GetProcessControlFlowInstance("NumFilesInSourceDir") < 1000)
            {
                lessThousandFiles = true;
                countOfRond = 0;
            }
        }

        private void getNextMap(ref int x, ref int y)
        {
            if (lessThousandFiles)
            {
                countOfRond = 0;
                if (x == 9)  //start condition
                {
                    x = 0;
                    y += 2;
                }
                else if (x == 0)
                {
                    x = 1;
                    y--;
                }
                else if (x >= 1)
                {
                    x++;
                }
            }
            else
            {
                if (x >= 0 && x < 9 && y != -1)
                {
                    x++;
                }
                else if (x == 9 && y == 99)
                {
                    countOfRond++;
                    x = 0;
                    y = 0;
                }
                else if (x == 9)
                {
                    x = 0;
                    y++;
                }
            }
        }

        public void CreateMappingStructure()
        {
            foreach (var dirListItem in _fileNameList)
            {
                getNextMap(ref mapx, ref mapy);

                string xx = mapx.ToString();
                string yy = ToString(mapy);

                string lockedWorkFolder = DeponaConfig.Configuration.GetProcessSettingsInstance("BatchNo") + _currBatchSuffix + ".lock";

                string workFolder = Path.Combine(DeponaConfig.Configuration.GetProcessSettingsInstance("DeponaFakt") + lockedWorkFolder, Path.Combine(xx, yy));

                if (!Directory.Exists(workFolder))
                {
                    Directory.SetCurrentDirectory(DeponaConfig.Configuration.GetMappSettingsInstance("Destination"));
                    Directory.CreateDirectory(workFolder);
                }

                
                ProcessFiles tr = new ProcessFiles(dirListItem,
                                                                        xx,
                                                                        yy,
                                                                       countOfRond, _currBatchSuffix);
                
                DeponaConfig.Configuration.IncrementProcessedFilesParam();
            }
            DeponaConfig.Configuration.WriteLogMessage($"Processade filer (.dat): {DeponaConfig.Configuration.GetProcessControlFlowInstance("NumProcessedFiles")}");
        }

        //override
        private string ToString(int a)
        {
            string retVal;
            if (a < 10)
            {
                retVal = "0" + a.ToString();
            }
            else
            {
                retVal = a.ToString();
            }
            return retVal;
        }
    }
}

