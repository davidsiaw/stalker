using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace KilnApi.Helpers {
    /// <summary>
    /// Provides useful methods to work with URL patterns.
    /// </summary>
    public static class PatternParser {
        /// <summary>
        /// Represents regular expression that matches supported URL placeholders.
        /// </summary>
        private static readonly Regex PlaceholderRegex = new Regex(@"{\w*}");

        /// <summary>
        /// Match placeholders within the URL with specified parameters.
        /// Extra parameters will be formatted as query string.
        /// </summary>
        /// <param name="url">An URL with placeholders.</param>
        /// <param name="parameters">Strongly typed parameters dictionary for an URL.</param>
        public static string MatchUrlPlaceholders(string url, IDictionary<string, string> parameters) {
            StringBuilder urlBuilder = new StringBuilder(url);
            Match match = PlaceholderRegex.Match(urlBuilder.ToString());

            while (match.Success) {
                // Retrieve parameter name from the placeholder.
                string paramName = match.Value.Trim('{', '}');

                // If there is no parameter with required name,
                // go to the next placeholder.
                if (!parameters.ContainsKey(paramName)) {
                    match = PlaceholderRegex.Match(urlBuilder.ToString());
                    continue;
                }

                // Replace matched placeholder with respective parameter.
                urlBuilder.Replace(match.Value, HttpUtility.UrlEncode(parameters[paramName]));
                parameters.Remove(paramName);
            }

            // Convert all remained parameters into query string.
            if (parameters.Count > 0) {
                var optionalParameters = string.Join("&", Array.ConvertAll(parameters.Keys.ToArray(),
                    key => string.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(parameters[key]))));
                urlBuilder.AppendFormat("&{0}", optionalParameters);
            }

            return urlBuilder.ToString();
        }

        /// <summary>
        /// Match placeholders within the URL with specified parameters.
        /// Extra parameters will be formatted as query string.
        /// </summary>
        /// <param name="url">An URL with placeholders.</param>
        /// <param name="parameters">Anonymous type represents parameters for an URL.</param>
        public static string MatchUrlPlaceholders(string url, object anonymousParams) {
            IDictionary<string, string> parameters = ParseAnonymous(anonymousParams);
            return MatchUrlPlaceholders(url, parameters);
        }

        /// <summary>
        /// Convert anonymous type to strongly typed dictionary, where
        /// key is a property's name, and value is a property's value.
        /// </summary>
        /// <param name="parameters">Anonymous type represents parameters for an URL.</param>
        /// <returns>Converted dictionary from anonymous type.</returns>
        public static Dictionary<string, string> ParseAnonymous(object parameters) {
            Dictionary<string, string> paramsDict = new Dictionary<string, string>();

            // If parameters is not specified, return empty dictionary.
            if (parameters == null)
                return paramsDict;

            // For each property in given anonymous type
            foreach (var prop in parameters.GetType().GetProperties()) {
                try {
                    // Try to add the property's key as dictionary item's key,
                    // and the property's value as dictionary item's value.
                    paramsDict.Add(prop.Name, prop.GetValue(parameters, null).ToString());
                }
                catch {
                    // Ignore any errors.
                }
            }
            return paramsDict;
        }
    }
}