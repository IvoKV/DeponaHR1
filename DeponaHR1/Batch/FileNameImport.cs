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
        private List<string> _fileNameListDAT;         // contains all the filenames found in the directory
        private List<string> _fileNameListPDF;

        public FileNameImport()
        {
            _fileNameListDAT = new List<string>();
            _fileNameListPDF = new List<string>();
        }

        // by 'this' the default constructor is called first
        public FileNameImport(string dirName) : this()
        {
            this._fileDirectory = dirName;
            this.ReadFileNamesToArray();
        }

        private void ReadFileNamesToArray()
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
                        _fileNameListDAT.Add(filenameClean);
                    }
                }

                foreach (var fileName in fileNames)
                {
                    var filenameClean = fileName.Replace(".\\", "");
                    if (Path.GetExtension(filenameClean).Equals(".pdf"))
                    {
                        _fileNameListPDF.Add(filenameClean);
                    }
                }

            }
            else
            {
                string message = $"Ogiltig parameter [{_fileDirectory}] i ini-filen.";
            }
        }

        //2019-10-29: Install generic IEnumerable Method: GetEnumerator(){}
        public IEnumerator<string> GetEnumerator()
        {
            foreach (var DATitem in _fileNameListDAT)
            {
                yield return DATitem;
            }

            foreach (var PDFitem in _fileNameListPDF)
            {
                yield return PDFitem;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public List<string> getFileNamesCollectionDAT()
        {
            return _fileNameListDAT;
        }

        public List<string> getFileNamesCollectionPDF()
        {
            return _fileNameListPDF;
        }

        //method
        public void ShowFileNames()
        {
            foreach (string s in _fileNameListDAT)
            {
                Console.WriteLine("filename: " + s);
            }
        }

        //method
        public int GetNumberOfDATFiles()
        {
            return _fileNameListDAT.Count;
        }

        public int GetNumberOfPDFFiles()
        {
            return _fileNameListPDF.Count;
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

