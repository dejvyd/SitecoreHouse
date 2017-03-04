using System;
using System.IO;
using Sitecore.Data.Items;
using Sitecore.Mvc.Configuration;
using Sitecore.Mvc.Extensions;
using Sitecore.Mvc.Pipelines.Response.GetRenderer;
using Sitecore.Mvc.Presentation;
using SitecoreHouse.Constants;

namespace SitecoreHouse.Pipelines
{
    /// <summary>
    /// Experience tooltip view renderer class
    /// </summary>
    public class GetExperienceTooltipViewRenderer : GetViewRenderer
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
            var viewPath = this.GetViewPath(rendering, args);
            if (StringExtensions.IsWhiteSpaceOrNull(viewPath))
            {
                return (Renderer) null;
            }

            return (Renderer) new ViewRenderer()
            {
                ViewPath = viewPath,
                Rendering = rendering
            };
        }

        /// <summary>
        /// Gets view path
        /// </summary>
        /// <param name="rendering">Rendering presentation item</param>
        /// <param name="args">Get renderer arguments</param>
        /// <returns>View path</returns>
        protected virtual string GetViewPath(Rendering rendering, GetRendererArgs args)
        {
            return this.GetViewPathFromRenderingType(rendering, args) ?? this.GetViewPathFromRenderingItem(rendering);
        }

        /// <summary>
        /// Gets view path from rendering type
        /// </summary>
        /// <param name="rendering">Rendering presentation item</param>
        /// <param name="args">Get renderer arguments</param>
        /// <returns>View path</returns>
        protected new string GetViewPathFromRenderingType(Rendering rendering, GetRendererArgs args)
        {
            if (StringExtensions.EqualsText(rendering.RenderingType, "View"))
            {
                return this.GetViewPathFromPathProperty(rendering);
            }

            if (StringExtensions.EqualsText(rendering.RenderingType, "Layout"))
            {
                return this.GetViewPathFromLayoutItem(args);
            }

            return (string)null;
        }

        /// <summary>
        /// Gets view path from path property
        /// </summary>
        /// <param name="rendering">Rendering presentation item</param>
        /// <returns>View path</returns>
        private string GetViewPathFromPathProperty(Rendering rendering)
        {
            var str = rendering["Path"];
            if (StringExtensions.IsWhiteSpaceOrNull(str))
            {
                return (string)null;
            }

            return str;
        }

        /// <summary>
        /// Gets view path from layout item
        /// </summary>
        /// <param name="args">Get renderer arguments</param>>
        /// <returns>View path</returns>
        private string GetViewPathFromLayoutItem(GetRendererArgs args)
        {
            var path = ObjectExtensions.ValueOrDefault<LayoutItem, string>(args.LayoutItem,
                (Func<LayoutItem, string>) (item => item.FilePath));
            if (StringExtensions.IsWhiteSpaceOrNull(path))
            {
                return (string) null;
            }

            if (!MvcSettings.IsViewExtension(Path.GetExtension(path)) && StringExtensions.IsAbsoluteViewPath(path))
            {
                return (string) null;
            }

            return path;
        }

        /// <summary>
        /// Gets view path from rendering item
        /// </summary>
        /// <param name="rendering">Rendering presentation item</param>
        /// <returns>View path</returns>
        protected new string GetViewPathFromRenderingItem(Rendering rendering)
        {
            var renderingItem = rendering.RenderingItem;
            if (renderingItem == null ||
                renderingItem.InnerItem.TemplateID != RenderingsTemplatesIds.ExpTooltipViewRendering)
            {
                return null;
            }

            var text = renderingItem.InnerItem["path"];
            if (text.IsWhiteSpaceOrNull())
            {
                return null;
            }

            return text;
        }
    }
}