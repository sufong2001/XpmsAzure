using AutoMapper;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xpms.AzureRepository.Extensions
{
    public static class ITableEntityExtensions
    {
        public static T MapTo<T>(this ITableEntity entity) where T : class
        {
            if (entity == null) return default(T);

            Mapper.CreateMap(entity.GetType(), typeof(T));
            return Mapper.Map<T>(entity);
        }

        public static TDestination MapToEntity<TDestination>(this object data)
            where TDestination : class
        {
            if (data == null) return null;

            Mapper.CreateMap(data.GetType(), typeof(TDestination));
            return Mapper.Map<TDestination>(data);
        }
    }
}
