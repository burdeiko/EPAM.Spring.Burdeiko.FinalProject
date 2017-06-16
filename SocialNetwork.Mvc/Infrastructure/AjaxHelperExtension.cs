using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using SocialNetwork.Mvc.Models;

namespace SocialNetwork.Mvc.Infrastructure
{
    public static class AjaxHelperExtension
    {
        public static MvcHtmlString PageLinks<T>(this AjaxHelper ajaxHelper, PagedList<T> pagedList, Func<int, string> pageUrl, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            var result = new StringBuilder();
            for (int i = 0; i < pagedList.PagesCount; i++)
            {
                var tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i + 1));
                tag.InnerHtml = (i+1).ToString();
                tag.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
                tag.MergeAttributes((ajaxOptions ?? new AjaxOptions()).ToUnobtrusiveHtmlAttributes());
                if (i + 1 == pagedList.CurrentPage)
                {
                    tag.AddCssClass("selected");
                    tag.AddCssClass("btn-primary");
                }
                tag.AddCssClass("btn btn-default");
                result.Append(tag.ToString());
            }
            return new MvcHtmlString(result.ToString());
        }
    }
}