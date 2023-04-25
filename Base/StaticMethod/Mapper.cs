using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections;
using System.Reflection;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Base.StaticMethod
{
    public static class Mapper
    //public static class Mapper<ModelDto, Model>
    {
        public static T Map<T>(object item, object model)
        {
            var result = Activator.CreateInstance<T>();

            var ss = item.GetType().GetProperties().ToList();

            foreach (var property in item.GetType().GetProperties())
            {
                var sourceValue = property.GetValue(item);
                var targetValue = model.GetType().GetProperty(property.Name);

                var sourceValueType = property.PropertyType;
                if (sourceValueType.IsValueType || sourceValueType == typeof(string))
                {
                    var sourceValue2 = item.GetType().GetProperty(property.Name);
                    targetValue.SetValue(result, sourceValue);
                }
                else if (sourceValueType.IsGenericType)
                {
                    var innerItem = property.PropertyType.GetGenericArguments()[0];
                    var innerModel = targetValue.PropertyType.GetGenericArguments()[0];

                    var listValues = new List<object>();
                    foreach (var single in sourceValue as IList)
                    {
                        listValues.Add(SingleMapping(single, innerModel));
                    }

                    targetValue.SetValue(result, listValues);
                }
                else if (sourceValueType.IsClass)
                {
                    var innerItem = property.PropertyType.GetGenericArguments()[0];
                    var innerModel = targetValue.PropertyType.GetGenericArguments()[0];

                    var value = SingleMapping(innerItem, innerModel);
                }
            }

            return result;
        }

        private static object SingleMapping(object innerItem, Type innerModel)
        {
            var result = Activator.CreateInstance<object>();

            //foreach(var property in innerModel.GetProperties())
            //{
            //    var sourceValue = innerItem.GetType().GetProperty(property.Name);
            //    if(sourceValue == null)
            //        continue;
            //}

            return result;
        }
        //public static Model Map(object dto, object model, int mappingLevel)
        //{
        //    var result = Activator.CreateInstance<Model>();
        //    var propetries = dto.GetType().GetProperties().ToList();

        //    foreach (var propertyInfo in dto.GetType().GetProperties())
        //    {
        //        var value = propertyInfo.GetValue(dto);

        //        if (!propertyInfo.PropertyType.IsSealed)
        //        {
        //            object innerModel = typeof(Model).GetProperty(propertyInfo.Name);
        //            object innerDto = propertyInfo.PropertyType;

        //            value = Map(dto, innerDto, 1);
        //        }
        //        else
        //        {
        //            typeof(Model).GetProperty(propertyInfo.Name).SetValue(model, value);
        //        }
        //    }

        //    return result;
        //}

        //public static Model Map<Model>(object source) where Model : new()
        //public static T Map<T>(object source) where T : new()
        //{
        //    var destination = new T();

        //    foreach (var sourceProperty in source.GetType().GetProperties())
        //    {
        //        var destinationProperty = typeof(Model).GetProperty(sourceProperty.Name);

        //        if (destinationProperty == null)
        //        {
        //            continue;
        //        }

        //        var sourceValue = sourceProperty.GetValue(source);
        //        var destinationValueType = destinationProperty.PropertyType;

        //        if (sourceValue == null || !destinationValueType.IsAssignableFrom(sourceProperty.PropertyType))
        //        {
        //            continue;
        //        }

        //        if (destinationValueType.IsPrimitive || destinationValueType == typeof(string))
        //        {
        //            destinationProperty.SetValue(destination, sourceValue);
        //        }
        //        else if (destinationValueType.IsClass)
        //        {
        //            var nestedDestination = Map<object>(sourceValue);
        //            destinationProperty.SetValue(destination, nestedDestination);
        //        }
        //    }

        //    return destination;
        //}

        //public static T Map<T>(object source, object destination) where T : class, new()
        //{
        //    var destination = Activator.CreateInstance<Model>();
        //    //var destination = new T();

        //    foreach (var sourceProperty in source.GetType().GetProperties())
        //    {
        //        var destinationProperty2 = destination.GetType().GetProperty(sourceProperty.Name);
        //        //var destinationProperty = typeof(Model).GetProperty(sourceProperty.Name);
        //        var destinationProperty = destination.GetType().GetProperty(sourceProperty.Name);

        //        if (destinationProperty == null)
        //        {
        //            continue;
        //        }

        //        var sourceValue = sourceProperty.GetValue(source);
        //        var destinationValueType = destinationProperty.PropertyType;

        //        if(destinationValueType.IsValueType || destinationValueType == typeof(string))
        //            destinationProperty.SetValue(destination, sourceValue);
        //        else
        //        {
        //            var nestedDestination = Map(sourceValue, destinationProperty);
        //            destinationProperty.SetValue(destination, nestedDestination);
        //        }
        //    }

        //    return destination;
        //}

        //public static Model Map<ModelDto, Model>(ModelDto dto) where Model : new()
        //{

        //    // do przebudowy
        //    var model = Activator.CreateInstance<Model>();

        //    foreach(var propertyInfo in model.GetType().GetProperties())
        //    {
        //        var value = propertyInfo.GetValue(dto);

        //        if (value is int || value is string || value is DateTime)
        //        {
        //            typeof(Model).GetProperty(propertyInfo?.Name)?.SetValue(model, value);
        //        }
        //        else
        //        {
        //            var innerModel = typeof(Model).GetProperty(propertyInfo.Name)?.GetValue(model);
        //            if(innerModel == null)
        //                innerModel = Activator.CreateInstance(propertyInfo.PropertyType);

        //            //typeof(Model).GetProperty(propertyInfo.Name)?.SetValue(model, Map<object, object>((dynamic) value, innerModel));
        //        }
        //    }

        //    return model;
        //}


    }
}
