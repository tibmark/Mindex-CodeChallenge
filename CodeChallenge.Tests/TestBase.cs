using CodeChallenge.Tests.Integration.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;

namespace CodeChallenge.Tests.Integration
{
    public class TestBase
    {
        public static HttpClient _httpClient;
        public static TestServer _testServer;

        [ClassInitialize(InheritanceBehavior.BeforeEachDerivedClass)]
        public static void ClassInitialize(TestContext context)
        {
            // gets called once for each class derived from this class
            // on initialization
            _testServer = new TestServer();
            _httpClient = _testServer.NewClient();
        }

        [ClassCleanup(InheritanceBehavior.BeforeEachDerivedClass)]
        public static void Cleanup()
        {
            // gets called once for each class derived from this class
            // on cleanup
            _httpClient.Dispose();
            _testServer.Dispose();
        }
    }
}