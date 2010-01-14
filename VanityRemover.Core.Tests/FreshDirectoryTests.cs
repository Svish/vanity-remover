using System;
using System.Linq;
using System.IO;
using NUnit.Framework;

namespace Geeky.VanityRemover.Core.Tests
{
    [TestFixture]
    public class FreshDirectoryTests
    {
        private DirectoryInfo emptyDirectory;

        private DirectoryInfo directoryWithSubs;

        [SetUp]
        public void Setup()
        {
            emptyDirectory = Directory.CreateDirectory(Guid.NewGuid().ToString());

            directoryWithSubs = Directory.CreateDirectory(Guid.NewGuid().ToString());
            directoryWithSubs.CreateSubdirectory("foo");
            directoryWithSubs.CreateSubdirectory("bar");
        }

        [TearDown]
        public void TearDown()
        {
            try { emptyDirectory.Delete(true); }
            catch (IOException) { }

            try { directoryWithSubs.Delete(true); }
            catch (IOException) { }
        }


        #region Full Name

        [Test]
        public void FullName_ReturnsCorrectFullName()
        {
            var dir = new FreshDirectory(emptyDirectory);

            Assert.AreEqual(emptyDirectory.FullName, dir.FullName);
        }

        #endregion


        #region Exists

        [Test]
        public void Exists_ExistingDirectory_ReturnsTrue()
        {
            var dir = new FreshDirectory(emptyDirectory);

            Assert.IsTrue(dir.Exists, "Directory should exist.");
        }

        [Test]
        public void Exists_NonExistingDirectory_ReturnsFalse()
        {
            var dir = new FreshDirectory(Guid.NewGuid().ToString());
            Assert.IsFalse(dir.Exists);
        }

        [Test]
        public void Exists_NewlyDeletedDirectory_ReturnsFalse()
        {
            var dir = new FreshDirectory(emptyDirectory);

            emptyDirectory.Delete();

            Assert.IsFalse(dir.Exists);
        }

        #endregion


        #region Delete

        [Test]
        public void Delete_EmptyDirectory_DirectoryIsDeleted()
        {
            var dir = new FreshDirectory(emptyDirectory);
            dir.Delete();

            emptyDirectory.Refresh();
            Assert.IsFalse(emptyDirectory.Exists);
        }

        [Test]
        public void Delete_NonExistingDirectory_ThrowsDirectoryNotFoundException()
        {
            var dir = new FreshDirectory(Guid.NewGuid().ToString());

            Assert.That(() => dir.Delete(), Throws.TypeOf<DirectoryNotFoundException>());
        }

        [Test]
        public void Delete_DirectoryWithSubs_ThrowsIOException()
        {
            var dir = new FreshDirectory(directoryWithSubs);

            Assert.That(() => dir.Delete(), Throws.TypeOf<IOException>());
        }

        #endregion


        #region Get Sub Directories

        [Test]
        public void GetSubDirectories_EmptyDirectory_ReturnsEmpty()
        {
            var dir = new FreshDirectory(emptyDirectory);

            Assert.That(dir.GetSubDirectories(), Is.Empty);
        }

        [Test]
        public void GetSubDirectories_DirectoryWithSubs_ReturnsDirectories()
        {
            var dir = new FreshDirectory(directoryWithSubs);
            var subs = dir.GetSubDirectories().ToArray();

            Assert.That(subs, Has.Length.EqualTo(2));
            Assert.That(subs.Any(x => x.FullName.EndsWith("foo")));
            Assert.That(subs.Any(x => x.FullName.EndsWith("bar")));
        }

        #endregion


        #region Info

        [Test]
        public void Info_CanGetIt()
        {
            var info = new DirectoryInfo("foo");
            var dir = new FreshDirectory(info);

            Assert.That(dir.Info, Is.EqualTo(info));
        }

        #endregion


        #region Equals

        [Test]
        public void Equals_TwoEqual_ReturnsTrue()
        {
            var a = new FreshDirectory("foo");
            var b = new FreshDirectory("foo");
            Assert.IsTrue(a.Equals(b));
            Assert.IsTrue(b.Equals(a));
        }

        [Test]
        public void Equals_TwoUnequal_ReturnsFalse()
        {
            var a = new FreshDirectory("foo");
            var b = new FreshDirectory("bar");
            Assert.IsFalse(a.Equals(b));
            Assert.IsFalse(b.Equals(a));
        }

        [Test]
        public void Equals_Null_ReturnsFalse()
        {
            var a = new FreshDirectory("foo");
            Assert.IsFalse(a.Equals(null));
        }

        [Test]
        public void Equals_OtherObject_ReturnsFalse()
        {
            var a = new FreshDirectory("foo");
            var b = new object();
            Assert.IsFalse(a.Equals(b));
        }

        #endregion


        #region GetHashCode

        [Test]
        public void GetHashCode_TwoEqual_SameHashCode()
        {
            var a = new FreshDirectory("foo");
            var b = new FreshDirectory("foo");
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode(), "Should have same hash code.");
        }

        #endregion


        #region To String

        [Test]
        public void ToString_ReturnsFullName()
        {
            var dir = new FreshDirectory(emptyDirectory);
            Assert.That(dir.ToString(), Is.EqualTo(dir.FullName));
        }

        #endregion
        
    }
}