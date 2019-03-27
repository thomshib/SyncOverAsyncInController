using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SyncOverAsyncInController.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        //Correct Method
        //improves RPS - bombardier-windows-amd64.exe https://localhost:44363/api/values  -n 30 -t 100s
        // GET api/values
        //[HttpGet]
        //public async Task<IEnumerable<string>> Get()
        //{
        //    //return GetValues().ToList();
        //    return await GetValuesAsync();
        //}


        //Incorrect Method
        //Calls  Async over Sync
        //reduces RPS - bombardier-windows-amd64.exe https://localhost:44363/api/values  -n 30 -t 100s
        //
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return GetValues().ToList();
           
        }

        public async Task<IEnumerable<string>> GetValuesAsync()
        {
            await Task.Delay(1000);

            return await  Task.FromResult(new string[] { "value1", "value2" });
        }

        public IEnumerable<string> GetValues()
        {
            return GetValuesAsync().Result;
        }
    }
}
