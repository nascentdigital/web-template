using System.ComponentModel.DataAnnotations;
using TemplateApp.Api.Helpers;
using TemplateApp.Api.Services;
using TemplateApp.Entities;


namespace TemplateApp.Api.Models
{
    public class UserModel
    {
        #region properties

        public int? id { get; set; }

        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        public string firstName { get; set; }

        [Required]
        public string lastName { get; set; }

        public string password { get; set; }

        #endregion


        #region public methods

        public static UserModel FromUser(User user)
        {
            return new UserModel()
            {
                id = user.Id,
                email = user.Email,
                firstName = user.FirstName,
                lastName = user.LastName
            };
        }

        public User ToUser(IConfigurationService configService)
        {
            User user = new User()
            {
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                PasswordHash = password == null
                    ? null
                    : HashHelper.GenerateSHA1(configService.PasswordSalt + password)
            };

            if (id != null)
            {
                user.Id = id.Value;
            }

            return user;
        }

        #endregion

    }
}