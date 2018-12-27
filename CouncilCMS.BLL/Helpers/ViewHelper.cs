using System;
using Bissoft.CouncilCMS.DAL.Entities;
using Bissoft.CouncilCMS.BLL.ViewModels;
using System.Collections.Generic;
using Bissoft.CouncilCMS.Core.Enums;

namespace Bissoft.CouncilCMS.BLL.Helpers
{
    public static class ViewHelper
    {
        public static bool GetCurrentMenuItems(List<CmsMenuItem> source, MenuItemType? currentType = null, MenuItemType? currentSecondType = null, String currentValue = null, String currentSecondValue = null)
        {
            var setted = false;

            if (source != null)
            {
                foreach (var item in source)
                {
                    if (item.Type == currentType && item.Value == currentValue)
                    {
                        item.Current = true;
                        item.Expanded = true;
                        setted = true;
                    }

                    var subSet = GetCurrentMenuItems(item.Items, currentType, null, currentValue, null);

                    if (!setted)
                    {
                        setted = subSet;
                        if (setted)
                            item.Expanded = true;
                    }

                }

                if (!setted && currentSecondValue != null && !String.IsNullOrEmpty(currentSecondValue))
                {
                    foreach (var item in source)
                    {
                        if (item.Type == currentSecondType && item.Value == currentSecondValue)
                        {
                            item.Current = true;
                            item.Expanded = true;
                            setted = true;
                        }

                        var subSet = GetCurrentMenuItems(item.Items, currentSecondType, null, currentSecondValue, null);

                        if (!setted)
                        {
                            setted = subSet;
                            if (setted)
                                item.Expanded = true;
                        }
                    }
                }
            }

            return setted;
        }

        public static string GetContentColumnClass(ContentColumn column)
        {
            var cssClass = String.Empty;

            if (column.LgWidth != column.MdWidth)
               cssClass += "col-lg-" + column.LgWidth + " "; 
            
            if (column.MdWidth != column.SmWidth)
                cssClass += "col-md-" + column.MdWidth + " ";
            
            if (column.SmWidth != column.XsWidth)
                cssClass += "col-sm-" + column.SmWidth + " ";
            
            if (String.IsNullOrEmpty(cssClass) || (column.XsWidth < 12))
                cssClass += "col-xs-" + column.SmWidth + " ";

            if (column.LgOffset != column.MdOffset)
                cssClass += "col-lg-offset-" + column.LgOffset + " ";

            if (column.MdOffset != column.SmOffset)
                cssClass += "col-md-offset-" + column.MdOffset + " ";

            if (column.SmOffset != column.XsOffset)
                cssClass += "col-sm-offset-" + column.SmOffset + " ";

            if (column.XsOffset > 0)
                cssClass += "col-xs-offset-" + column.XsOffset + " ";

            cssClass += column.CssClasses;

            return cssClass.Trim() + " contnet-column";
        }

        public static string GetContentColumnStyle(ContentColumn column)
        {
            var cssStyle = String.Empty;

            if (!String.IsNullOrEmpty(column.BackgroundColor))
                cssStyle += "background-color:" + column.BackgroundColor + ";";

            if (!String.IsNullOrEmpty(column.BackgroundImage))
                cssStyle += "background-image:url('" + column.BackgroundImage + "');";

            if (!String.IsNullOrEmpty(column.BackgroundSize))
                cssStyle += "background-size:" + column.BackgroundSize + ";";

            if (!String.IsNullOrEmpty(column.BackgroundRepeat))
                cssStyle += "background-repeat:" + column.BackgroundRepeat + ";";

            if (!String.IsNullOrEmpty(column.BackgroundPosition))
                cssStyle += "background-repeat:" + column.BackgroundPosition + ";";

            if (!String.IsNullOrEmpty(column.CssStyles))
                cssStyle += column.CssStyles;
            
            return cssStyle;
        }

        public static string GetContentRowStyle(ContentRow row)
        {
            var cssStyle = String.Empty;

            if (!String.IsNullOrEmpty(row.BackgroundColor))
                cssStyle += "background-color:" + row.BackgroundColor + ";";

            if (!String.IsNullOrEmpty(row.BackgroundImage))
                cssStyle += "background-image:url('" + row.BackgroundImage + "');";

            if (!String.IsNullOrEmpty(row.BackgroundSize))
                cssStyle += "background-size:" + row.BackgroundSize + ";";

            if (!String.IsNullOrEmpty(row.BackgroundRepeat))
                cssStyle += "background-repeat:" + row.BackgroundRepeat + ";";

            if (!String.IsNullOrEmpty(row.BackgroundPosition))
                cssStyle += "background-repeat:" + row.BackgroundPosition + ";";

            if (!String.IsNullOrEmpty(row.CssStyles))
                cssStyle += row.CssStyles;

            return cssStyle;
        }      
    }
}
