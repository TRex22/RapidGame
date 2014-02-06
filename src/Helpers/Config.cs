namespace RapidGame.Helpers
{
    public class GlobalSettings
    {
        public Properties.Settings Config()
        {
            /*TODO JMC Find a better solution*/
            //var directoryInfo = Directory.GetParent(Directory.GetCurrentDirectory()).Parent;
            //_config = directoryInfo != null ? new Config(directoryInfo.FullName) : null;
            return Properties.Settings.Default;
        }
    } 
}
