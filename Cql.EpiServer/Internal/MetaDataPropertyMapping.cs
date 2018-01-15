using System.Collections.Generic;
using EPiServer.Core;

namespace Cql.EpiServer.Internal
{
    internal class MetaDataPropertyTypeMapping
    {
        public static readonly IDictionary<string, PropertyDataType>
            Mappings = new Dictionary<string, PropertyDataType>
            {
                {"PageLink", PropertyDataType.PageReference},
                {"PageTypeID", PropertyDataType.PageType},
                {"PageParentLink", PropertyDataType.PageReference},
                {"PagePendingPublish", PropertyDataType.Boolean},
                {"PageWorkStatus", PropertyDataType.Number},
                {"PageDeleted", PropertyDataType.Boolean},
                {"PageSaved", PropertyDataType.Boolean},
                {"PageTypeName", PropertyDataType.String},
                {"PageChanged", PropertyDataType.Date},
                {"PageCreatedBy", PropertyDataType.String},
                {"PageChangedBy", PropertyDataType.String},
                {"PageMasterLanguageBranch", PropertyDataType.String},
                {"PageLanguageBranch", PropertyDataType.String},
                {"PageGUID", PropertyDataType.String},
                {"PageContentAssetsID", PropertyDataType.String},
                {"PageContentOwnerID", PropertyDataType.String},
                {"PageFolderID", PropertyDataType.Number},
                {"PageVisibleInMenu", PropertyDataType.Boolean},
                {"PageURLSegment", PropertyDataType.String},
                {"PagePeerOrder", PropertyDataType.Number},
                {"PageExternalURL", PropertyDataType.String},
                {"PageChangedOnPublish", PropertyDataType.Boolean},
                {"PageCategory", PropertyDataType.Category},
                {"PageStartPublish", PropertyDataType.Date},
                {"PageStopPublish", PropertyDataType.Date},
                {"PageCreated", PropertyDataType.Date},
                {"PageArchiveLink", PropertyDataType.PageReference},
                {"PageShortcutType", PropertyDataType.Number},
                {"PageShortcutLink", PropertyDataType.PageReference},
                {"PageTargetFrame", PropertyDataType.Number},
                {"PageLinkURL", PropertyDataType.String}
            };
        }
}
