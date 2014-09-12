

namespace TemplateApp.Entities
{
    public class User
    {
        #region properties

        public int Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Name { get { return FirstName + " " + LastName; } }

        public string PasswordHash { get; set; }

        #endregion
    }
}
