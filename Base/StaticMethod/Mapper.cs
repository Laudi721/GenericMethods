using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Base.StaticMethod
{
    public static class Mapper<ModelDto, Model>
    {
      public static Model Map<ModelDto, Model>(ModelDto dto) where Model : new()
        {

            // do przebudowy
            var model = Activator.CreateInstance<Model>();

            foreach(var propertyInfo in model.GetType().GetProperties())
            {
                var value = propertyInfo.GetValue(dto);

                if (value is int || value is string || value is DateTime)
                {
                    typeof(Model).GetProperty(propertyInfo?.Name)?.SetValue(model, value);
                }
                else
                {
                    var innerModel = typeof(Model).GetProperty(propertyInfo.Name)?.GetValue(model);
                    if(innerModel == null)
                        innerModel = Activator.CreateInstance(propertyInfo.PropertyType);

                    //typeof(Model).GetProperty(propertyInfo.Name)?.SetValue(model, Map<object, object>((dynamic) value, innerModel));
                }
            }

            return model;
        }

    }
}
