using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class ModelGeneration
    {
        /// <summary>
        /// 数据库名
        /// </summary>
        [Display(Name="数据库名")]
        public string DataBaseName { get; set; }

        /// <summary>
        /// 表名
        /// </summary>
        [Display(Name="表名")]
        public string TableName { get; set; }

        /// <summary>
        /// 表注释
        /// </summary>
        [Display(Name = "表名")]
        public string TableComments { get;set; }

        /// <summary>
        /// 表类型
        /// </summary>
        [Display(Name="表类型")]
        public string TableType { get; set; }

        /// <summary>
        /// 列ID
        /// </summary>
        [Display(Name="列ID")]
        public string ColumnId { get; set; }

        /// <summary>
        /// 列名
        /// </summary>
        [Display(Name="列名")]
        public string ColumnName { get; set; }

        /// <summary>
        /// 列注释 
        /// </summary>
        [Display(Name = "列名")]
        public string ColumnComments { get; set; }

        /// <summary>
        /// 主键 
        /// </summary>
        [Display(Name = "主键")]
        public bool IsPrimaryKey { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        [Display(Name="数据类型")]
        public string DataType { get; set; }

        /// <summary>
        /// 数据长度
        /// </summary>
        [Display(Name="数据长度")]
        public int DataLength { get; set; }

        /// <summary>
        /// 精度
        /// </summary>
        [Display(Name = "精度")]
        public string Prec { get; set; }

        /// <summary>
        /// 小数位数
        /// </summary>
        [Display(Name = "小数位数")]
        public string Scale { get; set; }

        /// <summary>
        /// 是否允许空
        /// </summary>
        [Display(Name="是否允许空")]
        public bool IsNull { get; set; }

        /// <summary>
        /// 私有列变量名
        /// </summary>
        [Display(Name="私有列变量名")]
        public string PrivateVarName
        {
            get
            {
                string strObjName = string.Empty; 
                    strObjName = !string.IsNullOrWhiteSpace(PublicVarName) && PublicVarName.Length > 1 ?
                            PublicVarName.Substring(0, 1).ToLower() + PublicVarName.Substring(1).ToLower() : PublicVarName.ToLower(); 
                return strObjName; 
            }
        }

        /// <summary>
        /// 公有列变量名
        /// </summary>
        [Display(Name = "公有列变量名")]
        public string PublicVarName
        {
            get
            {
                string strObjName = string.Empty;
                if (!string.IsNullOrWhiteSpace(ColumnName))
                {
                    string[] strColumnName = ColumnName.Split('_');
                    for (int i = 0; i < strColumnName.Length; i++)
                    {
                        strObjName += !string.IsNullOrWhiteSpace(strColumnName[i]) && strColumnName[i].Length > 1 ?
                            strColumnName[i].Substring(0, 1).ToUpper() + strColumnName[i].Substring(1).ToLower() : strColumnName[i].ToUpper();
                    }
                }
                return strObjName;
            }
        }

        /// <summary>
        /// 公共类名(表名)
        /// </summary>
        [Display(Name = "类名")]
        public string PublicClassName
        {
            get
            {
                string strObjName = string.Empty;
                if (!string.IsNullOrWhiteSpace(TableName))
                {
                    string[] strTableName = ColumnName.Split('_');
                    for (int i = 0; i < strTableName.Length; i++)
                    {
                        strObjName += !string.IsNullOrWhiteSpace(strTableName[i]) && strTableName[i].Length > 1 ?
                            strTableName[i].Substring(0, 1).ToUpper() + strTableName[i].Substring(1).ToLower() : strTableName[i].ToUpper();
                    }
                }
                return strObjName;
            }
        }
    }
}
