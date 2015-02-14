using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GameOfLife
{
    public class Settings
    {
        public int CellSize { get; set; }
        public int SeedPopulationDensity { get; set; }
        public int MaxCircleRadius { get; set; }
        public int CircleDropThreshold { get; set; }
        public int RedValue { get; set; }
        public int GreenValue { get; set; }
        public int BlueValue { get; set; }
        public int MaxFps { get; set; }
        public bool CycledColorsOn { get; set; }

        public const int DEFAULT_CELL_SIZE = 20;
        public const int DEFAULT_SEED_POPULATION_DENSITY = 35;
        public const int DEFAULT_MAX_CIRCLE_RADIUS = 100;
        public const int DEFAULT_CIRCLE_DROP_THRESHOLD = 15;
        public const int DEFAULT_MAX_FPS = 8;
        public const string DEFAULT_SETTINGS_FILE_NAME = @"gameOfLifeSettings.xml";
        public const bool DEFAULT_CYCLE_COLORS_ON = true;
        public const int DEFAULT_RED_VALUE = 205;
        public const int DEFAULT_GREEN_VALUE = 206;
        public const int DEFAULT_BLUE_VALUE = 157;

        private static Settings _currentSettings { get; set; }
        public static Settings CurrentSettings
        {
            get
            {
                if (_currentSettings == null)
                    _currentSettings = ReadSettings();
                return _currentSettings;
            }
        }

        /// <summary>
        /// Attempts to read settings from user default directory. If the file doesn't exist, then it uses default settings.
        /// </summary>
        /// <returns></returns>
        public static Settings ReadSettings()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Settings));
                StreamReader reader = new StreamReader(String.Format(@"{0}\{1}", GetCurrentUserDocumentFolderPath(), DEFAULT_SETTINGS_FILE_NAME));
                Settings settings = (Settings)serializer.Deserialize(reader);
                reader.Close();
                return settings;
            }
            catch (Exception e)
            {
                return new Settings
                {
                    CellSize = DEFAULT_CELL_SIZE,
                    SeedPopulationDensity = DEFAULT_SEED_POPULATION_DENSITY,
                    MaxCircleRadius = DEFAULT_MAX_CIRCLE_RADIUS,
                    CircleDropThreshold = DEFAULT_CIRCLE_DROP_THRESHOLD,
                    MaxFps = DEFAULT_MAX_FPS,
                    CycledColorsOn = DEFAULT_CYCLE_COLORS_ON,
                    RedValue = DEFAULT_RED_VALUE,
                    GreenValue = DEFAULT_GREEN_VALUE,
                    BlueValue = DEFAULT_BLUE_VALUE
                };
            }
        }

        /// <summary>
        /// Writes settings to default path in user home directory
        /// </summary>
        /// <param name="settings">Game of Life settings to be serialized out into xml.</param>
        public static void WriteSettings(Settings settings)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Settings));
            StreamWriter writer = new StreamWriter(String.Format(@"{0}\{1}", GetCurrentUserDocumentFolderPath(), DEFAULT_SETTINGS_FILE_NAME));
            serializer.Serialize(writer, settings);
            writer.Close();
        }

        public static string GetCurrentUserDocumentFolderPath()
        {
            // Don't use sketchy environment variables
            string path = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)).FullName;
            if (Environment.OSVersion.Version.Major >= 6)
                path = Directory.GetParent(path).ToString();
            return path;
        }
    }
}
