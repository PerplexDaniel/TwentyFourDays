using System.Web.Mvc;
using TwentyFourDays.Models;
using Umbraco.Web.Mvc;
using Umbraco.Web;
using Umbraco.Web.PublishedContentModels;

namespace TwentyFourDays.Controllers
{
    public class ContactFormController : SurfaceController
    {        
        [HttpPost, ValidateAntiForgeryToken]        
        public ActionResult Submit(ContactForm form)
        {
            if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage();
            }
            
            // Process form

            return RedirectToUmbracoPage(CurrentPage.FirstChild<Thanks>() ?? CurrentPage);            
        }
    }
}