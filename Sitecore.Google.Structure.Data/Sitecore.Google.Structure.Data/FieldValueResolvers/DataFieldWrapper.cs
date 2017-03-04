using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Feature.GoogleStructureData.DataFieldTypes;
using Sitecore.Feature.GoogleStructureData.Models;
using Sitecore.Mvc.Helpers;

namespace Sitecore.Feature.GoogleStructureData.FieldValueResolvers
{
    public class DataFieldWrapper
    {
        private BaseDataField _dataField;
        private ID _fieldId = ID.Null;
        private object _fieldValue;

        public DataFieldWrapper(BaseDataField dataField)
        {
            _dataField = dataField;
        }

        public ID GetFieldId(Item item)
        {
            if (!_dataField.IsEditable)
                return null;

            if (!_fieldId.IsNull)
            {
                return _fieldId;
            }

            var field = _dataField.Execute(item) as Field;
            if (field == null)
                return null;

            return field.ID;
        }

        public T GetFieldValue<T>(Item item)
        {
            var value = _dataField.Execute(item);

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