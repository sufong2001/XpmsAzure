using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceStack.ServiceClient.Web;
using ServiceStack.Service;
using System.Collections;
using System.Linq;
using ServiceStack.ServiceInterface.Auth;
using Xpms.Core.Models;
using Xpms.Core.Constants;

namespace Xpms.Web.Tests
{
    /// <summary>
    /// Summary description for SignupTest
    /// </summary>
    [TestClass]
    public class SignupTests
    {
        public SignupTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        public static IEnumerable ServiceClients
        {
            get
            {
                return new IServiceClient[] {
                    new JsonServiceClient(Config.ServiceStackBaseUri),
                    new JsvServiceClient(Config.ServiceStackBaseUri),
                    new XmlServiceClient(Config.ServiceStackBaseUri),
                    new Soap11ServiceClient(Config.ServiceStackBaseUri),
                    new Soap12ServiceClient(Config.ServiceStackBaseUri)
                };
            }
        }

        public static IEnumerable RestClients
        {
            get
            {
                return new IRestClient[] {
                    new JsonServiceClient(Config.ServiceStackBaseUri),
                    new JsvServiceClient(Config.ServiceStackBaseUri),
                    new XmlServiceClient(Config.ServiceStackBaseUri),
                };
            }
        }

        public IRestClient ServiceClient
        {
            get
            {
                return new JsonServiceClient(Config.ServiceStackBaseUri);
            }
        }

        [TestMethod]
        public void TestSingnup()
        {
            var respond = ServiceClient.Post<object>("/signup"
                , new SignupRequet { Email = "sufong", Password = "1234" });

            Assert.AreEqual(respond, respond);
        }


        [TestMethod]
        public void TestSingnupActivation()
        {
            var respond = ServiceClient.Patch<string>("/signup"
                , new SignupRequet {  Email = "sufong2001@gmail.com", Password = "gmail123" });

            respond = ServiceClient.Get<string>("/signup/activation/?activationKey=" + respond);

            Assert.AreEqual(respond, respond);
        }

        [TestMethod]
        public void TestForgotPassword()
        {
            var respond = ServiceClient.Post<object>("/password-reset/"
                , new ForgotPasswordRequest { Email = "sufong"});

            Assert.IsNotNull(respond);
        }

        [TestMethod]
        public void TestResetPassword()
        {
            var respond = ServiceClient.Post<string>("/password-reset/"
                , new ForgotPasswordRequest { Email = "sufong" });

            var hash = ServiceClient.Post<string>("/password-reset/"
                , new ForgotPasswordRequest { Stage = PasswordResetStage.Verification, Key = respond });

            var key = ServiceClient.Post<string>("/password-reset/"
                , new ForgotPasswordRequest { Stage = PasswordResetStage.Reset, Key = respond, Hash = hash, NewPassword = "new password" });

            Assert.AreEqual(key, respond);
        }

        [TestMethod]
        public void TestAuth()
        {
            var respond = ServiceClient.Post<object>("/auth/credentials"
                , new Auth { UserName = "sufong", Password = "new password" });


            Assert.AreEqual(respond, respond);
        }
    }
}
