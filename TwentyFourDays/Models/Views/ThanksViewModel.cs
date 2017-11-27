using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Models;
using Umbraco.Web.PublishedContentModels;

namespace TwentyFourDays.Models.Views
{
    public class ThanksViewModel : IBaseViewModel<Thanks>
    {
        public Thanks Content { get; }        

        public ThanksViewModel(Thanks thanks)
        {
            Content = thanks;            
        }
    }
}