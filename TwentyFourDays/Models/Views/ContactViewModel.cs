using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Models;
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