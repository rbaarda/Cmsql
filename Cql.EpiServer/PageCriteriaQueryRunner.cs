using System.Collections.Generic;
using System.Linq;
using Cql.EpiServer.Internal;
using Cql.Query;
using Cql.Query.Execution;
using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;

namespace Cql.EpiServer
{
    public class PageCriteriaQueryRunner : ICqlQueryRunner
    {
        private readonly IPageCriteriaQueryService _pageCriteriaQueryService;
        private readonly IContentTypeRepository _contentTypeRepository;

        public PageCriteriaQueryRunner(
            IPageCriteriaQueryService pageCriteriaQueryService,
            IContentTypeRepository contentTypeRepository)
        {
            _pageCriteriaQueryService = pageCriteriaQueryService;
            _contentTypeRepository = contentTypeRepository;
        }

        public CqlQueryExecutionResult ExecuteQueries(IEnumerable<CqlQuery> queries)
        {
            List<PageData> result = new List<PageData>();
            foreach (CqlQuery query in queries)
            {
                ContentType contentType = _contentTypeRepository.Load(query.ContentType);

                Stack<PropertyCriteriaCollection> propertyCriteriaCollectionStack = new Stack<PropertyCriteriaCollection>();
                propertyCriteriaCollectionStack.Push(new PropertyCriteriaCollection());

                if (query.Criteria != null)
                {
                    ExpressionVisitor visitor = new ExpressionVisitor(contentType, propertyCriteriaCollectionStack);
                    query.Criteria.Accept(visitor);
                }

                PageReference searchStartNodeRef = GetStartSearchFromNode(query.StartNode);

                foreach (PropertyCriteriaCollection propertyCriteriaCollection in propertyCriteriaCollectionStack)
                {
                    PageDataCollection foundPages = _pageCriteriaQueryService.FindPagesWithCriteria(
                        searchStartNodeRef,
                        propertyCriteriaCollection);
                    if (foundPages != null && foundPages.Any())
                    {
                        result.AddRange(foundPages);
                    }
                }
            }

            return new CqlQueryExecutionResult
            {
                QueryResults = result.Select(MapPageDataToCqlQueryResult)
            };
        }

        private PageReference GetStartSearchFromNode(CqlQueryStartNode startNode)
        {
            switch (startNode.StartNodeType)
            {
                case CqlQueryStartNodeType.Start:
                    return ContentReference.StartPage;
                case CqlQueryStartNodeType.Root:
                    return ContentReference.RootPage;
                case CqlQueryStartNodeType.Id:
                    if (int.TryParse(startNode.StartNodeId, out int rootNodeId))
                    {
                        return new PageReference(rootNodeId);
                    }
                    break;
            }
            return PageReference.EmptyReference;
        }

        private CqlQueryResult MapPageDataToCqlQueryResult(PageData page)
        {
            return new CqlQueryResult
            {
                Name = page.Name
            };
        }
    }
}
