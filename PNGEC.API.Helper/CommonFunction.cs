using PNGEC.API.Domain.User;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Runtime;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace PNGEC.API.Helper
{
    public static class CommonFunction
    {
        #region MD5 hash
        /// <summary>
        /// ms the d5 hash.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
        #endregion

        #region ToDataTable
        /// <summary>
        /// Converts to datatable.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The items.</param>
        /// <returns>return list to data table</returns>
        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties by using reflection   
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names  
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {

                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }

            return dataTable;
        }
        #endregion

        #region Password Encrypt Decrypt
        /// <summary>
        /// Encrypts the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static string Encrypt(string data, string key)
        {
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            rijndaelCipher.Mode = CipherMode.CBC; //remember this parameter
            rijndaelCipher.Padding = PaddingMode.PKCS7; //remember this parameter

            rijndaelCipher.KeySize = 0x80;
            rijndaelCipher.BlockSize = 0x80;
            byte[] pwdBytes = Encoding.UTF8.GetBytes(key);
            byte[] keyBytes = new byte[0x10];
            int len = pwdBytes.Length;

            if (len > keyBytes.Length)
            {
                len = keyBytes.Length;
            }

            Array.Copy(pwdBytes, keyBytes, len);
            rijndaelCipher.Key = keyBytes;
            rijndaelCipher.IV = keyBytes;
            ICryptoTransform transform = rijndaelCipher.CreateEncryptor();
            byte[] plainText = Encoding.UTF8.GetBytes(data);

            return Convert.ToBase64String(transform.TransformFinalBlock(plainText, 0, plainText.Length));
        }

        /// <summary>
        /// Decrypts the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static string Decrypt(string data, string key)
        {
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            rijndaelCipher.Mode = CipherMode.CBC;
            rijndaelCipher.Padding = PaddingMode.PKCS7;

            rijndaelCipher.KeySize = 0x80;
            rijndaelCipher.BlockSize = 0x80;
            byte[] encryptedData = Convert.FromBase64String(data);
            byte[] pwdBytes = Encoding.UTF8.GetBytes(key);
            byte[] keyBytes = new byte[0x10];
            int len = pwdBytes.Length;

            if (len > keyBytes.Length)
            {
                len = keyBytes.Length;
            }

            Array.Copy(pwdBytes, keyBytes, len);
            rijndaelCipher.Key = keyBytes;
            rijndaelCipher.IV = keyBytes;
            byte[] plainText = rijndaelCipher.CreateDecryptor().TransformFinalBlock
                        (encryptedData, 0, encryptedData.Length);

            return Encoding.UTF8.GetString(plainText);
        }

        /// <summary>
        /// Creates the zip file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="files">The files.</param>
        public static void CreateZipFile(string fileName, IEnumerable<string> files)
        {
            // Create and open a new ZIP file
            var zip = ZipFile.Open(fileName, ZipArchiveMode.Create);
            foreach (var file in files)
            {
                // Add the entry for each file
                zip.CreateEntryFromFile(file, Path.GetFileName(file), CompressionLevel.Optimal);
            }
            // Dispose of the object when we are done
            zip.Dispose();
        }

        /// <summary>
        /// Removes the special characters.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
        #endregion

        #region Custom Date Time Format
        /// <summary>
        /// Converts the custom date time format.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        public static string ConvertCustomDateTimeFormat(DateTime? dateTime)
        {
            return String.Format("{0:MM dd yyyy}", dateTime);
        }
        #endregion

        #region Activity Logger

        /// <summary>
        /// add AddActivity Log
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="logSource"></param>
        /// <param name="Event"></param>
        /// <param name="LogDescription"></param>
        /// <returns></returns>
        public static async Task<int> AddActivityLogForDesktop(int userId, string Event, string LogDescription)
        {
            try
            {
                ActivityLogModel objModel = new ActivityLogModel
                {
                    UserID = userId,
                    LogSource = 2,
                    Event = Event,
                    LogDescription = LogDescription
                };

                var activResponse = await GenericHttpClient.PostRequest<ActivityLogModel>("ActivityLog/AddActivityLog", objModel);

                if (activResponse != null && activResponse.Status)
                {
                    return 1;
                }
            }
            catch (Exception) { return 0; }

            return 0;

        }

        #endregion

    }
}
