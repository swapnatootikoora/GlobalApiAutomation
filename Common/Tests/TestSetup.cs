using System;
using Common.Api;
using Common.ConfigUtils;
using Common.Models;
using NUnit.Framework;

namespace Common.Tests
{
    [TestFixture]
    public class TestSetup<T> : Assertions<T> where T : class, IIdentity
    {
        protected AutomationVariables AutomationVariables => AppSettingsInitialization.GetConfigInstance();

        [SetUp]
        public void SetUp()
        {
            Report.WriteToFile("***********************************************************************");
            Report.WriteToFile(TestContext.CurrentContext.Test.Name);
            Report.WriteToFile("***********************************************************************");
        }

        [TearDown]
        public void TearDown()
        {
            Report.WriteToFile("Execution Status: " + TestContext.CurrentContext.Result.Outcome);
        }
    }
}
