using System.Collections.Generic;

namespace GeekyProductions.FolderVanityRemover
{
    internal static class StackExtensions
    {
        public static void Push<T>(this Stack<T> stack, IEnumerable<T> items)
        {
            foreach (var item in items)
                stack.Push(item);
        }
    }
}