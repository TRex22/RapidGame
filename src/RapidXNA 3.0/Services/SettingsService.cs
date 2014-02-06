using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Xml.Serialization;

namespace RapidXNA.Services
{
    /*http://wellroundedgeek.com/post/2011/01/25/Simple-XNA-Cross-Platform-Settings-Manager.aspx*/
    #region application settings
    /*TODO JMC properly implement There should no be two classes in same cs*/
    /// <summary>
    /// 
    /// </summary>
    public class AppSettings
    {

        /// <summary>
        /// Has Run Once setting to determine whether settings need to be generated.
        /// </summary>
        public bool HasRunOnce { get; set; }
        /// <summary>
        /// Determine fullscreen true or false
        /// </summary>
        public bool IsFullScreen { get; set; }
        /// <summary>
        /// Bool to Enable/Disable Music
        /// </summary>
        public bool EnableMusic { get; set; }
        /// <summary>
        /// Bool to Enable/Disable SFX
        /// </summary>
        public bool EnableSfx { get; set; }

        /// <summary>
        /// Sets Default Hardcoded Settings
        /// </summary>
        public AppSettings()
        {
            // Create our default settings
            HasRunOnce = true;
            EnableMusic = true;
            EnableSfx = true;

            // Since this is cross platform, you can decide what default values to use for a platform.
            // In the case of full screen, phones and XBoxes are always full screen.
            // In the phone an XBox applications, we don't let the user change this setting.
#if WINDOWS
            IsFullScreen = false;
#else
            IsFullScreen = true;
#endif

        }
    }

    #endregion


    #region settings manager

    static class SettingsManager
    {
        private const string FileName = "settings.xml";
        public static AppSettings Settings = new AppSettings();


        public static void LoadSettings()
        {
            // Create our exposed settings class. This class gets serialized to load/save the settings.
            Settings = new AppSettings();
            //Obtain a virtual store for application
#if WINDOWS
            var fileStorage = IsolatedStorageFile.GetUserStoreForDomain();
#else
            var fileStorage = IsolatedStorageFile.GetUserStoreForApplication();
#endif
            // Check if file is there
            if (fileStorage.FileExists(FileName))
            {
                var serializer = new XmlSerializer(Settings.GetType());
                var stream = new StreamReader(new IsolatedStorageFileStream(FileName, FileMode.Open, fileStorage));
                try
                {
                    Settings = (AppSettings)serializer.Deserialize(stream);
                    stream.Close();
                }
                catch
                {
                    // An error occurred so let's use the default settings.
                    stream.Close();
                    Settings = new AppSettings();
                    // Saving is optional - in this sample we assume it works and the error is due to the file not being there.
                    SaveSettings();
                    // Handle other errors here
                }
            }
            else
            {
                SaveSettings();
            }
        }


        public static void SaveSettings()
        {
            //Obtain a virtual store for application
#if WINDOWS
            var fileStorage = IsolatedStorageFile.GetUserStoreForDomain();
#else
IsolatedStorageFile fileStorage = IsolatedStorageFile.GetUserStoreForApplication();
#endif
            var serializer = new XmlSerializer(Settings.GetType());
            var stream = new StreamWriter(new IsolatedStorageFileStream(FileName, FileMode.Create, fileStorage));
            try
            {
                serializer.Serialize(stream, Settings);
            }
            catch
            {
                /*TODO JMC Implement*/
                throw new NotImplementedException();
                // Handle your errors here
            }
            stream.Close();
        }

    }

    #endregion

}