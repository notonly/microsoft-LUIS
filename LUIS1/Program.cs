using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LUIS1
{
	class Program
	{
		static void Main(string[] args)
		{
			MakeRequest();
			Console.WriteLine("Hit Enter Key to exit...");
			Console.ReadLine();
		}

		private static async void MakeRequest()
		{
			var client = new HttpClient();
			var query = HttpUtility.ParseQueryString(string.Empty);

			var luisAppId = "df67dcdb-c37d-46af-88e1-8b97951ca1c2";
			var endPointKey	= "f8be924eba234ec286a258a1b3d19082";

			client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", endPointKey);

			// "q" param to LUIS
			query["q"] = "turn on the left light";

			// optiona params
			query["timezoneOffset"] = "0";
			query["verbose"] = "false";
			query["spellCheck"] = "true";
			query["staging"] = "false";

			var endpointUrl = "https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/" + luisAppId + "?"
				//+ "subscription-key=" + endPointKey + query;
				+ query;


			/* POST Request
			
			HttpResponseMessage response;

			// Request body
			byte[] byteData = Encoding.UTF8.GetBytes("");

			using (var content = new ByteArrayContent(byteData))
			{
				content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

				response = await client.PostAsync(endpointUrl, content);

				Console.WriteLine(response.ToString());
			}
			*/

			var response = await client.GetAsync(endpointUrl);

			//var rspnStr = await response.Content.ReadAsByteArrayAsync();   // this returns System.byte[]
			var rspnStr = await response.Content.ReadAsStringAsync();

			// display JSON response from LUIS
			Console.WriteLine(rspnStr.ToString());
		}
	}
}
