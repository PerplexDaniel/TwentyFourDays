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