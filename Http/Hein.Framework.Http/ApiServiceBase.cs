using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace Hein.Framework.Http
{
    public class ApiServiceBase
    {
        protected Dictionary<string, string> _responseHeaders;
        protected string _contentType = HttpContentType.Text;
        protected string _responseString;
        protected HttpStatusCode _statusCode;

        protected ApiRequest _parameters;

        protected void ExecuteCall()
        {
            try
            {
                var webRequest = (HttpWebRequest)WebRequest.Create(_parameters.Url);
                webRequest.Method = _parameters.Method.ToString().ToUpper();
                webRequest.ContentType = _parameters.ContentType;
                webRequest.Accept = _parameters.Accept;
                if (_parameters.Headers != null && _parameters.Headers.Count > 0)
                {
                    foreach (var header in _parameters.Headers)
                    {
                        webRequest.Headers.Add(header.Key, header.Value);
                    }
                }

                if (_parameters.Timeout <= 0)
                {
                    webRequest.Timeout = 30000;
                }
                else
                {
                    webRequest.Timeout = _parameters.Timeout;
                }

                //ran thru post man to see what ones are eligible to contain a body
                if (_parameters.Method == HttpMethod.Post ||
                    _parameters.Method == HttpMethod.Put ||
                    _parameters.Method == HttpMethod.Patch ||
                    _parameters.Method == HttpMethod.Delete ||
                    _parameters.Method == HttpMethod.Options ||
                    _parameters.Method == HttpMethod.Link ||
                    _parameters.Method == HttpMethod.Unlink ||
                    _parameters.Method == HttpMethod.Lock ||
                    _parameters.Method == HttpMethod.Propfind ||
                    _parameters.Method == HttpMethod.View)
                {
                    using (var stream = new StreamWriter(webRequest.GetRequestStream()))
                    {
                        stream.Write(_parameters.RequestBody);
                        stream.Flush();
                        stream.Close();
                    }
                }

                _responseHeaders = new Dictionary<string, string>();
                this.GetResponse(webRequest.GetResponse());
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    this.GetResponse(ex.Response);
                }
            }
            catch (Exception ex)
            {
                _responseHeaders = new Dictionary<string, string>();
                _responseString = string.Concat("ApiServiceBase-Error: ", ex.Message);
                _contentType = HttpContentType.Text;
                _statusCode = HttpStatusCode.InternalServerError;
            }
        }

        private void GetResponse(WebResponse response)
        {
            var httpResponse = (HttpWebResponse)response;
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                var headerKeys = response.Headers.AllKeys;
                if (headerKeys != null && headerKeys.Any())
                {
                    foreach (var key in headerKeys)
                    {
                        var value = response.Headers[key];
                        _responseHeaders.Add(key, value);
                    }
                }

                _responseString = reader.ReadToEnd();
                _contentType = httpResponse.ContentType;
                _statusCode = httpResponse.StatusCode;
            }
        }
    }
}
