using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ionic.Zip;

namespace DeponaHR1.Zip
{
    internal class ZipUnpacker
    {
        private string _fromDirectory;
        private string _toDirectory;        // ONLY WHEN copying .dat files!
        private string _zipExtractFolder;
        //private static bool justHadByteUpdate = false;

        internal ZipUnpacker(string fromDir, string toDir)
        {
            this._fromDirectory = fromDir;
            this._toDirectory = toDir;
        }

        public static void ExtractProgress(object sender, ExtractProgressEventArgs e)
        {
            if (e.EventType == ZipProgressEventType.Extracting_EntryBytesWritten)
            {
                /*
                if (justHadByteUpdate)
                    Console.SetCursorPosition(0, Console.CursorTop);
                */
                
                DeponaConfig.Configuration.IncrementZipExtractCount();
                //string message = $"incrementZip: {DeponaConfig.Configuration.GetProcessControlFlowInstance("ZipExtractCount")}";
                //DeponaConfig.Configuration.WriteLogMessage(message);

                /*
                Console.Write("   {0}/{1} ({2:N0}%)", e.BytesTransferred, e.TotalBytesToTransfer,
                              e.BytesTransferred / (0.01 * e.TotalBytesToTransfer));
                justHadByteUpdate = true;
                */
            }
            else if (e.EventType == ZipProgressEventType.Extracting_BeforeExtractEntry)
            {
                /*
                if (justHadByteUpdate)
                    Console.WriteLine();
                Console.WriteLine("Extracting: {0}", e.CurrentEntry.FileName);
                justHadByteUpdate = false;
                */
            }
        }

        public async Task UnzipStartAsync(string zipFileName)
        {
            await Task.Run(() =>
            {
                var t = UnpackZipAndCopyToSource(zipFileName);
            });

        }

        internal async Task<int> UnpackZipAndCopyToSource(string zipFileName)
        {
            // zipFileName = c:\temp\DeponaWorkDir\Temp\611872237.zip

            _zipExtractFolder = Path.Combine(_fromDirectory, "temp");
            if (!Directory.Exists(_zipExtractFolder))
            {
                Directory.CreateDirectory(_zipExtractFolder);
            }
            else
            {
                string[] fileNames = Directory.GetFiles(_zipExtractFolder);

                foreach (var filename in fileNames)
                {
                    File.Delete(filename);
                }
            }

            using(ZipFile myZip = new ZipFile(zipFileName))
            {
                myZip.ExtractProgress += ExtractProgress;
                myZip.Password = Resource1.zipPassword;
                myZip.ExtractAll(_zipExtractFolder, ExtractExistingFileAction.DoNotOverwrite);
            }

            // MOVE all files from _zipExtractFolder (copy-move)
            var filteredFiles = Directory.EnumerateFiles(_zipExtractFolder, "*.*", SearchOption.TopDirectoryOnly)
                                                .Where(file => file.ToLower().EndsWith(".dat") || file.ToLower().EndsWith(".pdf"))
                                                .ToList();
            string sourcePath = DeponaConfig.Configuration.GetMappSettingsInstance("Source");
            foreach (var fileItem in filteredFiles)
            {
                string filenameToSource = Path.Combine(sourcePath, Path.GetFileName(fileItem));
                File.Move(fileItem.Trim(), filenameToSource);
                DeponaConfig.Configuration.IncrementZipExtractCount();
            }

            return 0;
        }

        internal int GetZipFileCount(string zipFileName)
        {
            using(ZipFile myZip = new ZipFile(zipFileName))
            {
                return myZip.Count();
            }
        }

        internal float GetZipCompression(string zipFileName)
        {
            using(ZipFile myZip = new ZipFile(zipFileName))
            {
                return (float)myZip.CompressionLevel;
            }
        }
    }
}
