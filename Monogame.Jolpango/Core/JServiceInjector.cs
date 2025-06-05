using MonoGame.Jolpango.ECS;
using MonoGame.Jolpango.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace MonoGame.Jolpango.Core
{
    public class JServiceInjector
    {
        private readonly Dictionary<Type, object> services = new Dictionary<Type, object>();

        public void RegisterService<T>(T service)
        {
            if (service == null)
                throw new ArgumentNullException(nameof(service));
            var serviceType = typeof(T);
            if (services.ContainsKey(serviceType))
                throw new InvalidOperationException($"Service of type {serviceType.Name} is already registered.");
            services[serviceType] = service;
        }

        public void InjectAll(IEnumerable<JEntity> entities)
        {
            if (entities is null)
                throw new ArgumentNullException(nameof(entities));
            foreach (var entity in entities)
            {
                foreach (var component in entity.ComponentsList)
                {
                    Inject(component);
                }
            }
        }
        public void InjectAll(JEntity entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));
            foreach (var component in entity.ComponentsList)
            {
                Inject(component);
            }
        }

        /// <summary>
        /// Injects required services into the injectable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="injectable"></param>
        public void Inject<T>(T injectable)
        {
            foreach (var kvp in services)
            {
                var injectableType = typeof(IJInjectable<>).MakeGenericType(kvp.Key);
                if (injectableType.IsAssignableFrom(injectable.GetType()))
                {
                    injectableType.GetMethod("Inject")?.Invoke(injectable, new[] { kvp.Value });
                }
            }
        }
    }
}
