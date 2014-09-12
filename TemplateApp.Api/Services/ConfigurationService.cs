using System.Collections.Specialized;


namespace TemplateApp.Api.Services
{
    public class ConfigurationService : IConfigurationService
    {
        #region constants

        private const string HostKey = "app.host";
        private const string PasswordSaltKey = "app.passwordSalt";

        #endregion


        #region constructors

        public ConfigurationService()
        {
            // initialize class variables
            NameValueCollection appSettings = 
                System.Configuration.ConfigurationManager.AppSettings;
            AppHost = appSettings[HostKey];
            PasswordSalt = appSettings[PasswordSaltKey];
        }

        #endregion


        #region properties

        public string AppHost { get; private set; }

        public string PasswordSalt { get; private set; }

        #endregion
    }
}