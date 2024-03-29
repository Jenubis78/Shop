﻿using System.Net.Http.Json;
using Shop.Shared;

namespace Shop.Client.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _http;

        public ProductService(HttpClient http)
        {
            _http = http;
        }

        public event Action? ProductsChanged;
        public List<Product> Products { get; set; }
        public async Task GetProducts(string? categoryUrl)
        {
            
            
            var  result = categoryUrl == null ?
                await _http.GetFromJsonAsync<ServiceResponse<List<Product>>>("api/product") 
                : await _http.GetFromJsonAsync<ServiceResponse<List<Product>>>($"api/product/category/{categoryUrl}");
            
           
            if (result != null && result.Data != null) Products = result.Data;
            ProductsChanged.Invoke();
        }

        public async Task<ServiceResponse<Product>> GetProduct(int productId)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<Product>>($"api/product/{productId}");
            return result;
        }

       
    }
}
