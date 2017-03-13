using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Iso8583WebClient
{
    public static class HtmlHelperExt
    {
        public static MvcHtmlString DropDownList(
            this HtmlHelper htmlHelper,
            String name,
            String defaultValue,
            IEnumerable<SelectListItem> selectList
        )
        {
            bool isSelected = false;
            foreach (SelectListItem item in selectList)
            {
                if (!isSelected && item.Value == defaultValue)
                {
                    item.Selected = true;
                    isSelected = true; //Ensure only one selected
                }
                else
                {
                    item.Selected = false;
                }
            }
            return htmlHelper.DropDownList(name, selectList);
        }
    }
}