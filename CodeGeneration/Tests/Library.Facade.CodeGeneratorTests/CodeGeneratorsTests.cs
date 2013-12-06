using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Facade.CodeGenerator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library.StringItemDict;
using Library.Logic.DalGeneration;
namespace Library.Facade.CodeGeneratorTests
{
    [TestClass()]
    public class CodeGeneratorsTests
    {
        [TestMethod()]
        public void ModelCodeGenerationTest()
        {
            string resultMsg = string.Empty;
            var gen = new CodeGenerators();
            //var code = gen.ModelCodeGeneration(out resultMsg, "Models", "SHIP_INFO", BaseDict.OracleData);
            var criteria = new GeneratorCriteria();
            criteria.ModelsNamespace = "Models";
            criteria.ModelClassNamePrefix = "Model";
            criteria.DalNamespace = "Logic";
            criteria.DalClassNamePrefix = "Logic";
            criteria.TableName = "TUSER";
            criteria.DataBaseType = BaseDict.OracleData;
            var code = gen.ModelCodeGeneration(out resultMsg, criteria.TableName, criteria.DataBaseType, criteria.DataBaseName, criteria.ModelsNamespace,
                criteria.ModelClassNamePrefix);
            var code1 = string.Empty;

            Assert.AreNotEqual(code, code1);
        }

        [TestMethod()]
        public void IDalCodeGenerationTest()
        {
            string resultMsg = string.Empty;
            var gen = new CodeGenerators();
            //var code = gen.ModelCodeGeneration(out resultMsg, "Models", "SHIP_INFO", BaseDict.OracleData);
            var criteria = new GeneratorCriteria();
            criteria.ModelsNamespace = "Models";
            criteria.ModelClassNamePrefix = "Model";
            criteria.DalNamespace = "Logic";
            criteria.DalClassNamePrefix = "Logic";
            criteria.TableName = "TUSER";//"Bank";
            criteria.DataBaseType = BaseDict.OracleData;
            criteria.DataBaseName = null;// "CardMaintain";
            var code = gen.IDalCodeGeneration(out resultMsg, criteria.TableName, criteria.DataBaseType, criteria.DataBaseName, criteria.ModelsNamespace,
                criteria.ModelClassNamePrefix, criteria.DalNamespace, criteria.DalClassNamePrefix);
            var code1 = string.Empty;
            Assert.AreNotEqual(code, code1);
        }

        [TestMethod()]
        public void DalCodeGenerationTest()
        {
            string resultMsg = string.Empty;
            var gen = new CodeGenerators();
            //var code = gen.ModelCodeGeneration(out resultMsg, "Models", "SHIP_INFO", BaseDict.OracleData);
            var criteria = new GeneratorCriteria();
            criteria.ModelsNamespace = "Models";
            criteria.ModelClassNamePrefix = "Model";
            criteria.DalNamespace = "Logic";
            criteria.DalClassNamePrefix = "Logic";
            criteria.TableName = "TUSER";//"Bank";
            criteria.DataBaseType = BaseDict.OracleData;
            criteria.DataBaseName = null;// "CardMaintain";
            var code = gen.DalCodeGeneration(out resultMsg,criteria.TableName,criteria.DataBaseType,criteria.DataBaseName,criteria.ModelsNamespace,
                criteria.ModelClassNamePrefix,criteria.DalNamespace ,criteria.DalClassNamePrefix);
            var code1 = string.Empty;
            Assert.AreNotEqual(code, code1);
        }
    }
}
