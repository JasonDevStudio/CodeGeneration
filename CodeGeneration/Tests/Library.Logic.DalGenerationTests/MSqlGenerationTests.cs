using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library.Models;
using Library.Logic.DalGeneration;
namespace Library.Logic.DalGenerationTests
{
    [TestClass()]
    public class MSqlGenerationTests
    {
        IGeneration dal = new MSqlGeneration();

        [TestMethod()]
        public void QueryDataBaseAllTest()
        {
            string errorMsg = string.Empty;
            IList<string> res1 = null;
            var res = dal.QueryDataBaseAll(out errorMsg);
            Assert.AreNotEqual(res, res1);
            var PublicTableName = string.Empty;
            var publicTName = res[0];
            Assert.AreNotEqual(PublicTableName, publicTName);
        }

        [TestMethod()]
        public void QueryTablesAllTest()
        {
            string errorMsg = string.Empty;
            var criteria = new GeneratorCriteria();
            criteria.DataBaseName = "CardMaintain";
            IList<ModelGeneration> res1 = null;
            var res = dal.QueryTablesAll(out errorMsg, criteria);
            Assert.AreNotEqual(res, res1);
            var PublicTableName = string.Empty;
            var publicTName = res[0].TableName;
            Assert.AreNotEqual(PublicTableName, publicTName);
        }

        [TestMethod()]
        public void QueryColumnsByTableTest()
        {
            string errorMsg = string.Empty;
            var criteria = new GeneratorCriteria();
            criteria.DataBaseName = "CardMaintain";
            criteria.TableName = "Bank";
            IList<ModelGeneration> res1 = null;
            var res = dal.QueryColumnsByTable(out errorMsg, criteria);
            Assert.AreNotEqual(res, res1);
            var PublicColName = string.Empty;
            var publicCName = res[0].PublicVarName;
            var pk = res[0].IsPrimaryKey;
            Assert.AreNotEqual(PublicColName, publicCName);
        }

        /// <summary>
        /// SQL Select Detail 存储过程代码生成
        /// </summary>
        [TestMethod()]
        public void GenerateSqlForSelectDetailTest()
        {
            string errorMsg = string.Empty;
            var criteria = new GeneratorCriteria();
            criteria.DataBaseName = "Invoice_System";
            criteria.TableName = "st_user_detail";
            IList<ModelGeneration> res1 = null;
            var res = dal.GenerateSqlForSelectDetail(out errorMsg, criteria);
            Assert.AreNotEqual(res, res1); 
        }

        /// <summary>
        ///SQL Insert/Update 语句生成
        /// </summary>
        [TestMethod()]
        public void GenerateSqlForInsertUpdateTest()
        {
            string errorMsg = string.Empty;
            var criteria = new GeneratorCriteria();
            criteria.DataBaseName = "Invoice_System";
            criteria.TableName = "st_user_detail";
            IList<ModelGeneration> res1 = null;
            var res = dal.GenerateSqlForInsertUpdate(out errorMsg, criteria);
            Assert.AreNotEqual(res, res1);
        }

        /// <summary>
        ///SQL Update Status 语句生成
        /// </summary>
        [TestMethod()]
        public void GenerateSqlForUpdateStatusTest()
        {
            string errorMsg = string.Empty;
            var criteria = new GeneratorCriteria();
            criteria.DataBaseName = "Invoice_System";
            criteria.TableName = "st_user_detail";
            IList<ModelGeneration> res1 = null;
            var res = dal.GenerateSqlForUpdateStatus(out errorMsg, criteria);
            Assert.AreNotEqual(res, res1);
        }

        /// <summary>
        ///SQL Update Status 语句生成
        /// </summary>
        [TestMethod()]
        public void GenerateSqlForDeleteTest()
        {
            string errorMsg = string.Empty;
            var criteria = new GeneratorCriteria();
            criteria.DataBaseName = "Invoice_System";
            criteria.TableName = "st_user_detail";
            IList<ModelGeneration> res1 = null;
            var res = dal.GenerateSqlForDelete(out errorMsg, criteria);
            Assert.AreNotEqual(res, res1);
        }

        /// <summary>
        ///SQL Select Pager 语句生成
        /// </summary>
        [TestMethod()]
        public void GenerateSqlForSelectPagerTest()
        {
            string errorMsg = string.Empty;
            var criteria = new GeneratorCriteria();
            criteria.DataBaseName = "Invoice_System";
            criteria.TableName = "st_user_detail";
            criteria.SqlParameterPrefix = null;
            IList<ModelGeneration> res1 = null;
            var res = dal.GenerateSqlForSelectPager(out errorMsg, criteria);
            Assert.AreNotEqual(res, res1);
        }

        /// <summary>
        ///SQL Select Pager 语句生成
        /// </summary>
        [TestMethod()]
        public void GenerateSqlForSelectAllPagerTest()
        {
            string errorMsg = string.Empty;
            var criteria = new GeneratorCriteria();
            criteria.DataBaseName = "Invoice_System";
            criteria.TableName = "st_user_detail";
            criteria.SqlParameterPrefix = null;
            IList<ModelGeneration> res1 = null;
            var res = dal.GenerateSqlForSelectAll(out errorMsg, criteria);
            Assert.AreNotEqual(res, res1);
        }
    }
}
