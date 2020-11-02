using Umbraco.Web.Mvc;
using System.Web.Mvc;
using Umbraco_Gallery.ViewModels;
using Umbraco.Core.Models;
using NPoco.Expressions;
using Umbraco.Web;
using System.Web;
using System.Net.Mail;
using System;
using Umbraco.Core.Logging;
using Umbraco_Gallery.Services;

namespace Umbraco_Gallery.Controllers
{
    public class ContactSurfaceController : SurfaceController
    {
        private readonly ISmtpService _smtpService;
        public ContactSurfaceController(ISmtpService smtpService)
        {
            _smtpService = smtpService;
        }

        [HttpGet]
        public ActionResult RenderForm()
        {
            ContactViewModel model = new ContactViewModel() {ContactFormId = CurrentPage.Id};
            return PartialView("/Views/Partials/Contact/ContactForm.cshtml", model);
        }

        [HttpPost]
        public ActionResult RenderForm(ContactViewModel model)
        {
            return PartialView("/Views/Partials/Contact/ContactForm.cshtml", model);
        }

        public ActionResult SubmitForm(ContactViewModel model)
        {
            bool success = false;

            if (ModelState.IsValid)
            {
                success = _smtpService.SendEmail(model); 
            }

            var contactPage = UmbracoContext.Content.GetById(false, model.ContactFormId);
            var sucessMessage = contactPage.Value<IHtmlString>("successMessage");
            var errorMessage = contactPage.Value<IHtmlString>("errorMessage");

            return PartialView("/Views/Partials/Contact/result.cshtml", success ? sucessMessage : errorMessage);
        }
       
    }
}
