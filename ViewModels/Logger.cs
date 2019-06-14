using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class Logger
    {
        private string _logFileFullName;
        public Logger()
        {
            _logFileFullName = Environment.CurrentDirectory;

            _logFileFullName = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),"LogFile.txt");
        }


        public void LogMessage(string message)
        {
            if (!File.Exists(_logFileFullName))
            {
                FileStream fileStream = File.Create(_logFileFullName);
                fileStream.Close();
            }
            File.WriteAllText(_logFileFullName, message);
        }
    }
}
