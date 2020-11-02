using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco_Gallery.ViewModels;

namespace Umbraco_Gallery.Services
{
    public interface ISmtpService
    {
        bool SendEmail(ContactViewModel model);
    }
}
