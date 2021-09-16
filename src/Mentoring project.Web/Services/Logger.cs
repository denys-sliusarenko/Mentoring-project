using System;
using System.Text;

namespace MentoringProject.Services
{
    public class Logger : IDisposable
    {
        private System.IO.StreamWriter _sw = null;
        private bool _disposed = false;

        public void Dispose()
        {
            WriteLog("Disposing object...");
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                Close();
            }

            _disposed = true;
        }

        public Logger(Encoding encoding)
        {
            _sw = new System.IO.StreamWriter("\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".log", true, encoding);
        }

        public void WriteLog(string message)
        {
            _sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ": " + message);
        }

        public void Close()
        {
            _sw.Dispose();
        }
    }
}