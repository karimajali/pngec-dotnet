using Dapper;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Domain.Mobile.Voter;
using PNGEC.API.Domain.Mobile.Water;
using PNGEC.API.Domain.User;
using PNGEC.API.Helper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Threading.Tasks;

namespace PNGEC.API.Repository.ProductionToTrainingService
{
    public class ProductionToTrainingRepository : IProductionToTraining
    {
        #region variable declaration
        /// <summary>
        /// The connection string
        /// </summary>
        private readonly string connectionString;

        /// <summary>
        /// The result
        /// </summary>
        private bool bitResult = false;

        #endregion

        #region constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        public ProductionToTrainingRepository(string connection)
        {
            connectionString = connection;
        }
        #endregion

        #region API

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public async Task<bool> BackupPngecCore(string filePath)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.Backup_Pngec_Core;

                DynamicParameters param = new DynamicParameters();
                param.Add("@FilePath", filePath);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                bitResult = await con.QueryFirstOrDefaultAsync<bool>(sQuery, param, commandType: CommandType.StoredProcedure, commandTimeout: 10000);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return bitResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public async Task<bool> BackupPngecFinal(string filePath)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.Backup_Pngec_Final;

                DynamicParameters param = new DynamicParameters();
                param.Add("@FilePath", filePath);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                bitResult = await con.QueryFirstOrDefaultAsync<bool>(sQuery, param, commandType: CommandType.StoredProcedure, commandTimeout: 10000);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return bitResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public async Task<bool> BackupPngecPreliminary(string filePath)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.Backup_Pngec_Preliminary;

                DynamicParameters param = new DynamicParameters();
                param.Add("@FilePath", filePath);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                bitResult = await con.QueryFirstOrDefaultAsync<bool>(sQuery, param, commandType: CommandType.StoredProcedure, commandTimeout: 10000);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return bitResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public async Task<bool> BackupPngecStaging(string filePath)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.Backup_Pngec_Staging;

                DynamicParameters param = new DynamicParameters();
                param.Add("@FilePath", filePath);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                bitResult = await con.QueryFirstOrDefaultAsync<bool>(sQuery, param, commandType: CommandType.StoredProcedure, commandTimeout: 10000);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return bitResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public async Task<bool> RestorePngecCore(string filePath)
        {
            using (SqlConnection con = new SqlConnection(connectionString.Replace("core", "staging")))
            {
                string sQuery = StoredProcedure.Restore_Pngec_Core;

                DynamicParameters param = new DynamicParameters();
                param.Add("@FilePath", filePath);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                bitResult = await con.QueryFirstOrDefaultAsync<bool>(sQuery, param, commandType: CommandType.StoredProcedure, commandTimeout: 10000);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return bitResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public async Task<bool> RestorePngecFinal(string filePath)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.Restore_Pngec_Final;

                DynamicParameters param = new DynamicParameters();
                param.Add("@FilePath", filePath);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                bitResult = await con.QueryFirstOrDefaultAsync<bool>(sQuery, param, commandType: CommandType.StoredProcedure, commandTimeout: 10000);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return bitResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public async Task<bool> RestorePngecPreliminary(string filePath)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.Restore_Pngec_Preliminary;

                DynamicParameters param = new DynamicParameters();
                param.Add("@FilePath", filePath);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                bitResult = await con.QueryFirstOrDefaultAsync<bool>(sQuery, param, commandType: CommandType.StoredProcedure, commandTimeout: 10000);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return bitResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public async Task<bool> RestorePngecStaging(string filePath)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sQuery = StoredProcedure.Restore_Pngec_Staging;

                DynamicParameters param = new DynamicParameters();
                param.Add("@FilePath", filePath);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                bitResult = await con.QueryFirstOrDefaultAsync<bool>(sQuery, param, commandType: CommandType.StoredProcedure, commandTimeout: 10000);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return bitResult;
        }
        #endregion

    }
}
