using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Mvc.Pipelines.Response.RenderRendering;

namespace SitecoreHouse.Models
{
    public class WraperTest : ExecuteRenderer
    {
        public override void Process(RenderRenderingArgs args)
        {
            if (args.Rendered || Context.Site == null || !Context.PageMode.IsPageEditorEditing || args.Rendering.RenderingType == "Layout")
            {
                return;
            }

            var marker = this.GetMarker(args);
            if (marker == null)
            {
                return;
            }

            var index = args.Disposables.FindIndex(x => x.GetType() == typeof(Wrapper));
            if (index < 0) index = 0;
            args.Disposables.Insert(index, new Wrapper(args.Writer, marker));
        }

        protected virtual IMarker GetMarker(RenderRenderingArgs args)
        {
            // logic to create your Marker goes here
        }
    }
}