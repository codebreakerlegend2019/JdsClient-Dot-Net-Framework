using JdsClient.Models;
using Newtonsoft.Json;
using Polly;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace JdsClient
{
    public static class JdsClientRequestor 
    {
        public static JdsClientConfiguration Configuration;

        public static async Task<JdsMultiReponse<T>> GetManyAsync<T>(string apiBaseLink, string token) where T : new()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var link = $"{Configuration.BaseUrl}{apiBaseLink}";
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                    var response = await Policy.HandleResult<HttpResponseMessage>(message => !message.IsSuccessStatusCode)
                        .WaitAndRetryAsync(Configuration.RequestRetryCount, i => Configuration.RequestRetryInterval, (request, timeSpan, retryCount, context) =>
                        {
                            Debug.WriteLine($"Error With a {request.Result.StatusCode} Waiting {timeSpan} Retry Attempt({retryCount})");
                        })
                        .ExecuteAsync(async () => await client.GetAsync(link));
                    var requestContent = await response.Content.ReadAsStringAsync();
                    if (!response.IsSuccessStatusCode) return new JdsMultiReponse<T>()
                    {
                        StatusCode = response.StatusCode,
                        RequestError = requestContent,
                    };
                    var requestResult = (List<T>)JsonConvert.DeserializeObject(requestContent, typeof(List<T>));
                    return new JdsMultiReponse<T>()
                    {
                        Data = requestResult,
                        StatusCode = response.StatusCode,
                        RequestError = requestContent,
                    };
                }
                
            }
            catch (Exception ex)
            {

            }
            return new JdsMultiReponse<T>()
            {
                StatusCode = HttpStatusCode.BadGateway,
                RequestError = "UnHandledException"
            };
        }
        public static async Task<JdsMultiReponse<T>> GetManyAsync<T>(string apiBaseLink) where T : new()
        {
            try
            {
               
                using (var client = new HttpClient())
                {
                    var link = $"{Configuration.BaseUrl}{apiBaseLink}";
                    var response = await Policy.HandleResult<HttpResponseMessage>(message => !message.IsSuccessStatusCode)
                        .WaitAndRetryAsync(Configuration.RequestRetryCount, i => Configuration.RequestRetryInterval, (request, timeSpan, retryCount, context) =>
                        {
                            Debug.WriteLine($"Error With a {request.Result.StatusCode} Waiting {timeSpan} Retry Attempt({retryCount})");
                        })
                        .ExecuteAsync(async () => await client.GetAsync(link));
                    var requestContent = await response.Content.ReadAsStringAsync();
                    if (!response.IsSuccessStatusCode) return new JdsMultiReponse<T>()
                    {
                        StatusCode = response.StatusCode,
                        RequestError = requestContent,
                    };
                    var requestResult = (List<T>)JsonConvert.DeserializeObject(requestContent, typeof(List<T>));
                    return new JdsMultiReponse<T>()
                    {
                        Data = requestResult,
                        StatusCode = response.StatusCode,
                        RequestError = requestContent,
                    };
                }
               
            }
            catch (Exception ex)
            {
            }
            return new JdsMultiReponse<T>()
            {
                StatusCode = HttpStatusCode.BadGateway,
                RequestError = "UnHandledException"
            };
        }
        public static async Task<JdsSingleResponse<T>> GetAsync<T>(string apiEndpoint, string token) where T : new()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var link = $"{Configuration.BaseUrl}{apiEndpoint}";
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                    var response = await Policy.HandleResult<HttpResponseMessage>(message => !message.IsSuccessStatusCode)
                        .WaitAndRetryAsync(Configuration.RequestRetryCount, i => Configuration.RequestRetryInterval, (request, timeSpan, retryCount, context) =>
                        {
                            Debug.WriteLine($"Error With a {request.Result.StatusCode} Waiting {timeSpan} Retry Attempt({retryCount})");
                        })
                        .ExecuteAsync(async () => await client.GetAsync(link));
                    var requestContent = await response.Content.ReadAsStringAsync();
                    if (!response.IsSuccessStatusCode) return new JdsSingleResponse<T>()
                    {
                        StatusCode = response.StatusCode,
                        RequestContent = requestContent,
                    };
                    var requestResult = (T)JsonConvert.DeserializeObject(requestContent, typeof(T));
                    return new JdsSingleResponse<T>()
                    {
                        StatusCode = response.StatusCode,
                        Data = requestResult,
                        RequestContent = requestContent
                    };
                }
                
            }
            catch
            {
            }
            return new JdsSingleResponse<T>()
            {
                StatusCode = HttpStatusCode.BadGateway,
                RequestContent = "UnHandledException"
            };
        }
        public static async Task<JdsSingleResponse<T>> GetAsync<T>(string apiEndpoint) where T : new()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var link = $"{Configuration.BaseUrl}{apiEndpoint}";
                    var response = await Policy.HandleResult<HttpResponseMessage>(message => !message.IsSuccessStatusCode)
                        .WaitAndRetryAsync(Configuration.RequestRetryCount, i => Configuration.RequestRetryInterval, (request, timeSpan, retryCount, context) =>
                        {
                            Debug.WriteLine($"Error With a {request.Result.StatusCode} Waiting {timeSpan} Retry Attempt({retryCount})");
                        })
                        .ExecuteAsync(async () => await client.GetAsync(link));
                    var requestContent = await response.Content.ReadAsStringAsync();
                    if (!response.IsSuccessStatusCode) return new JdsSingleResponse<T>()
                    {
                        StatusCode = response.StatusCode,
                        RequestContent = requestContent,
                    };
                    var requestResult = (T)JsonConvert.DeserializeObject(requestContent, typeof(T));
                    return new JdsSingleResponse<T>()
                    {
                        StatusCode = response.StatusCode,
                        Data = requestResult,
                        RequestContent = requestContent
                    };
                }
               
            }
            catch
            {
            }
            return new JdsSingleResponse<T>()
            {
                StatusCode = HttpStatusCode.BadGateway,
                RequestContent = "UnHandledException"
            };
        }
        public static async Task<JdsSingleResponse<TResult>> PostWithSingleReturnAsync<TPram, TResult>(string apiEndPoint, TPram model) where TResult : new()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var json = JsonConvert.SerializeObject(model);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await Policy.HandleResult<HttpResponseMessage>(message => !message.IsSuccessStatusCode)
                        .WaitAndRetryAsync(Configuration.RequestRetryCount, i => Configuration.RequestRetryInterval, (request, timeSpan, retryCount, context) =>
                        {
                            Debug.WriteLine($"Error With a {request.Result.StatusCode} Waiting {timeSpan} Retry Attempt({retryCount})");
                        }).ExecuteAsync(async ()=> await client.PostAsync($"{Configuration.BaseUrl}{apiEndPint}", content));
                        
                    var requestContent = await response.Content.ReadAsStringAsync();
                    if (!response.IsSuccessStatusCode)
                        return new JdsSingleResponse<TResult>()
                        {
                            StatusCode = response.StatusCode,
                            RequestContent = requestContent,
                        };
                    var convertToModel = (TResult)JsonConvert.DeserializeObject(requestContent, typeof(TResult));
                    return new JdsSingleResponse<TResult>()
                    {
                        StatusCode = response.StatusCode,
                        Data = convertToModel,
                        RequestContent = requestContent
                    };
                }
              
            }
            catch (Exception exception)
            {

                throw new Exception(exception.ToString());
            }
        }

        public static async Task<JdsSingleResponse<TResult>> PostWithSingleReturnAsync<TPram,TResult>(string apiEndPoint, TPram model, string token) where TResult : new()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                    var json = JsonConvert.SerializeObject(model);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await Policy.HandleResult<HttpResponseMessage>(message => !message.IsSuccessStatusCode)
                     .WaitAndRetryAsync(Configuration.RequestRetryCount, i => Configuration.RequestRetryInterval, (request, timeSpan, retryCount, context) =>
                     {
                         Debug.WriteLine($"Error With a {request.Result.StatusCode} Waiting {timeSpan} Retry Attempt({retryCount})");
                     }).ExecuteAsync(async () => await client.PostAsync($"{Configuration.BaseUrl}{apiEndPoint}", content));
                    var requestContent = await response.Content.ReadAsStringAsync();
                    if (!response.IsSuccessStatusCode)
                        return new JdsSingleResponse<TResult>()
                        {
                            StatusCode = response.StatusCode,
                            RequestContent = requestContent,
                        };
                    var convertToModel = (TResult)JsonConvert.DeserializeObject(requestContent, typeof(TResult));
                    return new JdsSingleResponse<TResult>()
                    {
                        StatusCode = response.StatusCode,
                        Data = convertToModel,
                        RequestContent = requestContent
                    };
                }
              
            }
            catch (Exception exception)
            {

                throw new Exception(exception.ToString());
            }
        }

        public static async Task<JdsResponse> PutAsync<T>(string apiEndPoint, T model, string token) where T : new()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                    var json = JsonConvert.SerializeObject(model);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await Policy.HandleResult<HttpResponseMessage>(message => !message.IsSuccessStatusCode)
            .WaitAndRetryAsync(Configuration.RequestRetryCount, i => Configuration.RequestRetryInterval, (request, timeSpan, retryCount, context) =>
            {
                Debug.WriteLine($"Error With a {request.Result.StatusCode} Waiting {timeSpan} Retry Attempt({retryCount})");
            }).ExecuteAsync(async () => await client.PutAsync($"{Configuration.BaseUrl}{apiEndPoint}", content));
                    return new JdsResponse()
                    {
                        RequestContent = await response.Content.ReadAsStringAsync(),
                        StatusCode = response.StatusCode
                    };
                }

            }
            catch (Exception exception)
            {

                throw new Exception(exception.ToString());
            }
        }
        public static async Task<JdsResponse> PutAsync<T>(string apiEndPoint, List<T> model, string token) where T : new()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                    var json = JsonConvert.SerializeObject(model);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await Policy.HandleResult<HttpResponseMessage>(message => !message.IsSuccessStatusCode)
            .WaitAndRetryAsync(Configuration.RequestRetryCount, i => Configuration.RequestRetryInterval, (request, timeSpan, retryCount, context) =>
            {
                Debug.WriteLine($"Error With a {request.Result.StatusCode} Waiting {timeSpan} Retry Attempt({retryCount})");
            }).ExecuteAsync(async () => await client.PutAsync($"{Configuration.BaseUrl}{apiEndPoint}", content));
                    return new JdsResponse()
                    {
                        RequestContent = await response.Content.ReadAsStringAsync(),
                        StatusCode = response.StatusCode
                    };
                }

            }
            catch (Exception exception)
            {

                throw new Exception(exception.ToString());
            }
        }

        public static async Task<JdsResponse> PutAsync<T>(string apiEndPoint, T model) where T : new()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var json = JsonConvert.SerializeObject(model);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await Policy.HandleResult<HttpResponseMessage>(message => !message.IsSuccessStatusCode)
            .WaitAndRetryAsync(Configuration.RequestRetryCount, i => Configuration.RequestRetryInterval, (request, timeSpan, retryCount, context) =>
            {
                Debug.WriteLine($"Error With a {request.Result.StatusCode} Waiting {timeSpan} Retry Attempt({retryCount})");
            }).ExecuteAsync(async () => await client.PutAsync($"{Configuration.BaseUrl}{apiEndPoint}", content));
                  
                        return new JdsResponse()
                        {
                            RequestContent = await response.Content.ReadAsStringAsync(),
                            StatusCode = response.StatusCode
                        };
                }
                

            }
            catch (Exception exception)
            {

                throw new Exception(exception.ToString());
            }
        }

        public static async Task<JdsResponse> PutAsync<T>(string apiEndPoint, List<T> model) where T : new()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var json = JsonConvert.SerializeObject(model);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await Policy.HandleResult<HttpResponseMessage>(message => !message.IsSuccessStatusCode)
            .WaitAndRetryAsync(Configuration.RequestRetryCount, i => Configuration.RequestRetryInterval, (request, timeSpan, retryCount, context) =>
            {
                Debug.WriteLine($"Error With a {request.Result.StatusCode} Waiting {timeSpan} Retry Attempt({retryCount})");
            }).ExecuteAsync(async () => await client.PutAsync($"{Configuration.BaseUrl}{apiEndPoint}", content));

                    return new JdsResponse()
                    {
                        RequestContent = await response.Content.ReadAsStringAsync(),
                        StatusCode = response.StatusCode
                    };
                }


            }
            catch (Exception exception)
            {

                throw new Exception(exception.ToString());
            }
        }

        public static async Task<JdsResponse> PutAsync(string apiEndPoint,string token)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                    var response = await Policy.HandleResult<HttpResponseMessage>(message => !message.IsSuccessStatusCode)
            .WaitAndRetryAsync(Configuration.RequestRetryCount, i => Configuration.RequestRetryInterval, (request, timeSpan, retryCount, context) =>
            {
                Debug.WriteLine($"Error With a {request.Result.StatusCode} Waiting {timeSpan} Retry Attempt({retryCount})");
            }).ExecuteAsync(async () => await client.PutAsync($"{Configuration.BaseUrl}{apiEndPoint}", null));
                    return new JdsResponse()
                    {
                        RequestContent = await response.Content.ReadAsStringAsync(),
                        StatusCode = response.StatusCode
                    };
                }

            }
            catch (Exception exception)
            {

                throw new Exception(exception.ToString());
            }
        }
        public static async Task<JdsResponse> PutAsync(string apiEndPoint)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await Policy.HandleResult<HttpResponseMessage>(message => !message.IsSuccessStatusCode)
            .WaitAndRetryAsync(Configuration.RequestRetryCount, i => Configuration.RequestRetryInterval, (request, timeSpan, retryCount, context) =>
            {
                Debug.WriteLine($"Error With a {request.Result.StatusCode} Waiting {timeSpan} Retry Attempt({retryCount})");
            }).ExecuteAsync(async () => await client.PutAsync($"{Configuration.BaseUrl}{apiEndPoint}", null));
                    return new JdsResponse()
                    {
                        RequestContent = await response.Content.ReadAsStringAsync(),
                        StatusCode = response.StatusCode
                    };
                }
            }
            catch (Exception exception)
            {

                throw new Exception(exception.ToString());
            }
        }

        public static async Task<JdsMultiReponse<TResult>> PostAsyncWithMultiReturn<TPram, TResult>(string apiEndPoint, TPram model, string token) where TResult : new()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var json = JsonConvert.SerializeObject(model);
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await Policy.HandleResult<HttpResponseMessage>(message => !message.IsSuccessStatusCode)
            .WaitAndRetryAsync(Configuration.RequestRetryCount, i => Configuration.RequestRetryInterval, (request, timeSpan, retryCount, context) =>
            {
                Debug.WriteLine($"Error With a {request.Result.StatusCode} Waiting {timeSpan} Retry Attempt({retryCount})");
            }).ExecuteAsync(async () => await client.PostAsync($"{Configuration.BaseUrl}{apiEndPoint}", content));
                    var requestContent = await response.Content.ReadAsStringAsync();
                    if (!response.IsSuccessStatusCode)
                    {
                        return new JdsMultiReponse<TResult>()
                        {
                            RequestError = requestContent.Trim('"'),
                            StatusCode = response.StatusCode
                        };
                    }
                    var convertedToListModel = (List<TResult>)JsonConvert.DeserializeObject(requestContent, typeof(List<TResult>));
                    return new JdsMultiReponse<TResult>()
                    {
                        StatusCode = response.StatusCode,
                        RequestError = String.Empty,
                        Data = convertedToListModel
                    };
                }
             
            }
            catch (Exception exception)
            {

                throw new Exception(exception.ToString());
            }
        }
        public static async Task<JdsMultiReponse<TResult>> PostAsyncWithMultiReturn<TPram, TResult>(string apiEndPoint, TPram model) where TResult : new()
        {
            try
            {
                using (var client= new HttpClient())
                {
                    var json = JsonConvert.SerializeObject(model);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await Policy.HandleResult<HttpResponseMessage>(message => !message.IsSuccessStatusCode)
            .WaitAndRetryAsync(Configuration.RequestRetryCount, i => Configuration.RequestRetryInterval, (request, timeSpan, retryCount, context) =>
            {
                Debug.WriteLine($"Error With a {request.Result.StatusCode} Waiting {timeSpan} Retry Attempt({retryCount})");
            }).ExecuteAsync(async () => await client.PostAsync($"{Configuration.BaseUrl}{apiEndPoint}", content));
                    var requestContent = await response.Content.ReadAsStringAsync();
                    if (!response.IsSuccessStatusCode)
                    {
                        return new JdsMultiReponse<TResult>()
                        {
                            RequestError = requestContent.Trim('"'),
                            StatusCode = response.StatusCode
                        };
                    }
                    var convertedToListModel = (List<TResult>)JsonConvert.DeserializeObject(requestContent, typeof(List<TResult>));
                    return new JdsMultiReponse<TResult>()
                    {
                        StatusCode = response.StatusCode,
                        RequestError = String.Empty,
                        Data = convertedToListModel
                    };
                }
            }
            catch (Exception exception)
            {

                throw new Exception(exception.ToString());
            }
        }

        public static async Task<JdsResponse> DeleteAsync(string apiEndPoint, string token)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                    var response = await Policy.HandleResult<HttpResponseMessage>(message => !message.IsSuccessStatusCode)
            .WaitAndRetryAsync(Configuration.RequestRetryCount, i => Configuration.RequestRetryInterval, (request, timeSpan, retryCount, context) =>
            {
                Debug.WriteLine($"Error With a {request.Result.StatusCode} Waiting {timeSpan} Retry Attempt({retryCount})");
            }).ExecuteAsync(async () => await client.DeleteAsync($"{Configuration.BaseUrl}{apiEndPoint}"));
                    return new JdsResponse()
                    {
                        RequestContent = await response.Content.ReadAsStringAsync(),
                        StatusCode = response.StatusCode
                    };
                }

            }
            catch (Exception exception)
            {

                throw new Exception(exception.ToString());
            }
        }
        public static async Task<JdsResponse> DeleteAsync(string apiEndPoint)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await Policy.HandleResult<HttpResponseMessage>(message => !message.IsSuccessStatusCode)
            .WaitAndRetryAsync(Configuration.RequestRetryCount, i => Configuration.RequestRetryInterval, (request, timeSpan, retryCount, context) =>
            {
                Debug.WriteLine($"Error With a {request.Result.StatusCode} Waiting {timeSpan} Retry Attempt({retryCount})");
            }).ExecuteAsync(async () => await client.DeleteAsync($"{Configuration.BaseUrl}{apiEndPoint}"));
                    return new JdsResponse()
                    {
                        RequestContent = await response.Content.ReadAsStringAsync(),
                        StatusCode = response.StatusCode
                    };
                }
            }
            catch (Exception exception)
            {

                throw new Exception(exception.ToString());
            }

        }


    }



}
