using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace P2FixAnAppDotNetCode.Models.Services
{
    /// <summary>
    /// Provides services method to manage the application language
    /// </summary>
    public class LanguageService : ILanguageService
    {
        /// <summary>
        /// Set the UI language
        /// </summary>
        public void ChangeUiLanguage(HttpContext context, string language)
        {
            string culture = SetCulture(language);
            UpdateCultureCookie(context, culture);
        }

        /// <summary>
        /// Set the culture
        /// </summary>
        public string SetCulture(string language)
        {
            string culture = "";
            switch (language.ToLower())
            {
                case "french":
                    culture = "fr";
                    CultureInfo.CurrentCulture = new CultureInfo(culture);
                    break;

                case "english":
                    culture = "en";
                    CultureInfo.CurrentCulture = new CultureInfo(culture);
                    break;

                default:
                    culture = "en";
                    CultureInfo.CurrentCulture = new CultureInfo(culture);
                    break;
            }
            
            return culture;
        }

        /// <summary>
        /// Update the culture cookie
        /// </summary>
        public void UpdateCultureCookie(HttpContext context, string culture)
        {
            context.Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)));
        }
    }
}
