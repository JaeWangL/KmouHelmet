﻿using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using KmouHelmet.Mobile.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Xamarin.Essentials;

namespace KmouHelmet.Mobile.Services.Request
{
    public class RequestService : IRequestService
    {
        readonly JsonSerializerSettings _serializerSettings;

        public RequestService()
        {
            _serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                NullValueHandling = NullValueHandling.Ignore
            };

            _serializerSettings.Converters.Add(new StringEnumConverter());
        }

        public async Task<TResult> GetAsync<TResult>(string uri, string token = "")
        {
            HttpClient httpClient = CreateHttpClient(token);
            HttpResponseMessage response = await httpClient.GetAsync(uri);

            await HandleResponse(response);

            var serialized = await response.Content.ReadAsStringAsync();
            TResult result = await Task.Run(() =>
                JsonConvert.DeserializeObject<TResult>(serialized, _serializerSettings));

            return result;
        }

        public Task<TResult> PostAsync<TResult>(string uri, TResult data, string token = "") => PostAsync<TResult, TResult>(uri, data, token);

        public async Task<TResult> PostAsync<TRequest, TResult>(string uri, TRequest data, string token = "")
        {
            HttpClient httpClient = CreateHttpClient(token);
            var serialized = await Task.Run(() => JsonConvert.SerializeObject(data, _serializerSettings));
            HttpResponseMessage response = await httpClient.PostAsync(
                uri, new StringContent(serialized, Encoding.UTF8, "application/json"));

            await HandleResponse(response);

            var responseData = await response.Content.ReadAsStringAsync();

            return await Task.Run(() => JsonConvert.DeserializeObject<TResult>(responseData, _serializerSettings));
        }

        public Task<TResult> PutAsync<TResult>(string uri, TResult data, string token = "") => PutAsync<TResult, TResult>(uri, data, token);

        public async Task<TResult> PutAsync<TRequest, TResult>(string uri, TRequest data, string token = "")
        {
            HttpClient httpClient = CreateHttpClient(token);
            var serialized = await Task.Run(() => JsonConvert.SerializeObject(data, _serializerSettings));
            HttpResponseMessage response = await httpClient.PutAsync(
                uri, new StringContent(serialized, Encoding.UTF8, "application/json"));

            await HandleResponse(response);

            var responseData = await response.Content.ReadAsStringAsync();

            return await Task.Run(() => JsonConvert.DeserializeObject<TResult>(responseData, _serializerSettings));
        }

        HttpClient CreateHttpClient(string token = "")
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                throw new ConnectivityException();
            }

            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (!string.IsNullOrEmpty(token))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return httpClient;
        }

        async Task HandleResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.Forbidden || response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new Exception(content);
                }

                throw new HttpRequestException(content);
            }
        }
    }
}
