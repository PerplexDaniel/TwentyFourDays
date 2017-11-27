using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Umbraco.Web.PublishedContentModels;

namespace TwentyFourDays.Code.Attributes.Validation
{
    public class UmbRequiredAttribute : UmbValidationAttribute
    {
        // Used only for its implementation of IsValid (= less code for us to write)
        private RequiredAttribute RequiredAttribute { get; } = new RequiredAttribute();

        public UmbRequiredAttribute(
            string errorMessageDoctypeAlias = Settings.ModelTypeAlias,
            string errorMessagePropertyAlias = nameof(Settings.RequiredField)            
        ) : base(errorMessageDoctypeAlias, errorMessagePropertyAlias) {}

        // Simply call the IsValid method of the built-in RequiredAttribute
        public override bool IsValid(object value) => RequiredAttribute.IsValid(value);

        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules(
            ModelMetadata metadata, ControllerContext context)
        {
            // Use the built-in ModelClientValidationRequiredRule, but supply our own error message
            yield return new ModelClientValidationRequiredRule(GetErrorMessage(metadata));
        }            
    }
}