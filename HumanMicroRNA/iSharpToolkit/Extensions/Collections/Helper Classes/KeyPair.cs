using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iSharpToolkit.Generic;

namespace iSharpToolkit.Extensions.Collections.Helper_Classes
{
    /// <summary>
    /// Holds a name and a function to select a key from the given type
    /// </summary>
    /// <typeparam name="T">Type to select key from</typeparam>
    public partial class KeyPair<T> : Pair<string, Func<T, string>>
    {
        /// <summary>
        /// Creates a new KeyPair&lt;T&gt;
        /// </summary>
        /// <param name="name">Name of the key</param>
        /// <param name="keySelector">Function that returns string representation of the key</param>
        public KeyPair(string name, Func<T, string> keySelector) : base(name, keySelector) { }
    }

    /// <summary>
    /// Holds a name and the name of a property
    /// </summary>
    public partial class KeyPair : Pair<string, string>
    {
        /// <summary>
        /// Creates a new KeyPair
        /// </summary>
        /// <param name="name">Name of the key</param>
        /// <param name="key">Name of the property</param>
        public KeyPair(string name, string key) : base(name, key) { }
    }
}
