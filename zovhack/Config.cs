using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using zovhack;

namespace zovhack
{
    public class Parameters
    {
        public bool aimBot { get; set; }
        public bool aimOnTeam { get; set; }
        public bool showWindow { get; set; }
        public bool enableBox { get; set; }
        public bool cornerBox { get; set; }
        public bool enableLine { get; set; }
        public bool enableName { get; set; }
        public bool enableBar { get; set; }
        public bool enableWeapon { get; set; }
        public bool enableOverlay { get; set; }
        public bool enableBypass { get; set; }
        public float boxRounding { get; set; }
        public float hpRounding { get; set; }
        public float boxThick { get; set; }
        public float hpThick { get; set; }
        public Vector4 enemyColor { get; set; }
        public Vector4 teamColor { get; set; }
        public Vector4 teamNameColor { get; set; }
        public Vector4 enemyNameColor { get; set; }
        public Vector4 teamHealthColor { get; set; }
        public Vector4 enemyHealthColor { get; set; }
        public Vector4 teamWeaponColor { get; set; }
        public Vector4 enemyWeaponColor { get; set; }
        public Vector4 enemyLineColor { get; set; }
        public Vector4 teamLineColor { get; set; }
    }

    public class Config
    { 
        public void Save(Parameters parameters, string configName)
        {
            string json = JsonConvert.SerializeObject(parameters, Formatting.Indented);
            var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var fullPath = Path.Combine(documents, "ZovHack\\");

            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }

            File.WriteAllText(fullPath + configName + ".json", json);
        }

        public Parameters Load(string configName)
        {
            var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var fullPath = Path.Combine(documents, "ZovHack\\");

            string contents = File.ReadAllText(fullPath + configName + ".json");

            return JsonConvert.DeserializeObject<Parameters>(contents);
        }

        public string[] GetFilesAndFolders(string folderPath)
        {
            string[] files = Directory.GetFiles(folderPath);
            string[] directories = Directory.GetDirectories(folderPath);

            string[] allItems = new string[files.Length + directories.Length];

            for (int i = 0; i < files.Length; i++)
            {
                allItems[i] = Path.GetFileNameWithoutExtension(files[i]);
            }

            for (int i = 0; i < directories.Length; i++)
            {
                allItems[files.Length + i] = Path.GetFileName(directories[i]);
            }

            return allItems;
        }
    }
}
