
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using System;

namespace FunctionApp1
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequest req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];
            string amount = req.Query["amount"];
            string discount = req.Query["discount"];
            int result = Convert.ToInt32(amount) - Convert.ToInt32(discount);
            string requestBody = new StreamReader(req.Body).ReadToEnd();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;
            amount = amount ?? data?.amount;
            discount = discount ?? data?.discount;
            return name != null
                ? (ActionResult)new OkObjectResult($"Hello, {result.ToString()}")
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }
    }
}
