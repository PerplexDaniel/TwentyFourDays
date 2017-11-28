using Umbraco.Core.Models;

namespace TwentyFourDays.Models.Views
{
    public interface IBaseViewModel<out T> where T : IPublishedContent
    {
        T Content { get; }        
    }
}