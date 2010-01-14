using NUnit.Framework;

namespace Geeky.VanityRemover.Core.Tests
{
    [TestFixture]
    public class OptionsParserTests
    {
        [Test]
        public void Parse_Null_ReturnsDefault()
        {
            var options = GetParser().Parse(null);
            Assert.That(options, Is.EqualTo(Options.Default));
        }

        [Test]
        public void Parse_Empty_ReturnsDefault()
        {
            var options = GetParser().Parse(new string[0]);
            Assert.That(options, Is.EqualTo(Options.Default));
        }

        [Test]
        public void Parse_SilentSwitch()
        {
            var options = GetParser().Parse("-s");
            Assert.IsTrue(options.Silent);
        }

        [Test]
        public void Parse_QuietSwitch()
        {
            var options = GetParser().Parse("-q");
            Assert.IsTrue(options.Quiet);
        }

        [Test]
        public void Parse_CleanAllSwitch()
        {
            var options = GetParser().Parse("-a");
            Assert.IsTrue(options.CleanAll);
        }

        [Test]
        public void Parse_Verbose()
        {
            var options = GetParser().Parse("-v");
            Assert.IsTrue(options.Verbose);
        }

        [Test]
        public void Parse_MultipleSwitches()
        {
            var options = GetParser().Parse("-q", "-s");
            Assert.IsTrue(options.Quiet);
            Assert.IsTrue(options.Silent);
        }

        private static OptionsParser GetParser()
        {
            return new OptionsParser();
        }
    }
}