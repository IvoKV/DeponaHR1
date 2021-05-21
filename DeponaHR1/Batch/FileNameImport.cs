using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DeponaHR1.Batch
{
    public class FileNameImport : IEnumerable<string>
    {
        private string _fileDirectory = null;
        private List<string> _fileNameList;         // contains all the filenames found in the directory

        public FileNameImport()
        {
            _fileNameList = new List<string>();
        }

        // by 'this' the default constructor is called first
        public FileNameImport(string dirName) : this()
        {
            this._fileDirectory = dirName;
            this.ReadFileNamesToArray();
        }

        internal void ReadFileNamesToArray()
        {
            if (Directory.Exists(_fileDirectory))
            {
                Directory.SetCurrentDirectory(_fileDirectory);
                string[] fileNames = Directory.GetFiles(".");

                foreach (var fileName in fileNames)
                {
                    var filenameClean = fileName.Replace(".\\", "");
                    if(Path.GetExtension(filenameClean).Equals(".dat"))
                    {
                        _fileNameList.Add(filenameClean);
                    }
                }
                
            }
            else
            {
                string message = $"Ogiltig parameter [{_fileDirectory}] i ini-filen.";
                //ConfigData.WriteLogMessage(message, "err");
            }
        }

        //2019-10-29: Install generic IEnumerable Method: GetEnumerator(){}
        public IEnumerator<string> GetEnumerator()
        {
            foreach (var item in _fileNameList)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public List<string> getFileNamesCollection()
        {
            return _fileNameList;
        }

        //method
        public void ShowFileNames()
        {
            foreach (string s in _fileNameList)
            {
                Console.WriteLine("filename: " + s);
            }
        }

        //method
        public int GetNumberOfFiles()
        {
            return _fileNameList.Count;
        }

        //property
        public string FileDirectory
        {
            get
            {
                return _fileDirectory;
            }

            set => _fileDirectory = value;
        }
    }
}

