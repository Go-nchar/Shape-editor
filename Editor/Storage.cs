using Newtonsoft.Json;
using System.IO;
using System.Windows.Forms;

namespace Editor
{
    internal class Storage
    {
        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings()
        {
            ReferenceLoopHandling = (ReferenceLoopHandling) 1,
            TypeNameHandling = (TypeNameHandling) 3
        };

        public static void Save<T>(string filePath, T records)
        {
            string contents = JsonConvert.SerializeObject(records, Settings);
            File.WriteAllText(filePath, contents);
        }

        public static T Load<T>(string filePath) where T : new()
        {
            if (!File.Exists(filePath))
                return new T();

            string str = File.ReadAllText(filePath);

            try
            {
                T obj = JsonConvert.DeserializeObject<T>(str, Settings);
                MessageBox.Show("File open");
                return obj;
            }
            catch
            {
                MessageBox.Show("The file does not fit!");
                return new T();
            }
        }
    }
}
