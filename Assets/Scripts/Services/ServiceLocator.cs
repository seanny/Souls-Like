using System;
using System.Collections.Generic;
using UnityEngine;

namespace SoulsLike
{
    public static class ServiceLocator
    {
        public static List<IService> serviceList = new List<IService>();

        public static T GetService<T>()
        {
            foreach (var service in serviceList)
            {
                if (service.GetType() == typeof(T))
                {
                    return (T)service;
                }
            }

            try
            {
                return AddService<T>();
            }
            catch (Exception e)
            {
                Debug.LogError($"Cannot return type for {typeof(T).ToString()}: {e.Message}");
                throw;
            }
        }

        public static T AddService<T>()
        {
            Debug.Log($"Added {typeof(T).ToString()} to serviceList");
            IService service = (IService)Activator.CreateInstance(typeof(T));
            serviceList.Add(service);
            service.OnStart();
            return (T)service;
        }

        public static T AddService<T>(T serviceObject)
        {
            Debug.Log($"Added {serviceObject.ToString()} to serviceList");
            IService service = (IService)serviceObject;
            serviceList.Add(service);
            service.OnStart();
            return (T)service;
        }

        public static void EndAllServices()
        {
            foreach (var service in serviceList)
            {
                service.OnEnd();
            }
            serviceList.Clear();
        }
    }
}
