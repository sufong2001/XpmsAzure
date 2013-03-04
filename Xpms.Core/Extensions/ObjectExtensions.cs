using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ServiceStack.Configuration;

namespace Xpms.Core.Extensions
{
    public static class ObjectExtensions
    {
        public static T As<T>(this object obj, IContainerAdapter ioC) where T : class
        {
            var data = ioC.TryResolve<T>();

            return Mapper.Map(obj, data, obj.GetType(), data.GetType()) as T;
        }

        public static TDestination MapAs<TDestination>(this object data) where TDestination : class
        {
            if (data == null) return null;

            if (Mapper.FindTypeMapFor(data.GetType(), typeof(TDestination)) == null)
                Mapper.CreateMap(data.GetType(), typeof(TDestination));

            return Mapper.Map<TDestination>(data);
        }

        public static object MapAs(this object data, Type targetType)
        {
            if (data == null) return null;

            if (Mapper.FindTypeMapFor(data.GetType(), targetType) == null)
                Mapper.CreateMap(data.GetType(), targetType);

            return Mapper.Map(data, data.GetType(), targetType);
        }
    }
}
