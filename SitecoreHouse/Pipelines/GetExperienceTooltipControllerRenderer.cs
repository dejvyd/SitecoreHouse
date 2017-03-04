using System;
using Sitecore.Mvc.Configuration;
using Sitecore.Mvc.Extensions;
using Sitecore.Mvc.Pipelines.Response.GetRenderer;
using Sitecore.Mvc.Presentation;
using SitecoreHouse.Constants;

namespace SitecoreHouse.Pipelines
{
    /// <summary>
    /// Experience tooltip controller renderer class
    /// </summary>
    public class GetExperienceTooltipControllerRenderer : GetRendererProcessor
    {
        /// <summary>
        /// Process method executed in mvc.getRenderer pipeline
        /// </summary>
        /// <param name="args">Get renderer arguments</param>
        public override void Process(GetRendererArgs args)
        {
            if (args.Result != null)
            {
                return;
            }

            args.Result = this.GetRenderer(args.Rendering, args);
        }

        /// <summary>
        /// Gets renderer object
        /// </summary>
        /// <param name="rendering">Rendering presentation item</param>
        /// <param name="args">Get renderer arguments</param>
        /// <returns>Renderer object</returns>
        protected virtual Renderer GetRenderer(Rendering rendering, GetRendererArgs args)
        {
            var controllerAndAction = this.GetControllerAndAction(rendering, args);
            if (controllerAndAction == null)
            {
                return null;
            }

            var item = controllerAndAction.Item1;
            var item2 = controllerAndAction.Item2;

            return new ControllerRenderer
            {
                ControllerName = item,
                ActionName = item2
            };
        }

        /// <summary>
        /// Gets tuple object with controller and action
        /// </summary>
        /// <param name="rendering">Rendering presentation item</param>
        /// <param name="args">Get renderer arguments</param>
        /// <returns>Tuple object with controller and action</returns>
        protected virtual Tuple<string, string> GetControllerAndAction(Rendering rendering, GetRendererArgs args)
        {
            var tuple = this.GetFromProperties(rendering);
            if (tuple == null)
            {
                tuple = this.GetFromRenderingItem(rendering, args);
            }

            if (tuple == null)
            {
                return null;
            }

            var item = tuple.Item1;
            var item2 = tuple.Item2;
            var controllerLocator = MvcSettings.ControllerLocator;

            return controllerLocator.GetControllerAndAction(item, item2);
        }

        /// <summary>
        /// Gets tuple object with controller and action from properties
        /// </summary>
        /// <param name="rendering">Rendering presentation item</param>
        /// <returns>Tuple object with controller and action</returns>
        protected Tuple<string, string> GetFromProperties(Rendering rendering)
        {
            if (rendering.RenderingType != "Controller")
            {
                return null;
            }

            var text = rendering["Controller"];
            var item = rendering["Controller Action"];
            if (text.IsWhiteSpaceOrNull())
            {
                return null;
            }

            return new Tuple<string, string>(text, item);
        }

        /// <summary>
        /// Gets tuple object with controller and action from rendering item
        /// </summary>
        /// <param name="rendering">Rendering presentation item</param>
        /// <param name="args">Get renderer arguments</param>
        /// <returns>Tuple object with controller and action</returns>
        protected Tuple<string, string> GetFromRenderingItem(Rendering rendering, GetRendererArgs args)
        {
            var renderingTemplate = args.RenderingTemplate;
            if (renderingTemplate == null)
            {
                return null;
            }

            if (!renderingTemplate.DescendsFromOrEquals(RenderingsTemplatesIds.ExpTooltipControllerRendering))
            {
                return null;
            }

            var renderingItem = rendering.RenderingItem;
            if (renderingItem == null)
            {
                return null;
            }

            var text = renderingItem.InnerItem["Controller"];
            var item = renderingItem.InnerItem["Controller Action"];
            if (text.IsWhiteSpaceOrNull())
            {
                return null;
            }

            return new Tuple<string, string>(text, item);
        }
    }
}