using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TemplateApp.Api.Services
{
    public interface IConfigurationService
    {
        string AppHost { get; }

        string PasswordSalt { get; }
    }
}