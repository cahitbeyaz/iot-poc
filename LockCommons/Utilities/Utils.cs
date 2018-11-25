using System;
using System.Collections.Generic;
using System.Text;



namespace LockCommons.Utilities
{
    public static class Utils
    {
        public static string GetRandomRequestReferenceNumber()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }


        /// <summary>
        /// Get the array slice between the two indexes.
        /// ... Inclusive for start index, exclusive for end index.
        /// </summary>
        public static T[] Slice<T>(this T[] source, int start, int len)
        {
            // Return new array.
            T[] res = new T[len];
            Array.Copy(source, start, res, 0, len);
            return res;
        }

    }
}
