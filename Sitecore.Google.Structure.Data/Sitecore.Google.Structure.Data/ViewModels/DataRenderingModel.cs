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
    /// <summary>
    /// This type defines the renering model for Google structure data rendering.
    /// It contains a field mapping collection base on the settings in sitecore and the current rendering type.
    /// It provides various opration to render the correct content in normal and experience mode and check whether a field has any value from the mapped item or not.
    /// </summary>
    public class DataRenderingModel : RenderingModel
    {
        private IDictionary<string, DataFieldWrapper> _fields = new Dictionary<string, DataFieldWrapper>();

        public override void Initialize(Rendering rendering)
        {
            base.Initialize(rendering);
             
           _fields = new MetadataService().GetFields(rendering); 
        }

        /// <summary>
        /// Checks whether a value exist for the field or not.
        /// </summary>
        /// <param name="fieldId"></param>
        /// <returns></returns>
        public bool HasValueFor(ID fieldId)
        {
            if (FieldExists(fieldId))
            {
                return !string.IsNullOrEmpty(_fields[fieldId.ToString()].GetFieldValue<string>());
            }

            return false;
        }

        /// <summary>
        /// Checks whehter a field exists or not.
        /// </summary>
        /// <param name="fieldId"></param>
        /// <returns></returns>

        private bool FieldExists(ID fieldId)
        {
            return _fields.ContainsKey(fieldId.ToString());
        }

        /// <summary>
        /// Gets the string value for the field.
        /// </summary>
        /// <param name="fieldId"></param>
        /// <returns></returns>

        public string GetStringValue(ID fieldId)
        {
            if (FieldExists(fieldId))
            {
                return _fields[fieldId.ToString()].GetFieldValue<string>();
            }

            return string.Empty;
        }

        /// <summary>
        /// Renders the field.
        /// </summary>
        /// <param name="fieldId"></param>
        /// <returns></returns>
        public HtmlString Field(ID fieldId)
        {
            if (FieldExists(fieldId))
            {
                var field = _fields[fieldId.ToString()];
                if (field != null)
                    return field.Field();
            }

            return new HtmlString(string.Empty);
        }

        /// <summary>
        /// Renders the image object google structure data content.
        /// In experience editor mode it does not render the google structured data but allows the editing of image if the field mapping allows.
        /// </summary>
        /// <param name="fieldId"></param>
        /// <returns></returns>
        public HtmlString RenderImageObject(ID fieldId)
        {
            if (FieldExists(fieldId))
            {
                var field = _fields[fieldId.ToString()];
                if (field.IsEditable() && Sitecore.Context.PageMode.IsExperienceEditor)
                {
                    return field.Field();
                }
                else
                {
                    var obj = _fields[fieldId.ToString()].GetFieldValue<object>();
                    if (obj != null)
                    {
                        return new ImageObject().Render(obj);
                    }
                }
            }
            return new HtmlString("");
        }

        /// <summary>
        /// Renders the publisher object google structure data content.
        /// In experience editor mode it does not render the google structured data but allows the editing of image if the field mapping allows.
        /// </summary>
        /// <param name="fieldId"></param>
        /// <returns></returns>
        public HtmlString RenderPublisherObject(ID fieldId)
        {
            if (FieldExists(fieldId))
            {
                var field = _fields[fieldId.ToString()];
                if (field.IsEditable() && Sitecore.Context.PageMode.IsExperienceEditor)
                {
                    return field.Field();
                }
                else
                {
                    var obj = _fields[fieldId.ToString()].GetFieldValue<object>();
                    if (obj != null)
                    {
                        return new OrganizationObject().Render(obj);
                    }
                }
            }

            return new HtmlString("");
        }

        /// <summary>
        /// Renders the date object google structure data content.
        /// </summary>
        /// <param name="fieldId"></param>
        /// <param name="IsEditable"></param>
        /// <returns></returns>
        public HtmlString DateObject(ID fieldId, bool IsEditable = false)
        {
            if (FieldExists(fieldId))
            {
                var field = _fields[fieldId.ToString()];
                if (IsEditable && field.IsEditable() && Sitecore.Context.PageMode.IsExperienceEditor)
                {
                    return field.Field();
                }
                else
                {
                    var obj = _fields[fieldId.ToString()].GetFieldValue<object>();
                    if (obj != null)
                    {
                        return new DateObject().Render(obj);
                    }
                }
            }
            return new HtmlString("");
        }
    }
}