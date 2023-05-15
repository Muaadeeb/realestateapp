using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using RealEstateApp.Models;
using System.Net.Http.Headers;

namespace RealEstateApp.Services
{
    public static class ApiService
    {
        public static async Task<bool> RegisterUser(string name, string email, string password, string phone)
        {
            var register = new Register()
            {
                Name = name, Email = email, Password = password, Phone = phone
            };

            var httpClient = new HttpClient();
            JsonContent content = JsonContent.Create(register);
            HttpResponseMessage response = await httpClient.PostAsync($"{AppSettings.ApiUrl}api/users/register", content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public static async Task<bool> Login(string email, string password)
        {
            var login = new Login()
            {
                Email = email, Password = password
            };

            var httpClient = new HttpClient();
            JsonContent content = JsonContent.Create(login);
            HttpResponseMessage response = await httpClient.PostAsync($"{AppSettings.ApiUrl}api/users/login", content);

            if (response.IsSuccessStatusCode is false)
            {
                return false;
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var result = JsonSerializer.Deserialize<Token>(responseContent, options) ?? new Token();

            if (result.AccessToken.Any())
            {
                // Application Isolated Storage
                Preferences.Set("accesstoken", result.AccessToken);
                Preferences.Set("userid", result.UserId);
                return true;
            }
            
            return false;
        }

        public static async Task<List<Category>> GetCategories()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));
            HttpResponseMessage response = await httpClient.GetAsync($"{AppSettings.ApiUrl}api/categories");

            var responseContent = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            return JsonSerializer.Deserialize<List<Category>>(responseContent, options) ?? new List<Category>();
        }

        public static async Task<List<TrendingProperty>> GetTrendingProperties()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));
            HttpResponseMessage response = await httpClient.GetAsync($"{AppSettings.ApiUrl}api/Properties/TrendingProperties");

            var responseContent = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            return JsonSerializer.Deserialize<List<TrendingProperty>>(responseContent, options) ?? new List<TrendingProperty>();
        }

        public static async Task<List<SearchProperty>> FindProperties(string address )
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));
            HttpResponseMessage response = await httpClient.GetAsync($"{AppSettings.ApiUrl}api/Properties/SearchProperties?address={address}");

            var responseContent = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            return JsonSerializer.Deserialize<List<SearchProperty>>(responseContent, options) ?? new List<SearchProperty>();
        }

        public static async Task<List<PropertyByCategory>> GetPropertyByCategory(int categoryId)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));
            HttpResponseMessage response = await httpClient.GetAsync($"{AppSettings.ApiUrl}api/Properties/PropertyList?categoryId={categoryId}");

            var responseContent = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            return JsonSerializer.Deserialize<List<PropertyByCategory>>(responseContent, options) ?? new List<PropertyByCategory>();
        }

        public static async Task<PropertyDetail> GetPropertyDetail(int propertyId)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));
            HttpResponseMessage response = await httpClient.GetAsync($"{AppSettings.ApiUrl}api/Properties/PropertyDetail?id={propertyId}");

            var responseContent = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            return JsonSerializer.Deserialize<PropertyDetail>(responseContent, options) ?? new PropertyDetail();
        }

        public static async Task<List<BookmarkList>> GetBookmarkList()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));
            HttpResponseMessage response = await httpClient.GetAsync($"{AppSettings.ApiUrl}api/bookmarks");

            var responseContent = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            return JsonSerializer.Deserialize<List<BookmarkList>>(responseContent, options) ?? new List<BookmarkList>();
        }

        public static async Task<bool> AddBookmark(AddBookmark addBookmark)
        {
            var httpClient = new HttpClient();
            JsonContent content = JsonContent.Create(addBookmark);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));
            HttpResponseMessage response = await httpClient.PostAsync($"{AppSettings.ApiUrl}api/bookmarks", content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public static async Task<bool> DeleteBookmark(int propertyId)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));
            HttpResponseMessage response = await httpClient.DeleteAsync($"{AppSettings.ApiUrl}api/bookmarks/{propertyId}");

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }
    }
}
