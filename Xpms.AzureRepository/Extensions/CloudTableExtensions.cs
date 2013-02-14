using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Data.Services.Client;

namespace Xpms.AzureRepository.Extensions
{
    public static class CloudTableExtensions
    {
        public static DataServiceQuery<T> AsQueryable<T>(this CloudTable table)
        {
            if (string.IsNullOrEmpty(table.Name)) throw new Exception("Table name is required!!");

            return table.ServiceClient.GetTableServiceContext().CreateQuery<T>(table.Name);
        }

        public static DataServiceContext DataContext(this CloudTable table)
        {
            return table.ServiceClient.GetTableServiceContext();
        }

        public static TableResult Insert(this CloudTable table, ITableEntity entity)
        {
            var operation = TableOperation.Insert(entity);
            return table.Execute(operation);
        }

        public static TableResult Update(this CloudTable table, ITableEntity entity)
        {
            entity.ETag = "*";
            var operation = TableOperation.Replace(entity);
            return table.Execute(operation);
        }

        public static TableResult Delete(this CloudTable table, ITableEntity entity)
        {
            entity.ETag = "*";
            var operation = TableOperation.Delete(entity);
            return table.Execute(operation);
        }

        public static IEnumerable<TableResult> DeleteBatch(this CloudTable table, IEnumerable<ITableEntity> entities)
        {
            List<TableResult> results = new List<TableResult>();

            var batchOperation = new TableBatchOperation();
            int itemsInBatch = 0;
            foreach (ITableEntity row in entities)
            {
                row.ETag = "*";

                batchOperation.Delete(row);
                itemsInBatch++;
                if (itemsInBatch == 100)
                {
                    var r = table.ExecuteBatch(batchOperation);

                    results.AddRange(r);
                    itemsInBatch = 0;
                    batchOperation = new TableBatchOperation();
                }
            }
            if (itemsInBatch > 0)
            {
                var r = table.ExecuteBatch(batchOperation);
                results.AddRange(r);
            }

            return results;
        }
    }
}