using System.Web.Mvc;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;
using Umbraco.Web.PublishedContentModels;
using TwentyFourDays.Models.Views;

namespace TwentyFourDays.Controllers
{
    public class ContactController : RenderMvcController
    {
        public override ActionResult Index(RenderModel model)
        {            
            return CurrentTemplate(new ContactViewModel(model.Content as Contact));               
        }  
    }
}