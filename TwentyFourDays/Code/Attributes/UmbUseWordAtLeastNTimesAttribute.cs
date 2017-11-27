using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Umbraco.Web.PublishedContentModels;

namespace TwentyFourDays.Code.Attributes.Validation
{
    public class UmbUseWordAtLeastNTimesAttribute : UmbValidationAttribute
    {
        private Func<string> GetWord { get; }
        private Func<int?> GetN { get; }                

        public UmbUseWordAtLeastNTimesAttribute(
            string wordPropertyAlias, string nPropertyAlias,
            string wordDoctypeAlias = null, string nDoctypeAlias = null,            
            string errorMessagePropertyAlias = nameof(Settings.WordNotUsedNtimes),
            string errorMessageDoctypeAlias = Settings.ModelTypeAlias
        ) : base(errorMessageDoctypeAlias, errorMessagePropertyAlias)
        {
            GetWord = GetPropertyFn<string>(wordDoctypeAlias, wordPropertyAlias);
            GetN = GetPropertyFn<int?>(nDoctypeAlias, nPropertyAlias);
        }

        protected override string GetErrorMessage(string fieldName) => 
            base.GetErrorMessage(fieldName)
                ?.Replace("[#word#]", GetWord())
                ?.Replace("[#times#]", GetN()?.ToString());

        public override bool IsValid(object value)
        {
            (bool paramsOk, string word, int? n) = GetParams();
            if (!paramsOk) return true;

            string stringValue = value?.ToString() ?? "";           
            return Regex.Matches(stringValue, word).Count >= n;
        }

        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules(
            ModelMetadata metadata, ControllerContext context)
        {
            (bool paramsOk, string word, int? n) = GetParams();
            if(!paramsOk) yield break;            

            var rule = new ModelClientValidationRule
            {
                ValidationType = "usewordatleastntimes",                    
                ErrorMessage = GetErrorMessage(metadata)             
            };

            rule.ValidationParameters["word"] = word;
            rule.ValidationParameters["n"] = n;

            yield return rule;
        }

        private (bool paramsOk, string word, int? n) GetParams()
        {
            string word = GetWord();
            int? n = GetN();            
            // Verify parameters are supplied and have valid values
            bool paramsOk = !string.IsNullOrEmpty(word) && n > 0;
            return (paramsOk, word, n);
        }
    }
}