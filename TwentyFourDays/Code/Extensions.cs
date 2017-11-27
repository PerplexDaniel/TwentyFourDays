using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TwentyFourDays.Code
{
    public static class Extensions
    {
        /// <summary>
        /// Retrieves an item from the HttpContext.Items cache for the current request.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="httpContext">HttpContext</param>
        /// <param name="key">Cache key</param>
        /// <param name="computeFn">Func to generate the value if it is not present yet. If left as null, nothing will be stored in cache.</param>
        /// <returns></returns>
        public static T GetItem<T>(this HttpContext httpContext, string key, Func<T> computeFn = null)
        {
            IDictionary items = httpContext?.Items;

            if (items == null)
            {
                return computeFn != null ? computeFn() : default(T);
            }

            if (!items.Contains(key))
            {
                if (computeFn == null)
                {
                    return default(T);
                }

                httpContext.Items[key] = computeFn();
            }

            return (T)httpContext.Items[key];
        }   

        public static string WhenNullOrEmpty(this string value, string ifNullOrEmpty)
            => string.IsNullOrEmpty(value) ? ifNullOrEmpty : value;        

        public static string WhenNullOrEmpty(this string value, Func<string> ifNullOrEmptyFn)
            => string.IsNullOrEmpty(value) ? ifNullOrEmptyFn() : value;         
    }
}