using Newtonsoft.Json;
using System;

namespace App1
{
    class Program
    {
        static void Main(string[] args)
        {
            var originalPerson = new Person()
            {
                FirstName = "Ivo",
                LastName = "Nelov",
                Age = 31
            };

            var template = new
            {
                FirstName = string.Empty,
                LastName = string.Empty,
                Age = default(int)
            };

            string json = @"{""Age"": ""31"", ""FirstName"":""Ivo"", ""LastName"":""Nelov""}";

            var person = JsonConvert.DeserializeObject(json, typeof(Person));

            //person.Contains("Age");

            //var person = JsonConvert.DeserializeAnonymousType(json, template);

            Console.WriteLine(JsonConvert.SerializeObject(originalPerson, Formatting.Indented));

            Console.ReadLine();
        }
    }
}
