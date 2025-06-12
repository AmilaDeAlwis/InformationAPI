using InformationAPI.interfaces;
using InformationAPI.Misc;
using InformationAPI.Models;
using Microsoft.Extensions.Hosting;

namespace InformationAPI.Impl
{
    public class ManageService : IManageService
    {
        private readonly IManageConnection _manageConnection;
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public ManageService(IManageConnection manageConnection, HttpClient httpClient, IConfiguration configuration)
        {
            _manageConnection = manageConnection;
            _httpClient = new HttpClient();
            _apiBaseUrl = configuration["ExternalApi:BaseUrl"]
                      ?? throw new ArgumentNullException("ExternalApi:BaseUrl not configured");
        }

        public async Task<InformationModel?> GetInformationByIdAsync(int id)
        {
            try
            {
                var post = _manageConnection.GetById(id);
                if (post != null) return post;

                var fetchedPost = await _httpClient.GetFromJsonAsync<InformationModel>($"{_apiBaseUrl}/{id}");
                if (fetchedPost == null) return null;

                _manageConnection.Insert(fetchedPost);
                return fetchedPost;
            }
            catch (DataAccessException ex)
            {
                throw new ServiceException("An error occurred accessing the database.", ex);
            }
            catch (Exception ex)
            {
                throw new ServiceException("An unexpected error occurred while getting post by id.", ex);
            }
        }

        public async Task<(List<InformationModel> Posts, int TotalCount)> GetAllInformationAsync(int page, int pageSize)
        {
            try
            {
                var posts = _manageConnection.GetAll();
                if (posts.Count == 0)
                {
                    var fetchedPosts = await _httpClient.GetFromJsonAsync<List<InformationModel>>($"{_apiBaseUrl}");
                    if (fetchedPosts != null)
                    {
                        foreach (var p in fetchedPosts)
                            _manageConnection.Insert(p);
                        posts = fetchedPosts;
                    }
                }

                int total = posts.Count;
                var paged = posts
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                return (paged, total);
            }
            catch (DataAccessException ex)
            {
                throw new ServiceException("An error occurred accessing the database.", ex);
            }
            catch (Exception ex)
            {
                throw new ServiceException("An unexpected error occurred while getting all posts.", ex);
            }
        }
    }
}
