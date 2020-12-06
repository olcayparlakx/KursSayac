using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace KursSayac
{
    public static class GobalVariables
    {

        public static HttpClient WebApiClient = new HttpClient();

        static GobalVariables()
        {

            WebApiClient.BaseAddress = new Uri("http://localhost:50951/api/");
            WebApiClient.DefaultRequestHeaders.Clear();
            WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }


    }
}