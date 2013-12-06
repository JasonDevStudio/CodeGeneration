using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Logic.DalGeneration
{
    public class GeneratorCriteria
    {
        /// <summary>
        /// Model层命名空间
        /// </summary>
        public string ModelsNamespace{get;set;} 
        
        /// <summary>
        /// DAL层命名空间
        /// </summary>
        public string DalNamespace{get;set;}

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName{get;set;} 
        
        /// <summary>
        /// 数据库类型
        /// </summary>
        public string DataBaseType{get;set;}
        
        /// <summary>
        /// 数据库名称
        /// </summary>
        public string DataBaseName {get;set;}

        /// <summary>
        /// DAL类名前缀
        /// </summary>
        public string DalClassNamePrefix{get;set; }

        /// <summary>
        /// Model类名前缀
        /// </summary>
        public string ModelClassNamePrefix { get; set; }

        /// <summary>
        /// SQL参数前缀
        /// </summary>
        public string SqlParameterPrefix { get; set; }

        /// <summary>
        /// 存储过程名称前缀
        /// </summary>
        public string SqlProcedurePrefix { get; set; }
    }
}
