using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Common.Helper
{
    public static class ProjectToExtension
    {
        private static T ProjectTo<T>(this object instance, IEnumerable<PropertyInfo> sourceProps, IEnumerable<PropertyInfo> destProps, IEnumerable<string> exceptions = null) where T : new()
        {
            T val = new T();
            foreach (PropertyInfo spi in sourceProps)
            {
                if (exceptions != null && exceptions.Contains(spi.Name))
                {
                    continue;
                }

                PropertyInfo propertyInfo = destProps.Where((PropertyInfo pi) => pi.Name == spi.Name).FirstOrDefault();
                if (!(propertyInfo != null) || !propertyInfo.CanWrite)
                {
                    continue;
                }

                if (spi.PropertyType.IsEnum)
                {
                    if (propertyInfo.PropertyType.IsEnum)
                    {
                        propertyInfo.SetValue(val, spi.GetValue(instance));
                    }
                    else if (propertyInfo.PropertyType == typeof(string))
                    {
                        object value = spi.GetValue(instance);
                        propertyInfo.SetValue(val, value.ToString());
                    }
                    else if (propertyInfo.PropertyType == typeof(int))
                    {
                        object value2 = spi.GetValue(instance);
                        propertyInfo.SetValue(val, Convert.ToInt32(value2));
                    }
                }
                else if (!propertyInfo.PropertyType.IsEnum)
                {
                    propertyInfo.SetValue(val, spi.GetValue(instance));
                }
                else if (spi.PropertyType == typeof(string))
                {
                    object value3 = Enum.Parse(propertyInfo.PropertyType, (string)spi.GetValue(instance));
                    propertyInfo.SetValue(val, value3);
                }
                else if (spi.PropertyType == typeof(int))
                {
                    propertyInfo.SetValue(val, spi.GetValue(instance));
                }
            }

            return val;
        }

        public static T ProjectTo<T>(this object instance, IEnumerable<string> exceptions = null) where T : new()
        {
            PropertyInfo[] properties = instance.GetType().GetProperties();
            PropertyInfo[] properties2 = typeof(T).GetProperties();
            return instance.ProjectTo<T>(properties, properties2, exceptions);
        }

        public static IEnumerable<T> ProjectTo<T>(this IEnumerable<object> instance, IEnumerable<string> exceptions = null) where T : new()
        {
            PropertyInfo[] array = null;
            PropertyInfo[] properties = typeof(T).GetProperties();
            List<T> list = new List<T>();
            foreach (object item in instance)
            {
                if (array == null)
                {
                    array = item.GetType().GetProperties();
                }

                list.Add(item.ProjectTo<T>(array, properties, exceptions));
            }

            return list;
        }

        public static T2 ProjectToWithImplicitMap<T1, T2>(this object instance, Action<T1, T2> map, IEnumerable<string> exceptions = null) where T2 : new()
        {
            PropertyInfo[] properties = instance.GetType().GetProperties();
            PropertyInfo[] properties2 = typeof(T2).GetProperties();
            T2 val = instance.ProjectTo<T2>(properties, properties2, exceptions);
            map((T1)instance, val);
            return val;
        }

        public static IEnumerable<T2> ProjectToWithImplicitMap<T1, T2>(this IEnumerable<object> instance, Action<T1, T2> map, IEnumerable<string> exceptions = null) where T2 : new()
        {
            PropertyInfo[] array = null;
            PropertyInfo[] properties = typeof(T2).GetProperties();
            List<T2> list = new List<T2>();
            foreach (object item in instance)
            {
                if (array == null)
                {
                    array = item.GetType().GetProperties();
                }

                T2 val = item.ProjectTo<T2>(array, properties, exceptions);
                map((T1)item, val);
                list.Add(val);
            }

            return list;
        }

        public static T2 ProjectToWithExplicitMap<T1, T2>(this object instance, Action<T1, T2> map) where T2 : new()
        {
            instance.GetType().GetProperties();
            typeof(T2).GetProperties();
            T2 val = new T2();
            map((T1)instance, val);
            return val;
        }

        public static IEnumerable<T2> ProjectToWithExplicitMap<T1, T2>(this IEnumerable<object> instance, Action<T1, T2> map) where T2 : new()
        {
            PropertyInfo[] array = null;
            typeof(T2).GetProperties();
            List<T2> list = new List<T2>();
            foreach (object item in instance)
            {
                if (array == null)
                {
                    array = item.GetType().GetProperties();
                }

                T2 val = new T2();
                map((T1)item, val);
                list.Add(val);
            }

            return list;
        }
    }

}
