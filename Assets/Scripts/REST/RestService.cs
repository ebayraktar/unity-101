using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace REST
{
    public class RestService : IRest
    {
        private readonly HttpClient _client;

        public RestService()
        {
            _client = new HttpClient();
        }

        public async Task<ResponseModel> AgeGroups(string id = "")
        {
            var uri = new Uri(Constants.AgeGroupsURL + id);
            return await BaseGetRequest(uri);
        }

        public async Task<ResponseModel> AnswerTypes(string id = "")
        {
            var uri = new Uri(Constants.AnswerTypesURL + id);
            return await BaseGetRequest(uri);
        }

        public async Task<ResponseModel> Categories(string id = "")
        {
            var uri = new Uri(Constants.CategoriesURL + id);
            return await BaseGetRequest(uri);
        }

        public async Task<ResponseModel> Answers(string id)
        {
            var uri = new Uri(Constants.AnswersURL + id);
            return await BaseGetRequest(uri);
        }

        public async Task<ResponseModel> PostAnswer(string json)
        {
            var uri = new Uri(Constants.AnswersURL);
            return await BasePostRequest(uri, json);
        }

        public async Task<ResponseModel> QuestionCategories(string id)
        {
            var uri = new Uri(Constants.QuestionCategoriesURL + id);
            return await BaseGetRequest(uri);
        }

        public async Task<ResponseModel> Questions(string id = "")
        {
            var uri = new Uri(Constants.QuestionsURL + id);
            return await BaseGetRequest(uri);
        }

        public async Task<ResponseModel> QuestionTags(string id)
        {
            var uri = new Uri(Constants.QuestionTagsURL + id);
            return await BaseGetRequest(uri);
        }

        public async Task<ResponseModel> QuestionTypes(string id = "")
        {
            var uri = new Uri(Constants.QuestionTypesURL + id);
            return await BaseGetRequest(uri);
        }

        public async Task<ResponseModel> Tags(string id = "")
        {
            var uri = new Uri(Constants.TagsURL + id);
            return await BaseGetRequest(uri);
        }

        #region BaseGroup

        private async Task<ResponseModel> BaseGetRequest(Uri uri)
        {
            try
            {
                if (!string.IsNullOrEmpty(Constants.Token))
                {
                    _client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", Constants.Token);
                }

                var response = await _client.GetAsync(uri);
                return await BaseResponse(response);
            }
            catch (Exception ex)
            {
                return new ResponseModel {Message = "Error", OpCode = -3, Result = ex.Message};
            }
        }

        private async Task<ResponseModel> BasePostRequest(Uri uri, string json)
        {
            try
            {
                if (!string.IsNullOrEmpty(Constants.Token))
                {
                    _client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", Constants.Token);
                }

                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _client.PostAsync(uri, content);
                return await BaseResponse(response);
            }
            catch (Exception ex)
            {
                return new ResponseModel {Message = "Error", OpCode = -3, Result = ex.Message};
            }
        }

        private async Task<ResponseModel> BasePutRequest(Uri uri, string json)
        {
            try
            {
                if (!string.IsNullOrEmpty(Constants.Token))
                {
                    _client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", Constants.Token);
                }

                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PutAsync(uri, content);
                return await BaseResponse(response);
            }
            catch (Exception ex)
            {
                return new ResponseModel {Message = "Error", OpCode = -3, Result = ex.Message};
            }
        }

        private static async Task<ResponseModel> BaseResponse(HttpResponseMessage response)
        {
            try
            {
                if (!response.IsSuccessStatusCode)
                    return new ResponseModel {Message = "Fail", OpCode = -2, Result = response.StatusCode};
                var readAs = await response.Content.ReadAsStringAsync();
                var mobileResult = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseModel>(readAs);
                return mobileResult;
            }
            catch (Exception ex)
            {
                return new ResponseModel {Message = "Error", OpCode = -3, Result = ex.Message};
            }
        }

        #endregion
    }
}