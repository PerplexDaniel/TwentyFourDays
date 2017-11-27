using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Umbraco.Core.Models;
using Umbraco.Web.PublishedContentModels;
using Tasks = System.Threading.Tasks;

namespace TwentyFourDays.Models.Views
{
    public class HomeViewModel : IBaseViewModel<Home>
    {
        public Home Content { get; }             

        public HomeViewModel(Home home)
        {
            Content = home;
        }
    }
}