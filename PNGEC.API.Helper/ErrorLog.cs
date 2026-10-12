using System;
using System.IO;
using System.Reflection;

namespace PNGEC.API.Helper
{
    public static class ErrorLog
    {
        #region variable declaration

        /// <summary>
        /// The executable path
        /// </summary>
        private static string exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        /// <summary>
        /// The root
        /// </summary>
        private static string root = Path.Combine(exePath, "Error");

        /// <summary>
        /// The subdir failure
        /// </summary>
        private static string subdirServiceName = string.Empty;
        #endregion

        #region write
        /// <summary>
        /// Logs the write.
        /// </summary>
        /// <param name="logMessage">The log message.</param>
        public static void Write(string serviceName, string errorLocation, Exception exception = null)
        {
            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }

            subdirServiceName = Path.Combine(root, serviceName);

            if (!Directory.Exists(subdirServiceName))
            {
                Directory.CreateDirectory(subdirServiceName);
            }
            else
            {
                // DeleteLog(subdirServiceName);
            }

            try
            {
                using (StreamWriter w = File.AppendText(subdirServiceName + "\\" + "Log_" + DateTime.Now.ToString("yyyyMMdd") + ".txt"))
                {
                    Log(errorLocation, exception, w);
                }
            }
            catch
            {
                //Write("ErrorLog", "PNGEC.API.Helper.ErrorLog => Method : Write", ex.Message, ex);
            }
        }
        #endregion

        #region log
        /// <summary>
        /// Logs the specified log message.
        /// </summary>
        /// <param name="logMessage">The log message.</param>
        /// <param name="txtWriter">The text writer.</param>
        private static void Log(string errorLocation, Exception exception, TextWriter txtWriter)
        {
            try
            {
                txtWriter.Write("\r\n");
                txtWriter.WriteLine("------------------" + DateTime.Now.ToLongDateString() + " | " + DateTime.Now.ToLongTimeString() + "------------------");
                txtWriter.WriteLine("Error Location : {0}", errorLocation);
                txtWriter.WriteLine("Error Message  : {0}", exception.Message);
                txtWriter.WriteLine("Error Stacktrash  : {0}", exception.StackTrace);
                txtWriter.WriteLine("------------------" + "**************************" + "------------------");
            }
            catch
            {
                ////Write("ErrorLog", "PNGEC.API.Helper.ErrorLog => Method : Log", ex);
            }
        }
        #endregion

        #region write log file
        /// <summary>
        /// Writes the log file.
        /// </summary>
        /// <param name="serviceName">Name of the service.</param>
        /// <param name="errorLocation">The error location.</param>
        /// <param name="exception">The exception.</param>
        public static void WriteLogFile(string serviceName, string message)
        {
            string directoryName = "Log";
            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }

            try
            {
                using (StreamWriter w = File.AppendText(directoryName + "\\" + "Log_" + DateTime.Now.ToString("yyyyMMdd") + ".txt"))
                {
                    w.Write("\r\n");
                    w.WriteLine("------------------" + DateTime.Now.ToLongDateString() + " | " + DateTime.Now.ToLongTimeString() + "------------------");
                    w.WriteLine("Log Message : {0}", message);
                    w.WriteLine("------------------" + "**************************" + "------------------");
                }
            }
            catch(Exception ex)
            {
                //Write("ErrorLog", "PNGEC.API.Helper.ErrorLog => Method : Write", ex.Message, ex);
            }
        }

        public static void WriteLog(string serviceName, string message)
        {
            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }

            subdirServiceName = Path.Combine(root, serviceName);

            if (!Directory.Exists(subdirServiceName))
            {
                Directory.CreateDirectory(subdirServiceName);
            }

            try
            {
                using (StreamWriter w = File.AppendText(subdirServiceName + "\\" + "Log_" + DateTime.Now.ToString("yyyyMMdd") + ".txt"))
                {
                    w.Write("\r\n");
                    w.WriteLine("------------------" + DateTime.Now.ToLongDateString() + " | " + DateTime.Now.ToLongTimeString() + "------------------");
                    w.WriteLine("Log Message : {0}", message);
                    w.WriteLine("------------------" + "**************************" + "------------------");
                }
            }
            catch (Exception ex)
            {
                //Write("ErrorLog", "PNGEC.API.Helper.ErrorLog => Method : Write", ex.Message, ex);
            }
        }
        #endregion

        #region delete log
        /// <summary>
        /// Deletes the log.
        /// </summary>
        /// <param name="DirPath">The dir path.</param>
        private static void DeleteLog(string DirPath)
        {
            string[] files = Directory.GetFiles(DirPath);
            foreach (string file in files)
            {
                FileInfo fi = new FileInfo(file);
                if (fi.LastAccessTime < DateTime.Now.AddMonths(-1))
                    fi.Delete();
            }
        }
        #endregion


    }
}