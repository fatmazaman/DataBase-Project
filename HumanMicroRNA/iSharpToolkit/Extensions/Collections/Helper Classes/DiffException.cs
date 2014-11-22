using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSharpToolkit.Extensions.Collections.Helper_Classes
{
    /// <summary>
    /// Difference Exception, contains two items considered to have the same
    /// ID based on the ID comparer given.
    /// </summary>
    /// <typeparam name="T">Type of Items contained</typeparam>
    public partial class DiffException<T> : Exception
    {
        /// <summary>
        /// Fist Item
        /// </summary>
        public T FirstItem { get; set; }
        /// <summary>
        /// Second Item
        /// </summary>
        public T SecondItem { get; set; }

        /// <summary>
        /// Initializes a new instance of the RJAC.Extensions.DiffException class with 
        /// a specified error message, a reference to the inner exception, and two 
        /// items that have the same ID which are the cause of this exception
        /// </summary>
        /// <param name="message">
        /// The error message that explains the reason for the exception.
        /// </param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, or a null reference
        /// (Nothing in Visual Basic) if no inner exception is specified.
        /// </param>
        /// <param name="firstItem">
        /// First Item
        /// </param>
        /// <param name="secondItem">
        /// Second Item
        /// </param>
        public DiffException(string message, Exception innerException, T firstItem, T secondItem)
            : base(message, innerException)
        {
            this.FirstItem = firstItem;
            this.SecondItem = secondItem;
        }

        /// <summary>
        /// Initializes a new instance of the RJAC.Extensions.DiffException class with 
        /// a specified error message and two items that have the same ID which are 
        /// the cause of this exception
        /// </summary>
        /// <param name="message">
        /// The error message that explains the reason for the exception.
        /// </param>
        /// <param name="firstItem">
        /// First Item
        /// </param>
        /// <param name="secondItem">
        /// Second Item
        /// </param>
        public DiffException(string message, T firstItem, T secondItem)
            : this(message, null, firstItem, secondItem)
        {
        }
    }
}
