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
    }
}
