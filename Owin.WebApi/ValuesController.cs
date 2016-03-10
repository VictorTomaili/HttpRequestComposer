using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace Owin.WebApi
{
    public class ValuesController : ApiController
    {
        public IEnumerable<string> Get()
        {
            return new[] { "value1", "value2" };
        }

        public string Get(int id)
        {
            return "value";
        }

        public async Task<ResponseMessageResult> Post()
        {
            var value = await Request.Content.ReadAsStringAsync()
                .ContinueWith(task => task.Result);

            Console.WriteLine(value);

            if(!value.Contains("world"))
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            return this.ResponseMessage(new HttpResponseMessage(HttpStatusCode.Created));
        }

        public async Task<ResponseMessageResult> Put(int id)
        {
            var value = await Request.Content.ReadAsStringAsync()
            .ContinueWith(task => task.Result);

            Console.WriteLine(value);

            if (!value.Contains("world"))
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            return this.ResponseMessage(new HttpResponseMessage(HttpStatusCode.Accepted));
        }

        public ResponseMessageResult Delete(int id)
        {
            return this.ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
        }
    }
}