using TwentyFourDays.Code.Attributes.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Umbraco.Web.PublishedContentModels;

namespace TwentyFourDays.Models
{
    public class ContactForm
    {
        [UmbDisplayName(nameof(Contact.LabelName))]
        [UmbRequired, UmbStringLength(50)]
        public string Name { get; set; }

        [UmbDisplayName(nameof(Contact.LabelEmailAddress))]
        [UmbRequired, UmbEmailAddress, UmbStringLength(50)]
        public string EmailAddress { get; set; }

        [UmbDisplayName(nameof(Contact.LabelMessage))]
        [UmbRequired, UmbStringLength(1000)]
        [UmbUseWordAtLeastNTimes(
            nameof(Contact.WordToUseInMessage), 
            nameof(Contact.MinimumAmountOfTimesToUseWord)
        )]
        public string Message { get; set; }
    }

    //public class ContactForm
    //{
    //    [DisplayName("Name"), Required(ErrorMessage = "Please provide your name"), StringLength(50)]
    //    public string Name { get; set; }

    //    [DisplayName("Email address"), Required, StringLength(50)]
    //    [EmailAddress(ErrorMessage = "Invalid email address")]
    //    public string EmailAddress { get; set; }

    //    [DisplayName("Message"), Required, StringLength(1000, ErrorMessage = "Use at most 1000 characters")]
    //    public string Message { get; set; }
    //}
}