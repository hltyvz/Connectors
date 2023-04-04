using Microsoft.Extensions.Configuration;
using Nest;

namespace BIK.ElasticsearchBase
{
	public class ElasticsearchService<T> : IElasticsearchService<T> where T : class
	{
		private string indexName;
		public ElasticsearchService(IConfiguration configuration, string index)
		{
			Configuration = configuration;
			_client = GetClient(index);
			indexName = index;
		}

		private IConfiguration Configuration { get; }

		private readonly IElasticClient _client;

		public async Task<bool> IndexDocument(T input)
		{
			try
			{
				var obj = await _client.IndexDocumentAsync(input);
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public async Task<bool> UpdateDocument(dynamic Id, T input)
		{
			try
			{
				var obj = await _client.UpdateAsync(Id, input);
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}			
		}

		public async Task<bool> DeleteDocument(QueryContainer queries)
		{
			return (await _client.DeleteByQueryAsync<T>(s => s
			.Query(q => queries))).IsValid; ;
		}

		public async Task<T?> GetDocument(QueryContainer queries)
		{
			var response = await _client.SearchAsync<T>(s => s
				.From(0)
				.Size(1)
				.Query(q => queries));
			return response.Documents.FirstOrDefault() ?? null;
		}

		public async Task<List<T>> GetDocuments(QueryContainer? queries = null, int? from = 0, int? size = 20)
		{
			ISearchResponse<T> response;
			var query = new QueryContainer();
			if (queries != null)
				response = await _client.SearchAsync<T>(s => s
					.From(from)
					.Size(size)
					.Query(q => queries));
			else
				response = await _client.SearchAsync<T>(s => s
				.From(from)
				.Size(size));

			return response.Documents.ToList();
		}

		public ElasticClient GetClient(string index)
		{
			var uri = Configuration["ElasticsearchServer:Uri"];
			var username = Configuration["ElasticsearchServer:Username"];
			var password = Configuration["ElasticsearchServer:Password"];

			var settings = new ConnectionSettings(new Uri(uri));
			if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
				settings.BasicAuthentication(username, password);
			settings.DefaultIndex(index.ToLower());
			return new ElasticClient(settings);
		}

		public async Task<bool> BulkIndexDocument(T[] input)
		{
			try
			{
				var result = _client.BulkAsync(b => b
				.Index("auditlog")
				.IndexMany(input));
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}
	}
}