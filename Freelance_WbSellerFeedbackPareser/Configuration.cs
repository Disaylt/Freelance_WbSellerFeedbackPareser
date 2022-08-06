using Freelance_WbSellerFeedbackPareser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Freelance_WbSellerFeedbackPareser
{
    internal class Configuration
    {
        private const string _settingsFilePath = @"settings\SellerSettings.json";
        private static Configuration? _instance;

        private Configuration()
        {
            SellerSettings = LoadSellerSettings();
        }

        public List<SellerSettingsModel> SellerSettings { get; }

        public static Configuration GetInstance()
        {
            if(_instance == null)
            {
                _instance = new Configuration();
            }
            return _instance;
        }

        private static List<SellerSettingsModel> LoadSellerSettings()
        {
            string settingsContent = File.ReadAllText(_settingsFilePath);
            List<SellerSettingsModel> settings = JToken.Parse(settingsContent)
                .ToObject<List<SellerSettingsModel>>() ?? new List<SellerSettingsModel>();
            return settings;
        }
    }
}
