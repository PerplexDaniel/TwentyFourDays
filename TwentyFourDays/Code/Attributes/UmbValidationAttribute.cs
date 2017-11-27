using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using TwentyFourDays.Code.Examine;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace TwentyFourDays.Code.Attributes.Validation
{
    public abstract class UmbValidationAttribute : ValidationAttribute, IClientValidatable
    {
        protected string ErrorMessageDoctypeAlias { get; }
        protected string ErrorMessagePropertyAlias { get; }        
        private UmbracoNodeSearcher Searcher { get; } = new UmbracoNodeSearcher();        

        public UmbValidationAttribute(string errorMessageDoctypeAlias, string errorMessagePropertyAlias)
        {
            ErrorMessageDoctypeAlias = errorMessageDoctypeAlias;
            ErrorMessagePropertyAlias = errorMessagePropertyAlias;            
        }
        
        // Validate + Generate error message (if invalid)
        protected override ValidationResult IsValid(object value, ValidationContext validationContext) 
            => IsValid(value) 
                ? ValidationResult.Success 
                : new ValidationResult(GetErrorMessage(validationContext), new[] { validationContext.MemberName });

        // Get error message from IPublishedContent based on property alias
        protected virtual string GetErrorMessage(string fieldName)
            => Content
                ?.GetPropertyValue<string>(ErrorMessagePropertyAlias)
                ?.Replace("[#field#]", Regex.Replace(fieldName, @"(?:[^A-z]|\s)+$", ""));

        // Client Side error message based on ModelMetadata
        protected virtual string GetErrorMessage(ModelMetadata metadata)
            // .DisplayName can be null, fallback to .PropertyName
            => GetErrorMessage(metadata.DisplayName ?? metadata.PropertyName);

        // Server Side error message based on ValidationContext
        protected virtual string GetErrorMessage(ValidationContext validationContext)
            // .DisplayName is never null
            => GetErrorMessage(validationContext.DisplayName);

        // Get IPublishedContent based on Document Type alias
        // When alias is NULL -> use current Umbraco page
        protected IPublishedContent Content => ErrorMessageDoctypeAlias != null
           ? Searcher.GetOne<IPublishedContent>(ErrorMessageDoctypeAlias)
           : UmbracoContext.Current?.PublishedContentRequest?.PublishedContent;

        // Generate rules for Client Side validation
        public virtual IEnumerable<ModelClientValidationRule> GetClientValidationRules(
            ModelMetadata metadata, ControllerContext context)
        {
            yield break;
        }

        protected Func<T> GetPropertyFn<T>(string doctypeAlias, string propertyAlias)
        => () =>
        {
            IPublishedContent ipc = doctypeAlias != null
                ? Searcher.GetOne<IPublishedContent>(doctypeAlias)
                : UmbracoContext.Current?.PublishedContentRequest?.PublishedContent;
            if (ipc == null) return default(T);
            return ipc.GetPropertyValue<T>(propertyAlias);
        };
    }
}