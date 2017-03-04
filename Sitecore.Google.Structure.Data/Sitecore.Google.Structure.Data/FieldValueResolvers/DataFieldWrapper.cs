using System.Web;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Feature.GoogleStructureData.DataFieldTypes;
using Sitecore.Feature.GoogleStructureData.Models;
using Sitecore.Mvc.Helpers;

namespace Sitecore.Feature.GoogleStructureData.FieldValueResolvers
{
    /// <summary>
    /// This is a wrapper class to manage the operations with various types of fields.
    /// </summary>
    public class DataFieldWrapper
    {
        private BaseDataField _dataField;
        private ID _fieldId = ID.Null;
        private object _fieldValue;

        public DataFieldWrapper(BaseDataField dataField)
        {
            _dataField = dataField;
        }

        public HtmlString Field()
        {
            return _dataField.Render();
        }

        public bool IsEditable()
        {
            return _dataField.IsEditable;
        }

        public ID GetFieldId()
        {
            if (!_dataField.IsEditable)
                return ID.Null;

            if (!_fieldId.IsNull)
            {
                return _fieldId;
            }

            var field = _dataField.Execute() as Field;
            if (field == null)
                return ID.Null;

            return field.ID;
        }

        public T GetFieldValue<T>()
        {
            var value = _dataField.Execute();

            if (_dataField.IsEditable)
            {
                var field = value as Field;
                if (field != null)
                return (T)(field.Value as object);
            }

            return (T) value;
        }
    }
}