using Library.Common;
using Library.Logic.DalGeneration;
using Library.Models;
using Library.StringItemDict;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Facade.CodeGenerator
{
    public class CodeGenerators
    {
        /// <summary>
        /// 根据数据库类型 实例化接口对象
        /// </summary>
        /// <param name="dataBaseType">数据库类型</param>
        /// <returns></returns>
        private IGeneration CreateInstance(string dataBaseType)
        {
            IGeneration gen = null;
            if (string.IsNullOrWhiteSpace(dataBaseType) || dataBaseType.Equals(BaseDict.SqlServerData))
                gen = new MSqlGeneration();
            else if (dataBaseType.Equals(BaseDict.OracleData))
                gen = new OracleGeneration();
            return gen;
        }

        public string IFacadeCodeGeneration(out string resultMsg, string tableName, string dataBaseType = BaseDict.SqlServerData, string dataBaseName = null,
            string modelsNamespace = null, string modelClassNamePrefix = "Model", string dalNamespace = null, string dalClassNamePrefix = "Logic",
            string facadeNamespace = null, string FacadeClassNamePrefix = "Facade")
        {
            resultMsg = string.Empty;
            var criteria = new GeneratorCriteria();
            criteria.DataBaseType = dataBaseType;
            criteria.TableName = tableName;
            criteria.ModelsNamespace = modelsNamespace;
            criteria.DalNamespace = dalNamespace;
            criteria.DalClassNamePrefix = dalClassNamePrefix;
            criteria.ModelClassNamePrefix = modelClassNamePrefix;
            criteria.DataBaseName = dataBaseName; 

            IGeneration gen = CreateInstance(criteria.DataBaseType);

            var colList = gen.QueryColumnsByTable(out resultMsg, criteria);

            string classNamePrivate = CommonMethod.StringToPrivateVar(criteria.TableName);
            string classNamePublic = CommonMethod.StringToPublicVar(criteria.TableName);
            string dbName = CommonMethod.StringToPublicVar(dataBaseName);
            var strParameter = string.Empty; //参数

            //主键列集合
            var colPK = (from ModelGeneration col in colList
                         where col.IsPrimaryKey == true
                         select col).ToList();

            StringBuilder sb = new StringBuilder();
            #region Top
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using System.Linq;");
            sb.AppendLine("using System.Text;");
            sb.AppendLine("using System.Data;");
            sb.AppendLine("using Library.Kernel.DataBaseHelper;");
            sb.AppendLine("using Library.StringItemDict;");
            sb.AppendLine("using Library.Common;");
            sb.AppendLine("using System.Data.Common;");

            if (string.IsNullOrWhiteSpace(modelsNamespace))
                sb.AppendFormat("using Library.Models.{0}", dbName);
            else
                sb.AppendFormat("using Library.{0}", criteria.ModelsNamespace);

            sb.AppendLine();

            if (string.IsNullOrWhiteSpace(criteria.DalNamespace))
            {
                sb.AppendFormat("using Library.{0}", "Logics");
            }
            else
            {
                sb.AppendFormat("using " + criteria.DalNamespace + ".Interfaces", dbName);
                sb.AppendLine();
                sb.AppendFormat("using " + criteria.DalNamespace + ".Classes", dbName);
            }

            sb.AppendLine();

            if (string.IsNullOrWhiteSpace(criteria.DalNamespace))
                sb.AppendFormat("namespace Library.{0}", "Facades");
            else
                sb.AppendFormat("namespace " + facadeNamespace + ".Interfaces", dbName);

            sb.AppendLine();
            sb.AppendLine("{");
            sb.AppendFormat("    public interface I{0}{1}", FacadeClassNamePrefix, classNamePublic);
            sb.AppendLine();
            sb.AppendLine("    {");
            #endregion
            #region Pager
            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// 分页查询 ");
            sb.AppendLine("        /// </summary>");
            sb.AppendLine("        /// <param name=\"recordCount\">输出参数 数据总数</param>");
            sb.AppendLine("        /// <param name=\"criteria\">查询条件对象</param>");
            sb.AppendLine("        /// <param name=\"pageSize\">每页显示数量</param>");
            sb.AppendLine("        /// <param name=\"pageIndex\">当前页索引</param>");
            sb.AppendLine("        /// <returns>结果集 泛型</returns>");
            sb.AppendFormat("        public IList<{0}{1}> Query{1}ListPager(out string resultMsg, out decimal recordCount, Criteria{1} criteria, int pageSize = 10, int pageIndex = 1)",
                criteria.ModelClassNamePrefix, classNamePublic);
            sb.AppendLine();
            sb.AppendLine();
            #endregion
            #region Detail
            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        ///  查询实体");
            sb.AppendLine("        /// </summary>");

            foreach (var item in colPK)
            {
                string dataTypeName = CommonMethod.SqlTypeToCsharpType(item.DataType).Name;
                sb.AppendFormat("        /// <param name=\"{0}\">{1}{2} {3}</param>", item.PrivateVarName,
                    criteria.ModelClassNamePrefix, item.PublicVarName, item.ColumnComments);
                sb.AppendLine();
            }
            sb.AppendFormat("        /// <returns>{0}{1}</returns>", criteria.ModelClassNamePrefix, classNamePublic);
            sb.AppendLine();
            sb.AppendFormat("        {0}{1} {2}Detail(out string resultMsg,", criteria.ModelClassNamePrefix, classNamePublic, classNamePublic);

            foreach (var item in colPK)
            {
                string dataTypeName = CommonMethod.SqlTypeToCsharpType(item.DataType).Name;
                if (string.IsNullOrWhiteSpace(strParameter))
                    strParameter += string.Format("{0} {1} ", dataTypeName, item.PrivateVarName);
                else
                    strParameter += string.Format(",{0} {1} ", dataTypeName, item.PrivateVarName);
            }
            sb.Append(strParameter);
            sb.AppendLine(");");
            sb.AppendLine();

            #endregion
            #region InsertUpdate
            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// 数据 添加/更新");
            sb.AppendLine("        /// </summary>");
            sb.AppendFormat("        /// <param name=\"{0}\">实体</param>", classNamePrivate);
            sb.AppendLine();
            sb.AppendLine("        /// <returns>执行结果</returns>");
            sb.AppendFormat("        int {0}InsertUpdate(out string resultMsg,{1}{2} {3},DbTransaction tran =null);", classNamePublic, criteria.ModelClassNamePrefix, classNamePublic, classNamePrivate);
            sb.AppendLine();
            sb.AppendLine();
            #endregion
            #region UpdateStatus
            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// 数据状态 更新");
            sb.AppendLine("        /// </summary>");
            foreach (var item in colList)
            {
                if (item.IsPrimaryKey)//判断主键
                {
                    sb.AppendFormat("        /// <param name=\"{0}\">{1} {2}</param>", item.PrivateVarName, item.PublicVarName, item.ColumnComments);
                    sb.AppendLine();
                }
                if (item.ColumnName.ToLower().IndexOf("deleted") > -1 || item.ColumnName.ToLower().IndexOf("status") > -1)//判断状态字段
                {
                    sb.AppendFormat("        /// <param name=\"{0}\">状态</param>", item.PrivateVarName);
                    sb.AppendLine();
                }

            }
            sb.AppendLine("        /// <returns>执行结果</returns>");
            sb.AppendFormat("        int {0}UpdateStatus(out string resultMsg", classNamePublic);
            foreach (var item in colList)
            {
                string dataTypeName = CommonMethod.SqlTypeToCsharpType(item.DataType).Name;

                strParameter = string.Empty;
                if (item.IsPrimaryKey || item.ColumnName.ToLower().IndexOf("deleted") > -1 || item.ColumnName.ToLower().IndexOf("status") > -1)
                {
                    strParameter += string.Format(",{0} {1} ", dataTypeName, item.PrivateVarName);
                    sb.Append(strParameter);
                }
            }
            sb.AppendLine(",DbTransaction tran=null);");
            sb.AppendLine();

            #endregion
            #region Detele
            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// 数据 物理删除");
            sb.AppendLine("        /// </summary>");
            foreach (var item in colList)
            {
                if (item.IsPrimaryKey)//判断主键
                {
                    sb.AppendFormat("        /// <param name=\"{0}\">{1} {2}</param>", item.PrivateVarName, item.PublicVarName, item.ColumnComments);
                    sb.AppendLine();
                }
            }
            sb.AppendLine("        /// <returns>执行结果</returns>");
            sb.AppendFormat("        int {0}Delete(out string resultMsg", classNamePublic);
            foreach (var item in colList)
            {
                string dataTypeName = CommonMethod.SqlTypeToCsharpType(item.DataType).Name;

                strParameter = string.Empty;
                if (item.IsPrimaryKey)
                {
                    strParameter += string.Format(",{0} {1} ", dataTypeName, item.PrivateVarName);
                    sb.Append(strParameter);
                }
            }
            sb.AppendLine(",DbTransaction tran=null);");
            sb.AppendLine();

            #endregion
            sb.AppendLine("    }");
            sb.AppendLine("}");
            return sb.ToString();
        }
        
        /// <summary>
        /// 生成Model Code
        /// </summary>
        /// <param name="resultMsg">执行结果信息</param>
        /// <param name="tableName">表名</param>
        /// <param name="dataBaseType">数据库类型</param>
        /// <param name="dataBaseName">数据库名</param>
        /// <param name="modelsNamespace">Model层命名空间</param>
        /// <param name="modelClassNamePrefix">Model层类名前缀</param>
        public string ModelCodeGeneration(out string resultMsg, string tableName, string dataBaseType = BaseDict.SqlServerData,
            string dataBaseName = null, string modelsNamespace = null, string modelClassNamePrefix = "Model")
        {
            resultMsg = string.Empty;
            var criteria = new GeneratorCriteria();
            criteria.DataBaseType = dataBaseType;
            criteria.TableName = tableName;
            criteria.ModelsNamespace = modelsNamespace;
            criteria.ModelClassNamePrefix = modelClassNamePrefix;
            criteria.DataBaseName = dataBaseName;

            IGeneration gen = CreateInstance(criteria.DataBaseType);

            string dbName = CommonMethod.StringToPublicVar(dataBaseName);
            var className = CommonMethod.StringToPublicVar(criteria.TableName);
            var colList = gen.QueryColumnsByTable(out resultMsg, criteria);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using System.Linq;");
            sb.AppendLine("using System.Text;");
            sb.AppendLine("using System.ComponentModel.DataAnnotations;");
            sb.AppendLine();

            if (string.IsNullOrWhiteSpace(modelsNamespace))
                sb.AppendFormat("namespace Library.Models.{0}", dbName);
            else
                sb.AppendFormat("namespace Library.{0}", criteria.ModelsNamespace);

            sb.AppendLine();
            sb.AppendLine("{");
            sb.AppendFormat("    public class {0}{1}", criteria.ModelClassNamePrefix, className);
            sb.AppendLine();
            sb.AppendLine("    {");

            foreach (ModelGeneration item in colList)
            {
                Type type = CommonMethod.SqlTypeToCsharpType(item.DataType);
                var regExpression = CommonMethod.GetRegularExpression(type.Name);

                if (!string.IsNullOrWhiteSpace(item.ColumnComments))
                {
                    sb.AppendLine("        /// <summary> ");
                    sb.AppendFormat("        /// {0} ", item.ColumnComments);
                    sb.AppendLine();
                    sb.AppendLine("        /// </summary>");
                    if (!item.IsNull)
                        sb.AppendLine("        [Required]");
                    sb.AppendFormat("        [Display(Name = \"{0}\")]", item.ColumnComments);
                    sb.AppendLine();
                }

                if (!string.IsNullOrWhiteSpace(regExpression))
                {
                    sb.Append("        " + string.Format(regExpression, item.ColumnComments));
                    sb.AppendLine();
                }

                sb.AppendFormat("        public {0} {1} ", type.Name, item.PublicVarName);
                sb.Append("{ get;set;} ");
                sb.AppendLine(Environment.NewLine);
            }
            sb.AppendLine("    }");
            sb.AppendLine("}");

            var code = sb.ToString();

            return code;
        }

        /// <summary>
        /// IDAL层代码生成
        /// </summary>
        /// <param name="resultMsg">执行结果信息</param>
        /// <param name="tableName">表名</param>
        /// <param name="dataBaseType">数据库类型</param>
        /// <param name="dataBaseName">数据库名</param>
        /// <param name="modelsNamespace">Model层命名空间</param>
        /// <param name="modelClassNamePrefix">Model层类名前缀</param>
        /// <param name="dalNamespace">Dal层命名空间</param>
        /// <param name="dalClassNamePrefix">Dal层类名前缀</param> 
        public string IDalCodeGeneration(out string resultMsg, string tableName, string dataBaseType = BaseDict.SqlServerData, string dataBaseName = null,
            string modelsNamespace = null, string modelClassNamePrefix = "Model", string dalNamespace = null, string dalClassNamePrefix = "Logic")
        {
            resultMsg = string.Empty;
            var criteria = new GeneratorCriteria();
            criteria.DataBaseType = dataBaseType;
            criteria.TableName = tableName;
            criteria.ModelsNamespace = modelsNamespace;
            criteria.DalNamespace = dalNamespace;
            criteria.DalClassNamePrefix = dalClassNamePrefix;
            criteria.ModelClassNamePrefix = modelClassNamePrefix;
            criteria.DataBaseName = dataBaseName;

            IGeneration gen = CreateInstance(criteria.DataBaseType);

            var colList = gen.QueryColumnsByTable(out resultMsg, criteria);

            string classNamePrivate = CommonMethod.StringToPrivateVar(criteria.TableName);
            string classNamePublic = CommonMethod.StringToPublicVar(criteria.TableName);
            string dbName = CommonMethod.StringToPublicVar(dataBaseName);
            var strParameter = string.Empty; //参数

            //主键列集合
            var colPK = (from ModelGeneration col in colList
                         where col.IsPrimaryKey == true
                         select col).ToList();

            StringBuilder sb = new StringBuilder();
            #region Top
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using System.Linq;");
            sb.AppendLine("using System.Text;");
            sb.AppendLine("using System.Data;");
            sb.AppendLine("using Library.Kernel.DataBaseHelper;");
            sb.AppendLine("using Library.StringItemDict;");
            sb.AppendLine("using Library.Common;");
            sb.AppendLine("using System.Data.Common;");
            sb.AppendFormat("using Library.{0};", criteria.ModelsNamespace);
            sb.AppendLine();

            if (string.IsNullOrWhiteSpace(criteria.DalNamespace))
                sb.AppendFormat("namespace Library.{0}", "Logics");
            else
                sb.AppendFormat("namespace " + criteria.DalNamespace + ".Interfaces", dbName);

            sb.AppendLine();
            sb.AppendLine("{");
            sb.AppendFormat("    public interface I{0}{1}", criteria.DalClassNamePrefix, classNamePublic);
            sb.AppendLine();
            sb.AppendLine("    {");
            #endregion
            #region Pager
            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// 分页查询 ");
            sb.AppendLine("        /// </summary>");
            sb.AppendLine("        /// <param name=\"recordCount\">输出参数 数据总数</param>");
            sb.AppendLine("        /// <param name=\"criteria\">查询条件对象</param>");
            sb.AppendLine("        /// <param name=\"pageSize\">每页显示数量</param>");
            sb.AppendLine("        /// <param name=\"pageIndex\">当前页索引</param>");
            sb.AppendLine("        /// <returns>结果集 泛型</returns>");
            sb.AppendFormat("        public IList<{0}{1}> Query{1}ListPager(out string resultMsg, out decimal recordCount, Criteria{1} criteria, int pageSize = 10, int pageIndex = 1)",
                criteria.ModelClassNamePrefix, classNamePublic);
            sb.AppendLine();
            sb.AppendLine();
            #endregion
            #region Detail
            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        ///  查询实体");
            sb.AppendLine("        /// </summary>");

            foreach (var item in colPK)
            {
                string dataTypeName = CommonMethod.SqlTypeToCsharpType(item.DataType).Name;
                sb.AppendFormat("        /// <param name=\"{0}\">{1}{2} {3}</param>", item.PrivateVarName,
                    criteria.ModelClassNamePrefix, item.PublicVarName, item.ColumnComments);
                sb.AppendLine();
            }
            sb.AppendFormat("        /// <returns>{0}{1}</returns>", criteria.ModelClassNamePrefix, classNamePublic);
            sb.AppendLine();
            sb.AppendFormat("        {0}{1} {2}Detail(out string resultMsg,", criteria.ModelClassNamePrefix, classNamePublic, classNamePublic);

            foreach (var item in colPK)
            {
                string dataTypeName = CommonMethod.SqlTypeToCsharpType(item.DataType).Name;
                if (string.IsNullOrWhiteSpace(strParameter))
                    strParameter += string.Format("{0} {1} ", dataTypeName, item.PrivateVarName);
                else
                    strParameter += string.Format(",{0} {1} ", dataTypeName, item.PrivateVarName);
            }
            sb.Append(strParameter);
            sb.AppendLine(");");
            sb.AppendLine();

            #endregion
            #region InsertUpdate
            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// 数据 添加/更新");
            sb.AppendLine("        /// </summary>");
            sb.AppendFormat("        /// <param name=\"{0}\">实体</param>", classNamePrivate);
            sb.AppendLine();
            sb.AppendLine("        /// <returns>执行结果</returns>");
            sb.AppendFormat("        int {0}InsertUpdate(out string resultMsg,{1}{2} {3},DbTransaction tran =null);", classNamePublic, criteria.ModelClassNamePrefix, classNamePublic, classNamePrivate);
            sb.AppendLine();
            sb.AppendLine();
            #endregion
            #region UpdateStatus
            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// 数据状态 更新");
            sb.AppendLine("        /// </summary>");
            foreach (var item in colList)
            {
                if (item.IsPrimaryKey)//判断主键
                {
                    sb.AppendFormat("        /// <param name=\"{0}\">{1} {2}</param>", item.PrivateVarName, item.PublicVarName, item.ColumnComments);
                    sb.AppendLine();
                }
                if (item.ColumnName.ToLower().IndexOf("deleted") > -1 || item.ColumnName.ToLower().IndexOf("status") > -1)//判断状态字段
                {
                    sb.AppendFormat("        /// <param name=\"{0}\">状态</param>", item.PrivateVarName);
                    sb.AppendLine();
                }

            }
            sb.AppendLine("        /// <returns>执行结果</returns>");
            sb.AppendFormat("        int {0}UpdateStatus(out string resultMsg", classNamePublic);
            foreach (var item in colList)
            {
                string dataTypeName = CommonMethod.SqlTypeToCsharpType(item.DataType).Name;

                strParameter = string.Empty;
                if (item.IsPrimaryKey || item.ColumnName.ToLower().IndexOf("deleted") > -1 || item.ColumnName.ToLower().IndexOf("status") > -1)
                {
                    strParameter += string.Format(",{0} {1} ", dataTypeName, item.PrivateVarName);
                    sb.Append(strParameter);
                }
            }
            sb.AppendLine(",DbTransaction tran=null);");
            sb.AppendLine();

            #endregion
            #region Detele
            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// 数据 物理删除");
            sb.AppendLine("        /// </summary>");
            foreach (var item in colList)
            {
                if (item.IsPrimaryKey)//判断主键
                {
                    sb.AppendFormat("        /// <param name=\"{0}\">{1} {2}</param>", item.PrivateVarName, item.PublicVarName, item.ColumnComments);
                    sb.AppendLine();
                }
            }
            sb.AppendLine("        /// <returns>执行结果</returns>");
            sb.AppendFormat("        int {0}Delete(out string resultMsg", classNamePublic);
            foreach (var item in colList)
            {
                string dataTypeName = CommonMethod.SqlTypeToCsharpType(item.DataType).Name;

                strParameter = string.Empty;
                if (item.IsPrimaryKey)
                {
                    strParameter += string.Format(",{0} {1} ", dataTypeName, item.PrivateVarName);
                    sb.Append(strParameter);
                }
            }
            sb.AppendLine(",DbTransaction tran=null);");
            sb.AppendLine();

            #endregion
            sb.AppendLine("    }");
            sb.AppendLine("}");
            return sb.ToString();
        }

        /// <summary>
        /// DAL层代码生成
        /// </summary>
        /// <param name="resultMsg">执行结果信息</param>
        /// <param name="tableName">表名</param>
        /// <param name="dataBaseType">数据库类型</param>
        /// <param name="dataBaseName">数据库名</param>
        /// <param name="modelsNamespace">Model层命名空间</param>
        /// <param name="modelClassNamePrefix">Model层类名前缀</param>
        /// <param name="dalNamespace">Dal层命名空间</param>
        /// <param name="dalClassNamePrefix">Dal层类名前缀</param> 
        public string DalCodeGeneration(out string resultMsg, string tableName, string dataBaseType = BaseDict.SqlServerData, string dataBaseName = null,
            string modelsNamespace = null, string modelClassNamePrefix = "Model", string dalNamespace = null, string dalClassNamePrefix = "Logic")
        {
            resultMsg = string.Empty;
            var criteria = new GeneratorCriteria();
            criteria.DataBaseType = dataBaseType;
            criteria.TableName = tableName;
            criteria.ModelsNamespace = modelsNamespace;
            criteria.DalNamespace = dalNamespace;
            criteria.DalClassNamePrefix = dalClassNamePrefix;
            criteria.ModelClassNamePrefix = modelClassNamePrefix;
            criteria.DataBaseName = dataBaseName;

            IGeneration gen = CreateInstance(criteria.DataBaseType);
            var colList = gen.QueryColumnsByTable(out resultMsg, criteria);

            string classNamePrivate = CommonMethod.StringToPrivateVar(criteria.TableName);
            string classNamePublic = CommonMethod.StringToPublicVar(criteria.TableName);
            string dbName = CommonMethod.StringToPublicVar(dataBaseName);

            var strParameter = string.Empty; //参数

            //主键列集合
            var colPK = (from ModelGeneration col in colList
                         where col.IsPrimaryKey == true
                         select col).ToList();

            StringBuilder sb = new StringBuilder();
            #region Top
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using System.Linq;");
            sb.AppendLine("using System.Text;");
            sb.AppendLine("using System.Data;");
            sb.AppendLine("using Library.Kernel.DataBaseHelper;");
            sb.AppendLine("using Library.StringItemDict;");
            sb.AppendLine("using Library.Common;");
            sb.AppendLine("using System.Data.Common;");
            
            if(string.IsNullOrWhiteSpace(criteria.ModelsNamespace))
                sb.AppendFormat("using Library.Models.CustomsModels;", dbName);
            else
                sb.AppendFormat("using Library.Models;");

            sb.AppendLine();

            if (string.IsNullOrWhiteSpace(criteria.DalNamespace))
                sb.AppendFormat("namespace Library.{0}", "Logics");
            else
                sb.AppendFormat("namespace " + criteria.DalNamespace + ".Classes", dbName);

            sb.AppendLine();
            sb.AppendLine("{");
            sb.AppendFormat("    public class {0}{1} : I{0}{1}", criteria.DalClassNamePrefix, classNamePublic);
            sb.AppendLine();
            sb.AppendLine("    {");
            sb.AppendLine();
            #endregion
            #region GetModel IDataReader
            sb.AppendLine("        #region 私有函数");
            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// Model 赋值 IDataReader");
            sb.AppendLine("        /// </summary>");
            sb.AppendFormat("        private IList<{0}{1}> GetModel(IDataReader dr)", criteria.ModelClassNamePrefix, classNamePublic);
            sb.AppendLine();
            sb.AppendLine("        {");
            sb.AppendFormat("            var modelList = new List<{0}{1}>();", criteria.ModelClassNamePrefix, classNamePublic);
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine("            while (dr.Read())");
            sb.AppendLine("            {");
            sb.AppendFormat("                var model = new {0}{1}();", criteria.ModelClassNamePrefix, classNamePublic);
            sb.AppendLine();
            foreach (var item in colList)
            {
                var dataType = CommonMethod.SqlTypeToCsharpType(item.DataType);
                var dataValue = CommonMethod.GetDataValueIsNotNull(dataType.Name, string.Format("dr[\"{0}\"]", item.ColumnName));
                sb.AppendFormat("                model.{0} = {1};", item.PublicVarName, dataValue);
                sb.AppendLine();
            }
            sb.AppendLine("                modelList.Add(model);");
            sb.AppendLine("            }");
            sb.AppendLine("            return modelList;");
            sb.AppendLine("        }");
            sb.AppendLine();
            #endregion
            #region GetModel DataSet
            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// Model 赋值 DataSet");
            sb.AppendLine("        /// </summary>");
            sb.AppendFormat("        private IList<{0}{1}> GetModel(DataSet ds)", criteria.ModelClassNamePrefix, classNamePublic);
            sb.AppendLine();
            sb.AppendLine("        {");

            sb.AppendLine("            var modelList = (from DataRow dr in ds.Tables[0].Rows");
            sb.AppendFormat("                            select new {0}{1}()", criteria.ModelClassNamePrefix, classNamePublic);
            sb.AppendLine();
            sb.AppendLine("                            {");
            foreach (var item in colList)
            {
                var dataType = CommonMethod.SqlTypeToCsharpType(item.DataType);
                var dataValue = CommonMethod.GetDataValueIsNotNull(dataType.Name, string.Format("dr[\"{0}\"]", item.ColumnName));
                sb.AppendFormat("                                {0} = {1},", item.PublicVarName, dataValue);
                sb.AppendLine();
            }
            sb.Remove(sb.Length - 1, 1);
            sb.AppendLine("                            }).ToList();");
            sb.AppendLine("            return modelList;");
            sb.AppendLine("        }");
            sb.AppendLine();

            sb.AppendLine("        #endregion");
            sb.AppendLine();
            #endregion
            #region Pager
            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// 分页查询 ");
            sb.AppendLine("        /// </summary>");
            sb.AppendLine("        /// <param name=\"recordCount\">输出参数 数据总数</param>");
            sb.AppendLine("        /// <param name=\"criteria\">查询条件对象</param>");
            sb.AppendLine("        /// <param name=\"pageSize\">每页显示数量</param>");
            sb.AppendLine("        /// <param name=\"pageIndex\">当前页索引</param>");
            sb.AppendLine("        /// <returns>结果集 泛型</returns>");
            sb.AppendFormat("        public IList<{0}{1}> Query{1}ListPager(out string resultMsg, out decimal recordCount, Criteria{1}.Pager criteria, int pageSize = 10, int pageIndex = 1)",
                criteria.ModelClassNamePrefix, classNamePublic);
            sb.AppendLine();
            sb.AppendLine("        {");
            sb.AppendLine("            recordCount = decimal.Zero;");
            sb.AppendLine("            resultMsg = string.Empty;");
            sb.AppendFormat("            IList<{0}{1}> list = new List<{0}{1}>();", criteria.ModelClassNamePrefix, classNamePublic);
            sb.AppendLine();
            sb.AppendLine("            try");
            sb.AppendLine("            {");
            sb.AppendLine("                //存储过程名称");
            sb.AppendFormat("                string sql = \"USP_{0}_SELECT_SEARCH_PAGER\";", criteria.TableName.ToUpper());
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine("                //参数添加");
            sb.AppendLine("                IList<DBParameter> parm = new List<DBParameter>();");
            sb.AppendLine("                parm.Add(new DBParameter() { ParameterName = \"PagerSize\", ParameterValue = pageSize, ParameterInOut = BaseDict.ParmIn, ParameterType = DbType.String });");
            sb.AppendLine("                parm.Add(new DBParameter() { ParameterName = \"PagerIndex\", ParameterValue = pageIndex, ParameterInOut = BaseDict.ParmIn, ParameterType = DbType.String });");
            sb.AppendLine("                parm.Add(new DBParameter() { ParameterName = \"RowCount\", ParameterInOut = BaseDict.ParmOut, ParameterType = DbType.String });");
            sb.AppendLine();
            sb.AppendLine("                //查询执行");
            sb.AppendLine("                using (IDataReader dr = DBHelper.ExecuteReader(sql, true, parm))");
            sb.AppendLine("                {");
            sb.AppendLine("                    //DataReader 转换成 List");
            sb.AppendLine("                    list = GetModel(dr);");
            sb.AppendLine("                    foreach (var item in parm)");
            sb.AppendLine("                    {");
            sb.AppendLine("                        //获取输出参数值");
            sb.AppendLine("                        if (item.ParameterName == \"RowCount\")");
            sb.AppendLine("                        {");
            sb.AppendLine("                            decimal.TryParse(item.ParameterValue.ToString(), out recordCount);");
            sb.AppendLine("                            break;");
            sb.AppendLine("                        }");
            sb.AppendLine("                    }");
            sb.AppendLine("                }");
            sb.AppendLine("            }");
            sb.AppendLine("            catch (Exception ex)");
            sb.AppendLine("            {");
            sb.AppendLine("                resultMsg = string.Format(\"{0} {1}\", BaseDict.ErrorPrefix, ex.ToString());");
            sb.AppendLine("            }");
            sb.AppendLine("            return list;");
            sb.AppendLine("        }");
            sb.AppendLine();
            sb.AppendLine();
            #endregion
            #region Detail
            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        ///  查询实体");
            sb.AppendLine("        /// </summary>");

            foreach (var item in colPK)
            {
                string dataTypeName = CommonMethod.SqlTypeToCsharpType(item.DataType).Name;
                sb.AppendFormat("        /// <param name=\"{0}\">{1}{2} {3}</param>", item.PrivateVarName,
                    criteria.ModelClassNamePrefix, item.PublicVarName, item.ColumnComments);
                sb.AppendLine();
            }
            sb.AppendFormat("        /// <returns>{0}{1}</returns>", criteria.ModelClassNamePrefix, classNamePublic);
            sb.AppendLine();
            sb.AppendFormat("        public {0}{1} {2}Detail(out string resultMsg,", criteria.ModelClassNamePrefix, classNamePublic, classNamePublic);

            foreach (var item in colPK)
            {
                string dataTypeName = CommonMethod.SqlTypeToCsharpType(item.DataType).Name;
                if (string.IsNullOrWhiteSpace(strParameter))
                    strParameter += string.Format("{0} {1} ", dataTypeName, item.PrivateVarName);
                else
                    strParameter += string.Format(",{0} {1} ", dataTypeName, item.PrivateVarName);
            }
            sb.Append(strParameter);
            sb.AppendLine(")");
            sb.AppendLine("        {");
            sb.AppendLine("            resultMsg = string.Empty;");
            sb.AppendFormat("            var model = new {0}{1}();", criteria.ModelClassNamePrefix, classNamePublic);
            sb.AppendLine();
            sb.AppendLine("            try");
            sb.AppendLine("            {");
            sb.AppendLine("                //存储过程名称");
            sb.AppendFormat("                string sql = \"USP_{0}_SELECT_DETAIL_BY", criteria.TableName.ToUpper());
            foreach (var item in colPK)
            {
                sb.AppendFormat("_{0}", item.ColumnName.ToUpper());
            }
            sb.AppendLine("\";");
            sb.AppendLine();
            sb.AppendLine("                //参数添加");
            sb.AppendLine("                IList<DBParameter> parm = new List<DBParameter>();");

            foreach (var item in colPK)
            {
                sb.Append("                parm.Add(new DBParameter() { ");
                sb.AppendFormat("ParameterName = \"{0}\", ", item.PrivateVarName.ToUpper());

                sb.AppendFormat("ParameterValue = {0}, ParameterInOut = BaseDict.ParmIn, ParameterType = {1} ",
                    item.PrivateVarName, CommonMethod.SqlTypeToCsharpTypeString(item.DataType));
                sb.AppendLine("});");
            }
            sb.AppendLine();
            sb.AppendLine("                //查询执行");
            sb.AppendLine("                using (IDataReader dr = DBHelper.ExecuteReader(sql, true, parm))");
            sb.AppendLine("                {");
            sb.AppendFormat("                    IList<{0}{1}> list = GetModel(dr);", criteria.ModelClassNamePrefix, classNamePublic);
            sb.AppendLine();
            sb.AppendLine("                    model = list.First();");
            sb.AppendLine("                }");
            sb.AppendLine("            }");
            sb.AppendLine("            catch (Exception ex)");
            sb.AppendLine("            {");
            sb.AppendLine("                resultMsg = string.Format(\"{0} {1}\", BaseDict.ErrorPrefix, ex.ToString());");
            sb.AppendLine("            }");
            sb.AppendLine("            return model;");

            sb.AppendLine("        }");
            sb.AppendLine();

            #endregion
            #region InsertUpdate
            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// 数据 添加/更新");
            sb.AppendLine("        /// </summary>");
            sb.AppendFormat("        /// <param name=\"{0}\">实体</param>", classNamePrivate);
            sb.AppendLine();
            sb.AppendLine("        /// <returns>执行结果</returns>");
            sb.AppendFormat("        public int {0}InsertUpdate(out string resultMsg,{1}{2} {3},DbTransaction tran =null)", classNamePublic, criteria.ModelClassNamePrefix, classNamePublic, classNamePrivate);
            sb.AppendLine();
            sb.AppendLine("        {");
            sb.AppendLine("            resultMsg = string.Empty;");
            sb.AppendLine("            int res = 0;");
            sb.AppendLine("            try");
            sb.AppendLine("            {");
            sb.AppendLine("                //存储过程名称");
            sb.AppendFormat("                string sql = \"USP_{0}_INSERT_UPDATE\";", criteria.TableName.ToUpper());
            sb.AppendLine();
            sb.AppendLine("                //参数添加");
            sb.AppendLine("                IList<DBParameter> parm = new List<DBParameter>();");
            foreach (var item in colList)
            {
                sb.Append("                parm.Add(new DBParameter() { ");
                sb.AppendFormat("ParameterName = \"{0}\", ", item.PrivateVarName.ToUpper());

                sb.AppendFormat("ParameterValue = {0}.{1}, ParameterInOut = BaseDict.ParmIn, ParameterType = {2} ",
                    classNamePrivate, item.PublicVarName, CommonMethod.SqlTypeToCsharpTypeString(item.DataType));
                sb.AppendLine("});");
            }
            sb.AppendLine("                parm.Add(new DBParameter() { ParameterName = \"resultMsg\", ParameterInOut = BaseDict.ParmOut, ParameterType = DbType.String });");
            sb.AppendLine();
            sb.AppendLine("                //新增/更新执行");
            sb.AppendLine("                res = DBHelper.ExecuteNonQuery(sql, true, parm, tran);");
            sb.AppendLine("                foreach (var item in parm)");
            sb.AppendLine("                {");
            sb.AppendLine("                    //获取输出参数值");
            sb.AppendLine("                    if (item.ParameterName == \"resultMsg\")");
            sb.AppendLine("                    {");
            sb.AppendLine("                        resultMsg = item.ParameterValue.ToString();");
            sb.AppendLine("                        break;");
            sb.AppendLine("                    }");
            sb.AppendLine("                }");
            sb.AppendLine("            }");
            sb.AppendLine("            catch (Exception ex)");
            sb.AppendLine("            {");
            sb.AppendLine("                if(tran != null)");
            sb.AppendLine("                    tran.Rollback();");
            sb.AppendLine("                resultMsg = string.Format(\"{0} {1}\", BaseDict.ErrorPrefix, ex.ToString());");
            sb.AppendLine("            }");
            sb.AppendLine("            return res;");
            sb.AppendLine("        }");
            sb.AppendLine();
            #endregion
            #region UpdateStatus
            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// 数据状态 更新");
            sb.AppendLine("        /// </summary>");
            foreach (var item in colList)
            {
                if (item.IsPrimaryKey)//判断主键
                {
                    sb.AppendFormat("        /// <param name=\"{0}\">{1} {2}</param>", item.PrivateVarName, item.PublicVarName, item.ColumnComments);
                    sb.AppendLine();
                }
                if (item.ColumnName.ToLower().IndexOf("deleted") > -1 || item.ColumnName.ToLower().IndexOf("status") > -1)//判断状态字段
                {
                    sb.AppendFormat("        /// <param name=\"{0}\">状态</param>", item.PrivateVarName);
                    sb.AppendLine();
                }

            }
            sb.AppendLine("        /// <returns>执行结果</returns>");
            sb.AppendFormat("        public int {0}UpdateStatus(out string resultMsg", classNamePublic);
            foreach (var item in colList)
            {
                string dataTypeName = CommonMethod.SqlTypeToCsharpType(item.DataType).Name;

                strParameter = string.Empty;
                if (item.IsPrimaryKey || item.ColumnName.ToLower().IndexOf("deleted") > -1 || item.ColumnName.ToLower().IndexOf("status") > -1)
                {
                    strParameter += string.Format(",{0} {1} ", dataTypeName, item.PrivateVarName);
                    sb.Append(strParameter);
                }
            }

            sb.AppendLine(",DbTransaction tran=null)");
            sb.AppendLine("        {");
            sb.AppendLine("            resultMsg = string.Empty;");
            sb.AppendLine("            int res = 0;");
            sb.AppendLine("            try");
            sb.AppendLine("            {");
            sb.AppendLine("                //存储过程名称");
            sb.AppendFormat("                string sql = \"USP_{0}_UPDATE_STATUS\";", criteria.TableName);
            sb.AppendLine();
            sb.AppendLine("                //参数添加");
            sb.AppendLine("                IList<DBParameter> parm = new List<DBParameter>();");

            foreach (var item in colList)
            {
                strParameter = string.Empty;
                if (item.IsPrimaryKey || item.ColumnName.ToLower().IndexOf("deleted") > -1 || item.ColumnName.ToLower().IndexOf("status") > -1)
                {
                    sb.Append("                parm.Add(new DBParameter() { ");
                    sb.AppendFormat("ParameterName = \"{0}\", ", item.PrivateVarName.ToUpper());
                    sb.AppendFormat("ParameterValue = {0}, ParameterInOut = BaseDict.ParmIn, ParameterType = {1} ",
                        item.PrivateVarName, CommonMethod.SqlTypeToCsharpTypeString(item.DataType));
                    sb.AppendLine("});");
                }
            }
            sb.AppendLine("                parm.Add(new DBParameter() { ParameterName = \"resultMsg\", ParameterInOut = BaseDict.ParmOut, ParameterType = DbType.String });");
            sb.AppendLine("                //更新执行");
            sb.AppendLine("                res = DBHelper.ExecuteNonQuery(sql, true, parm, tran);");
            sb.AppendLine("                foreach (var item in parm)");
            sb.AppendLine("                {");
            sb.AppendLine("                    //获取输出参数值");
            sb.AppendLine("                    if (item.ParameterName == \"resultMsg\")");
            sb.AppendLine("                    {");
            sb.AppendLine("                        resultMsg = item.ParameterValue.ToString();");
            sb.AppendLine("                        break;");
            sb.AppendLine("                    }");
            sb.AppendLine("                }");
            sb.AppendLine("            }");
            sb.AppendLine("            catch (Exception ex)");
            sb.AppendLine("            {");
            sb.AppendLine("                if(tran != null)");
            sb.AppendLine("                    tran.Rollback();");
            sb.AppendLine("                resultMsg = string.Format(\"{0} {1}\", BaseDict.ErrorPrefix, ex.ToString());");
            sb.AppendLine("            }");
            sb.AppendLine("            return res;");
            sb.AppendLine("        }");
            sb.AppendLine();

            #endregion
            #region Detele
            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// 数据 删除");
            sb.AppendLine("        /// </summary>");
            foreach (var item in colList)
            {
                if (item.IsPrimaryKey)//判断主键
                {
                    sb.AppendFormat("        /// <param name=\"{0}\">{1} {2}</param>", item.PrivateVarName, item.PublicVarName, item.ColumnComments);
                    sb.AppendLine();
                }

            }
            sb.AppendLine("        /// <returns>执行结果</returns>");
            sb.AppendFormat("        public int {0}Delete(out string resultMsg", classNamePublic);
            foreach (var item in colList)
            {
                string dataTypeName = CommonMethod.SqlTypeToCsharpType(item.DataType).Name;

                strParameter = string.Empty;
                if (item.IsPrimaryKey)
                {
                    strParameter += string.Format(",{0} {1} ", dataTypeName, item.PrivateVarName);
                    sb.Append(strParameter);
                }
            }

            sb.AppendLine(",DbTransaction tran=null)");
            sb.AppendLine("        {");
            sb.AppendLine("            resultMsg = string.Empty;");
            sb.AppendLine("            int res = 0;");
            sb.AppendLine("            try");
            sb.AppendLine("            {");
            sb.AppendLine("                //存储过程名称");
            sb.AppendFormat("                string sql = \" USP_{0}_DELETE \" ;", criteria.TableName.ToUpper());
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine("                //参数添加");
            sb.AppendLine("                IList<DBParameter> parm = new List<DBParameter>();");

            foreach (var item in colList)
            {
                strParameter = string.Empty;
                if (item.IsPrimaryKey)
                {
                    sb.Append("                parm.Add(new DBParameter() { ");
                    sb.AppendFormat("ParameterName = \"{0}\", ", item.PrivateVarName.ToUpper());

                    sb.AppendFormat("ParameterValue = {0}, ParameterInOut = BaseDict.ParmIn, ParameterType = {1} ",
                        item.PrivateVarName, CommonMethod.SqlTypeToCsharpTypeString(item.DataType));
                    sb.AppendLine("});");
                }
            }
            sb.AppendLine("                parm.Add(new DBParameter() { ParameterName = \"resultMsg\", ParameterInOut = BaseDict.ParmOut, ParameterType = DbType.String });");
            sb.AppendLine("                //更新执行");
            sb.AppendLine("                res = DBHelper.ExecuteNonQuery(sql, true, parm, tran);");
            sb.AppendLine("                foreach (var item in parm)");
            sb.AppendLine("                {");
            sb.AppendLine("                    //获取输出参数值");
            sb.AppendLine("                    if (item.ParameterName == \"resultMsg\")");
            sb.AppendLine("                    {");
            sb.AppendLine("                        resultMsg = item.ParameterValue.ToString();");
            sb.AppendLine("                        break;");
            sb.AppendLine("                    }");
            sb.AppendLine("                }");
            sb.AppendLine("            }");
            sb.AppendLine("            catch (Exception ex)");
            sb.AppendLine("            {");
            sb.AppendLine("                if(tran != null)");
            sb.AppendLine("                    tran.Rollback();");
            sb.AppendLine("                resultMsg = string.Format(\"{0} {1}\", BaseDict.ErrorPrefix, ex.ToString());");
            sb.AppendLine("            }");
            sb.AppendLine("            return res;");
            sb.AppendLine("        }");
            sb.AppendLine();

            #endregion
            sb.AppendLine("    }");
            sb.AppendLine("}");
            return sb.ToString();
        }

        /// <summary>
        /// Criteria 类 代码生成
        /// </summary>
        /// <param name="resultMsg"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public string CriteriaCodeGeneration(out string resultMsg, string dataBaseName = null, string tableName = null, string dalNamespace = null)
        {
            resultMsg = string.Empty;
            StringBuilder sb = new StringBuilder();
            string classNamePrivate = CommonMethod.StringToPrivateVar(tableName);
            string classNamePublic = CommonMethod.StringToPublicVar(tableName);
            string dbName = CommonMethod.StringToPublicVar(dataBaseName);
            #region Top
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using System.Linq;");
            sb.AppendLine("using System.Text;");
            sb.AppendLine("using System.ComponentModel.DataAnnotations;");
            sb.AppendLine();

            sb.AppendFormat("namespace Library.Criterias.{0}", dbName);

            sb.AppendLine();
            sb.AppendLine("{");
            sb.AppendFormat("    public class Criteria{0} ", classNamePublic);
            sb.AppendLine();
            sb.AppendLine("    {");
            //sb.AppendLine("        public class Pager");
            //sb.AppendLine("        {");
            //sb.AppendLine("        }");
            sb.AppendLine("    }");
            sb.AppendLine("}");
            sb.AppendLine();
            var code = sb.ToString();
            return code;
            #endregion
        }

        /// <summary>
        /// TSQL 语句生成
        /// </summary>
        /// <param name="ResultMsg">执行结果信息</param>
        /// <param name="TableName">表名</param>
        /// <param name="DataBaseType">数据库类型</param>
        /// <param name="DataBaseName">数据库名</param>
        /// <param name="SqlParameterPrefix">TSql参数名前缀</param>
        /// <param name="SqlProcedurePrefix">TSql存储过程名称前缀</param>
        /// <param name="IsSqlInsertUpdate">是否生成 Insert/Update</param>
        /// <param name="IsSqlSelectDetail">是否生成 SelectDetail</param>
        /// <param name="IsSqlUpdateStatus">是否生成 UpdateStatus</param>
        /// <param name="IsSqlSelectPager">是否生成 SelectPager</param>
        /// <param name="IsSqlSelectAll">是否生成 SqlSelectAll</param> 
        public string TSqlCodeGeneration(out string ResultMsg, string TableName, string DataBaseType = BaseDict.SqlServerData, string DataBaseName = null,
            string SqlParameterPrefix = null, string SqlProcedurePrefix = null, string IsSqlInsertUpdate = "True", string IsSqlSelectDetail = "True", string IsSqlUpdateStatus = "True",
            string IsSqlSelectPager = "True", string IsSqlSelectAll = "True", string IsSqlDelete = "True")
        {
            ResultMsg = string.Empty;
            var criteria = new GeneratorCriteria();                 // 条件对象
            criteria.DataBaseName = DataBaseName;                   // 数据库名称
            criteria.TableName = TableName;                         // 表名
            criteria.SqlParameterPrefix = SqlParameterPrefix;       // SQL参数前缀
            criteria.DataBaseType = DataBaseType;                   // 数据库类型
            criteria.SqlProcedurePrefix = SqlProcedurePrefix;       // 存储过程名称前缀

            IGeneration dal = CreateInstance(criteria.DataBaseType);

            var code = string.Empty;
            if (IsSqlUpdateStatus.Equals("True"))
                code += dal.GenerateSqlForUpdateStatus(out ResultMsg, criteria);
            if (IsSqlInsertUpdate.Equals("True"))
                code += Environment.NewLine + dal.GenerateSqlForInsertUpdate(out ResultMsg, criteria);
            if (IsSqlSelectDetail.Equals("True"))
                code += Environment.NewLine + dal.GenerateSqlForSelectDetail(out ResultMsg, criteria);
            if (IsSqlSelectPager.Equals("True"))
                code += Environment.NewLine + dal.GenerateSqlForSelectPager(out ResultMsg, criteria);
            if (IsSqlSelectAll.Equals("True"))
                code += Environment.NewLine + dal.GenerateSqlForSelectAll(out ResultMsg, criteria);
            if (IsSqlDelete.Equals("True"))
                code += Environment.NewLine + dal.GenerateSqlForDelete(out ResultMsg, criteria);
            return code;
        }


        /// <summary>
        /// 查询所有数据库名称 集合
        /// </summary>
        /// <param name="resultMsg">输出结果</param>
        /// <param name="DataBaseType">数据库类型</param>
        /// <returns></returns>
        public IList<string> QueryDataBaseAll(out string resultMsg, string DataBaseType)
        {
            resultMsg = string.Empty;
            IGeneration gen = CreateInstance(DataBaseType);
            var list = gen.QueryDataBaseAll(out resultMsg);
            return list;
        }

        /// <summary>
        /// 查询数据表 集合
        /// </summary>
        /// <param name="resultMsg"></param>
        /// <param name="DataBaseType">数据库类型</param>
        /// <param name="DataBaseName">数据库名称</param>
        /// <returns></returns>
        public IList<ModelGeneration> QueryTablesAll(out string resultMsg, string DataBaseType, string DataBaseName = null)
        {
            resultMsg = string.Empty;
            GeneratorCriteria criteria = new GeneratorCriteria() { DataBaseName = DataBaseName };
            IGeneration gen = CreateInstance(DataBaseType);
            var list = gen.QueryTablesAll(out resultMsg, criteria);
            return list;
        }

    }
}
