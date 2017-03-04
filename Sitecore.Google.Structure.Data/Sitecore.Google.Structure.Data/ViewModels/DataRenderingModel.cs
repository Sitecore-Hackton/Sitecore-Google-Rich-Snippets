using System.Collections.Generic;
using System.Web;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Feature.GoogleStructureData.DataFieldTypes;
using Sitecore.Feature.GoogleStructureData.FieldValueResolvers;
using Sitecore.Feature.GoogleStructureData.Models;
using Sitecore.Feature.GoogleStructureData.Services;
using Sitecore.Feature.GoogleStructureData.StructureDataObjects;
using Sitecore.Foundation.SitecoreExtensions.Extensions;
using Sitecore.Mvc.Helpers;
using Sitecore.Mvc.Presentation;

namespace Sitecore.Feature.GoogleStructureData.ViewModels
{
    public class DataRenderingModel : RenderingModel
    {
        private IDictionary<string, DataFieldWrapper> _fields = new Dictionary<string, DataFieldWrapper>();

        public override void Initialize(Rendering rendering)
        {
            base.Initialize(rendering);

            ////var title = new ItemFieldNameDataField(Rendering.Item)
            ////{
            ////    ItemFieldName = "NewsTitle",
            ////};

            ////_fields.Add(Constants.Items.ArticleFieldItems.HeadlineFieldID.ToString(), new DataFieldWrapper(title));

            ////var mainentryofpage = new ComputedValueDataField(Rendering.Item)
            ////{
            ////    TypeName = "Sitecore.Feature.GoogleStructureData.FieldValueResolvers.GetItemUrl,Sitecore.Feature.GoogleStructureData"
            ////};

            ////_fields.Add(Constants.Items.ArticleFieldItems.MainEntryofPageFieldID.ToString(), new DataFieldWrapper(mainentryofpage));

            ////var author = new ReferenceItemFieldNameDataField(Rendering.Item)
            ////{
            ////    ItemFieldName = "Author",
            ////    ReferenceItemTemplateId = "{94A8C8E9-690B-4E65-98E7-F95800222767}",
            ////    RefernceItemFieldName = "Title"
            ////};

            ////_fields.Add(Constants.Items.ArticleFieldItems.AuthorFieldID.ToString(), new DataFieldWrapper(author));

            ////var datemodified = new ItemFieldNameDataField(Rendering.Item)
            ////{
            ////    ItemFieldName = "__Updated"
            ////};

            ////_fields.Add(Constants.Items.ArticleFieldItems.DateModifiedFieldID.ToString(), new DataFieldWrapper(datemodified));

            ////var datepublished = new ItemFieldNameDataField(Rendering.Item)
            ////{
            ////    ItemFieldName = "NewsDate"
            ////};

            ////_fields.Add(Constants.Items.ArticleFieldItems.DatePublishedFieldID.ToString(), new DataFieldWrapper(datepublished));

            ////var description = new ItemFieldNameDataField(Rendering.Item)
            ////{
            ////    ItemFieldName = "NewsBody"
            ////};

            ////_fields.Add(Constants.Items.ArticleFieldItems.DescriptionFieldID.ToString(), new DataFieldWrapper(description));

            ////var newsimage = new ItemFieldNameDataField(Rendering.Item)
            ////{
            ////    ItemFieldName = "NewsImage"
            ////};

            ////_fields.Add(Constants.Items.ArticleFieldItems.ImageFieldID.ToString(), new DataFieldWrapper(newsimage));

            ////var publisher = new ItemFieldNameDataField(Rendering.Item)
            ////{
            ////    ItemFieldName = ""
            ////};

            ////_fields.Add(Constants.Items.ArticleFieldItems.PublisherFieldID.ToString(), new DataFieldWrapper(publisher));
             
           _fields = new MetadataService().GetFields(rendering); 
        }

        public bool HasValueFor(ID fieldId)
        {
            if (FieldExists(fieldId))
            {
                return !string.IsNullOrEmpty(_fields[fieldId.ToString()].GetFieldValue<string>());
            }

            return false;
        }

        private bool FieldExists(ID fieldId)
        {
            return _fields.ContainsKey(fieldId.ToString());
        }

        public string GetStringValue(ID fieldId)
        {
            if (FieldExists(fieldId))
            {
                return _fields[fieldId.ToString()].GetFieldValue<string>();
            }

            return string.Empty;
        }

        public HtmlString Field(ID fieldId)
        {
            return _fields[fieldId.ToString()].Field();
        }

        public HtmlString RenderImageObject(ID fieldId)
        {
            if (FieldExists(fieldId))
            {
                var obj = _fields[fieldId.ToString()].GetFieldValue<object>();
                if (obj != null)
                {
                    return new ImageObject().Render(obj);
                }
            }
            return new HtmlString("");
        }

        public HtmlString RenderPublisherObject(ID fieldId)
        {
            if (FieldExists(fieldId))
            {
                var obj = _fields[fieldId.ToString()].GetFieldValue<object>();
                if (obj != null)
                {
                    return new OrganizationObject().Render(obj);
                }
            }
            return new HtmlString("");
        }

        public HtmlString DateObject(ID fieldId)
        {
            if (FieldExists(fieldId))
            {
                var obj = _fields[fieldId.ToString()].GetFieldValue<object>();
                if (obj != null)
                {
                    return new DateObject().Render(obj);
                }
            }
            return new HtmlString("");
        }
    }

}