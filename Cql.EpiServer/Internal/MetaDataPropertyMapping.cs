using System.Collections.Generic;
using EPiServer.Core;
using EPiServer.DataAbstraction;

namespace Cql.EpiServer.Internal
{
    internal class MetaDataPropertyTypeMapping
    {
        public static readonly IDictionary<string, PropertyDataType>
            Mappings = new Dictionary<string, PropertyDataType>
            {
                {MetaDataProperties.PageLink, PropertyDataType.PageReference},
                {MetaDataProperties.PageTypeID, PropertyDataType.PageType},
                {MetaDataProperties.PageParentLink, PropertyDataType.PageReference},
                {MetaDataProperties.PagePendingPublish, PropertyDataType.Boolean},
                {MetaDataProperties.PageWorkStatus, PropertyDataType.Number},
                {MetaDataProperties.PageDeleted, PropertyDataType.Boolean},
                {MetaDataProperties.PageSaved, PropertyDataType.Boolean},
                {MetaDataProperties.PageTypeName, PropertyDataType.String},
                {MetaDataProperties.PageChanged, PropertyDataType.Date},
                {MetaDataProperties.PageCreatedBy, PropertyDataType.String},
                {MetaDataProperties.PageChangedBy, PropertyDataType.String},
                {MetaDataProperties.PageDeletedBy, PropertyDataType.String},
                {MetaDataProperties.PageDeletedDate, PropertyDataType.Date},
                {MetaDataProperties.PageCreated, PropertyDataType.Date},
                {MetaDataProperties.PageMasterLanguageBranch, PropertyDataType.String},
                {MetaDataProperties.PageLanguageBranch, PropertyDataType.String},
                {MetaDataProperties.PageGUID, PropertyDataType.String},
                {MetaDataProperties.PageContentAssetsID, PropertyDataType.String},
                {MetaDataProperties.PageContentOwnerID, PropertyDataType.String},
                //{MetaDataProperties.PageFolderID, PropertyDataType.Number},
                {MetaDataProperties.PageVisibleInMenu, PropertyDataType.Boolean},
                {MetaDataProperties.PageURLSegment, PropertyDataType.String},
                {MetaDataProperties.PagePeerOrder, PropertyDataType.Number},
                {MetaDataProperties.PageExternalURL, PropertyDataType.String},
                {MetaDataProperties.PageChangedOnPublish, PropertyDataType.Boolean},
                {MetaDataProperties.PageCategory, PropertyDataType.Category},
                {MetaDataProperties.PageStartPublish, PropertyDataType.Date},
                {MetaDataProperties.PageStopPublish, PropertyDataType.Date},
                {MetaDataProperties.PageCreated, PropertyDataType.Date},
                {MetaDataProperties.PageArchiveLink, PropertyDataType.PageReference},
                {MetaDataProperties.PageShortcutType, PropertyDataType.Number},
                {MetaDataProperties.PageShortcutLink, PropertyDataType.PageReference},
                {MetaDataProperties.PageTargetFrame, PropertyDataType.Number},
                {MetaDataProperties.PageLinkURL, PropertyDataType.String},
                {MetaDataProperties.PageName, PropertyDataType.String}
            };
        }
}
