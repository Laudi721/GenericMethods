using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.StaticMethod
{
    public static class Mapper
    {
        public static TTarget Map<TSource, TTarget>(TSource source) where TTarget : new()
        {
            var target = new TTarget();

            foreach (var sourceProperty in typeof(TSource).GetProperties())
            {
                var targetProperty = typeof(TTarget).GetProperty(sourceProperty.Name);
                if (targetProperty != null && targetProperty.CanWrite)
                {
                    var sourceValue = sourceProperty.GetValue(source);
                    if (sourceValue != null)
                    {
                        if (sourceProperty.PropertyType.Namespace == typeof(TSource).Namespace)
                        {
                            var nestedTarget = Activator.CreateInstance(targetProperty.PropertyType);
                            var nestedSource = sourceValue;
                            var nestedTargetMapped = Map<object, object>(nestedSource);
                            targetProperty.SetValue(target, nestedTargetMapped);
                        }
                        else
                        {
                            targetProperty.SetValue(target, sourceValue);
                        }
                    }
                }
            }

            return target;
        }

    }
}
