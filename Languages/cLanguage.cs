using System.Globalization;
using System.Resources;

namespace EasySave.Languages
{
    public class cLanguage
    {
        // Declaration of resources for French and English languages
        private static ResourceManager _englishResourceManager;
        private static ResourceManager _frenchResourceManager;

        // Temporary string variable
        string strigue;

        // Property for current culture
        public static CultureInfo CurrentCulture { get; set; }

        // Static constructor
        static cLanguage()
        {
            // Initialize resources for English and French languages
            _englishResourceManager = new ResourceManager("EasySave.Languages.English", typeof(cLanguage).Assembly);
            _frenchResourceManager = new ResourceManager("EasySave.Languages.Francais", typeof(cLanguage).Assembly);

            // Set the current culture to French language
            CurrentCulture = CultureInfo.CreateSpecificCulture("fr-FR");
        }

        // Get a string from a resource key
        public static string GetString(string key)
        {
            // Initialize a resource manager
            ResourceManager? resourceManager = null;

            // Determine if the current culture is French or English
            if (CurrentCulture.Name == "en-US")
            {
                // Use English resources
                resourceManager = _englishResourceManager;
            }
            else if (CurrentCulture.Name == "fr-FR")
            {
                // Use French resources
                resourceManager = _frenchResourceManager;
            }

            // Return the corresponding string or an empty string if the key is unknown
            return resourceManager?.GetString(key) ?? "";
        }

        // Change the current culture based on the specified culture name
        public static void SetLanguage(string cultureName)
        {
            // Create a culture object from the specified culture name
            CultureInfo culture = CultureInfo.GetCultureInfo(cultureName);

            // If the culture is found, set the current culture to the new culture
            if (culture != null)
            {
                CurrentCulture = culture;
            }
        }
    }
}
