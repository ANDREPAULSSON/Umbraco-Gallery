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

namespace Umbraco_Gallery.Controllers
{
    public class ContactSurfaceController : SurfaceController
    {
        private readonly ILogger _logger;
        public ContactSurfaceController(ILogger logger)
        {
            _logger = logger;
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
                success = SendEmail(model); 
            }

            var contactPage = UmbracoContext.Content.GetById(false, model.ContactFormId);
            var sucessMessage = contactPage.Value<IHtmlString>("successMessage");
            var errorMessage = contactPage.Value<IHtmlString>("errorMessage");

            return PartialView("/Views/Partials/Contact/result.cshtml", success ? sucessMessage : errorMessage);
        }

        public bool SendEmail(ContactViewModel model)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient client = new SmtpClient();

                string toAddress = "to@test.com";
                string fromAddress = "from@test.com";
                message.Subject = $"Enquiry from: {model.Name} - {model.Email}";
                message.Body = model.Message;
                message.To.Add(new MailAddress(toAddress, toAddress));
                message.From = new MailAddress(fromAddress, fromAddress);

                client.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(typeof(ContactSurfaceController), ex, "Error sending contact form.");
                return false;
            }
        }
    }
}
