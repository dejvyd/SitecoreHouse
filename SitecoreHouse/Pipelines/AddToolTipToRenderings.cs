using System;
using Sitecore;
using Sitecore.Diagnostics;
using Sitecore.Mvc.ExperienceEditor.Presentation;
using Sitecore.Mvc.Pipelines.Response.RenderRendering;
using Sitecore.Mvc.Presentation;
using SitecoreHouse.Extensions;

namespace SitecoreHouse.Pipelines
{
    public class AddToolTipToRenderings : RenderRenderingProcessor
    {
        public override void Process(RenderRenderingArgs args)
        {
            if (!string.IsNullOrEmpty(args.Rendering.DataSource) && args.PageContext.Item.Database.Name != "core" && args.PageContext.Item.Database.GetItem(args.Rendering.DataSource) == null)
            {
                Log.Warn(string.Format("'{0}' is not valid datasource.", (object)args.Rendering.DataSource), (object)this);
                args.AbortPipeline();
            }
            else
            {
                if (args.Rendered || Context.Site == null || !Context.PageMode.IsExperienceEditorEditing)
                    return;
                IMarker marker = this.GetMarker();
                if (marker == null)
                    return;
                int index = args.Disposables.FindIndex((Predicate<IDisposable>)(x => x.GetType() == typeof(Wrapper)));
                if (index < 0)
                    index = 0;
                args.Disposables.Insert(index, (IDisposable)new Wrapper(args.Writer, marker));
            }
        }

        protected virtual IMarker GetMarker()
        {
            RenderingContext currentOrNull1 = RenderingContext.CurrentOrNull;
            if (currentOrNull1 == null || currentOrNull1.Rendering == null)
                return (IMarker)null;
            //if (!currentOrNull1.Rendering.IsXmlBasedRendering())
            //    return (IMarker)null;
            PlaceholderContext currentOrNull2 = PlaceholderContext.CurrentOrNull;
            if (currentOrNull2 == null)
                return (IMarker)null;
            //return (IMarker)null;
            return (IMarker)new TooltipMarker(currentOrNull1, currentOrNull2);
        }
    }
}