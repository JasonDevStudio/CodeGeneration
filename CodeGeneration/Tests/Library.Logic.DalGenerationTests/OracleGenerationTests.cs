using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library.Models;
using Library.Logic.DalGeneration; 

namespace Library.Logic.DalGeneration.Tests
{
    [TestClass()]
    public class OracleGenerationTests
    {
        IGeneration dal = new OracleGeneration();

        /// <summary>
        /// 查询所有表名集合
        /// </summary>
        [TestMethod()]
        public void QueryTablesAllTest()
        {
            string errorMsg = string.Empty;
            IList<ModelGeneration> res1 = null;            
            var res = dal.QueryTablesAll(out errorMsg);
            Assert.AreNotEqual(res, res1);
            var PublicTableName = string.Empty;
            var publicTName = res[0].PublicVarName;
            Assert.AreNotEqual(PublicTableName, publicTName);
        }

        /// <summary>
        /// 根据表名查询列集合
        /// </summary> 
        [TestMethod()]
        public void QueryColumnsByTableTest()
        {
            string resultMsg = string.Empty;
            var criteria = new GeneratorCriteria();
            criteria.TableName = "SHIP_INFO";
            IList<ModelGeneration> res1 = null;
            var res = dal.QueryColumnsByTable(out resultMsg, criteria);
            Assert.AreNotEqual(res, res1);
            var PublicColName = string.Empty;
            var publicCName = res[0].PublicVarName;
            var pk = res[0].IsPrimaryKey;
            Assert.AreNotEqual(PublicColName, publicCName);
        }


        [TestMethod()]
        public void GenerateSqlSelectDetailTest()
        {
            string resultMsg = string.Empty;
            var criteria = new GeneratorCriteria();
            criteria.TableName = "TUSERCOMP";
            criteria.SqlParameterPrefix = "v_";
            var res = dal.GenerateSqlForSelectDetail(out resultMsg, criteria);
            var res1 = string.Empty;
            Assert.AreNotEqual(res, res1);

        }

        [TestMethod()]
        public void GenerateSqlInsertUpdateTest()
        {
            string resultMsg = string.Empty;
            var criteria = new GeneratorCriteria();
            criteria.TableName = "TUSERCOMP";
            criteria.SqlParameterPrefix = "v_";
            var res = dal.GenerateSqlForInsertUpdate(out resultMsg, criteria);
            var res1 = string.Empty;
            Assert.AreNotEqual(res, res1);
        }

        [TestMethod()]
        public void GenerateSqlUpdateStatusTest()
        {
            string resultMsg = string.Empty;
            var criteria = new GeneratorCriteria();
            criteria.TableName = "TUSERCOMP";
            criteria.SqlParameterPrefix = "v_";
            var res = dal.GenerateSqlForUpdateStatus(out resultMsg, criteria);
            var res1 = string.Empty;
            Assert.AreNotEqual(res, res1);
        }

        [TestMethod()]
        public void GenerateSqlDeleteTest()
        {
            string resultMsg = string.Empty;
            var criteria = new GeneratorCriteria();
            criteria.TableName = "TUSERCOMP";
            criteria.SqlParameterPrefix = "v_";
            var res = dal.GenerateSqlForDelete(out resultMsg, criteria);
            var res1 = string.Empty;
            Assert.AreNotEqual(res, res1);
        }

        [TestMethod()]
        public void GenerateSqlForSelectPagerTest()
        {
            string resultMsg = string.Empty;
            var criteria = new GeneratorCriteria();
            criteria.TableName = "TUSERCOMP";
            criteria.SqlParameterPrefix = "v_";
            var res = dal.GenerateSqlForSelectPager(out resultMsg, criteria);
            var res1 = string.Empty;
            Assert.AreNotEqual(res, res1);
        }

        [TestMethod()]
        public void GenerateSqlForSelectAllPagerTest()
        {
            string resultMsg = string.Empty;
            var criteria = new GeneratorCriteria();
            criteria.TableName = "TUSERCOMP";
            criteria.SqlParameterPrefix = "v_";
            var res = dal.GenerateSqlForSelectAll(out resultMsg, criteria);
            var res1 = string.Empty;
            Assert.AreNotEqual(res, res1);
        }
    }
}
