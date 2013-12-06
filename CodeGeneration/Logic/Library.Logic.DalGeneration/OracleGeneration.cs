using Library.Common;
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
    public class OracleGeneration : IGeneration
    {
        #region 私有函数

        // <summary>
        /// TableModel 赋值
        /// </summary> 
        private IList<ModelGeneration> GetTableModel(IDataReader dr)
        {
            var modelList = new List<ModelGeneration>();

            while (dr.Read())
            {
                var model = new ModelGeneration();
                model.TableName = dr["TABLE_NAME"] == DBNull.Value ? string.Empty : dr["TABLE_NAME"].ToString();
                model.TableComments = dr["COMMENTS"] == DBNull.Value ? string.Empty : dr["COMMENTS"].ToString();
                model.TableType = dr["TABLE_TYPE"] == DBNull.Value ? string.Empty : dr["TABLE_TYPE"].ToString();
                modelList.Add(model);
            }

            return modelList;
        }

        // <summary>
        /// TableModel 赋值
        /// </summary> 
        private IList<ModelGeneration> GetTableModel(DataSet ds)
        {
            var cardList = (from DataRow dr in ds.Tables[0].Rows
                            select new ModelGeneration()
                            {
                                TableName = dr["TABLE_NAME"] == DBNull.Value ? string.Empty : dr["TABLE_NAME"].ToString(),
                                TableComments = dr["COMMENTS"] == DBNull.Value ? string.Empty : dr["COMMENTS"].ToString(),
                                TableType = dr["TABLE_TYPE"] == DBNull.Value ? string.Empty : dr["TABLE_TYPE"].ToString()
                            }).ToList();
            return cardList;
        }

        // <summary>
        /// ColumnModel 赋值
        /// </summary> 
        private IList<ModelGeneration> GetColumnModel(IDataReader dr)
        {
            var modelList = new List<ModelGeneration>();

            while (dr.Read())
            {
                var model = new ModelGeneration();
                model.TableName = dr["TABLE_NAME"] == DBNull.Value ? string.Empty : dr["TABLE_NAME"].ToString();
                model.TableComments = dr["TABCOMMENTS"] == DBNull.Value ? string.Empty : dr["TABCOMMENTS"].ToString();
                model.TableType = dr["TABLE_TYPE"] == DBNull.Value ? string.Empty : dr["TABLE_TYPE"].ToString();
                model.ColumnId = dr["COLUMN_ID"] == DBNull.Value ? string.Empty : dr["COLUMN_ID"].ToString();
                model.ColumnName = dr["COLUMN_NAME"] == DBNull.Value ? string.Empty : dr["COLUMN_NAME"].ToString();
                model.ColumnComments = dr["COLCOMMENTS"] == DBNull.Value ? string.Empty : dr["COLCOMMENTS"].ToString();
                model.DataType = dr["DATA_TYPE"] == DBNull.Value ? string.Empty : dr["DATA_TYPE"].ToString();
                model.DataLength = dr["DATA_LENGTH"] == DBNull.Value ? 0 : Convert.ToInt32(dr["DATA_LENGTH"]);
                model.IsNull = dr["NULLABLE"] == DBNull.Value ? true : dr["NULLABLE"].ToString() == "Y" ? true : false;
                model.IsPrimaryKey = dr["PRIMARYKEY"] == DBNull.Value ? false : dr["PRIMARYKEY"].ToString() == "1" ? true : false;
                model.DataBaseName = string.Empty;
                modelList.Add(model);
            }

            return modelList;
        }

        // <summary>
        /// ColumnModel 赋值
        /// </summary> 
        private IList<ModelGeneration> GetColumnModel(DataSet ds)
        {
            var cardList = (from DataRow dr in ds.Tables[0].Rows
                            select new ModelGeneration()
                            {
                                TableName = dr["TABLE_NAME"] == DBNull.Value ? string.Empty : dr["TABLE_NAME"].ToString(),
                                TableComments = dr["TABCOMMENTS"] == DBNull.Value ? string.Empty : dr["TABCOMMENTS"].ToString(),
                                TableType = dr["TABLE_TYPE"] == DBNull.Value ? string.Empty : dr["TABLE_TYPE"].ToString(),
                                ColumnId = dr["COLUMN_ID"] == DBNull.Value ? string.Empty : dr["COLUMN_ID"].ToString(),
                                ColumnName = dr["COLUMN_NAME"] == DBNull.Value ? string.Empty : dr["COLUMN_NAME"].ToString(),
                                ColumnComments = dr["COLCOMMENTS"] == DBNull.Value ? string.Empty : dr["COLCOMMENTS"].ToString(),
                                DataType = dr["DATA_TYPE"] == DBNull.Value ? string.Empty : dr["DATA_TYPE"].ToString(),
                                DataLength = dr["DATA_LENGTH"] == DBNull.Value ? 0 : Convert.ToInt32(dr["DATA_LENGTH"]),
                                IsNull = dr["NULLABLE"] == DBNull.Value ? true : dr["NULLABLE"].ToString() == "Y" ? true : false,
                                IsPrimaryKey = dr["PRIMARYKEY"] == DBNull.Value ? false : dr["PRIMARYKEY"].ToString() == "1" ? true : false,
                                DataBaseName = string.Empty
                            }).ToList();
            return cardList;
        }

        #endregion

        /// <summary>
        /// 查询所有数据库名
        /// </summary> 
        public IList<string> QueryDataBaseAll(out string resultMsg)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 查询所有表名集合
        /// </summary> 
        public IList<ModelGeneration> QueryTablesAll(out string resultMsg, GeneratorCriteria criteria = null)
        {
            IList<ModelGeneration> list = new List<ModelGeneration>();
            resultMsg = string.Empty;
            try
            {
                //存储过程名称
                string strSql = "SELECT * FROM USER_TAB_COMMENTS ";

                //查询执行
                using (IDataReader dr = DBHelper.ExecuteReader(strSql, false))
                {
                    list = GetTableModel(dr);
                }
            }
            catch (Exception EX)
            {
                resultMsg = string.Format("{0} {1}", BaseDict.ErrorPrefix, EX.ToString());
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

                #region SQL拼接

                StringBuilder SQL = new StringBuilder();

                SQL.Append("SELECT ");
                SQL.Append("  CASE ");
                SQL.Append("    WHEN F.COLUMN_NAME = A.COLUMN_NAME ");
                SQL.Append("    THEN 1 ");
                SQL.Append("    ELSE 0 ");
                SQL.Append("  END AS PRIMARYKEY, ");
                SQL.Append("  C.TABLE_TYPE, ");
                SQL.Append("  C.COMMENTS TABCOMMENTS, ");
                SQL.Append("  B.COMMENTS COLCOMMENTS, ");
                SQL.Append("  A.* ");
                SQL.Append("FROM USER_TAB_COLUMNS A ");
                SQL.Append("INNER JOIN USER_COL_COMMENTS B ");
                SQL.Append("ON A.TABLE_NAME   = B.TABLE_NAME ");
                SQL.Append("AND A.TABLE_NAME  =:TABLE_NAME ");
                SQL.Append("AND A.COLUMN_NAME = B.COLUMN_NAME ");
                SQL.Append("INNER JOIN USER_TAB_COMMENTS C ");
                SQL.Append("ON C.TABLE_NAME = A.TABLE_NAME ");
                SQL.Append("LEFT JOIN ");
                SQL.Append("  (SELECT COL.* ");
                SQL.Append("  FROM USER_CONSTRAINTS CON, ");
                SQL.Append("    USER_CONS_COLUMNS COL ");
                SQL.Append("  WHERE CON.CONSTRAINT_NAME=COL.CONSTRAINT_NAME ");
                SQL.Append("  AND CON.CONSTRAINT_TYPE  ='P' ");
                SQL.Append("  AND COL.TABLE_NAME       =:TABLE_NAME ");
                SQL.Append("  ) F ");
                SQL.Append("ON F.COLUMN_NAME = A.COLUMN_NAME ");
                #endregion

                string sql = SQL.ToString();

                //参数添加
                IList<DBParameter> parm = new List<DBParameter>();
                parm.Add(new DBParameter() { ParameterName = "TABLE_NAME", ParameterValue = criteria.TableName, ParameterInOut = BaseDict.ParmIn, ParameterType = DbType.String });

                //查询执行
                using (IDataReader dr = DBHelper.ExecuteReader(sql, false, parm))
                {
                    list = GetColumnModel(dr);
                }
            }
            catch (Exception EX)
            {
                resultMsg = string.Format("{0} {1}", BaseDict.ErrorPrefix, EX.ToString());
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

            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("-- ===========================================================");
            sbSql.AppendLine("-- Author:        Jason.Yao");
            sbSql.AppendFormat("-- Create date:   {0}", DateTime.Now.ToString("yyyy-MM-dd"));
            sbSql.AppendLine();
            sbSql.AppendFormat("-- Description:   {0} SELECT DETAIL", criteria.TableName);
            sbSql.AppendLine();
            sbSql.AppendLine("-- ===========================================================");

            sbSql.AppendFormat("CREATE OR REPLACE PROCEDURE USP_{0}_SELECT_DETAIL_BY", criteria.TableName.ToUpper());
            foreach (var item in colPK)
            {
                sbSql.AppendFormat("_{0}", item.ColumnName.ToUpper());
            }
            sbSql.Append("(");
            sbSql.AppendLine();
            foreach (var item in colPK)
            {
                sbSql.AppendFormat("  {0}{1} IN {2},", criteria.SqlParameterPrefix, item.PublicVarName, item.DataType);
                sbSql.AppendLine();
            }
            sbSql.Append("  CUR_OUT OUT SYS_REFCURSOR");
            sbSql.AppendLine(")");
            sbSql.AppendLine("IS");
            sbSql.AppendLine("BEGIN ");
            sbSql.AppendFormat("  OPEN CUR_OUT FOR SELECT * FROM {0} WHERE ", criteria.TableName);
            foreach (var item in colPK)
            {
                if (string.IsNullOrWhiteSpace(strParameter))
                    strParameter += string.Format("{0} = {1}{2} ", item.ColumnName, criteria.SqlParameterPrefix, item.PublicVarName);
                else
                    strParameter += string.Format(" AND {0} = {1}{2} ", item.ColumnName, criteria.SqlParameterPrefix, item.PublicVarName);
            }
            sbSql.Append(strParameter);
            sbSql.Append(";");
            sbSql.AppendLine();
            sbSql.AppendLine("END;");

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

            sbSql.AppendFormat("CREATE OR REPLACE PROCEDURE USP_{0}_INSERT_UPDATE(", criteria.TableName.ToUpper());  
            sbSql.AppendLine();
            foreach (var item in colList)
            {
                if(string.IsNullOrWhiteSpace(strParameter))
                    strParameter += string.Format("  {0}{1} IN {2}", criteria.SqlParameterPrefix, item.PublicVarName, item.DataType);
                else
                    strParameter += string.Format(",{0}  {1}{2} IN {3}", Environment.NewLine,criteria.SqlParameterPrefix, item.PublicVarName, item.DataType);
            }
            sbSql.AppendLine(strParameter);
            //sbSql.Append("  resultMsg OUT VARCHAR2");
            sbSql.AppendLine(")");
            sbSql.AppendLine("IS");
            sbSql.AppendLine("  ROWCOUNT NUMBER; ");
            sbSql.AppendLine("BEGIN ");
            sbSql.AppendFormat("  SELECT COUNT(*) INTO ROWCOUNT FROM {0} WHERE  ",criteria.TableName);
            foreach (var item in colPK)
            {
                if (string.IsNullOrWhiteSpace(strWhereSql))
                    strWhereSql += string.Format("{0} = {1}{2} ", item.ColumnName, criteria.SqlParameterPrefix, item.PublicVarName);
                else
                    strWhereSql += string.Format(" AND {0} = {1}{2} ", item.ColumnName, criteria.SqlParameterPrefix, item.PublicVarName);
            }
            sbSql.Append(strWhereSql);
            sbSql.Append(";");
            sbSql.AppendLine();
            sbSql.AppendLine("  IF ROWCOUNT > 0 THEN ");
            sbSql.AppendFormat("    UPDATE  {0} ", criteria.TableName);
            sbSql.AppendLine();
            sbSql.AppendLine("    SET");

            strParameter = string.Empty;
            foreach (var item in colList)
            {
                if (string.IsNullOrWhiteSpace(strParameter))
                    strParameter += string.Format("  {0} = CASE WHEN {1}{2} IS NOT NULL THEN {3}{4} ELSE {5} END  ",
                    item.ColumnName, criteria.SqlParameterPrefix, item.PublicVarName, criteria.SqlParameterPrefix, item.PublicVarName, item.ColumnName);
                else
                    strParameter += string.Format(",{0}  {1} = CASE WHEN {2}{3} IS NOT NULL THEN {4}{5} ELSE {6} END ",
                    Environment.NewLine, item.ColumnName, criteria.SqlParameterPrefix, item.PublicVarName, criteria.SqlParameterPrefix, item.PublicVarName, item.ColumnName); 
                
            } 
            sbSql.AppendLine(strParameter);
           
            sbSql.AppendFormat("    WHERE {0}; ",strWhereSql);
            sbSql.AppendLine();
            sbSql.AppendLine("  ELSE "); 
            sbSql.AppendFormat("    INSERT INTO  {0} ", criteria.TableName);
            sbSql.AppendLine();
            sbSql.AppendLine("      ( ");
            foreach (var item in colList)
            {
                if (string.IsNullOrWhiteSpace(strCols))
                    strCols += string.Format("        {0}",item.ColumnName);
                else
                    strCols += string.Format(",{0}        {1}  ", Environment.NewLine,item.ColumnName);                 
            }
            sbSql.AppendLine(strCols);
            sbSql.AppendLine("      ) ");
            sbSql.AppendLine("      VALUES ");
            sbSql.AppendLine("      ( ");
            strCols = string.Empty;
            foreach (var item in colList)
            {
                if (string.IsNullOrWhiteSpace(strCols))
                    strCols += string.Format("        {0}{1}", criteria.SqlParameterPrefix, item.PublicVarName);
                else
                    strCols += string.Format(",{0}        {1}{2}  ", Environment.NewLine, criteria.SqlParameterPrefix , item.PublicVarName);                 
            }
            sbSql.AppendLine(strCols);
            sbSql.AppendLine("      ); ");
            sbSql.AppendLine("  END IF; ");
            sbSql.AppendLine("END;");

            string sql = sbSql.ToString();
            return sql;
        }

        /// <summary>
        /// SQL UpdateStatus 语句生成
        /// </summary> 
        public string GenerateSqlForUpdateStatus(out string resultMsg, GeneratorCriteria criteria)
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
            sbSql.AppendFormat("-- Description:   {0} UPDATE STATUS", criteria.TableName);
            sbSql.AppendLine();
            sbSql.AppendLine("-- ===========================================================");

            sbSql.AppendFormat("CREATE OR REPLACE PROCEDURE USP_{0}_UPDATE_STATUS(", criteria.TableName.ToUpper());
            sbSql.AppendLine();
            foreach (var item in colList)
            {
                if (item.IsPrimaryKey || item.ColumnName.ToUpper().Contains("STATUS") || item.ColumnName.ToUpper().Contains("DELETE"))
                {
                    sbSql.AppendFormat("  {0}{1} IN {2},", criteria.SqlParameterPrefix, item.PublicVarName, item.DataType);
                    sbSql.AppendLine();                   
                }
            }
            sbSql.Append("  resultMsg OUT VARCHAR2");
            sbSql.AppendLine(")");
            sbSql.AppendLine("IS");
            sbSql.Append("  ROWCOUNT NUMBER; ");
            sbSql.Append("BEGIN ");
            sbSql.AppendFormat("  SELECT COUNT(*) INTO ROWCOUNT FROM {0} WHERE  ", criteria.TableName);
            foreach (var item in colPK)
            {
                if (string.IsNullOrWhiteSpace(strWhereSql))
                    strWhereSql += string.Format("{0} = {1}{2} ", item.ColumnName, criteria.SqlParameterPrefix, item.PublicVarName);
                else
                    strWhereSql += string.Format(" AND {0} = {1}{2} ", item.ColumnName, criteria.SqlParameterPrefix, item.PublicVarName);
            }
            sbSql.Append(strWhereSql);
            sbSql.Append(";");
            sbSql.AppendLine();
            sbSql.AppendLine("  IF ROWCOUNT > 0 THEN ");
            sbSql.AppendFormat("    UPDATE {0} ",criteria.TableName);
            sbSql.AppendLine();
            sbSql.AppendLine("    SET");

            strParameter = string.Empty;
            foreach (var item in colList)
            {
                if (item.ColumnName.ToUpper().Contains("STATUS") || item.ColumnName.ToUpper().Contains("DELETE"))
                {
                    if (string.IsNullOrWhiteSpace(strParameter))
                        strParameter += string.Format("  {0} = CASE WHEN {1}{2} IS NOT NULL THEN {3}{4} ELSE {5} END  ",
                        item.ColumnName, criteria.SqlParameterPrefix, item.PublicVarName, criteria.SqlParameterPrefix, item.PublicVarName, item.ColumnName);
                    else
                        strParameter += string.Format(",{0}  {1} = CASE WHEN {2}{3} IS NOT NULL THEN {4}{5} ELSE {6} END ",
                        Environment.NewLine, item.ColumnName, criteria.SqlParameterPrefix, item.PublicVarName, criteria.SqlParameterPrefix, item.PublicVarName, item.ColumnName);
                }
            }
            sbSql.AppendLine(strParameter);
            sbSql.AppendFormat("    WHERE {0}; ", strWhereSql);
            sbSql.AppendLine();
            sbSql.AppendFormat("      resultMsg:='{0}{1}'; ", BaseDict.SuccessPrefix, BaseDict.Success);
            sbSql.AppendLine();
            sbSql.AppendLine("  ELSE ");
            sbSql.AppendFormat("    resultMsg :='{0}{1}' ; ",BaseDict.ErrorPrefix,BaseDict.SqlExMsgNoData);
            sbSql.AppendLine();
            sbSql.AppendLine("  END IF; ");
            sbSql.AppendLine("END;");

            string sql = sbSql.ToString();
            return sql;
        }

        /// <summary>
        /// SQL UpdateStatus 语句生成
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

            sbSql.AppendFormat("CREATE OR REPLACE PROCEDURE USP_{0}_DELETE(", criteria.TableName.ToUpper());
            sbSql.AppendLine();
            foreach (var item in colPK)
            { 
                    sbSql.AppendFormat("  {0}{1} IN {2},", criteria.SqlParameterPrefix, item.PublicVarName, item.DataType);
                    sbSql.AppendLine(); 
            }
            sbSql.Append("  resultMsg OUT VARCHAR2");
            sbSql.AppendLine(")");
            sbSql.AppendLine("IS");
            sbSql.Append("  ROWCOUNT NUMBER; ");
            sbSql.Append("BEGIN ");
            sbSql.AppendFormat("  SELECT COUNT(*) INTO ROWCOUNT FROM {0} WHERE  ", criteria.TableName);
            foreach (var item in colPK)
            {
                if (string.IsNullOrWhiteSpace(strWhereSql))
                    strWhereSql += string.Format("{0} = {1}{2} ", item.ColumnName, criteria.SqlParameterPrefix, item.PublicVarName);
                else
                    strWhereSql += string.Format(" AND {0} = {1}{2} ", item.ColumnName, criteria.SqlParameterPrefix, item.PublicVarName);
            }
            sbSql.Append(strWhereSql);
            sbSql.Append(";");
            sbSql.AppendLine();
            sbSql.AppendLine("  IF ROWCOUNT > 0 THEN ");
            sbSql.AppendFormat("    DELETE FROM {0}  ", criteria.TableName);             
            sbSql.AppendFormat("    WHERE {0}; ", strWhereSql);
            sbSql.AppendLine();
            sbSql.AppendFormat("      resultMsg:='{0}{1}'; ", BaseDict.SuccessPrefix, BaseDict.Success);
            sbSql.AppendLine();
            sbSql.AppendLine("  ELSE ");
            sbSql.AppendFormat("    resultMsg :='{0}{1}' ; ", BaseDict.ErrorPrefix, BaseDict.SqlExMsgNoData);
            sbSql.AppendLine();
            sbSql.AppendLine("  END IF; ");
            sbSql.AppendLine("END;");

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

            string strParmPagerIndex = string.Format(" {0}PagerIndex", criteria.SqlParameterPrefix);
            string strParmPagerSize = string.Format(" {0}PagerSize", criteria.SqlParameterPrefix);
            string strParmRowCount = string.Format(" {0}RowCount", criteria.SqlParameterPrefix);
            string strParmTotalPages = string.Format(" {0}TotalPages", criteria.SqlParameterPrefix);
            string strParmStartRow = string.Format(" {0}StartRow", criteria.SqlParameterPrefix);
            string strParmEndRow = string.Format(" {0}EndRow", criteria.SqlParameterPrefix); 

            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("-- ===========================================================");
            sbSql.AppendLine("-- Author:        Jason.Yao");
            sbSql.AppendFormat("-- Create date:   {0}", DateTime.Now.ToString("yyyy-MM-dd"));
            sbSql.AppendLine();
            sbSql.AppendFormat("-- Description:   {0} SELECT PAGER", criteria.TableName);
            sbSql.AppendLine();
            sbSql.AppendLine("-- ===========================================================");

            sbSql.AppendFormat("CREATE OR REPLACE PROCEDURE USP_{0}_SELECT_PAGER",criteria.TableName);
            sbSql.Append("(");
            sbSql.AppendLine();
            sbSql.AppendFormat("    {0} IN NUMBER,", strParmPagerIndex);
            sbSql.AppendLine();
            sbSql.AppendFormat("    {0} IN NUMBER,", strParmPagerSize);
            sbSql.AppendLine();
            sbSql.AppendFormat("    {0} OUT NUMBER,", strParmRowCount);
            sbSql.AppendLine(); 
            sbSql.Append("  CUR_OUT OUT SYS_REFCURSOR");
            sbSql.AppendLine(")");
            sbSql.AppendLine("IS");
            sbSql.AppendFormat("  {0} NUMBER :=0;", strParmTotalPages);
            sbSql.AppendLine();
            sbSql.AppendFormat("  {0} NUMBER := 0;", strParmStartRow);
            sbSql.AppendLine();
            sbSql.AppendFormat("  {0} NUMBER :=0;", strParmEndRow);
            sbSql.AppendLine(); 
            sbSql.AppendLine("BEGIN ");
            sbSql.AppendFormat("  SELECT COUNT(*) INTO {0} FROM {1} ; ", strParmRowCount,criteria.TableName);            
            sbSql.AppendLine();
            sbSql.AppendFormat("  {0} := CEIL({1} / {2}); ", strParmTotalPages, strParmRowCount, strParmPagerSize);
            sbSql.AppendLine();
            sbSql.AppendFormat("  {0}   := ({1}   - 1 ) * {2} + 1; ", strParmStartRow, strParmPagerIndex, strParmPagerSize);
            sbSql.AppendLine();
            sbSql.AppendFormat("  {0}  :=  CASE WHEN {1} > {2}*{3} THEN ({4} * {5}) ELSE {6} END ;",
                strParmEndRow, strParmRowCount, strParmPagerIndex, strParmPagerSize, strParmPagerIndex, strParmPagerSize, strParmRowCount);
            sbSql.AppendLine();
            sbSql.AppendLine("  OPEN CUR_OUT FOR SELECT * FROM ("); 
            sbSql.AppendFormat("    SELECT * FROM {0} -- Where 条件 ,Order By 排序 放这里",criteria.TableName);
            sbSql.AppendLine();
            sbSql.AppendFormat("  )WHERE ROWNUM BETWEEN {0} AND {1} ;", strParmStartRow, strParmEndRow);
            sbSql.AppendLine();
            sbSql.AppendLine("END;");

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

            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("-- ===========================================================");
            sbSql.AppendLine("-- Author:        Jason.Yao");
            sbSql.AppendFormat("-- Create date:   {0}", DateTime.Now.ToString("yyyy-MM-dd"));
            sbSql.AppendLine();
            sbSql.AppendFormat("-- Description:   {0} SELECT ALL", criteria.TableName);
            sbSql.AppendLine();
            sbSql.AppendLine("-- ===========================================================");

            sbSql.AppendFormat("CREATE OR REPLACE PROCEDURE USP_{0}_SELECT_ALL", criteria.TableName.ToUpper());            
            sbSql.Append("(");
            sbSql.AppendLine(); 
            sbSql.Append("  CUR_OUT OUT SYS_REFCURSOR");
            sbSql.AppendLine(")");
            sbSql.AppendLine("IS");
            sbSql.AppendLine("BEGIN ");
            sbSql.AppendFormat("  OPEN CUR_OUT FOR SELECT * FROM {0} ; ", criteria.TableName);            
            sbSql.AppendLine();
            sbSql.AppendLine("END;");

            string sql = sbSql.ToString();
            return sql;
        }
    }
}
