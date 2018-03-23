using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAppAspCore.ViewModels;

namespace TestAppAspCore.Infrastructure
{
    public class PageLinkTagHelper : TagHelper
    {
        private IUrlHelperFactory urlHelperFactory;
        public PageLinkTagHelper(IUrlHelperFactory helperFactory)
        {
            urlHelperFactory = helperFactory;
        }
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        public PageViewModel PageModel { get; set; }
        public string PageAction { get; set; }
        public string PageController { get; set; }

        [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
        public Dictionary<string, object> PageUrlValues { get; set; } = new Dictionary<string, object>();

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            output.TagName = "div";

            TagBuilder tag = new TagBuilder("ul");
            tag.AddCssClass("pagination");
            string symbol;
            // prevButton
            symbol = "<";
            TagBuilder prevItem = CreateTag(PageModel.PageNumber - 1, urlHelper, symbol);
            if (!PageModel.HasPreviousPage)
            {
                prevItem.AddCssClass("disabled");
            }
            tag.InnerHtml.AppendHtml(prevItem);

            // numPages
            for(int i=1; i<=PageModel.TotalPages; i++)
            {
                TagBuilder currentItem = CreateTag(i, urlHelper, i.ToString());               
                if (i == PageModel.PageNumber)
                    currentItem.AddCssClass("active");
                tag.InnerHtml.AppendHtml(currentItem);
            }

            //nextButton
            symbol = ">";
            TagBuilder nextItem = CreateTag(PageModel.PageNumber + 1, urlHelper, symbol);
            if (!PageModel.HasNextPage)
            {
                nextItem.AddCssClass("disabled");
            }
            tag.InnerHtml.AppendHtml(nextItem);
            output.Content.AppendHtml(tag);
        }

        TagBuilder CreateTag(int pageNumber, IUrlHelper urlHelper, string symbol)
        {
            TagBuilder item = new TagBuilder("li");
            item.AddCssClass("page-item");
            TagBuilder link = new TagBuilder("a");
            link.AddCssClass("page-link");

            PageUrlValues["page"] = pageNumber;
            link.Attributes["href"] = urlHelper.Action(PageAction, PageController, PageUrlValues);
            link.InnerHtml.Append(symbol);
            item.InnerHtml.AppendHtml(link);
            return item;
        }
    }
}
