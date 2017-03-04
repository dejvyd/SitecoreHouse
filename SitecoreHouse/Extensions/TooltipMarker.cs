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
    internal class TooltipMarker : IMarker
    {
        private readonly RenderingContext renderingContext;
        private readonly PlaceholderContext placeholderContext;
        private ChromeData clientData;

        protected ChromeData ClientData
        {
            get
            {
                return this.clientData ?? (this.clientData = this.GetClientData());
            }
        }

        public TooltipMarker(RenderingContext renderingContext, PlaceholderContext placeholderContext)
        {
            Assert.ArgumentNotNull((object)renderingContext, "renderingContext");
            Assert.ArgumentNotNull((object)placeholderContext, "placeholderContext");
            this.renderingContext = renderingContext;
            this.placeholderContext = placeholderContext;
        }

        protected virtual ChromeData GetClientData()
        {
            GetChromeDataArgs args = new GetChromeDataArgs("rendering", this.renderingContext.Rendering.Item);
            //RenderingReference renderingReference = this.renderingContext.Rendering.GetRenderingReference(Context.Language, this.renderingContext.PageContext.Database);
            //args.CustomData["renderingReference"] = (object)renderingReference;
            GetChromeDataPipeline.Run(args);
            return args.ChromeData;
        }

        public virtual string GetEnd()
        {
            return string.Empty;
        }

        public virtual string GetStart()
        {
            return "<div style=\"width: 20px; height: 20px; background-color: green; position: absolute;\"></div>";
        }
    }
}