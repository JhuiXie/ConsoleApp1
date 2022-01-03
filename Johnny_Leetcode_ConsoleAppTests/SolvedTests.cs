using Microsoft.VisualStudio.TestTools.UnitTesting;
using Johnny_Leetcode_ConsoleApp;
using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework.Internal;

namespace Johnny_Leetcode_ConsoleApp.Tests
{
    [TestClass()]
    public class SolvedTests
    {
        Solved solved = new Solved();

        [TestMethod()]
        public void CheckStringTest()
        {
            string a = "adasdasdasd";
            string b = "asdasdasdad";
            string c = "adasd";

            var result = solved.CheckString(a, b);

            Assert.IsTrue(solved.CheckString(a, b));
            Assert.IsFalse(solved.CheckString(a, c));


        }

        [TestMethod()]
        public void CheckStringTest1()
        {
            Assert.Fail();
        }
    }
}