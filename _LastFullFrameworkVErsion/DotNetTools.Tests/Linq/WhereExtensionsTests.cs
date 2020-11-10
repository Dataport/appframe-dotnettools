using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dataport.AppFrameDotNet.DotNetTools.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dataport.AppFrameDotNet.DotNetTools.Tests.Linq
{
    [TestClass()]
    public class WhereClauseExpressionsTests
    {
        protected PrivateType Accessor = new PrivateType(typeof(WhereClauseExpressions));

        protected IQueryable<string> Data = new List<string> {
            "Hallo Halli Welt",
            "Hallo Halli Du",
            "Grausame Welt"

        }.AsQueryable();

        [TestMethod()]
        public void WhereClauseExpressions_GetLikeMethod_StartsWith()
        {
            var result = GetLikeMethodExec("Hallo*");
            Assert.AreEqual("StartsWith", result);
        }

        [TestMethod()]
        public void WhereClauseExpressions_GetLikeMethod_EndsWith()
        {
            var result = GetLikeMethodExec("*Hallo");
            Assert.AreEqual("EndsWith", result);
        }

        [TestMethod()]
        public void WhereClauseExpressions_GetLikeMethod_Contains()
        {
            var result = GetLikeMethodExec("*Hallo*");
            Assert.AreEqual("Contains", result);
        }

        [TestMethod()]
        public void WhereClauseExpressions_GetLikeMethod_Equals()
        {
            var result = GetLikeMethodExec("Hallo");
            Assert.AreEqual("Equals", result);
        }

        [TestMethod()]
        public void WhereClauseExpressions_GetLikeMethod_Contains_NurWildcard()
        {
            var result = GetLikeMethodExec("*");
            Assert.AreEqual("Contains", result);
        }


        private string GetLikeMethodExec(string filter)
        {
            return ((MethodInfo)Accessor.InvokeStatic("GetLikeMethod", filter, '*')).Name;
        }

        [TestMethod()]
        public void WhereClauseExpressions_WhereLinke_OhneWildcard()
        {
            var result = Data.WhereLike(x => x, "Grausame Welt", '*');
            Assert.AreEqual(1, result.Count());
        }

        [TestMethod()]
        public void WhereClauseExpressions_WhereLinke_WildcardAmEnde()
        {
            var result = Data.WhereLike(x => x, "Hallo*", '*');
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod()]
        public void WhereClauseExpressions_WhereLinke_WildcardAmAnfang()
        {
            var result = Data.WhereLike(x => x, "*Welt", '*');
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod()]
        public void WhereClauseExpressions_WhereLinke_WildcardBeideSeiten()
        {
            var result = Data.WhereLike(x => x, "*Halli*", '*');
            Assert.AreEqual(2, result.Count());
        }
    }
}
