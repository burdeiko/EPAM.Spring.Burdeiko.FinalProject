using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace SocialNetwork.Mvc.Infrastructure
{
    public static class AjaxHelperExtension
    {
        public static MvcHtmlString PagedListPager(this AjaxHelper ajaxHelper, string name, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            var tag = new TagBuilder("ul");
            tag.MergeAttribute("class", "pagination");
            tag.MergeAttribute("name", name);
            tag.MergeAttribute("id", name);

            tag.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            //tag.MergeAttributes((ajaxOptions ?? new AjaxOptions()).ToUnobtrusiveHtmlAttributes());
            var liTag = new TagBuilder("li")
        }
    }
}