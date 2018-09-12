using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlueVends.Presentation.HTML_Helpers
{
    public static class ImageHelper

    {

        public static MvcHtmlString Image(this HtmlHelper helper, string src, string altText)

        {

            var builder = new TagBuilder("img");

            builder.MergeAttribute("src", src);

            builder.MergeAttribute("height", "30px");
            builder.MergeAttribute("width", "30px");

            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));

        }

    }
}