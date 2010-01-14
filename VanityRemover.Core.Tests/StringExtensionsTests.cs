using System.IO;
using NUnit.Framework;
using Geeky.VanityRemover.Core.Extensions;

namespace Geeky.VanityRemover.Core.Tests
{
    [TestFixture]
    public class StringExtensionsTests
    {

        [Test]
        public void IsCleanablePath_Null_IsFalse()
        {
            Assert.IsFalse(StringExtensions.IsCleanablePath(null));
        }


        [Test]
        public void IsCleanablePath_EmptyString_IsFalse()
        {
            Assert.IsFalse("".IsCleanablePath());
        }


        [Test]
        public void IsCleanablePath_CurrentDirectory_IsTrue()
        {
            Assert.IsTrue(".".IsCleanablePath());
        }


        [Test]
        public void IsCleanablePath_NonExistingDirectory_IsFalse()
        {
            Assume.That(Directory.Exists("foobar"), Is.False);
            Assert.IsFalse("foobar".IsCleanablePath());
        }
    }
}