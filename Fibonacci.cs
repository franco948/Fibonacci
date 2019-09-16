using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Fibonacci
{
  public static class Fibonacci
  {
    [FunctionName("Fibonacci")]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
        ILogger log)
    {
      log.LogInformation("C# HTTP trigger function processed a request.");

      int n = 0;

      // Get the parameter from the query string and try parsing it
      bool isANumber = int.TryParse(req.Query["n"], out n);

      if (!isANumber || n < 2)
      {
        return new BadRequestObjectResult("Please, pass a integer value equal or greater than 2 as the parameter of the function");
      }

      return new OkObjectResult($"Fibonacci number at position {n} is equal to {Fibo(n)}");
    }

    private static int Fibo(int n)
    {
      if (n == 0) return 0;
      if (n == 1) return 1;

      return Fibo(n - 1) + Fibo(n - 2);
    }
  }
}
