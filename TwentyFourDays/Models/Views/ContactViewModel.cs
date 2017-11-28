using Umbraco.Web.PublishedContentModels;

namespace TwentyFourDays.Models.Views
{
    public class ContactViewModel : IBaseViewModel<Contact>
    {
        public Contact Content { get; }
        public ContactForm Form { get; }

        public ContactViewModel(Contact contact)
        {
            Content = contact;
            Form = new ContactForm();
        }
    }
}