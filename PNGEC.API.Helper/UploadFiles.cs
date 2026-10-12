using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PNGEC.API.Domain;

namespace PNGEC.API.Helper
{
    public static class UploadFiles
    {
        #region variable declaration

        /// <summary>
        /// The file name
        /// </summary>
        private static string fileName = string.Empty;

        /// <summary>
        /// The folder path
        /// </summary>
        private static string folderPath = string.Empty;
        #endregion

        #region Methods
        /// <summary>
        /// Uploads the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="moduleName">Name of the module.</param>
        /// <returns></returns>
        public async static Task<string> UploadFile(IFormFile file, string moduleName, string fName)
        {
            if (!string.IsNullOrEmpty(fName) && !string.IsNullOrWhiteSpace(fName))
            {
                fileName = fName;
            }
            else
            {
                fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(file.FileName);
            }
            folderPath = Directory.GetCurrentDirectory() + "\\" + Constants.UploadFolderPath + "\\" + moduleName + "\\";
            using (var fileStream = new FileStream(Path.Combine(folderPath, fileName), FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return fileName;
        }

        /// <summary>
        /// Deletes the file.
        /// </summary>
        /// <param name="profileImageName">Name of the profile image.</param>
        /// <param name="moduleName">Name of the module.</param>
        public static void DeleteFile(string profileImageName, string moduleName)
        {
            folderPath = Directory.GetCurrentDirectory() + "\\" + Constants.UploadFolderPath + "\\" + moduleName + "\\";

            if (File.Exists(Path.Combine(folderPath, profileImageName)))
            {
                // If file found, delete it    
                File.Delete(Path.Combine(folderPath, profileImageName));
            }
        }

        #region Document

        public async static Task<string> UploadDocument(IFormFile file,string fName)
        {
            if (!string.IsNullOrEmpty(fName) && !string.IsNullOrWhiteSpace(fName))
            {
                fileName = fName;
            }
            else
            {
                fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(file.FileName);
            }
            //fileName = DocumentId + "_" + fileName;

            folderPath = Directory.GetCurrentDirectory() + "\\" + Constants.UploadFolderPath + "\\" + 
                Constants.DocumentFolderPath + "\\";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            
            using (var fileStream = new FileStream(Path.Combine(folderPath, fileName), FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return fileName;
        }
        #endregion


        #endregion
    }
}
