using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;

namespace Geeky.VanityRemover.Core
{
    /// <summary>
    /// Wrapper of <see cref="DirectoryInfo"/> that refreshes to make sure things are updated.
    /// </summary>
    public class FreshDirectory : IEquatable<FreshDirectory>
    {
        private readonly DirectoryInfo directory;


        public FreshDirectory(string path)
        {
            directory = new DirectoryInfo(path);
        }


        public FreshDirectory(DirectoryInfo directoryInfo)
        {
            directory = directoryInfo;
        }

        public string FullName
        {
            get { return directory.FullName; }
        }


        public bool Exists
        {
            get
            {
                directory.Refresh();
                return directory.Exists;
            }
        }

        public DirectoryInfo Info { get { return directory; } }


        public void Delete()
        {
            directory.Delete();
        }


        public IEnumerable<FreshDirectory> GetSubDirectories()
        {
            return directory
                .GetDirectories()
                .Select(x => new FreshDirectory(x));
        }

        public bool Equals(FreshDirectory other)
        {
            if(ReferenceEquals(other, null))
                return false;

            return ReferenceEquals(this, other) 
                || Equals(directory.FullName, other.directory.FullName);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as FreshDirectory);
        }

        public override int GetHashCode()
        {
            return directory.FullName.GetHashCode();
        }

        public override string ToString()
        {
            return directory.FullName;
        }
    }
}