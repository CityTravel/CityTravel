using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace CityTravel.Web.Controllers
{
    public class ValuesController : ApiController
    {
        // GET /api/values
        public async Task<string> Get()
        {
            WebClient client = new WebClient();
            try
            {
                return await client.DownloadStringTaskAsync(new Uri("http://google.com"));
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }

        // GET /api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST /api/values
        public void Post(string value)
        {
        }

        // PUT /api/values/5
        public void Put(int id, string value)
        {
        }

        // DELETE /api/values/5
        public void Delete(int id)
        {
        }
    }
}