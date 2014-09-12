using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using TemplateApp.Entities;


namespace TemplateApp.Api.Services
{
    public class UserService : IUserService
    {
        #region class variables

        private static readonly User _nullUser;

        #endregion


        #region instance variables

        private readonly TemplateAppContext _dbContext;
        private User _user;

        #endregion


        #region constructors

        static UserService()
        {
            // initialize class variables
            _nullUser = new User();
        }

        public UserService(TemplateAppContext dbContext)
        {
            _dbContext = dbContext;
            _user = null;
        }

        #endregion


        #region public methods

        /// <summary>
        /// Returns the currently logged in user if any, but must be <b>invoked 
        /// after</b> the <see cref="GetCurrentUserAsync()"/> method has been called.
        /// </summary>
        public User PeekCurrentUser()
        {
            return _user == _nullUser
                ? null
                : _user;
        }

        /// <summary>
        /// Returns the currently logged in user, or <c>null</c> if there is no
        /// user currently logged in.
        /// </summary>
        public async Task<User> GetCurrentUserAsync()
        {
            // get current user (or fetch user)
            User currentUser = _user;
            if (currentUser == null)
            {
                // fetch user based on user id (if any)
                IPrincipal principal = Thread.CurrentPrincipal;
                IIdentity identity = principal == null
                    ? null
                    : principal.Identity;
                if (identity != null
                    && identity.IsAuthenticated)
                {
                    var query =
                        from u in _dbContext.Users
                        where u.Email == identity.Name
                        select u;
                    currentUser = await query.FirstOrDefaultAsync();
                }

                // or use null user
                else
                {
                    currentUser = _nullUser;
                }

                // persist user
                _user = currentUser;
            }

            // return result (or null if it is the null user object)
            return currentUser == _nullUser
                ? null
                : currentUser;
        }

        #endregion

    }
}