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
    /// Provides REST services for Project resources.
    /// </summary>
    [Authorize]
    public class ProjectController : ApiController
    {
        #region instance variables

        private readonly IUserService _userService;
        private readonly TemplateAppContext _dbContext;

        #endregion


        #region constructors

        public ProjectController(IUserService userService, 
            TemplateAppContext dbContext)
        {
            // initialize instance variables
            _userService = userService;
            _dbContext = dbContext;
        }

        #endregion


        #region actions

        [Authorize]
        [HttpGet]
        [Route("projects")]
        public async Task<HttpResponseMessage> GetProjects()
        {
            // query projects
            var query =
                from p in _dbContext.Projects
                orderby p.Name
                select p;

            // return projects
            Project[] projects = await query.ToArrayAsync();
            return Request.CreateResponse(HttpStatusCode.OK, projects);
        }

        [HttpGet]
        [Route("project/{projectId}")]
        public async Task<HttpResponseMessage> GetProject(int projectId)
        {
            // find project
            var query =
                from p in _dbContext.Projects
                where p.Id == projectId
                select p;
            Project project = await query.FirstOrDefaultAsync();

            // fail if user is not found
            if (project == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound,
                    "Invalid project id specified.");
            }

            // or return project
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, project);
            }
        }

        #endregion

    }
}