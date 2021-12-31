using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpamDashkevichLab9.Services
{
    public class TextFileService
    {
        public string Open(string path)
        {
            try
            {
                string text;
                using (StreamReader sr = new StreamReader(path))
                {
                    text = sr.ReadToEnd();
                }
                return text;
            }
            catch
            {
                throw;
            }
            
        }
        public void Save(string path, string text)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(path, false))
                {
                    sw.Write(text);
                }
            }
            catch
            {
                throw;
            }
            
        }
    }
}
