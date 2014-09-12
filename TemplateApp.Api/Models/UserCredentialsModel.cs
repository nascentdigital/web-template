using System.ComponentModel.DataAnnotations;


namespace TemplateApp.Api.Models
{
    public class UserCredentialsModel
    {
        #region properties

        [Required]
        [EmailAddress]
        public string username { get; set; }

        [Required]
        public string password { get; set; }

        #endregion

    }
}