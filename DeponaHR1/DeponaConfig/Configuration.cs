using System;
using System.Configuration;
using System.Collections.Generic;
using System.Text;
using DeponaHR1.CustomMappConfig;
using DeponaHR1.CustomProcessConfig;
using System.IO;

namespace DeponaHR1.DeponaConfig
{
    public struct Configuration
    {
        private static Dictionary<string, string> _dictMapp;
        private static Dictionary<string, string> _dictProcess;
        private static Dictionary<string, int> _dictProcessParams;
        static readonly string _logFilePathName;
        private static string _firstProcessedFile = null;
        private static string _lastProcessedFile = null;


        static Configuration()
        {
            _dictMapp = new Dictionary<string, string>(4);
            _dictProcess = new Dictionary<string, string>(4);
            _dictProcessParams = new Dictionary<string, int>();

            Console.WriteLine("In Configuration constructor!");
            InitializeDictionaries();
            _logFilePathName = initializeLogFile();
            InitializeLogFile();
        }

        private static void InitializeLogFile()
        {
            if (_logFilePathName != null)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(GetDateTimeStamp("log"));
                sb.Append(" --> ");
                sb.Append("Initierar batch ");
                sb.Append(_dictProcess["DeponaFakt"]);
                sb.Append(_dictProcess["BatchNo"]);
                sb.Append("\n");
                sb.Append(GetDateTimeStamp("log"));
                sb.Append(" --> ");
                sb.Append("Version: ");
                sb.Append(_dictProcess["SystemVersion"]);
                sb.Append("\n");
                File.WriteAllText(_logFilePathName, sb.ToString());
            }
        }

        private static string initializeLogFile()
        {
            StringBuilder sbLogFileName = new StringBuilder();
            sbLogFileName.Append("Syslog_DeponaHR1_");
            sbLogFileName.Append(GetDateTimeStamp("file"));
            sbLogFileName.Append(".log");
            string logFilePathName = Path.Combine(DeponaConfig.Configuration.GetMappSettingsInstance("Log"), sbLogFileName.ToString());

            if (Directory.Exists(_dictMapp["Log"]))
            {
                return logFilePathName;
            }
            else
            {
                return null;
            }
        }

        public static string GetDateTimeStamp(string type)
        {
            DateTime time = DateTime.Now;

            if (type == "file")
            {
                return time.ToString("yyyy-MM-dd_HH.mm");
            }
            else
            {
                return time.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }


        private static void InitializeDictionaries()
        {
            var mappConfig = (MappConfig)ConfigurationManager.GetSection("mappSettings");
            var processConfig = (ProcessConfig)ConfigurationManager.GetSection("processSettings");

            foreach (MappInstanceElement instance in mappConfig.MappInstances)
            {
                _dictMapp.Add(instance.Name, instance.Path);
            }

            foreach (ProcessInstanceElement instance in processConfig.ProcessInstances)
            {
                _dictProcess.Add(instance.Name, instance.Value);
            }

            // Process Params Section
            _dictProcessParams.Add("NumFilesInSourceDir", 0);
            _dictProcessParams.Add("NumProcessedFiles", 0);
            _dictProcessParams.Add("BatchInProgress", 0);
            _dictProcessParams.Add("FileManipCount", 0);
            _dictProcessParams.Add("ZipExtractCount", 0);
        }

        internal static void SaveMappSettingsDictionary(Dictionary<string, string> dict)
        {
            var mappConfig = (MappConfig)ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).GetSection("mappSettings");

            foreach (MappInstanceElement instance in mappConfig.MappInstances)
            {
                string dictValue = dict[instance.Name];
                instance.Path = dictValue;
            }
            mappConfig.CurrentConfiguration.Save();
            ConfigurationManager.RefreshSection(mappConfig.MappInstances.ToString());
        }

        internal static void SaveProcessSettingsDictionary(Dictionary<string, string> dict)
        {
            var processConfig = (ProcessConfig)ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).GetSection("processSettings");

            foreach (ProcessInstanceElement instance in processConfig.ProcessInstances)
            {
                string dictValue = dict[instance.Name];
                instance.Value = dictValue;
            }
            processConfig.CurrentConfiguration.Save();
            ConfigurationManager.RefreshSection(processConfig.ProcessInstances.ToString());
        }

        internal static Dictionary<string, string> GetMappSettingsDictionary()
        {
            return _dictMapp;
        }

        internal static string GetMappSettingsInstance(string key)
        {
            return _dictMapp[key];
        }

        internal static Dictionary<string, string> GetProcessSettingsDictionay()
        {
            return _dictProcess;
        }

        internal static string GetProcessSettingsInstance(string key)
        {
            return _dictProcess[key];
        }

        internal static int GetProcessControlFlowInstance(string key)
        {
            return _dictProcessParams[key];
        }

        internal static void SetProcessControlFlowInstance(string key, int val)
        {
            _dictProcessParams[key] = val;
        }

        internal static void IncrementProcessedFilesParam()
        {
            _dictProcessParams["NumProcessedFiles"] += 1;
        }

        internal static void IncrementFileManipCount()
        {
            _dictProcessParams["FileManipCount"] += 1;
        }

        internal static void IncrementZipExtractCount()
        {
            _dictProcessParams["ZipExtractCount"] += 1;
        }

        internal static void IncrementBatchNo()
        {
            string batchNo = _dictProcess["BatchNo"];
            int IbatchNo = int.Parse(batchNo) + 1;
            batchNo = IbatchNo.ToString("000");

            _dictProcess["BatchNo"] = batchNo;
            SaveProcessSettingsDictionary(_dictProcess);
        }

        internal static void WriteLogMessage(string logMess)
        {
            if (_logFilePathName != null)
            {
                using (StreamWriter sw = File.AppendText(_logFilePathName))
                {
                    sw.Write(GetDateTimeStamp("log"));
                    sw.Write(" --> ");
                    sw.WriteLine(logMess);
                }
            }
        }

        internal static string FirstProcessedFile
        {
            get { return _firstProcessedFile;  }
            set { _firstProcessedFile = value;  }
        }

        internal static string LastProcessedFile
        {
            get { return _lastProcessedFile; }
            set { _lastProcessedFile = value; }
        }
    }
}
