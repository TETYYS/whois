using NUnit.Framework;
using Whois.Models;
using Whois.Parsers;

namespace Whois.Parsing.Whois.Dotgov.Gov.Gov
{
    [TestFixture]
    public class GovParsingTests : ParsingTests
    {
        private WhoisParser parser;

        [SetUp]
        public void SetUp()
        {
            SerilogConfig.Init();

            parser = new WhoisParser();
        }

        [Test]
        public void Test_not_found()
        {
            var sample = SampleReader.Read("whois.dotgov.gov", "gov", "not_found.txt");
            var response = parser.Parse("whois.dotgov.gov", "gov", sample);

            Assert.Greater(sample.Length, 0);
            Assert.AreEqual(WhoisResponseStatus.NotFound, response.Status);

            Assert.AreEqual(0, response.ParsingErrors);
            Assert.AreEqual("whois.dotgov.gov/gov/NotFound", response.TemplateName);

            Assert.AreEqual("u34jedzcq.gov", response.DomainName);

            Assert.AreEqual(2, response.FieldsParsed);
        }

        [Test]
        public void Test_found()
        {
            var sample = SampleReader.Read("whois.dotgov.gov", "gov", "found.txt");
            var response = parser.Parse("whois.dotgov.gov", "gov", sample);

            Assert.Greater(sample.Length, 0);
            Assert.AreEqual(WhoisResponseStatus.Found, response.Status);

            Assert.AreEqual(0, response.ParsingErrors);
            Assert.AreEqual("whois.dotgov.gov/gov/Found", response.TemplateName);

            Assert.AreEqual("gsa.gov", response.DomainName);

            // Domain Status
            Assert.AreEqual(1, response.DomainStatus.Count);
            Assert.AreEqual("ACTIVE", response.DomainStatus[0]);

            Assert.AreEqual(3, response.FieldsParsed);
        }
    }
}
