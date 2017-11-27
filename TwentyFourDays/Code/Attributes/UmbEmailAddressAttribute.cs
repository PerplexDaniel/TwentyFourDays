using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Umbraco.Web.PublishedContentModels;

namespace TwentyFourDays.Code.Attributes.Validation
{
    public class UmbEmailAddressAttribute : UmbValidationAttribute
    {
        private EmailAddressAttribute EmailAddressAttribute { get; } = new EmailAddressAttribute();

        public UmbEmailAddressAttribute(                        
            string errorMessageDoctypeAlias = Settings.ModelTypeAlias,
            string errorMessagePropertyAlias = nameof(Settings.EmailAddressInvalid)
        ) : base(errorMessageDoctypeAlias, errorMessagePropertyAlias) {}

        public override bool IsValid(object value)
            => EmailAddressAttribute.IsValid(value);

        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
            => new[] { new ModelClientValidationRule
            {
                ValidationType = "email",
                ErrorMessage = GetErrorMessage(metadata)
            } };
    }
}