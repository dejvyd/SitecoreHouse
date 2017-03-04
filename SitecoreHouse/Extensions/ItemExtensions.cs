using System;
using System.Linq;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;

namespace SitecoreHouse.Extensions
{
    /// <summary>
    /// This class is responsible for storing extensions related to Sitecore Items.
    /// </summary>
    public static class ItemExtensions
    {
        /// <summary>
        /// Checks if item template is derived from specific template
        /// </summary>
        /// <param name="item">Item to be processed.</param>
        /// <param name="templateId">ID of the template to be checked</param>
        /// <returns>True if item is derived from specific template, otherwise false</returns>
        public static bool IsDerived(this Item item, ID templateId)
        {
            return item != null &&
                   TemplateManager.GetTemplate(item).IsDerived(templateId);
        }
    }
}