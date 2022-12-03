using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AspnetRunBasics.Models;

namespace AspnetRunBasics.Services
{
    public interface ICatalogService
    {
        Task<IEnumerable<CatalogModel>> GetCatalog();
        Task<IEnumerable<CatalogModel>> GetCatalogByCategory(string category);
        Task<CatalogModel> GetCatalog(string id);
        Task<CatalogModel> CreateCatalog(CatalogModel model);
    }

    public class CatalogService : ICatalogService
    {
        private readonly HttpClient httpClient;

        public CatalogService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public Task<CatalogModel> CreateCatalog(CatalogModel model)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CatalogModel>> GetCatalog()
        {
            throw new NotImplementedException();
        }

        public Task<CatalogModel> GetCatalog(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CatalogModel>> GetCatalogByCategory(string category)
        {
            throw new NotImplementedException();
        }
    }
}

