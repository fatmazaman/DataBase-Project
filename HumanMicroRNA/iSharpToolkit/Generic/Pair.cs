using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSharpToolkit.Generic
{

    /// <summary>
    /// Provides a basic utility class that is used to store two related objects.
    /// </summary>
    public static class Pair
    {
        /// <summary>
        /// Initializes a new instance of the Pair&lt;FirstT, SecondT&gt; class, 
        /// using the specified object pair. 
        /// </summary>
        /// <param name="first">Object assigned to First.</param>
        /// <param name="second">Object assigned to Second.</param>
        public static Pair<FirstT, SecondT> Create<FirstT, SecondT>(FirstT first, SecondT second)
        {
            return new Pair<FirstT, SecondT>(first, second);
        }
    }

    /// <summary>
    /// Provides a basic utility class that is used to store two related objects.
    /// </summary>
    /// <typeparam name="FirstT">Type of the first object</typeparam>
    /// <typeparam name="SecondT">Type of the second object</typeparam>
    [Serializable]
    public partial class Pair<FirstT, SecondT>
    {
        /// <summary>
        /// Gets or sets the fist object of pair.
        /// </summary>
        public FirstT First { get; set; }
        /// <summary>
        /// Gets or sets the second object of pair.
        /// </summary>
        public SecondT Second { get; set; }
        /// <summary>
        /// Initializes a new instance of the Pair&lt;FirstT, SecondT&gt; class, 
        /// using the specified object pair. 
        /// </summary>
        /// <param name="first">Object assigned to First.</param>
        /// <param name="second">Object assigned to Second.</param>
        public Pair(FirstT first, SecondT second)
        {
            this.First = first;
            this.Second = second;
        }
    }
    /// <summary>
    /// Provides a basic utility class that is used to store two related objects.
    /// </summary>
    /// <typeparam name="T">Type of both object</typeparam>
    [Serializable]
    public partial class Pair<T> : Pair<T, T>
    {
        /// <summary>
        /// Initializes a new instance of the Pair&lt;FirstT, SecondT&gt; class, 
        /// using the specified object pair. 
        /// </summary>
        /// <param name="first">Object assigned to First.</param>
        /// <param name="second">Object assigned to Second.</param>
        public Pair(T first, T second) : base(first, second) { }
    }
}
