using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Umbraco.Web.PublishedContentModels;

namespace TwentyFourDays.Code.Attributes.Validation
{
    public class UmbStringLengthAttribute : UmbValidationAttribute
    {
        private Func<int?> GetMaxLength { get; }        

        public UmbStringLengthAttribute(
            int maxLength = 50, 
            string errorMessageDoctypeAlias = Settings.ModelTypeAlias, 
            string errorMessagePropertyAlias = nameof(Settings.FieldLengthExceeded)
        ) : base(errorMessageDoctypeAlias, errorMessagePropertyAlias)
        {
            GetMaxLength = () => maxLength;
        }

        public UmbStringLengthAttribute(
            string maxLengthDoctypeAlias, 
            string maxLengthPropertyAlias, 
            string errorMessageDoctypeAlias = Settings.ModelTypeAlias, 
            string errorMessagePropertyAlias = nameof(Settings.FieldLengthExceeded)
        ) : base(errorMessageDoctypeAlias, errorMessagePropertyAlias)
        {
            GetMaxLength = GetPropertyFn<int?>(maxLengthDoctypeAlias, maxLengthPropertyAlias);                
        }

        protected override string GetErrorMessage(string fieldName) => 
            base.GetErrorMessage(fieldName)
                ?.Replace("[#max-length#]", GetMaxLength()?.ToString());

        public override bool IsValid(object value)
        {
            int? maxLength = GetMaxLength();
            return maxLength == null                 
                ? true // No maxLength specified => valid
                : new StringLengthAttribute(maxLength.Value).IsValid(value);
        }

        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            int? maxLength = GetMaxLength();
            if(!maxLength.HasValue)
            {
                yield break;
            } else
            {
                yield return new ModelClientValidationStringLengthRule(GetErrorMessage(metadata), 0, maxLength.Value);
            }
        }
    }
}