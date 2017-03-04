using System;
using System.Text;
using System.Web;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Resources.Media;
using Sitecore.Xml;

namespace Sitecore.Feature.GoogleStructureData.StructureDataObjects
{
    /// <summary>
    /// This type defines the structured data value for the publisher information.
    /// </summary>
    public class OrganizationObject : BaseObject
    {
        public string Url { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public string Name { get; set; }

        public override HtmlString Render(object value)
        {
            try
            {
                var field = value as Field;
                if (field != null)
                {
                    value = field.Value;
                }

                var imageXml = value as string;
                if (!string.IsNullOrEmpty(imageXml))
                {
                    var imageId = XmlUtil.GetAttribute("mediaid", XmlUtil.LoadXml(imageXml));
                    if (!string.IsNullOrEmpty(imageId) && ID.IsID(imageId))
                    {
                        MediaItem imageItem = Sitecore.Context.Database.GetItem(imageId);
                        if (imageItem != null)
                        {
                            Url = MediaManager.GetMediaUrl(imageItem);
                            Width = MainUtil.GetInt(imageItem.InnerItem["Width"], 0);
                            Height = MainUtil.GetInt(imageItem.InnerItem["Height"], 0);
                            if (!string.IsNullOrEmpty(Url))
                            {

                                var sb = new StringBuilder(string.Empty);
                                sb.Append(
                                    @"<div itemprop=""publisher"" itemscope itemtype=""https://schema.org/Organization"">");
                                sb.Append(@"<div itemprop=""logo"" itemscope itemtype=""https://schema.org/ImageObject"">");
                                sb.Append(@"<img src =""");
                                sb.Append(Url);
                                sb.Append(@""" />");

                                sb.Append(@"<meta itemprop=""url"" content=""");
                                sb.Append(Url);
                                sb.Append(@""">");

                                if (Width > 0)
                                {
                                    sb.Append(@"<meta itemprop=""width"" content=""");
                                    sb.Append(Width);
                                    sb.Append(@""">");
                                }

                                if (Height > 0)
                                {
                                    sb.Append(@"<meta itemprop=""height"" content=""");
                                    sb.Append(Height);
                                    sb.Append(@""">");
                                }

                                sb.Append("</div>");

                                if (!string.IsNullOrEmpty(Name))
                                {
                                    sb.Append(@"<meta itemprop=""name"" content=""");
                                    sb.Append(Name);
                                    sb.Append(@""">");
                                }
                                sb.Append("</div>");

                                return new HtmlString(sb.ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
            }

            return new HtmlString(string.Empty);
        }
    }
}