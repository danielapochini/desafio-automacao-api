using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace DesafioAutomacaoAPI.Model
{
    public class BaseRequest<T>
    {
        public HttpStatusCode StatusCode { get; set; }

        public IRestResponse Response { get; set; }
    }
}
