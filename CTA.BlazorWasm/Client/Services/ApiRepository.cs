using CTA.BlazorWasm.Client.ViewModels.Shared;
using CTA.BlazorWasm.Shared.Interfaces;
using CTA.BlazorWasm.Shared.Models;
using CTA.BlazorWasm.Shared.Responses;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;

namespace CTA.BlazorWasm.Client.Services
{
    public class ApiRepository<TEntity> : IAsyncRepository<TEntity> where TEntity : class
    {
        private readonly HttpClient http;
        private string controllerName;
        private string primaryKeyName;

        public ApiRepository(HttpClient _http, string _controllerName, string _primaryKeyName)
        {
            http = _http;
            controllerName = _controllerName;
            primaryKeyName = _primaryKeyName;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(PaginationFilter? paginationFilter = null)
        {
            try
            {
                var result = await http.GetAsync(controllerName);
                result.EnsureSuccessStatusCode();
                
                string responseBody = await result.Content.ReadAsStringAsync();

                if (paginationFilter == null)
                {
                    var response = JsonConvert.DeserializeObject<PagedResponse<TEntity>>(responseBody);
                    
                    if (paginationFilter == null)
                    {
                        if (response is not null && response.Data is not null && response.Success)
                            return response.Data.ToList();
                        else
                            return new List<TEntity>();
                    }
                }
                
                var skip = (paginationFilter.PageNumber -1 ) * paginationFilter.PageSize;

                var filtered = responseBody
                    .Take(paginationFilter.PageSize)
                    .Skip(skip)
                    .ToList();

                var pagedResponse = JsonConvert.DeserializeObject<PagedResponse<TEntity>>(responseBody);

                if (pagedResponse is not null && pagedResponse.Data is not null)
                    return pagedResponse.Data.ToList();
                else
                    return new List<TEntity>();

            }
            catch (Exception)
            {
                return new List<TEntity>();
            }
        }

        public async Task<TEntity> GetByIdAsync(object id)
        {
            try
            {
                var arg = WebUtility.HtmlEncode(id.ToString());
                var url = controllerName + "/" + arg;
                var result = await http.GetAsync(url);
                result.EnsureSuccessStatusCode();
                string responseBody = await result.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<Response<TEntity>>(responseBody);
                
                if (response is not null && response.Data is not null && response.Success)
                    return response.Data;
                else
                    return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            try
            {
                var result = await http.PostAsJsonAsync(controllerName, entity);
                result.EnsureSuccessStatusCode();
                string responseBody = await result.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<Response<TEntity>>(responseBody);

                if ( response is not null && response.Data is not null && response.Success)
                    return response.Data;
                else
                    return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<TEntity> UpdateAsync(TEntity entityToUpdate)
        {
            try
            {
                var result = await http.PutAsJsonAsync(controllerName, entityToUpdate);
                result.EnsureSuccessStatusCode();
                string responseBody = await result.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<Response<TEntity>>(responseBody);
                if ( response is not null && response.Data is not null && response.Success)
                    return response.Data;
                else
                    return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> DeleteAsync(TEntity entityToDelete)
        {
            try
            {
                    var value = entityToDelete.GetType()
                    .GetProperty(primaryKeyName)
                    .GetValue(entityToDelete, null)
                    .ToString();
                    var arg = WebUtility.HtmlEncode(value);
                    var url = controllerName + "/" + arg;
                    var result = await http.DeleteAsync(url);
                    result.EnsureSuccessStatusCode();
                    return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(object id)
        {
            try
            {
                var url = controllerName + "/" + WebUtility.HtmlEncode(id.ToString());
                var result = await http.DeleteAsync(url);
                result.EnsureSuccessStatusCode();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        public Task<IReadOnlyList<TEntity>> GetPagedReponseAsync(int page, int size)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<TEntity>> ListAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}