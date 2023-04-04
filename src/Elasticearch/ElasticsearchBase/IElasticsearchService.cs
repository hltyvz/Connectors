using Nest;

namespace BIK.ElasticsearchBase
{
	public interface IElasticsearchService<T> where T : class
	{
		ElasticClient GetClient(string index);
		Task<bool> IndexDocument(T input);
		Task<bool> BulkIndexDocument(T[] input);
		Task<bool> UpdateDocument(dynamic Id, T input);
		Task<bool> DeleteDocument(QueryContainer queries);
		Task<T?> GetDocument(QueryContainer? queries);
		Task<List<T>> GetDocuments(QueryContainer? queries, int? from, int? size);
	}
}
