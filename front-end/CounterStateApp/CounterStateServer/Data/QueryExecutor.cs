using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace CounterStateServer.Data
{
    public class QueryExecutor
    {
        private readonly string _connectionString = @"";
        public QueryExecutor()
        {
        }

        public async Task<DataSetResult> ExecuteAsync(string sql)
        {
            DataSetResult dsResult = new DataSetResult();
            DataSet ds = new DataSet();
            using (var con = new MySqlConnection(_connectionString))
            {
                await con.OpenAsync();
                try
                {
                    var list = await con.ExecuteReaderAsync(sql);
                    ds = ConvertDataReaderToDataSet(list);
                    
                    // using (var cmd = new MySqlCommand(sql, con))
                    // {
                    //     cmd.CommandText = sql;
                    //     cmd.CommandType = CommandType.Text;
                    //     using (var adapter = new MySqlDataAdapter(cmd))
                    //     {
                    //         adapter.Fill(ds);
                    //     }                        
                    // }
                }
                catch (Exception e)
                {
                    dsResult.ErrorCode = "01";
                    dsResult.ErrorMessage = e.Message;
                }
                finally
                {
                    await con.CloseAsync();
                }
            }

            if (ds == null || ds.Tables.Count <= 0)
            {
                dsResult.ErrorCode = "02";
                dsResult.ErrorMessage = "Not found data.";
                goto END;
            }
            
            if (ds != null && ds.Tables.Count > 0)
            {
                var dataTableResults = new List<DataTableResult>();
                foreach (DataTable dsTable in ds.Tables)
                {
                    var datatableResult = new DataTableResult()
                    {
                        Columns = new List<string>(),
                        Rows = new List<Dictionary<string, object>>()
                    };
                    
                    var columns = new List<string>();
                    var results = new List<Dictionary<string, object>>();
                    foreach (DataColumn dsTableColumn in dsTable.Columns)
                    {
                        datatableResult.Columns.Add(dsTableColumn.ColumnName);
                    }

                    
                    foreach (DataRow dsTableRow in dsTable.Rows)
                    {
                        var row = new Dictionary<string, object>();
                        datatableResult.Columns.ForEach(column =>
                        {
                            row.Add(column, dsTableRow[column]);
                        });                        
                        datatableResult.Rows.Add(row);
                    }
                    dataTableResults.Add(datatableResult);
                }

                dsResult.Results = dataTableResults;
            }

            END:
            return dsResult;
        }

        private DataSet ConvertDataReaderToDataSet(IDataReader data)
        {
            DataSet ds = new DataSet();
            int i = 0;
            while (!data.IsClosed)
            {
                ds.Tables.Add("Table" + (i + 1));
                ds.EnforceConstraints = false;
                ds.Tables[i].Load(data);
                i++;
            }                    
            return ds;
        }

        private async Task<string[]> QuerySelector(string sql)
        {
            var queries = sql.Split(';');
            return queries;
        }
    }

    public class DataTableResult
    {
        public List<string> Columns { get; set; }
        public List<Dictionary<string, object>> Rows { get; set; }
    }
    public class DataSetResult
    {
        public IEnumerable<DataTableResult> Results { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorCode { get; set; } = "00";
    }
}