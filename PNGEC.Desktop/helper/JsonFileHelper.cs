using System.IO;
using System.Reflection;

namespace PNGEC.Desktop.Helper
{
    public static class JsonFileHelper
    {
        /// <summary>
        /// The executable path
        /// </summary>
        private static string exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        /// <summary>
        /// Writes the content of the json.
        /// </summary>
        /// <param name="jsonData">The json data.</param>
        /// <param name="fileName">Name of the file.</param>
        public static async void WriteJsonContent(string jsonData, string fileName)
        {
            File.WriteAllText(exePath + "\\" + fileName + ".json", string.Empty);

            using (StreamWriter w = File.AppendText(exePath + "\\" + fileName + ".json"))
            {
                await w.WriteAsync(jsonData);
                w.Close();
            }
        }

        /// <summary>
        /// Reads the content of the json.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public static string ReadJsonContent(string path)
        {
            string jsonToReturn = string.Empty;
            using (var reader = new StreamReader(path))
            {
                jsonToReturn = reader.ReadToEnd();
                reader.Close();
            }
            return jsonToReturn;
        }

        /// <summary>
        /// Removes the geo file.
        /// </summary>
        public static void RemoveGeoFile()
        {
            if (System.IO.File.Exists(System.IO.Path.Combine(exePath, "geolist.json")))
            {
                File.Delete(System.IO.Path.Combine(exePath, "geolist.json"));
            }
        }
    }
}
