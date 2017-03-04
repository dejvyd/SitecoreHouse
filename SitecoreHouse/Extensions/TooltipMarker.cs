using Sitecore;
using Sitecore.Data;
using Sitecore.Diagnostics;
using Sitecore.Layouts;
using Sitecore.Mvc.ExperienceEditor.Presentation;
using Sitecore.Mvc.Presentation;
using Sitecore.Pipelines.GetChromeData;
using Sitecore.Web.UI.PageModes;
using Sitecore.Web.UI.WebControls;

namespace SitecoreHouse.Extensions
{
    /// <summary>
    /// Class responsible for Tooltip Marker
    /// </summary>
    /// <seealso cref="Sitecore.Mvc.ExperienceEditor.Presentation.IMarker" />
    internal class TooltipMarker : IMarker
    {
        /// <summary>
        /// The rendering context
        /// </summary>
        private readonly RenderingContext renderingContext;
        /// <summary>
        /// The placeholder context
        /// </summary>
        private readonly PlaceholderContext placeholderContext;
        /// <summary>
        /// The client data
        /// </summary>
        private ChromeData clientData;
        /// <summary>
        /// Gets the client data.
        /// </summary>
        /// <value>
        /// The client data.
        /// </value>
        protected ChromeData ClientData
        {
            get
            {
                return this.clientData ?? (this.clientData = this.GetClientData());
            }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="TooltipMarker"/> class.
        /// </summary>
        /// <param name="renderingContext">The rendering context.</param>
        /// <param name="placeholderContext">The placeholder context.</param>
        public TooltipMarker(RenderingContext renderingContext, PlaceholderContext placeholderContext)
        {
            Assert.ArgumentNotNull((object)renderingContext, "renderingContext");
            Assert.ArgumentNotNull((object)placeholderContext, "placeholderContext");
            this.renderingContext = renderingContext;
            this.placeholderContext = placeholderContext;
        }
        /// <summary>
        /// Gets the client data.
        /// </summary>
        /// <returns></returns>
        protected virtual ChromeData GetClientData()
        {
            GetChromeDataArgs args = new GetChromeDataArgs("rendering", this.renderingContext.Rendering.Item);
            GetChromeDataPipeline.Run(args);
            return args.ChromeData;
        }
        /// <summary>
        /// Gets the end marker.
        /// </summary>
        /// <returns></returns>
        public virtual string GetEnd()
        {
            return string.Empty;
        }
        /// <summary>
        /// Gets the start marker.
        /// </summary>
        /// <returns></returns>
        public virtual string GetStart()
        {
            string tooltipText = " Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi ornare eu tortor auctor pulvinar. Mauris vulputate, dolor sed feugiat tempus.";
            return "<div class=\"sh-comments ninja\">" + tooltipText + "</div>";
        }
    }
}