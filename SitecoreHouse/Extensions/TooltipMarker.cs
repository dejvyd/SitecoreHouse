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
            return "</div><div class=\"koniec-jebania\"></div>";
            //if (this.ClientData == null)
            //    return string.Empty;
            //return Placeholder.GetControlEndMarker(this.clientData, string.Empty);
        }

        public virtual string GetStart()
        {
            return "<div class=\"start-jebania\">";
            //ChromeData clientData = this.ClientData;
            //if (clientData == null)
            //    return string.Empty;
            //string @string = ID.Parse(this.renderingContext.Rendering.UniqueId).ToShortID().ToString();
            //bool selectable = this.placeholderContext.IsEditable();
            //return Placeholder.GetControlStartMarker(@string, clientData, selectable);
        }
    }
}