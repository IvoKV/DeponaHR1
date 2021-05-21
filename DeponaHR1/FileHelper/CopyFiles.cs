using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DeponaHR1.FileHelper
{
    public class CopyFiles
    {
        private string _fromDir, _toDir;

        public CopyFiles(string fromDir, string toDir)
        {
            this._fromDir = fromDir;
            this._toDir = toDir;
        }

        private bool destinationDirIsEmpty(string toDir)
        {
            int filecount = 0;
            if (Directory.Exists(_toDir))
            {
                filecount = (from file in Directory.EnumerateFiles(toDir, "*.*", SearchOption.TopDirectoryOnly)
                                 select file).Count();
            }
            return filecount > 0 ?  false : true;
        }

        public int copyFiles(string fromDir, string toDir, bool isEmpty = true)
        {
            int copied = 0;

            if (fromDir.Equals(_fromDir) && toDir.Equals(_toDir))
            {
                if (destinationDirIsEmpty(toDir))
                {
                    var filteredFiles = Directory.EnumerateFiles(fromDir, "*.*", SearchOption.TopDirectoryOnly)
                                                .Where(file => file.ToLower().EndsWith("dat") || file.ToLower().EndsWith("pdf"))
                                                .ToList();

                    if (filteredFiles.Count == 0)
                    {
                        return -1;                              // Source directory is Emtpy
                    }

                    foreach (var fileName in filteredFiles)
                    {
                        string fullDestPathName = Path.Combine(toDir, Path.GetFileName(fileName));
                        File.Copy(fileName, fullDestPathName);
                        copied++;
                    }
                    DeponaConfig.Configuration.WriteLogMessage($"Copied files from {_fromDir} to {_toDir}: {copied}");
                }
                else
                {
                    return -2;                                  // Destination directory is not empty
                }
            }
            return copied;
        }
    }
}
