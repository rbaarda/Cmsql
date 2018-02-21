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
            List<CqlQueryExecutionError> errors = new List<CqlQueryExecutionError>();

            List<PageData> result = new List<PageData>();
            foreach (CqlQuery query in queries)
            {
                ContentType contentType = _contentTypeRepository.Load(query.ContentType);
                if (contentType == null)
                {
                    errors.Add(new CqlQueryExecutionError($"Couldn't load content-type '{query.ContentType}'."));
                    return new CqlQueryExecutionResult(Enumerable.Empty<ICqlQueryResult>(), errors);
                }

                CqlExpressionParser expressionParser = new CqlExpressionParser();
                IEnumerable<PropertyCriteriaCollection> propertyCriteria =
                    expressionParser.Parse(contentType, query.Criteria);

                PageReference searchStartNodeRef = GetStartSearchFromNode(query.StartNode);

                foreach (PropertyCriteriaCollection propertyCriteriaCollection in propertyCriteria)
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

            IEnumerable<ICqlQueryResult> pageDataCqlQueryResults =
                result.Select(p => new PageDataCqlQueryResult(p)).ToList();

            return new CqlQueryExecutionResult(pageDataCqlQueryResults, errors);
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
    }
}
