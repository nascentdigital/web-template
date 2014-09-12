using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TemplateApp.Entities;


namespace TemplateApp.Api.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Returns the currently logged in user if any, but must be <b>invoked 
        /// after</b> the <see cref="GetCurrentUserAsync()"/> method has been called.
        /// </summary>
        User PeekCurrentUser();

        /// <summary>
        /// Returns the currently logged in user, or <c>null</c> if there is no
        /// user currently logged in.
        /// </summary>
        Task<User> GetCurrentUserAsync();
    }
}