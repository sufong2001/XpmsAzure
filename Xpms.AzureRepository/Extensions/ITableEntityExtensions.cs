using AutoMapper;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xpms.Core.Extensions;

namespace Xpms.AzureRepository.Extensions
{
    public static class TableEntityExtensions
    {
        public static T MapTo<T>(this ITableEntity entity) where T : class
        {
            return entity.MapAs<T>();
        }

        public static TDestination MapToEntity<TDestination>(this object data)
            where TDestination : class
        {
            return data.MapAs<TDestination>();
        }
    }
}
