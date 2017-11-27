using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Models;
using Umbraco.Web.PublishedContentModels;

namespace TwentyFourDays.Models.Views
{
    public interface IBaseViewModel<out T> where T : IPublishedContent
    {
        T Content { get; }        
    }
}