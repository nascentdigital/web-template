using Microsoft.Owin.Security.Cookies;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Security;
using TemplateApp.Api.Helpers;
using TemplateApp.Api.Models;
using TemplateApp.Api.Services;
using TemplateApp.Entities;


namespace TemplateApp.Api.Controllers
{
    /// <summary>
    /// Provides various authentication and profile services.
    /// </summary>
    public class UserController : ApiController
    {
        #region instance variables

        private readonly IConfigurationService _configService;
        private readonly IUserService _userService;
        private readonly TemplateAppContext _dbContext;

        #endregion


        #region constructors

        public UserController(IConfigurationService configService, 
            IUserService userService, TemplateAppContext dbContext)
        {
            // initialize instance variables
            _configService = configService;
            _userService = userService;
            _dbContext = dbContext;
        }

        #endregion


        #region actions

        [Authorize]
        [HttpGet]
        [Route("user/current")]
        public async Task<HttpResponseMessage> GetCurrentUser()
        {
            // create response by converting to model
            User user = await _userService.GetCurrentUserAsync();
            return Request.CreateResponse(HttpStatusCode.OK,
                UserModel.FromUser(user));
        }

        [HttpPost]
        [Route("user/login")]
        public async Task<HttpResponseMessage> Login(
            UserCredentialsModel credentials)
        {
            // fail if model is invalid
            if (ModelState.IsValid == false)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                    "Invalid credentials specified.");
            }

            // determine password hash
            string passwordHash = HashHelper.GenerateSHA1(
                _configService.PasswordSalt + credentials.password);

            // find matching user
            var query =
                from u in _dbContext.Users
                where u.Email == credentials.username
                    && u.PasswordHash == passwordHash
                select u;
            User user = await query.FirstOrDefaultAsync();

            // fail if user is not found
            if (user == null)
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden,
                    "Invalid username and password.");
            }

            // otherwise, we are good to login
            else
            {
                // update last login
                // user.LastLoginOn = DateTime.UtcNow;
                // await context.SaveChangesAsync();

                // set login token
                FormsAuthentication.SetAuthCookie(credentials.username,
                    true);

                // return success
                return Request.CreateResponse(HttpStatusCode.OK,
                    UserModel.FromUser(user));
            }
        }

        [Authorize]
        [HttpGet]
        [Route("user/logout")]
        public HttpResponseMessage Logout()
        {
            // clear claims
            Request.GetOwinContext().Authentication.SignOut(
                CookieAuthenticationDefaults.AuthenticationType);

            // return success
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        #endregion

    }
}