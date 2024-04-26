using System;
using System.Collections.Generic;
using System.Linq;
using Attributes;
using Extensions;
using School.Core;

namespace Core
{
    public static class ReflectionService
    {
        public static IEnumerable<Type> GetAllAutoRegisteredServices()
        {
            return AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(assembly => assembly.GetTypesWithCustomAttribute<AutoRegisteredService>())
                .Where(service => typeof(IRegistrable).IsAssignableFrom(service));
        }
    }
}
