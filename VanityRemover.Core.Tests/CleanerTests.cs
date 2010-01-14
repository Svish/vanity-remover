using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using System.IO;

namespace Geeky.VanityRemover.Core.Tests
{
    [TestFixture]
    public class CleanerTests
    {
        private Cleaner cleaner;
        private FreshDirectory directory;


        #region SetUp + TearDown

        [SetUp]
        public void SetUp()
        {
            cleaner = new Cleaner();

            var dir = Directory.CreateDirectory(Guid.NewGuid().ToString());
            Assume.That(dir.Exists);

            directory = new FreshDirectory(dir);
        }

        [TearDown]
        public void TearDown()
        {
            try { directory.Info.Delete(true); }
            catch (IOException) { }
        }

        #endregion


        #region Helpers: AddSub + AddFile

        private static FreshDirectory AddSub(FreshDirectory dir, string name)
        {
            return new FreshDirectory(dir.Info.CreateSubdirectory(name));
        }

        private static void AddFile(FreshDirectory dir, string name)
        {
            using (File.Create(Path.Combine(dir.FullName, name))) { }
        }

        #endregion

        
        #region Context

        [Test]
        public void Context_Default_IsNotNull()
        {
            Assert.That(cleaner.Context, Is.Not.Null);
        }


        [Test]
        public void Context_SetToNewContext_CanGetSame()
        {
            var expectedContext = new SynchronizationContext();
            cleaner.Context = expectedContext;
            Assert.That(cleaner.Context, Is.SameAs(expectedContext));
        }


        [Test]
        public void Context_SetToNull_ThrowsArgumentNullException()
        {
            Assert.That(() => cleaner.Context = null,
                Throws.TypeOf<ArgumentNullException>());
        }

        #endregion


        #region Clean

        [Test]
        public void Clean_Null_ThrowsArgumentNullException()
        {
            Assert.That(() => cleaner.Clean(null), Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void Clean_Empty_DirectoryIsDeleted()
        {
            cleaner.Clean(directory);
            Assert.IsFalse(directory.Exists);
        }

        [Test]
        public void Clean_EmptyDirectoryInside_DirectoriesAreDeleted()
        {
            AddSub(directory, "foo");

            cleaner.Clean(directory);
            Assert.IsFalse(directory.Exists);
        }

        [Test]
        public void Clean_FileInside_DirectoryIsNotDeleted()
        {
            AddFile(directory, "foo.txt");
            cleaner.Clean(directory);
            Assert.IsTrue(directory.Exists);
        }

        [Test]
        public void Clean_DirectoryWithFileInside_DirectoriesAreNotDeleted()
        {
            var sub = AddSub(directory, "foo");
            AddFile(sub, "foo.txt");

            cleaner.Clean(directory);

            Assert.IsTrue(sub.Exists);
            Assert.IsTrue(directory.Exists);
        }

        [Test]
        public void Clean_EmptyDirectoryAndNotEmptyDirectoryInside_OnlyEmptyDirectoryIsDeleted()
        {
            var foo = AddSub(directory, "foo");
            AddFile(foo, "foo.txt");

            var bar = AddSub(directory, "bar");

            cleaner.Clean(directory);

            Assert.IsTrue(directory.Exists);
            Assert.IsTrue(foo.Exists);
            Assert.IsFalse(bar.Exists);
        }

        #endregion


        #region Start Clean

        [Test]
        public void StartClean_StartsCleaningAndEventIsRaised()
        {
            var eventWasRaised = new ManualResetEvent(false);
            cleaner.CleaningDone += (s, e) => eventWasRaised.Set();

            cleaner.StartCleaning(directory);

            Assert.That(eventWasRaised.WaitOne(250), "Event was not raised");
            Assert.That(directory.Exists, Is.False);
        }

        #endregion


        #region Cleaning Done

        [Test]
        public void CleaningDone_CleaningIsDone_EventIsRaised()
        {
            var eventWasRaised = false;
            cleaner.CleaningDone += (s, e) => eventWasRaised = true;

            cleaner.Clean(directory);

            Assert.IsTrue(eventWasRaised);
        }
        [Test]
        public void CleaningDone_CleaningIsDone_EventIsRaisedOnlyOnce()
        {
            AddSub(directory, "foo");
            AddSub(directory, "bar");

            var counter = 0;
            cleaner.CleaningDone += (s, e) => counter++;

            cleaner.Clean(directory);

            Assert.That(counter, Is.EqualTo(1));
        }

        #endregion


        #region Event: Directory Scanned

        [Test]
        public void DirectoryScanned_DirectoryWithSubs_RaisedOncePerSub()
        {
            AddSub(directory, "foo");
            AddSub(directory, "bar");

            var counter = 0;
            cleaner.DirectoryScanned += (s, e) => counter++;

            cleaner.Clean(directory);
            Assert.That(counter, Is.EqualTo(3));
        }


        [Test]
        public void DirectoryScanned_EmptyDirectory_RaisedWithDirectoryDeleted()
        {
            DirectoryDeletedEventArgs eventArgs = null;
            cleaner.DirectoryScanned += (s, e) => eventArgs = e as DirectoryDeletedEventArgs;

            cleaner.Clean(directory);

            Assert.That(eventArgs, Is.Not.Null);
            Assert.That(eventArgs.Directory, Is.EqualTo(directory));
        }


        [Test]
        public void DirectoryScanned_DirectoryWithSubs_RaisedWithDirectoryDeletedOncePerDirectory()
        {
            var expected = new[]
                               {
                                   directory, 
                                   AddSub(directory, "foo"),
                                   AddSub(directory, "bar"),
                               };

            var actual = new List<DirectoryDeletedEventArgs>();
            cleaner.DirectoryScanned += (s, e) => actual.Add(e as DirectoryDeletedEventArgs);

            cleaner.Clean(directory);
            Assert.That(actual.Select(x => x.Directory), Is.EquivalentTo(expected));
        }

        [Test]
        public void DirectoryScanned_NonExistingDirectory_RaisedWithDirectorySkipped()
        {
            directory.Delete();

            DirectorySkippedEventArgs args = null;
            cleaner.DirectoryScanned += (s, e) => args = e as DirectorySkippedEventArgs;

            cleaner.Clean(directory);

            Assert.That(args, Is.Not.Null);
            Assert.That(args.Directory, Is.EqualTo(directory));
            Assert.That(args.Exception, Is.TypeOf<DirectoryNotFoundException>());
        }

        [Test]
        public void DirectoryScanned_NotEmptyDirectory_RaisedWithDirectorySkipped()
        {
            AddFile(directory, "foobar.txt");

            DirectorySkippedEventArgs args = null;
            cleaner.DirectoryScanned += (s, e) => args = e as DirectorySkippedEventArgs;

            cleaner.Clean(directory);

            Assert.That(args, Is.Not.Null);
            Assert.That(args.Directory, Is.EqualTo(directory));
            Assert.That(args.Exception, Is.TypeOf<IOException>());
        }

        [Test]
        public void DirectoryScanned_Cancelled_RaisedWithDirectorySkipped()
        {
            cleaner.Cancel();

            DirectorySkippedEventArgs args = null;
            cleaner.DirectoryScanned += (s, e) => args = e as DirectorySkippedEventArgs;

            cleaner.Clean(directory);

            Assert.That(args, Is.Not.Null);
            Assert.That(args.Directory, Is.EqualTo(directory));
            Assert.That(args.Exception, Is.TypeOf<UserCancelledException>());
        }
        #endregion


    }
}