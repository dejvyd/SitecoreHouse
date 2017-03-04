using System;
using Sitecore;
using Sitecore.Diagnostics;
using Sitecore.Mvc.ExperienceEditor.Presentation;
using Sitecore.Mvc.Pipelines.Response.RenderRendering;
using Sitecore.Mvc.Presentation;
using SitecoreHouse.Constants;
using SitecoreHouse.Extensions;

namespace SitecoreHouse.Pipelines
{
    /// <summary>
    /// Adds Tool Tip Into Current Rendering
    /// </summary>
    /// <seealso cref="Sitecore.Mvc.Pipelines.Response.RenderRendering.RenderRenderingProcessor" />
    public class AddToolTipToRenderings : RenderRenderingProcessor
    {
        /// <summary>
        /// Processes the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public override void Process(RenderRenderingArgs args)
        {
            if (!string.IsNullOrEmpty(args.Rendering.DataSource) && 
                args.PageContext.Item.Database.Name != "core" && 
                args.PageContext.Item.Database.GetItem(args.Rendering.DataSource) == null)
            {
                Log.Warn(string.Format("'{0}' is not valid datasource.", (object)args.Rendering.DataSource), (object)this);
                args.AbortPipeline();
            }
            else
            {
                if (args.Rendered || Context.Site == null || !Context.PageMode.IsExperienceEditorEditing)
                {
                    return;
                }

                var marker = this.GetMarker();
                if (marker == null)
                {
                    return;
                }

                var index = args.Disposables.FindIndex((Predicate<IDisposable>)(x => x.GetType() == typeof(Wrapper)));
                if (index < 0)
                {
                    index = 0;
                }
                args.Disposables.Insert(index, (IDisposable)new Wrapper(args.Writer, marker));
            }
        }
        /// <summary>
        /// Gets the html marker for Renderings with Tooltips.
        /// </summary>
        /// <returns>Html marker</returns>
        protected virtual IMarker GetMarker()
        {
            var currentRendering = RenderingContext.CurrentOrNull;
            if (currentRendering == null || 
                currentRendering.Rendering == null || 
                currentRendering.Rendering.RenderingItem == null ||
                currentRendering.Rendering.RenderingItem.InnerItem == null)
            {
                return (IMarker)null;
            }
            
            var currentPlaceholder = PlaceholderContext.CurrentOrNull;
            if (currentPlaceholder == null)
            {
                return (IMarker)null;
            }

            var renderingItem = currentRendering.Rendering.RenderingItem.InnerItem;
            if (renderingItem.IsDerived(RenderingsTemplatesIds.ExpTooltipControllerRendering) ||
                renderingItem.IsDerived(RenderingsTemplatesIds.ExpTooltipViewRendering))
            {
                return (IMarker)new TooltipMarker(currentRendering, currentPlaceholder);
            }

            return (IMarker)null;
        }
    }
}