using Sitecore.Data;

namespace Sitecore.Feature.GoogleStructureData
{
    public static class Constants
    {
        public static class Fields
        {
            
        }

        public static class Templates
        {
            
        }

        public static class Items
        {
            public static readonly ID ArticleDataTypeID = new ID("{FBDE3F28-6E51-4463-9597-71659833711F}");

            public static class ArticleFieldItems
            {
                public static readonly ID HeadlineFieldID = new ID("{07EA7C29-1CE3-4304-8AEC-9C5E851571D1}");

                public static readonly ID MainEntryofPageFieldID = new ID("{2900D215-9EE3-4CC7-B7E6-B7D78217EB9A}");

                public static readonly ID DescriptionFieldID = new ID("{3034BD4C-139E-42E7-9565-3EE279DBAE92}");

                public static readonly ID AuthorFieldID = new ID("{53D5461F-F9BE-4608-98CE-167D7850CDC0}");

                public static readonly ID DateModifiedFieldID = new ID("{5702AED2-4569-4007-AB7B-B840699A5426}");

                public static readonly ID DatePublishedFieldID = new ID("{09614B5F-689B-437B-86D5-E33EB200D0BD}");

                public static readonly ID ImageFieldID = new ID("{BA93188F-7F77-4C5A-916E-DB04841B72A9}");

                public static readonly ID PublisherFieldID = new ID("{EDDD992B-3F15-477C-8359-6FE8BCDEC6CA}");
            }
        }

        public static class RenderingTypeSitecore
        {
            public static readonly ID ArticleTypeRenderingId = new ID("{79C08F7A-78C5-42BB-8C0B-11EE56302C5E}");
            public static readonly ID ArticleMappingId = new ID("{FBDE3F28-6E51-4463-9597-71659833711F}");
        }

        public static class DataFieldItemName
        {
            public static string ItemFieldName = "Item Field Name";
            public static string FiledTypeName = "Type Name";
            public static string OverrideTemplate = "Templates";
            public static string ReferenceItemTemplate = "Reference Item Template";
            public static string ReferenceItemFieldName = "Reference Item Field Name";
        }

        public static class GstructuredTemplate
        {
            public const string ComputedValueDataField = "{9ED0BB09-419A-4D0D-8875-315283BF5CE0}";
            public const string ItemFieldNameDataField = "{6DAEA56C-DB28-4FC1-A507-38F0E8FA1840}";
            public const string ReferenceItemFieldNameDataField = "{7C29E331-7259-4620-BB90-2537F50313AE}";
            public const string OverrideComputedValueDataField = "{2D3B4F77-5BAE-4D35-9D77-12D9EF132E61}";
            public const string OverrideItemFieldNameDataField = "{5CCDEBF2-7069-4FFF-8E01-E1F3EC5E5947}";
            public const string OverrideReferenceItemFieldNameDataField = "{B6A4CEDF-8565-410B-8C8B-34DB3267E924}";
        }
    }
}