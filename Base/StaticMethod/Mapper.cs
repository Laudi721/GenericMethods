using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections;
using System.Dynamic;
using System.Reflection;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Base.StaticMethod
{
    public static class Mapper
    {
        /// <summary>
        /// Metoda zwracająca zmapowaną kolekcję.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="mappingLevel"></param>
        /// <returns></returns>
        public static List<T> MapCollection<T>(object source, int mappingLevel = 2) where T : class
        {
            return MapCollection(source, typeof(List<T>), mappingLevel) as List<T>;
        }

        /// <summary>
        /// Metoda mapująca kolekcję.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="resultType"></param>
        /// <param name="mappingLevel"></param>
        /// <returns></returns>
        public static object MapCollection(object source, Type resultType, int mappingLevel = 2)
        {
            var result = Activator.CreateInstance(resultType) as IList;

            var singleElementType = resultType.GenericTypeArguments[0];            

            foreach (var item in (source as IEnumerable))
            {
                if((bool)item.GetType().GetProperty("IsDeleted").GetValue(item))
                    continue;

                result.Add(Map(item, singleElementType, mappingLevel));
            }

            return result;
        }

        /// <summary>
        /// Metoda zwracająca zmapowany obiekt.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="mappingLevel"></param>
        /// <returns></returns>
        public static T Map<T>(object source, int mappingLevel = 2) where T : class
        {
            return Map(source, typeof(T), mappingLevel) as T;
        }

        /// <summary>
        /// Metoda mapująca obiekt.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="result"></param>
        /// <param name="mappingLevel"></param>
        /// <returns></returns>
        private static object Map(object source, Type result, int mappingLevel = 2)
        {
            if (source == null)
                return null;

            var sourceType = source.GetType();

            var resultProperties = GetTypeProperties(result);
            var sourceProperties = GetTypeProperties(sourceType);

            var resultObject = Activator.CreateInstance(result) as object;

            foreach (var property in resultProperties)
            {
                var setter = SetterProperty(result, property.Name);
                
                if (setter == null)
                    continue;

                if (!sourceProperties.Any(a => a.Name.Equals(property.Name)))
                    continue;

                var getter = GetterProperty(sourceType, property.Name);

                if (getter == null)
                    continue;

                var propertyType = property.PropertyType;

                if (propertyType.IsSealed)
                    HandleSimpleMapping(getter, setter, source, resultObject, propertyType);
                else if (!propertyType.IsSealed && mappingLevel > 0)
                    HandleModelMapping(getter, setter, source, resultObject, propertyType, mappingLevel - 1);
            }

            return resultObject;
        }

        /// <summary>
        /// Metoda mapująca typy prymitywne.
        /// </summary>
        /// <param name="getter"></param>
        /// <param name="setter"></param>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="propertyType"></param>
        private static void HandleSimpleMapping(PropertyInfo getter, PropertyInfo setter, object source, object destination, Type propertyType)
        {
            if (getter.PropertyType == propertyType && setter.CanWrite)
            {
                var value = getter.GetValue(source);
                setter.SetValue(destination, value);
            }
        }

        /// <summary>
        /// Metoda mapująca model wewnątrz modelu
        /// </summary>
        /// <param name="getter"></param>
        /// <param name="setter"></param>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="propertyType"></param>
        /// <param name="mappingLevel"></param>
        private static void HandleModelMapping(PropertyInfo getter, PropertyInfo setter, object source, object destination, Type propertyType, int mappingLevel)
        {
            var sourceValue = getter.GetValue(source);

            if (sourceValue == null)
            {
                setter.SetValue(destination, null);
                return;
            }

            if (!(sourceValue is IEnumerable))
            {
                var resultValue = Activator.CreateInstance(propertyType).GetType();
                var obj = Map(sourceValue, resultValue, mappingLevel - 1);
                setter.SetValue(destination, obj);

            }
            else if(sourceValue is IEnumerable)
            {
                var propType = propertyType.GenericTypeArguments.First();
                var collection = Activator.CreateInstance(propertyType) as IList;

                if (!propType.IsSealed)
                {
                    foreach (var item in (sourceValue as IEnumerable))
                    {
                        var genericInstance = Map(item, propType, mappingLevel);

                        collection.Add(genericInstance);
                    }
                }

                setter.SetValue(destination, collection);
            }
        }

        /// <summary>
        /// Metoda ustawiająca setter
        /// </summary>
        /// <param name="type"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        private static PropertyInfo SetterProperty(Type type, string propertyName)
        {
            return type.GetProperty(propertyName);
        }

        /// <summary>
        /// Metoda ustawiająca getter
        /// </summary>
        /// <param name="type"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        private static PropertyInfo GetterProperty(Type type, string propertyName)
        {
            return type.GetProperty(propertyName);
        }

        /// <summary>
        /// Metoda pobierająca property modelu
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<PropertyInfo> GetTypeProperties(Type type)
        {
            return type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).ToList();
        }
    }
}
