using NUnit.Framework;

namespace Geeky.VanityRemover.Core.Tests
{
    [TestFixture]
    public class OptionsTests
    {
        [Test]
        public void DefaultOptionsAreCorrect()
        {
            var options = Options.Default;

            Assert.IsFalse(options.Quiet, "Quiet should be false.");
            Assert.IsFalse(options.Silent, "Silent should be false.");
            Assert.IsFalse(options.CleanAll, "CleanAll should be false.");

            Assert.IsFalse(options.Verbose, "Verbose should be false.");
        }
    }
}