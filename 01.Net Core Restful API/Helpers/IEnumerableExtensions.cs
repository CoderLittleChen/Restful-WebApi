using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace _01.Net_Core_Restful_API.Helpers
{
    //扩展方法必须在非泛型静态类中定义
    public static class IEnumerableExtensions
    {
        public static IEnumerable<ExpandoObject> ShapeData<TSource>(this IEnumerable<TSource> source, string fields)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var expandoObjectList = new List<ExpandoObject>(source.Count());
            var propertyInfoList = new List<PropertyInfo>();

            if (string.IsNullOrWhiteSpace(fields))
            {
                //未指定API要返回的字段  则全部返回
                var propertyInfos = typeof(TSource).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                propertyInfoList.AddRange(propertyInfos);
            }
            else
            {
                //返回指定字段
                //分割字符串
                var fieldAfterSplit = fields.Split(",");
                for (int i = 0; i < fieldAfterSplit.Count(); i++)
                {
                    string propertyName = fieldAfterSplit[i].Trim();
                    var propertyInfo = typeof(TSource).GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance
                                            | BindingFlags.IgnoreCase);
                    if (propertyInfo == null)
                    {
                        throw new Exception($"Property:{propertyName}没有找到：{typeof(TSource)}");
                    }
                    propertyInfoList.Add(propertyInfo);
                }
            }

            foreach (TSource item in source)
            {
                var shapeObj = new ExpandoObject();
                foreach (var propertyInfo in propertyInfoList)
                {
                    var propertyValue = propertyInfo.GetValue(item);
                    ((IDictionary<string, object>)shapeObj).Add(propertyInfo.Name, propertyValue);
                }
                expandoObjectList.Add(shapeObj);
            }

            return expandoObjectList;
        }

    }
}
