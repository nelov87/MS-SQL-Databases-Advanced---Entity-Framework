using MyApp.Core.Commands.Contracts;
using MyApp.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MyApp.Core
{
    public class ComandInterpreter : IComandIterpreter
    {
        private const string Suffix = "Command";
        private readonly IServiceProvider serviceProvider;

        public ComandInterpreter(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public string Read(string[] inputArgs)
        {
            string commandName = inputArgs[0] + Suffix;
            string[] comandParams = inputArgs.Skip(1).ToArray();

            var type = Assembly.GetCallingAssembly()
                .GetTypes()
                .FirstOrDefault(x => x.Name == commandName);

            if (type == null)
            {
                throw new ArgumentException("Invalid command!");
            }

            var constructor = type.GetConstructors()
                .FirstOrDefault();

            var constructorParameters = constructor
                .GetParameters()
                .Select(x => x.ParameterType)
                .ToArray();

            var services = constructorParameters
                .Select(this.serviceProvider.GetService)
                .ToArray();

            var command =(ICommand) constructor.Invoke(services);

            string result = command.Execute(comandParams);

            return result;


        }
    }
}
