// Helpers/Settings.cs
using Grasshoppers.Enums;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace Grasshoppers.Helpers
{
	/// <summary>
	/// This is the Settings static class that can be used in your Core solution or in any
	/// of your client applications. All settings are laid out the same exact way with getters
	/// and setters. 
	/// </summary>
	public static class Settings
	{
		private static ISettings AppSettings
		{
			get
			{
				return CrossSettings.Current;
			}
		}
        
		public static string Email
		{
			get
			{
				return AppSettings.GetValueOrDefault("email", "");
			}
			set
			{
				AppSettings.AddOrUpdateValue("email", value);
			}
		}

        public static string Password
        {
            get
            {
                return AppSettings.GetValueOrDefault("password", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("password", value);
            }
        }

        public static int IdPlayer
        {
            get
            {
                return AppSettings.GetValueOrDefault("idPlayer", 0);
            }
            set
            {
                AppSettings.AddOrUpdateValue("idPlayer", value);
            }
        }

        public static int AdminLevel
        {
            get
            {
                return AppSettings.GetValueOrDefault("adminLevel", 0);
            }
            set
            {
                AppSettings.AddOrUpdateValue("adminLevel", value);
            }
        }

    }
}