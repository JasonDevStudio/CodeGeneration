﻿using Library.Common;
using Library.Kernel.DataBaseHelper;
using Library.Models;
using Library.StringItemDict;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Logic.DalGeneration
{
    public class MSqlGeneration : IGeneration
    {
        #region 私有函数

        /// <summary>
        /// TableModel 赋值
        /// </summary> 
        private IList<ModelGeneration> GetTableModel(IDataReader dr)
        {
            var modelList = new List<ModelGeneration>();

            while (dr.Read())
            {
                var model = new ModelGeneration();
                model.TableName = dr["NAME"] == DBNull.Value ? string.Empty : dr["NAME"].ToString();
                model.TableComments = dr["REMARK"] == DBNull.Value ? string.Empty : dr["REMARK"].ToString();
                model.TableType = dr["TYPE"] == DBNull.Value ? string.Empty : dr["TYPE"].ToString();
                modelList.Add(model);
            }

            return modelList;
        }

        /// <summary>
        /// TableModel 赋值
        /// </summary> 
        private IList<ModelGeneration> GetTableModel(DataSet ds)
        {
            var cardList = (from DataRow dr in ds.Tables[0].Rows
                            select new ModelGeneration()
                            {
                                TableName = dr["NAME"] == DBNull.Value ? string.Empty : dr["NAME"].ToString(),
                                TableComments = dr["REMARK"] == DBNull.Value ? string.Empty : dr["REMARK"].ToString(),
                                TableType = dr["TYPE"] == DBNull.Value ? string.Empty : dr["TYPE"].ToString()
                            }).ToList();
            return cardList;
        }

        /// <summary>
        /// ColumnModel 赋值
        /// </summary> 
        private IList<ModelGeneration> GetColumnModel(IDataReader dr)
        {
            var modelList = new List<ModelGeneration>();

            while (dr.Read())
            {
                var model = new ModelGeneration();
                model.ColumnName = dr["COLUMN_NAME"] == DBNull.Value ? string.Empty : dr["COLUMN_NAME"].ToString();
                model.Prec = dr["PREC"] == DBNull.Value ? string.Empty : dr["PREC"].ToString();
                model.Scale = dr["SCALE"] == DBNull.Value ? string.Empty : dr["SCALE"].ToString();
                model.ColumnComments = dr["COLCOMMENTS"] == DBNull.Value ? string.Empty : dr["COLCOMMENTS"].ToString();
                model.DataType = dr["DATA_TYPE"] == DBNull.Value ? string.Empty : dr["DATA_TYPE"].ToString();
                model.DataLength = dr["DATA_LENGTH"] == DBNull.Value ? 0 : Convert.ToInt32(dr["DATA_LENGTH"]);
                model.IsNull = dr["NULLABLE"] == DBNull.Value ? true : dr["NULLABLE"].ToString() == "1" ? true : false;
                model.IsPrimaryKey = dr["PRIMARYKEY"] == DBNull.Value ? false : dr["PRIMARYKEY"].ToString() == "1" ? true : false;
                model.DataBaseName = string.Empty;
                modelList.Add(model);
            }

            return modelList;
        }

        ///<summary>
        /// ColumnModel 赋值
        /// </summary> 
        private IList<ModelGeneration> GetColumnModel(DataSet ds)
        {
            var cardList = (from DataRow dr in ds.Tables[0].Rows
                            select new ModelGeneration()
                            {
                                ColumnName = dr["COLUMN_NAME"] == DBNull.Value ? string.Empty : dr["COLUMN_NAME"].ToString(),
                                ColumnComments = dr["COLCOMMENTS"] == DBNull.Value ? string.Empty : dr["COLCOMMENTS"].ToString(),
                                DataType = dr["DATA_TYPE"] == DBNull.Value ? string.Empty : dr["DATA_TYPE"].ToString(),
                                DataLength = dr["DATA_LENGTH"] == DBNull.Value ? 0 : Convert.ToInt32(dr["DATA_LENGTH"]),
                                IsNull = dr["NULLABLE"] == DBNull.Value ? true : dr["NULLABLE"].ToString() == "1" ? true : false,
                                IsPrimaryKey = dr["PRIMARYKEY"] == DBNull.Value ? false : dr["PRIMARYKEY"].ToString() == "1" ? true : false,
                                DataBaseName = string.Empty
                            }).ToList();
            return cardList;
        }

        /// <summary>
        /// Sql 字段类型整理
        /// </summary>
        /// <param name="model">Sql字段对象</param>
        /// <returns></returns>
        private static string GetSqlTypeAndLength(ModelGeneration model)
        {
            switch (model.DataType.ToLower())
            {
                case "numeric":
                    return  string.Format("{0}({1},{2})",model.DataType,model.Prec,model.Scale);    
                case "char": 
                case "binary": 
                case "nchar":
                case "ntext":
                case "nvarchar":
                case "varbinary":
                case "varchar":
                case "variant":
                    return  string.Format("{0}({1})",model.DataType,model.DataLength);   
                case "bigint":
                case "bit":
                case "datetime":
                case "datetime2": 
                case "date": 
                case "datetimeoffset": 
                case "decimal": 
                case "float": 
                case "image": 
                case "int": 
                case "money": 
                case "real": 
                case "smalldatetime": 
                case "smallint": 
                case "smallmoney": 
                case "text": 
                case "time": 
                case "timestamp": 
                case "tinyint": 
                case "udt"://自定义的数据类型 
                case "uniqueidentifier":                 
                case "xml": 
                default:
                    return model.DataType;  
            }
        }

        #endregion

        /// <summary>
        /// 查询所有数据库名
        /// </summary> 
        public IList<string> QueryDataBaseAll(out string resultMsg)
        {
            IList<string> list = new List<string>();
            resultMsg = string.Empty;
            try
            {
                //SQL语句
                string strSql = "select [name] from [sysdatabases] order by [crdate] desc,[name]";

                //查询执行
                using (IDataReader dr = DBHelper.ExecuteReader(strSql, false))
                {
                    while (dr.Read())
                    {
                        var dataBaseName = dr["name"] == DBNull.Value ? string.Empty : dr["name"].ToString();
                        list.Add(dataBaseName);
                    }
                }
            }
            catch (Exception EX)
            {
                resultMsg = string.Format("{0} {1}", BaseDict.ErrorPrefix, EX.Message);
            }
            return list;
        }

        /// <summary>
        /// 查询所有表名集合
        /// </summary> 
        public IList<ModelGeneration> QueryTablesAll(out string resultMsg, GeneratorCriteria criteria)
        {
            IList<ModelGeneration> list = new List<ModelGeneration>();
            resultMsg = string.Empty;
            try
            {
                StringBuilder SQL = new StringBuilder();
                #region SQL拼接
                SQL.AppendFormat("USE {0} ", string.IsNullOrWhiteSpace(criteria.DataBaseName) ? "master" : criteria.DataBaseName);
                SQL.Append("SELECT C.NAME , ");
                SQL.Append("  C.TYPE , ");
                SQL.Append("  CAST(ISNULL(F.[VALUE], '') AS NVARCHAR(100)) AS REMARK ");
                SQL.Append("FROM SYS.OBJECTS C ");
                SQL.Append("LEFT JOIN SYS.EXTENDED_PROPERTIES F ");
                SQL.Append("ON F.MAJOR_ID  = C.OBJECT_ID ");
                SQL.Append("AND F.MINOR_ID = 0 ");
                SQL.Append("AND F.CLASS    = 1 ");
                SQL.Append("WHERE C.TYPE   = 'u' ");
                SQL.Append("OR C.TYPE      = 'v' ");
                SQL.Append("ORDER BY C.TYPE ,C.NAME");
                #endregion
                //存储过程名称
                string strSql = SQL.ToString();

                //查询执行
                using (IDataReader dr = DBHelper.ExecuteReader(strSql, false))
                {
                    list = GetTableModel(dr);
                }
            }
            catch (Exception EX)
            {
                resultMsg = string.Format("{0} {1}", BaseDict.ErrorPrefix, EX.Message);
            }
            return list;
        }

        /// <summary>
        /// 根据表名查询列集合
        /// </summary>
        /// <param name="tableName">表名</param> 
        public IList<ModelGeneration> QueryColumnsByTable(out string resultMsg, GeneratorCriteria criteria)
        {
            IList<ModelGeneration> list = new List<ModelGeneration>();
            resultMsg = string.Empty;
            try
            {
                //存储过程名称

                #region SQL语句

                string strSql = @"USE {0} 
                                SELECT  COL.name AS [COLUMN_NAME] ,
                                        COL.isnullable AS [NULLABLE] ,
                                        col.length AS [DATA_LENGTH] ,
                                        col.prec AS [PREC] ,
                                        col.scale AS [SCALE] ,
                                        SEP.Value AS [COLCOMMENTS] ,
                                        ST.name AS [DATA_TYPE] ,
                                        COLUMNPROPERTY(COL.id, COL.name, 'IsIdentity') AS [IsIdentity] ,
                                        [PRIMARYKEY] = CASE WHEN EXISTS ( SELECT    1
                                                                            FROM      sysobjects
                                                                            WHERE     xtype = 'PK'
                                                                                    AND parent_obj = COL.id
                                                                                    AND name IN (
                                                                                    SELECT  name
                                                                                    FROM    sysindexes
                                                                                    WHERE   indid IN (
                                                                                            SELECT
                                                                                                indid
                                                                                            FROM
                                                                                                sysindexkeys
                                                                                            WHERE
                                                                                                id = COL.id
                                                                                                AND colid = COL.colid ) ) )
                                                            THEN '1'
                                                            ELSE '0'
                                                        END
                                FROM    SysColumns COL
                                        LEFT JOIN sys.extended_properties SEP ON COL.id = SEP.major_id
                                                                                    AND COL.colid = SEP.minor_id
                                        LEFT JOIN systypes ST ON COL.xusertype = ST.xusertype
                                WHERE   COL.id = OBJECT_ID(@TableName)";

                strSql = string.Format(strSql, criteria.DataBaseName);

                #endregion 

                //参数添加
                IList<DBParameter> parm = new List<DBParameter>();
                parm.Add(new DBParameter() { ParameterName = "@TableName", ParameterValue = criteria.TableName, ParameterInOut = BaseDict.ParmIn, ParameterType = DbType.String });

                //查询执行
                using (IDataReader dr = DBHelper.ExecuteReader(strSql, false, parm))
                {
                    list = GetColumnModel(dr);
                }
            }
            catch (Exception EX)
            {
                resultMsg = string.Format("{0} {1}", BaseDict.ErrorPrefix, EX.Message);
            }
            return list;
        }

        /// <summary>
        /// SQL Detail 语句生成
        /// </summary>
        public string GenerateSqlForSelectDetail(out string resultMsg, GeneratorCriteria criteria)
        {
            resultMsg = string.Empty;

            //查询字段列表
            var colList = QueryColumnsByTable(out resultMsg, criteria);

            //查询主键字段列表
            var colPK = (from ModelGeneration model in colList
                         where model.IsPrimaryKey == true
                         select model).ToList();

            string classNamePrivate = CommonMethod.StringToPrivateVar(criteria.TableName);
            string classNamePublic = CommonMethod.StringToPublicVar(criteria.TableName);
            string strParameter = string.Empty;
            string strWhereSql = string.Empty;

            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("-- ===========================================================");
            sbSql.AppendLine("-- Author:        Jason.Yao");
            sbSql.AppendFormat("-- Create date:   {0}", DateTime.Now.ToString("yyyy-MM-dd"));
            sbSql.AppendLine();
            sbSql.AppendFormat("-- Description:   {0} SELECT DETAIL", criteria.TableName);
            sbSql.AppendLine();
            sbSql.AppendLine("-- ===========================================================");

            sbSql.AppendFormat("CREATE PROCEDURE  usp_{0}_select_detail_by", criteria.TableName.ToLower());
            foreach (var item in colPK)
            {
                sbSql.AppendFormat("_{0}", item.ColumnName.ToLower());
            }
            sbSql.AppendLine();
            foreach (var item in colPK)
            {
                var strSqlTypeAndLength = GetSqlTypeAndLength(item);
                if (string.IsNullOrWhiteSpace(strParameter))
                    strParameter += string.Format("  @{0}{1} {2}", criteria.SqlParameterPrefix, item.PublicVarName, strSqlTypeAndLength);
                else
                    strParameter += string.Format(",{0}  @{1}{2}  {3}", Environment.NewLine, criteria.SqlParameterPrefix, item.PublicVarName, strSqlTypeAndLength);
            }

            sbSql.AppendLine(strParameter);
            sbSql.AppendLine("AS");
            sbSql.AppendLine("    BEGIN ");
            sbSql.AppendFormat("        SELECT * FROM {0} WHERE ", criteria.TableName);
            foreach (var item in colPK)
            {
                if (string.IsNullOrWhiteSpace(strWhereSql))
                    strWhereSql += string.Format("[{0}] = @{1}{2} ", item.ColumnName, criteria.SqlParameterPrefix, item.PublicVarName);
                else
                    strWhereSql += string.Format(" AND [{0}] = @{1}{2} ", item.ColumnName, criteria.SqlParameterPrefix, item.PublicVarName);
            }
            sbSql.Append(strWhereSql);
            sbSql.AppendLine();
            sbSql.AppendLine("    END ");

            string sql = sbSql.ToString();
            return sql;
        }

        /// <summary>
        /// SQL Insert/Update 语句生成
        /// </summary> 
        public string GenerateSqlForInsertUpdate(out string resultMsg, GeneratorCriteria criteria)
        {
            resultMsg = string.Empty;

            //查询字段列表
            var colList = QueryColumnsByTable(out resultMsg, criteria);

            //查询主键字段列表
            var colPK = (from ModelGeneration model in colList
                         where model.IsPrimaryKey == true
                         select model).ToList();

            string classNamePrivate = CommonMethod.StringToPrivateVar(criteria.TableName);
            string classNamePublic = CommonMethod.StringToPublicVar(criteria.TableName);
            string strParameter = string.Empty;
            string strWhereSql = string.Empty;
            string strCols = string.Empty;

            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("-- ===========================================================");
            sbSql.AppendLine("-- Author:        Jason.Yao");
            sbSql.AppendFormat("-- Create date:   {0}", DateTime.Now.ToString("yyyy-MM-dd"));
            sbSql.AppendLine();
            sbSql.AppendFormat("-- Description:   {0} INSERT UPDATE", criteria.TableName);
            sbSql.AppendLine();
            sbSql.AppendLine("-- ===========================================================");

            sbSql.AppendFormat("CREATE PROCEDURE usp_{0}_insert_update ", criteria.TableName.ToLower());
            sbSql.AppendLine();
            foreach (var item in colList)
            {
                var strSqlTypeAndLength = GetSqlTypeAndLength(item);
                if (string.IsNullOrWhiteSpace(strParameter))
                    strParameter += string.Format("	@{0}{1} {2}", criteria.SqlParameterPrefix, item.PublicVarName, strSqlTypeAndLength);
                else
                    strParameter += string.Format(",{0}	@{1}{2} {3}", Environment.NewLine, criteria.SqlParameterPrefix, item.PublicVarName, strSqlTypeAndLength);
            }
            sbSql.AppendLine(strParameter);
            //sbSql.AppendLine("	@resultMsg NVARCHAR(500) OUT");  
            sbSql.AppendLine("AS");
            sbSql.AppendLine("BEGIN ");
            sbSql.AppendFormat("IF EXISTS(SELECT * FROM {0} WHERE ", criteria.TableName);
            foreach (var item in colPK)
            {
                if (string.IsNullOrWhiteSpace(strWhereSql))
                    strWhereSql += string.Format("[{0}] = @{1}{2} ", item.ColumnName, criteria.SqlParameterPrefix, item.PublicVarName);
                else
                    strWhereSql += string.Format(" AND [{0}] = @{1}{2} ", item.ColumnName, criteria.SqlParameterPrefix, item.PublicVarName);
            }
            sbSql.Append(strWhereSql);
            sbSql.Append(")");
            sbSql.AppendLine();
            sbSql.AppendLine("	BEGIN");
            sbSql.AppendFormat("		UPDATE  {0} ", criteria.TableName);
            sbSql.AppendLine();
            sbSql.AppendLine("		SET");

            strParameter = string.Empty;
            foreach (var item in colList)
            {
                if (string.IsNullOrWhiteSpace(strParameter))
                {
                    if (item.IsNull)
                        strParameter += string.Format("		[{0}] = @{1}{2} ", criteria.SqlParameterPrefix, item.PublicVarName);
                    else
                        strParameter += string.Format("		[{0}] = CASE WHEN @{1}{2} IS NOT NULL THEN @{1}{2} ELSE [{0}] END  ",
                                            item.ColumnName, criteria.SqlParameterPrefix, item.PublicVarName );
                }
                else
                {
                    if(item.IsNull)
                        strParameter += string.Format(",{0}		[{1}] =  @{2}{3} ",
                            Environment.NewLine, item.ColumnName, criteria.SqlParameterPrefix, item.PublicVarName );
                    else
                        strParameter += string.Format(",{0}		[{1}] = CASE WHEN @{2}{3} IS NOT NULL THEN @{2}{3} ELSE [{1}] END ",
                            Environment.NewLine, item.ColumnName, criteria.SqlParameterPrefix, item.PublicVarName );
                }
            }
            sbSql.AppendLine(strParameter);

            sbSql.AppendFormat("		WHERE {0}  ", strWhereSql);
            sbSql.AppendLine();
            sbSql.AppendLine("	END ");
            sbSql.AppendLine("	ELSE ");
            sbSql.AppendLine("	BEGIN ");
            sbSql.AppendFormat("		INSERT INTO  {0}   ( ", criteria.TableName);
            sbSql.AppendLine();
            foreach (var item in colList)
            {
                if (string.IsNullOrWhiteSpace(strCols))
                    strCols += string.Format("		[{0}]", item.ColumnName);
                else
                    strCols += string.Format(",{0}		[{1}]  ", Environment.NewLine, item.ColumnName);
            }
            sbSql.AppendLine(strCols);
            sbSql.AppendLine("		) VALUES ( ");
            strCols = string.Empty;
            foreach (var item in colList)
            {
                if (string.IsNullOrWhiteSpace(strCols))
                    strCols += string.Format("		@{0}{1}", criteria.SqlParameterPrefix, item.PublicVarName);
                else
                    strCols += string.Format(",{0}		@{1}{2}  ", Environment.NewLine, criteria.SqlParameterPrefix, item.PublicVarName);
            }
            sbSql.AppendLine(strCols);
            sbSql.AppendLine("		) ");
            sbSql.AppendLine("	END");
            sbSql.AppendLine("END");
            //sbSql.AppendLine("IF @@ERROR <> 0 ");
            //sbSql.AppendLine("BEGIN ");
            //sbSql.AppendLine("	SET @resultMsg = 'Error:Failed to InsertUpdate charge.' ");
            //sbSql.AppendLine("	RETURN ");
            //sbSql.AppendLine("END ");
            //sbSql.AppendLine("ELSE ");
            //sbSql.AppendLine("BEGIN ");
            //sbSql.AppendLine("	SET @resultMsg = 'Success' "); 
            //sbSql.AppendLine("END;");

            string sql = sbSql.ToString();
            return sql;
        }

        /// <summary>
        /// SQL UpdateStatus 语句生成
        /// </summary> 
        public string GenerateSqlForUpdateStatus(out string resultMsg, GeneratorCriteria criteria)
        {
            resultMsg = string.Empty;

            // 查询字段列表
            var colList = QueryColumnsByTable(out resultMsg, criteria);

            // 查询主键字段列表
            var colPK = (from ModelGeneration model in colList
                         where model.IsPrimaryKey == true
                         select model).ToList();

            string classNamePrivate = CommonMethod.StringToPrivateVar(criteria.TableName);          // 类名 私有
            string classNamePublic = CommonMethod.StringToPublicVar(criteria.TableName);            // 类名 公有
            string strParameter = string.Empty;
            string strWhereSql = string.Empty;
            string strCols = string.Empty;

            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("-- ===========================================================");
            sbSql.AppendLine("-- Author:        Jason.Yao");
            sbSql.AppendFormat("-- Create date:   {0}", DateTime.Now.ToString("yyyy-MM-dd"));
            sbSql.AppendLine();
            sbSql.AppendFormat("-- Description:   {0} UPDATE STATUS", criteria.TableName);
            sbSql.AppendLine();
            sbSql.AppendLine("-- ===========================================================");

            sbSql.AppendFormat("CREATE PROCEDURE usp_{0}_update_status ", criteria.TableName.ToLower());
            sbSql.AppendLine();
            foreach (var item in colList)
            {
                if (item.IsPrimaryKey || item.ColumnName.ToUpper().Contains("STATUS") || item.ColumnName.ToUpper().Contains("DELETE"))
                {
                    var strSqlTypeAndLength = GetSqlTypeAndLength(item);
                    sbSql.AppendFormat("	@{0}{1} {2}, ", criteria.SqlParameterPrefix, item.PublicVarName, strSqlTypeAndLength);
                    sbSql.AppendLine();
                }
            }
            sbSql.AppendLine("	@resultMsg NVARCHAR(500) OUT");
            sbSql.AppendLine("AS");
            sbSql.AppendLine("BEGIN ");
            sbSql.AppendFormat("IF EXISTS(SELECT * FROM {0} WHERE ", criteria.TableName);
            foreach (var item in colPK)
            {
                if (string.IsNullOrWhiteSpace(strWhereSql))
                    strWhereSql += string.Format("[{0}] = @{1}{2} ", item.ColumnName, criteria.SqlParameterPrefix, item.PublicVarName);
                else
                    strWhereSql += string.Format(" AND [{0}] = @{1}{2} ", item.ColumnName, criteria.SqlParameterPrefix, item.PublicVarName);
            }
            sbSql.Append(strWhereSql);
            sbSql.Append(")");
            sbSql.AppendLine();
            sbSql.AppendLine("	BEGIN");
            sbSql.AppendFormat("		UPDATE  {0} ", criteria.TableName);
            sbSql.AppendLine();
            sbSql.AppendLine("		SET");

            strParameter = string.Empty;
            foreach (var item in colList)
            {
                if (item.ColumnName.ToUpper().Contains("STATUS") || item.ColumnName.ToUpper().Contains("DELETE"))
                {
                    if (string.IsNullOrWhiteSpace(strParameter))
                        strParameter += string.Format("		[{0}] = CASE WHEN @{1}{2} IS NOT NULL THEN @{1}{3} ELSE [{4}] END  ",
                        item.ColumnName, criteria.SqlParameterPrefix, item.PublicVarName, item.PublicVarName, item.ColumnName);
                    else
                        strParameter += string.Format(",{0}		[{1}] = CASE WHEN @{2}{3} IS NOT NULL THEN @{2}{4} ELSE [{5}] END ",
                        Environment.NewLine, item.ColumnName, criteria.SqlParameterPrefix, item.PublicVarName,item.PublicVarName, item.ColumnName);
                }
            }
            sbSql.AppendLine(strParameter);
            sbSql.AppendFormat("		WHERE {0}  ", strWhereSql);
            sbSql.AppendLine();
            sbSql.AppendFormat("		SET @resultMsg = '{0}{1}' ", BaseDict.SuccessPrefix, BaseDict.Success);
            sbSql.AppendLine();
            sbSql.AppendLine("	END ");
            sbSql.AppendLine("	ELSE ");
            sbSql.AppendLine("	BEGIN ");
            sbSql.AppendFormat("		SET @resultMsg = '{0}{1}' ", BaseDict.ErrorPrefix, BaseDict.SqlExMsgNoData);
            sbSql.AppendLine();
            sbSql.AppendLine("	END");
            sbSql.AppendLine("END");

            string sql = sbSql.ToString();
            return sql;
        }

        /// <summary>
        /// SQL Delete 语句生成
        /// </summary> 
        public string GenerateSqlForDelete(out string resultMsg, GeneratorCriteria criteria)
        {
            resultMsg = string.Empty;

            //查询字段列表
            var colList = QueryColumnsByTable(out resultMsg, criteria);

            //查询主键字段列表
            var colPK = (from ModelGeneration model in colList
                         where model.IsPrimaryKey == true
                         select model).ToList();

            string classNamePrivate = CommonMethod.StringToPrivateVar(criteria.TableName);
            string classNamePublic = CommonMethod.StringToPublicVar(criteria.TableName);
            string strParameter = string.Empty;
            string strWhereSql = string.Empty;
            string strCols = string.Empty;

            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("-- ===========================================================");
            sbSql.AppendLine("-- Author:        Jason.Yao");
            sbSql.AppendFormat("-- Create date:   {0}", DateTime.Now.ToString("yyyy-MM-dd"));
            sbSql.AppendLine();
            sbSql.AppendFormat("-- Description:   {0} DELETE", criteria.TableName);
            sbSql.AppendLine();
            sbSql.AppendLine("-- ===========================================================");

            sbSql.AppendFormat("CREATE PROCEDURE usp_{0}_delete ", criteria.TableName.ToLower());
            sbSql.AppendLine();
            foreach (var item in colList)
            {
                if (item.IsPrimaryKey)
                {
                    var strSqlTypeAndLength = GetSqlTypeAndLength(item);
                    sbSql.AppendFormat("	@{0}{1} {2}, ", criteria.SqlParameterPrefix, item.PublicVarName, strSqlTypeAndLength);
                    sbSql.AppendLine();
                }
            }
            sbSql.AppendLine("	@resultMsg NVARCHAR(500) OUT");
            sbSql.AppendLine("AS");
            sbSql.AppendLine("BEGIN ");
            sbSql.AppendFormat("IF EXISTS(SELECT * FROM {0} WHERE ", criteria.TableName);
            foreach (var item in colPK)
            {
                if (string.IsNullOrWhiteSpace(strWhereSql))
                    strWhereSql += string.Format("[{0}] = @{1}{2} ", item.ColumnName, criteria.SqlParameterPrefix, item.PublicVarName);
                else
                    strWhereSql += string.Format(" AND [{0}] = @{1}{2} ", item.ColumnName, criteria.SqlParameterPrefix, item.PublicVarName);
            }
            sbSql.Append(strWhereSql);
            sbSql.Append(")");
            sbSql.AppendLine();
            sbSql.AppendLine("	BEGIN");
            sbSql.AppendFormat("		DELETE  {0} ", criteria.TableName);
            sbSql.AppendLine();
            sbSql.AppendFormat("		WHERE {0}  ", strWhereSql);
            sbSql.AppendLine();
            sbSql.AppendFormat("		SET @resultMsg = '{0}{1}' ", BaseDict.SuccessPrefix, BaseDict.Success);
            sbSql.AppendLine();
            sbSql.AppendLine("	END ");
            sbSql.AppendLine("	ELSE ");
            sbSql.AppendLine("	BEGIN ");
            sbSql.AppendFormat("		SET @resultMsg = '{0}{1}' ", BaseDict.ErrorPrefix, BaseDict.SqlExMsgNoData);
            sbSql.AppendLine();
            sbSql.AppendLine("	END");
            sbSql.AppendLine("END");

            string sql = sbSql.ToString();
            return sql;
        }

        /// <summary>
        /// SQL Select Pager 语句生成
        /// </summary> 
        public string GenerateSqlForSelectPager(out string resultMsg, GeneratorCriteria criteria)
        {
            resultMsg = string.Empty;

            //查询字段列表
            var colList = QueryColumnsByTable(out resultMsg, criteria);

            //查询主键字段列表
            var colPK = (from ModelGeneration model in colList
                         where model.IsPrimaryKey == true
                         select model).ToList();

            string classNamePrivate = CommonMethod.StringToPrivateVar(criteria.TableName);
            string classNamePublic = CommonMethod.StringToPublicVar(criteria.TableName);
            string strParameter = string.Empty;
            string strWhereSql = string.Empty;
            string strCols = string.Empty;

            string strParmPagerIndex = string.Format("{0}PagerIndex", criteria.SqlParameterPrefix);
            string strParmPagerSize = string.Format("{0}PagerSize", criteria.SqlParameterPrefix);
            string strParmRowCount = string.Format("{0}RowCount", criteria.SqlParameterPrefix);
            string strParmTotalPages = string.Format("{0}TotalPages", criteria.SqlParameterPrefix);
            string strParmStartRow = string.Format("{0}StartRow", criteria.SqlParameterPrefix);
            string strParmEndRow = string.Format("{0}EndRow", criteria.SqlParameterPrefix);

            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("-- ===========================================================");
            sbSql.AppendLine("-- Author:        Jason.Yao");
            sbSql.AppendFormat("-- Create date:   {0}", DateTime.Now.ToString("yyyy-MM-dd"));
            sbSql.AppendLine();
            sbSql.AppendFormat("-- Description:   {0} DELETE", criteria.TableName);
            sbSql.AppendLine();
            sbSql.AppendLine("-- ===========================================================");

            sbSql.AppendFormat("CREATE PROCEDURE usp_{0}_select_pager ", criteria.TableName.ToLower());
            sbSql.AppendLine();
            sbSql.AppendFormat("	@{0} INT , ", strParmPagerIndex);
            sbSql.AppendLine();
            sbSql.AppendFormat("	@{0} INT , ", strParmPagerSize);
            sbSql.AppendLine();
            sbSql.AppendFormat("	@{0} INT OUT ,", strParmRowCount);
            sbSql.AppendLine();
            sbSql.AppendFormat("	@{0} INT OUT ", strParmTotalPages);
            sbSql.AppendLine();
            sbSql.AppendLine("AS");
            sbSql.AppendLine("BEGIN ");
            sbSql.AppendFormat("	DECLARE @{0} INT ,@{1} INT", strParmStartRow, strParmEndRow);
            sbSql.AppendLine();
            sbSql.AppendFormat("	SET @{0} = (", strParmRowCount);
            sbSql.AppendLine();
            sbSql.AppendFormat("                    SELECT COUNT(*) FROM {0}", criteria.TableName);
            sbSql.AppendLine();
            sbSql.AppendLine("                      --WHERE 语句"); 
            sbSql.AppendFormat("                    )", criteria.TableName);
            sbSql.AppendLine();
            sbSql.AppendFormat("	SET @{0} = CEILING(CONVERT(FLOAT, @{1}) / @{2})",
                strParmTotalPages, strParmRowCount, strParmPagerSize);
            sbSql.AppendLine();
            sbSql.AppendFormat("	IF @{0} > @{1} ", strParmPagerIndex, strParmTotalPages);
            sbSql.AppendLine();
            sbSql.AppendFormat("		SET @{0} = @{1}", strParmPagerIndex, strParmTotalPages);
            sbSql.AppendLine();
            sbSql.AppendFormat("	IF @{0} < 1 ", strParmPagerIndex);
            sbSql.AppendLine();
            sbSql.AppendFormat("		SET @{0} = 1", strParmPagerIndex);
            sbSql.AppendLine();
            sbSql.AppendFormat("	SET @{0} = ( @{1} - 1 ) * @{2} + 1", strParmStartRow, strParmPagerIndex, strParmPagerSize);
            sbSql.AppendLine();
            sbSql.AppendFormat("	IF @{0} > @{1} * @{2} ", strParmRowCount, strParmPagerIndex, strParmPagerSize);
            sbSql.AppendLine();
            sbSql.AppendFormat("		SET @{0} = @{1} * @{2} ", strParmEndRow, strParmPagerIndex, strParmPagerSize);
            sbSql.AppendLine();
            sbSql.AppendLine("	ELSE ");
            sbSql.AppendFormat("		SET @{0} = @{1} ; ", strParmEndRow, strParmRowCount);
            sbSql.AppendLine();
            sbSql.AppendLine("	WITH temptbl AS ( ");
            sbSql.Append("					SELECT ROW_NUMBER() OVER ( ORDER BY ");

            strCols = string.Empty;
            foreach (var item in colPK)
            {
                if (string.IsNullOrWhiteSpace(strCols))
                    strCols += string.Format(" [{0}] ", item.ColumnName);
                else
                    strCols += string.Format(", [{0}] ",item.ColumnName);
            }
            sbSql.Append(strCols);
            sbSql.Append(" DESC ) AS 'row_no' ,* ");
            sbSql.AppendLine();
            sbSql.AppendFormat("					FROM {0} ",criteria.TableName);
            sbSql.AppendLine();
            sbSql.AppendLine("					--WHERE --where 条件");
            sbSql.AppendLine("					)");
            sbSql.AppendLine("	SELECT  * FROM temptbl ");
            sbSql.AppendFormat("	WHERE row_no BETWEEN @{0} AND @{1}", strParmStartRow, strParmEndRow);
            sbSql.AppendLine();
            sbSql.AppendLine("END");

            string sql = sbSql.ToString();
            return sql;
        }

        /// <summary>
        /// SQL Select All 语句生成
        /// </summary> 
        public string GenerateSqlForSelectAll(out string resultMsg, GeneratorCriteria criteria)
        {
            resultMsg = string.Empty;

            //查询字段列表
            var colList = QueryColumnsByTable(out resultMsg, criteria);

            //查询主键字段列表
            var colPK = (from ModelGeneration model in colList
                         where model.IsPrimaryKey == true
                         select model).ToList();

            string classNamePrivate = CommonMethod.StringToPrivateVar(criteria.TableName);
            string classNamePublic = CommonMethod.StringToPublicVar(criteria.TableName);
            string strParameter = string.Empty;
            string strWhereSql = string.Empty;

            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("-- ===========================================================");
            sbSql.AppendLine("-- Author:        Jason.Yao");
            sbSql.AppendFormat("-- Create date:   {0}", DateTime.Now.ToString("yyyy-MM-dd"));
            sbSql.AppendLine();
            sbSql.AppendFormat("-- Description:   {0} SELECT ALL", criteria.TableName);
            sbSql.AppendLine();
            sbSql.AppendLine("-- ===========================================================");

            sbSql.AppendFormat("CREATE PROCEDURE  usp_{0}_select_all", criteria.TableName.ToLower());             
            sbSql.AppendLine();
             
            sbSql.AppendLine(strParameter);
            sbSql.AppendLine("AS");
            sbSql.AppendLine("BEGIN ");
            sbSql.AppendFormat("    SELECT * FROM {0}  ", criteria.TableName); 
            sbSql.AppendLine();
            sbSql.AppendLine("END ");

            string sql = sbSql.ToString();
            return sql;
        }
    }
}
