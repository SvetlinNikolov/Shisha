﻿namespace ShishaProject.Common.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Microsoft.Extensions.DependencyInjection;

    public static class DependencyInjectionHelper
    {
        public static void AddAllTypes<T>(
            this IServiceCollection services
           , Assembly[] assemblies
           , bool additionalRegisterTypesByThemself = false
           , ServiceLifetime lifetime = ServiceLifetime.Transient
       )
        {
            var typesFromAssemblies = assemblies.SelectMany(a =>
                a.DefinedTypes.Where(x => x.GetInterfaces().Any(i => i == typeof(T))));
            foreach (var type in typesFromAssemblies)
            {
                services.Add(new ServiceDescriptor(typeof(T), type, lifetime));
                if (additionalRegisterTypesByThemself)
                {
                    services.Add(new ServiceDescriptor(type, type, lifetime));
                }
            }
        }

        public static void AddAllGenericTypes(this IServiceCollection services
            , Type t
            , Assembly[] assemblies
            , bool additionalRegisterTypesByThemself = false
            , ServiceLifetime lifetime = ServiceLifetime.Transient
        )
        {
            var genericType = t;
            var typesFromAssemblies = assemblies.SelectMany(a => a.DefinedTypes.Where(x => x.GetInterfaces()
                .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == genericType)));

            foreach (var type in typesFromAssemblies)
            {
                services.Add(new ServiceDescriptor(t, type, lifetime));
                if (additionalRegisterTypesByThemself)
                    services.Add(new ServiceDescriptor(type, type, lifetime));
            }
        }
    }
}
