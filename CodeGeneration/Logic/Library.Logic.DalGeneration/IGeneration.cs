using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Models;

namespace Library.Logic.DalGeneration
{
    public interface IGeneration
    {
        /// <summary>
        /// 查询所有数据库名
        /// </summary> 
        IList<string> QueryDataBaseAll(out string resultMsg);

        /// <summary>
        /// 查询所有表名集合
        /// </summary> 
        IList<ModelGeneration> QueryTablesAll(out string resultMsg, GeneratorCriteria criteria=null);

        /// <summary>
        /// 根据表名查询列集合
        /// </summary>
        IList<ModelGeneration> QueryColumnsByTable(out string resultMsg, GeneratorCriteria criteria);

        /// <summary>
        /// SQL Detail 语句生成
        /// </summary>
        string GenerateSqlForSelectDetail(out string resultMsg, GeneratorCriteria criteria);

        /// <summary>
        /// SQL Insert/Update 语句生成
        /// </summary> 
        string GenerateSqlForInsertUpdate(out string resultMsg, GeneratorCriteria criteria);

        /// <summary>
        /// SQL UpdateStatus 语句生成
        /// </summary> 
        string GenerateSqlForUpdateStatus(out string resultMsg, GeneratorCriteria criteria);

        /// <summary>
        /// SQL Delete 语句生成
        /// </summary> 
        string GenerateSqlForDelete(out string resultMsg, GeneratorCriteria criteria);

        /// <summary>
        /// SQL Select Pager 语句生成
        /// </summary> 
        string GenerateSqlForSelectPager(out string resultMsg, GeneratorCriteria criteria);

        /// <summary>
        /// SQL Select All 语句生成
        /// </summary> 
        string GenerateSqlForSelectAll(out string resultMsg, GeneratorCriteria criteria);
    }
}
