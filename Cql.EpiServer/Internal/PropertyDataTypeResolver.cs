using System;
using System.Linq;
using EPiServer.Core;
using EPiServer.DataAbstraction;

namespace Cql.EpiServer.Internal
{
    internal class PropertyDataTypeResolver
    {
        private readonly ContentType _contentType;

        internal PropertyDataTypeResolver(ContentType contentType)
        {
            _contentType = contentType;
        }

        internal bool TryResolve(string propertyIdentifier, out PropertyDataType propertyDataType)
        {
            if (string.IsNullOrWhiteSpace(propertyIdentifier))
            {
                throw new ArgumentException($"Parameter '{nameof(propertyIdentifier)}' is null or whitespace.");
            }

            propertyDataType = PropertyDataType.String;
            if (MetaDataPropertyTypeMapping.Mappings.ContainsKey(propertyIdentifier))
            {
                propertyDataType = MetaDataPropertyTypeMapping.Mappings[propertyIdentifier];
                return true;
            }

            PropertyDefinition propDef = _contentType.PropertyDefinitions
                .FirstOrDefault(prop =>
                    prop.Name.Equals(propertyIdentifier, StringComparison.InvariantCultureIgnoreCase));
            if (propDef != null)
            {
                propertyDataType = propDef.Type.DataType;
                return true;
            }

            return false;
        }
    }
}
