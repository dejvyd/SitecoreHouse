using System.Linq;
using Sitecore.Data;
using Sitecore.Data.Templates;

namespace SitecoreHouse.Extensions
{
    /// <summary>
    /// This class is responsible for storing extensions related to Sitecore templates.
    /// </summary>
    public static class TemplateExtensions
    {
        /// <summary>
        /// Checks if template is derived from specific template
        /// </summary>
        /// <param name="template">Template to be processed.</param>
        /// <param name="templateId">ID of the template to be checked</param>
        /// <returns>True if template is derived from specific template, otherwise false</returns>
        public static bool IsDerived(this Template template, ID templateId)
        {
            return template != null &&
                   (template.ID == templateId ||
                    template.GetBaseTemplates().Any(baseTemplate => baseTemplate.IsDerived(templateId)));
        }
    }
}