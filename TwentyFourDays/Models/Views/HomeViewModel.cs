using Umbraco.Web.PublishedContentModels;

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