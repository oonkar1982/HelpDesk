using System;
using System.Collections.Generic;

namespace HelpDesk.Common.Entities
{
    public partial class StringMap
    {
        public Guid StringMapId { get; set; }
        public int ObjectTypeCode { get; set; }
        public string AttributeName { get; set; } = null!;
        public int AttributeValue { get; set; }
        public string? DisplayValue { get; set; }
        public int? DisplayOrder { get; set; }
    }
}
