using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Library.Common
{
    public class CommonMethod
    {
        /// <summary>
        /// sql 数据类型字符串获取 C#数据类型
        /// </summary> 
        public static Type SqlTypeToCsharpType(string sqlType)
        {
            switch (sqlType.ToLower())
            {
                case "bit":
                    return typeof(Boolean);
                case "nchar":
                case "ntext":
                case "nvarchar":
                case "nvarchar2":
                case "char":
                case "text":
                case "varchar":
                case "varchar2":
                case "nclob":
                case "clob":
                case "long":
                case "rowid":
                    return typeof(String);
                case "datetime":
                case "datetime2":
                case "date":
                case "datetimeoffset":
                case "smalldatetime":
                case "time":
                case "timestamp":
                    return typeof(DateTime);
                case "interval day to second":
                    return typeof(TimeSpan);
                case "money":
                case "decimal":
                case "smallmoney":
                case "number":
                case "integer":
                    return typeof(Decimal);
                case "float":
                    return typeof(Double);
                case "smallint":
                    return typeof(Int16);
                case "int":
                case "interval year to month":
                    return typeof(int);
                case "bigint":
                    return typeof(Int64);
                case "real":
                case "binary_float":
                    return typeof(Single);
                case "tinyint":
                    return typeof(Byte);
                case "bfile":
                case "blob":
                case "long raw":
                case "raw":
                    return typeof(Byte[]);
                case "uniqueidentifier":
                    return typeof(Guid);
                case "binary":
                case "image": 
                case "udt"://自定义的数据类型
                case "varbinary":
                case "variant":
                case "xml":
                default:
                    return typeof(Object);
            }
        }

        /// <summary>
        /// sql 数据类型字符串获取 C#数据类型
        /// </summary> 
        public static string SqlTypeToCsharpTypeString(string sqlType)
        {
            switch (sqlType.ToLower())
            {
                case "bit":
                    return "DbType.Boolean";
                case "nchar":
                case "ntext":
                case "nvarchar":
                case "nvarchar2":
                case "char":
                case "text":
                case "varchar":
                case "varchar2":
                case "nclob":
                case "clob":
                case "long":
                case "rowid":
                    return "DbType.String";
                case "datetime":
                case "datetime2":
                case "date":
                case "datetimeoffset":
                case "smalldatetime":
                case "time":
                case "timestamp":
                case "interval day to second":
                    return "DbType.DateTime"; 
                case "money":
                case "decimal":
                case "smallmoney":
                case "number":
                case "integer":
                    return "DbType.Decimal";
                case "float":
                    return "DbType.Double";
                case "smallint":
                    return "DbType.Int16";
                case "int":
                case "interval year to month":
                    return "DbType.Int32";
                case "bigint":
                    return "DbType.Int64";
                case "real":
                case "binary_float":
                    return "DbType.Single";
                case "tinyint":
                    return "DbType.Byte"; 
                case "uniqueidentifier":
                    return "DbType.Guid";
                case "binary":
                case "image":
                case "udt"://自定义的数据类型
                case "varbinary":
                case "variant":
                case "xml":
                case "bfile":
                case "blob":
                case "long raw":
                case "raw":
                default:
                    return "DbType.Object";
            }
        }

        /// <summary>
        /// C#数据类型 转字符串
        /// </summary> 
        public static string CsharpTypeToString(Type cSharpType)
        {
            switch (cSharpType.Name.ToLower())
            {
                case "bool":
                case "boolean":
                    return "DbType.Boolean";
                case "string":  
                case "char":
                    return "DbType.String";
                case "datetime":
                case "timespan":
                    return "DbType.DateTime";
                case "decimal":
                    return "DbType.Decimal"; 
                case "double":
                    return "DbType.Double";
                case "int16":
                    return "DbType.Int16";
                case "int":
                case "int32": 
                    return "DbType.Int32";
                case "int64":
                    return "DbType.Int64";
                case "single": 
                    return "DbType.Single";
                case "byte":
                    return "DbType.Byte"; 
                case "guid":
                    return "DbType.Guid";                 
                case "object": 
                default:
                    return "DbType.Object";
            }
        } 

        /// <summary>
        /// 数据读取以及类型转换
        /// </summary>
        /// <param name="sqlType">数据库数据类型</param>
        /// <param name="value">数据值</param>
        /// <returns>转换后字符串</returns>
        public static string GetDataValueIsNotNull(string sqlType, string value)
        {
            switch (sqlType)
            {
                case "Int64":
                    return value + " == DBNull.Value ? 0 : Convert.ToInt64(" + value + ")";               
                case "Boolean":
                    return value + " == DBNull.Value ? false : Convert.ToBoolean(" + value + ")";
                case "String":
                    return value + "== DBNull.Value ? string.Empty : " + value + ".ToString()";
                case "DateTime":
                    return value + "== DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(" + value + ")";
                case "Decimal":
                    return value + "== DBNull.Value ? decimal.Zero : Convert.ToDecimal(" + value + ")";
                case "float":
                    return value + "== DBNull.Value ? 0 : Convert.ToDouble(" + value + ")";
                case "Int32":
                    return value + "== DBNull.Value ? 0 : Convert.ToInt32(" + value + ")";
                case "money":
                    return value + "== DBNull.Value ? decimal.Zero : Convert.ToDecimal(" + value + ")";
                case "Int16":
                    return value + "== DBNull.Value ? 0 : Convert.ToInt16(" + value + ")";
                case "Byte":
                    return value + "== DBNull.Value ? null : Convert.ToByte(" + value + ")";
                case "Guid":
                    return value + "== DBNull.Value ? null : new Guid(" + value + ")";
                case "Object": 
                default:
                    return value + "== DBNull.Value ? null : (Object)" + value;
            }
        }

        /// <summary>
        /// 字符串转换为公共变量模式
        /// </summary> 
        public static string StringToPublicVar(string value)
        {
            string strObjName = string.Empty;
            if (!string.IsNullOrWhiteSpace(value))
            {
                string[] strTableName = value.Split('_');
                for (int i = 0; i < strTableName.Length; i++)
                {
                    strObjName += !string.IsNullOrWhiteSpace(strTableName[i]) && strTableName[i].Length > 1 ?
                        strTableName[i].Substring(0, 1).ToUpper() + strTableName[i].Substring(1).ToLower() : strTableName[i].ToUpper();
                }
            }
            return strObjName;
        } 

        /// <summary>
        /// 字符串转换为私有变量模式
        /// </summary> 
        public static string StringToPrivateVar(string value)
        {
            string strObjName = string.Empty;
            if (!string.IsNullOrWhiteSpace(value))
            {
                string[] strTableName = value.Split('_');
                for (int i = 0; i < strTableName.Length; i++)
                {
                    strObjName += !string.IsNullOrWhiteSpace(strTableName[i]) && strTableName[i].Length > 1 ?
                        strTableName[i].Substring(0, 1).ToLower() + strTableName[i].Substring(1).ToLower() : strTableName[i].ToLower();
                }
            }
            return strObjName; 
        }

        /// <summary>
        /// 获取数据验证 正则表达式 字符串
        /// </summary>
        public static string GetRegularExpression(string vaule)
        {
            switch (vaule.ToLower())
            {
                case "int":
                case "int32":
                case "int64":
                    return "[RegularExpression(@\"^[0-9]*$\", ErrorMessage = \"{0}输入错误！\")]";
                default:
                    return "";

            }

        }

        /// <summary>
        /// 引用类型 转 静态类型
        /// </summary>
        /// <param name="typeName">类型名称</param>
        /// <returns>string</returns>
        public static string TypeConversion(string typeName)
        {
            switch (typeName)
            {
                case "Boolean":
                    return "bool";
                case "Double":
                    return "double";
                case "Single":
                    return "float";
                case "Uint32":
                    return "uint";
                case "Uint64":
                    return "ulong";
                case "Byte":
                    return "byte";
                case "Decimal":
                    return "decimal";
                case "Int64":
                    return "long";
                case "Int32":
                    return "int";
                case "Int16":
                    return "short";
                case "String":
                    return "string";
                case "Object":
                    return "object";
                default:
                    return typeName;
            }
        }
    }
}
