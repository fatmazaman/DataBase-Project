using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq.Expressions;
using System.Data;
using System.IO;
using System.Reflection;
using System.Web.UI.WebControls;
using iSharpToolkit.Extensions.Collections.Helper_Classes;
using iSharpToolkit.Text;

namespace iSharpToolkit.Extensions.Collections
{
    /// <summary>
    ///		Class Containing IEnumerable Extensions.
    /// </summary>
    public static partial class IEnumerableExtensions
    {
        #region Constants
        private const string STR_InvalidParam = "The sortExpression contains invalid parameter: {0}!";
        private const string STR_SrtExprsn = "sortExpression";
        #endregion

        /// <summary>
        ///		Converts the IEnumerable&lt;T&gt; to DataTable
        /// </summary>
        /// <typeparam name="T">
        ///		The type of elements in the list.
        /// </typeparam>
        /// <param name="list">
        ///		IEnumerable&lt;T&gt;
        /// </param>
        /// <returns>
        ///		Returns a DataTable containing the data in the IEnumerable&lt;T&gt; given.
        /// </returns>

        public static DataTable ToDataTable<T>(this List<T> items)
        {
            var tb = new DataTable(typeof(T).Name);

            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in props)
            {
                tb.Columns.Add(prop.Name, prop.PropertyType);
            }

            foreach (var item in items)
            {
                var values = new object[props.Length];
                for (var i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }

                tb.Rows.Add(values);
            }

            return tb;
        }

        public static DataTable ToDataTable<T>(this IEnumerable<T> list)
        {
            return ToDataTable<T>(list, null);
        }
        /// <summary>
        ///		Converts the IEnumerable&lt;T&gt; to DataTable
        /// </summary>
        /// <typeparam name="T">
        ///		The type of elements in the list.
        /// </typeparam>
        /// <param name="list">
        ///		IEnumerable&lt;T&gt;
        /// </param>
        /// <param name="ProperiesToAdd">
        ///		Properties to add
        /// </param>
        /// <returns>
        ///		Returns a DataTable containing the data in the IEnumerable&lt;T&gt; given.
        /// </returns>
        public static DataTable ToDataTable<T>(this IEnumerable<T> list, params string[] ProperiesToAdd)
        {
            if (list == null)
                throw new ArgumentNullException("list", "list is null.");

            List<PropertyInfo> properties = CreatePropertiesMapping<T>(ProperiesToAdd);

            DataTable dt = CreateDataTable<T>(properties);

            foreach (T t in list)
                FillData(properties, dt, t);

            return dt;
        }

        /// <summary>
        ///		Converts the IEnumerable&lt;T&gt; to XML string
        /// </summary>
        /// <typeparam name="T">
        ///		The type of elements in the list.
        /// </typeparam>
        /// <param name="list">
        ///		IEnumerable&lt;T&gt;
        /// </param>
        /// <returns>
        ///		Returns an XML string containing the data in the IEnumerable&lt;T&gt; given.
        /// </returns>
        public static string ToXML<T>(this IEnumerable<T> list)
        {
            return ToXML<T>(list, XmlWriteMode.WriteSchema, null);
        }
        /// <summary>
        /// Converts the IEnumerableT to XML string
        /// </summary>
        /// <typeparam name="T">
        /// The type of elements in the list.
        /// </typeparam>
        /// <param name="list">
        /// IEnumerableT
        /// </param>
        /// <param name="ProperiesToAdd">
        /// Properties to add
        /// </param>
        /// <returns>
        /// Returns an XML string containing the data in the IEnumerableT given.
        /// </returns>
        public static string ToXML<T>(this IEnumerable<T> list, params string[] ProperiesToAdd)
        {
            XmlWriteMode mode = XmlWriteMode.WriteSchema;
            return ToXML<T>(list, mode, ProperiesToAdd);
        }
        /// <summary>
        ///		Converts the IEnumerable&lt;T&gt; to XML string
        /// </summary>
        /// <typeparam name="T">
        ///		The type of elements in the list.
        /// </typeparam>
        /// <param name="list">
        ///		IEnumerable&lt;T&gt;
        /// </param>
        /// <param name="mode">
        ///		Xml Write Mode
        /// </param>
        /// <param name="ProperiesToAdd">
        ///		Properties to add
        /// </param>
        /// <returns>
        ///		Returns an XML string containing the data in the IEnumerable&lt;T&gt; given.
        /// </returns>
        public static string ToXML<T>(this IEnumerable<T> list, XmlWriteMode mode, params string[] ProperiesToAdd)
        {
            if (list == null)
                return String.Empty;

            StringWriter xmlString = new StringWriter();

            DataTable dt = list.ToDataTable(ProperiesToAdd);
            dt.TableName = typeof(T).Name;
            dt.WriteXml(xmlString, mode);

            return xmlString.ToString();
        }
        /// <summary>
        ///		Writes the current data, for the IEnumerable&lt;T&gt; to the specified file.
        /// </summary>
        /// <typeparam name="T">
        ///		The type of elements in the list.
        /// </typeparam>
        /// <param name="list">
        ///		IEnumerable&lt;T&gt;
        /// </param>
        /// <param name="fileName">
        ///		The file name (including the path) to which to write.
        /// </param>
        /// <param name="ProperiesToAdd">
        ///		Properties to add
        /// </param>
        /// <exception cref="System.Security.SecurityException">
        ///     System.Security.Permissions.FileIOPermission is not set to System.Security.Permissions.FileIOPermissionAccess.Write.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// If list is null. 
        /// If fileName is null or empty.
        /// </exception>
        public static void WriteXml<T>(this IEnumerable<T> list, string fileName, params string[] ProperiesToAdd)
        {
            if (list == null)
                throw new ArgumentNullException("list", "list is null.");
            if (String.IsNullOrEmpty(fileName))
                throw new ArgumentException("fileName is null or empty.", "fileName");

            DataTable dt = list.ToDataTable(ProperiesToAdd);
            dt.TableName = typeof(T).Name;
            dt.WriteXml(fileName, XmlWriteMode.WriteSchema);
        }

        /// <summary>
        ///		Converts the IEnumerable&lt;T&gt; to CSV string
        /// </summary>
        /// <typeparam name="T">
        ///		The type of elements in the list.
        /// </typeparam>
        /// <param name="list">
        ///		IEnumerable&lt;T&gt;
        /// </param>
        /// <returns>
        ///		Returns a CSV string containing the data in the IEnumerable&lt;T&gt; given.
        /// </returns>
        public static string ToCSV<T>(this IEnumerable<T> list)
        {
            if (list == null)
                return String.Empty;

            List<PropertyInfo> properties = CreatePropertiesMapping<T>(null);
            StringBuilder csv = new StringBuilder();

            FillCSVData(csv, properties, pi => pi.Name);
            foreach (T t in list)
                FillCSVData(csv, properties, pi =>
                {
                    object piGetValue = pi.GetValue(t, null);
                    if (piGetValue != null)
                        return piGetValue.ToString();
                    else
                        return "";
                });

            return csv.ToString();
        }
        /// <summary>
        ///		Converts the IEnumerable&lt;T&gt; to CSV string
        /// </summary>
        /// <typeparam name="T">
        ///		The type of elements in the list.
        /// </typeparam>
        /// <param name="list">
        ///		IEnumerable&lt;T&gt;
        /// </param>
        /// <param name="columns">
        ///		Parent to add. 
        ///		'First' = Column Name
        ///		'Second' = Name of the property.
        /// </param>
        /// <returns>
        ///		Returns a CSV string containing the data in the IEnumerable&lt;T&gt; given.
        /// </returns>
        public static string ToCSV<T>(this IEnumerable<T> list, params KeyPair[] columns)
        {
            if (list == null)
                return String.Empty;

            if (columns == null || columns.Length == 0)
                return ToCSV<T>(list);

            List<PropertyInfo> properties = CreatePropertiesMapping<T>(columns.Select(c => c.Second));
            StringBuilder csv = new StringBuilder();

            FillCSVData(csv, columns.Select(col => col.First));
            foreach (T t in list)
                FillCSVData(csv, properties, pi =>
                {
                    object piGetValue = pi.GetValue(t, null);
                    if (piGetValue != null)
                        return piGetValue.ToString();
                    else
                        return "";
                });

            return csv.ToString();
        }
        /// <summary>
        ///		Converts the IEnumerable&lt;T&gt; to CSV string
        /// </summary>
        /// <typeparam name="T">
        ///		The type of elements in the list.
        /// </typeparam>
        /// <param name="list">
        ///		IEnumerable&lt;T&gt;
        /// </param>
        /// <param name="columns">
        ///		Parent to add. 
        ///		'First' = Column Name
        ///		'Second' = That given a &lt;T&gt; object retuns a string representation of the data.
        /// </param>
        /// <returns>
        ///		Returns a CSV string containing the data in the IEnumerable&lt;T&gt; given.
        /// </returns>
        public static string ToCSV<T>(this IEnumerable<T> list, params KeyPair<T>[] columns)
        {
            if (list == null)
                return String.Empty;

            if (columns == null || columns.Length == 0)
                return ToCSV<T>(list);

            StringBuilder csv = new StringBuilder();

            FillCSVData(csv, columns, k => k.First);
            foreach (T t in list)
                FillCSVData(csv, columns, k => k.Second(t));

            return csv.ToString();
        }

        /// <summary>
        ///		Determines the diferences between the current collection(newCollection) and the oldCollection.
        /// </summary>
        /// <remarks>
        ///		Caution: When updating first remove items then add.
        /// </remarks>
        /// <typeparam name="T">
        ///		The type of elements in the list.
        ///	</typeparam>
        /// <param name="newCollection">
        ///		The newer collection. (Usually the collection you want to delete/insert the database collection to.)
        /// </param>
        /// <param name="oldCollection">
        ///		The older collection. (Usually the collection from database.)
        /// </param>
        /// <param name="Added">
        ///		Elements that have been added from to the new collection.
        /// </param>
        /// <param name="Removed">
        ///		Elements that have been removed from the old collection.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        ///		If either collection is null.
        /// </exception>
        /// <exception cref="System.Exception">
        ///		If either collection contains duplicate unique IDs
        /// </exception>
        public static void GetDiferences<T>(this IEnumerable<T> newCollection, IEnumerable<T> oldCollection, out HashSet<T> Added, out HashSet<T> Removed)
        {
            GetDiferences<T>(newCollection, oldCollection, null, out Added, out Removed);
        }
        /// <summary>
        ///		Determines the diferences between the current collection(newCollection) and the oldCollection.
        /// </summary>
        /// <remarks>
        ///		Caution: When updating first remove items then add.
        /// </remarks>
        /// <typeparam name="T">
        ///		The type of elements in the list.
        ///	</typeparam>
        /// <param name="newCollection">
        ///		The newer collection. (Usually the collection you want to delete/insert the database collection to.)
        /// </param>
        /// <param name="oldCollection">
        ///		The older collection. (Usually the collection from database.)
        /// </param>
        /// <param name="comparer">
        ///		The System.Collections.Generic.IEqualityComparer&lt;T&gt; implementation to use
        ///     when comparing values in the set, or null to use the default 
        ///     System.Collections.Generic.EqualityComparer&lt;T&gt; implementation for the set type.
        /// </param>
        /// <param name="Added">
        ///		Elements that have been added from to the new collection.
        /// </param>
        /// <param name="Removed">
        ///		Elements that have been removed from the old collection.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        ///		If either collection is null.
        /// </exception>
        /// <exception cref="System.Exception">
        ///		If either collection contains duplicate unique IDs
        /// </exception>
        public static void GetDiferences<T>(this IEnumerable<T> newCollection, IEnumerable<T> oldCollection, IEqualityComparer<T> comparer, out HashSet<T> Added, out HashSet<T> Removed)
        {
            if (newCollection == null)
                throw new ArgumentNullException("newCollection", "newCollection is null.");
            if (oldCollection == null)
                throw new ArgumentNullException("oldCollection", "oldCollection is null.");

            Added = new HashSet<T>(comparer);
            fillHash<T>(newCollection, comparer, Added, "newCollection has duplicate unique IDs!");
            Added.ExceptWith(oldCollection);

            Removed = new HashSet<T>(comparer);
            fillHash<T>(oldCollection, comparer, Removed, "oldCollection has duplicate unique IDs!");
            Removed.ExceptWith(newCollection);
        }
        /// <summary>
        ///		Determines the diferences between the current collection(newCollection) and the oldCollection.
        /// </summary>
        /// <typeparam name="T">
        ///		The type of elements in the list.
        ///	</typeparam>
        /// <param name="newCollection">
        ///		The newer collection. (Usually the collection you want to delete/insert/update the database collection to.)
        /// </param>
        /// <param name="oldCollection">
        ///		The older collection. (Usually the collection from database.)
        /// </param>
        /// <param name="IdComparer">
        ///		The System.Collections.Generic.IEqualityComparer&lt;T&gt; implementation to use
        ///     when comparing the ID of the values in the set, or null to use the default 
        ///     System.Collections.Generic.EqualityComparer&lt;T&gt; implementation for the set type.
        ///     (Both GetHashCode and Equals methods should only compare ID(s) of T)
        /// </param>
        /// <param name="FullComparer">
        ///		The System.Collections.Generic.IEqualityComparer&lt;T&gt; implementation to use
        ///     when comparing values in the set, or null to use the default 
        ///     System.Collections.Generic.EqualityComparer&lt;T&gt; implementation for the set type.
        ///     (Both GetHashCode method should only compare Id(s) of T, Equals method should compare all values of T)
        /// </param>
        /// <param name="Added">
        ///		Elements that have been added from to the new collection.
        /// </param>
        /// <param name="Removed">
        ///		Elements that have been removed from the old collection.
        /// </param>
        /// <param name="Updated">
        ///		Elements that have been updated from the old collection.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        ///		If either collection is null.
        /// </exception>
        /// <exception cref="System.Exception">
        ///		If either collection contains duplicate unique IDs
        /// </exception>
        public static void GetDiferences<T>(this IEnumerable<T> newCollection, IEnumerable<T> oldCollection, IEqualityComparer<T> IdComparer, IEqualityComparer<T> FullComparer, out HashSet<T> Added, out HashSet<T> Removed, out HashSet<T> Updated)
        {
            GetDiferences(newCollection, oldCollection, IdComparer, out Added, out Removed);

            HashSet<T> Same = new HashSet<T>(newCollection, IdComparer);
            Same.ExceptWith(Added);

            Updated = new HashSet<T>(Same, FullComparer);
            Updated.ExceptWith(oldCollection);
        }

        /// <summary>
        ///		Determines the diferences between the current collection(newCollection) and the oldCollection.
        /// </summary>
        /// <remarks>
        ///		Caution: When updating first remove items then add.
        /// </remarks>
        /// <typeparam name="T">
        ///		The type of elements in the list.
        ///	</typeparam>
        /// <param name="newCollection">
        ///		The newer collection. (Usually the collection you want to delete/insert the database collection to.)
        /// </param>
        /// <param name="oldCollection">
        ///		The older collection. (Usually the collection from database.)
        /// </param>
        /// <param name="Added">
        ///		Elements that have been added from to the new collection.
        /// </param>
        /// <param name="Removed">
        ///		Elements that have been removed from the old collection.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        ///		If either collection is null.
        /// </exception>
        public static void GetDiferences<T>(this IEnumerable<T> newCollection, IEnumerable<T> oldCollection, out IEnumerable<T> Added, out IEnumerable<T> Removed)
        {
            GetDiferences<T>(newCollection, oldCollection, null, out Added, out Removed);
        }
        /// <summary>
        ///		Determines the diferences between the current collection(newCollection) and the oldCollection.
        /// </summary>
        /// <remarks>
        ///		Caution: When updating first remove items then add.
        /// </remarks>
        /// <typeparam name="T">
        ///		The type of elements in the list.
        ///	</typeparam>
        /// <param name="newCollection">
        ///		The newer collection. (Usually the collection you want to delete/insert the database collection to.)
        /// </param>
        /// <param name="oldCollection">
        ///		The older collection. (Usually the collection from database.)
        /// </param>
        /// <param name="comparer">
        ///		The System.Collections.Generic.IEqualityComparer&lt;T&gt; implementation to use
        ///     when comparing values in the set, or null to use the default 
        ///     System.Collections.Generic.EqualityComparer&lt;T&gt; implementation for the set type.
        /// </param>
        /// <param name="Added">
        ///		Elements that have been added from to the new collection.
        /// </param>
        /// <param name="Removed">
        ///		Elements that have been removed from the old collection.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        ///		If either collection is null.
        /// </exception>
        public static void GetDiferences<T>(this IEnumerable<T> newCollection, IEnumerable<T> oldCollection, IEqualityComparer<T> comparer, out IEnumerable<T> Added, out IEnumerable<T> Removed)
        {
            if (newCollection == null)
                throw new ArgumentNullException("newCollection", "newCollection is null.");
            if (oldCollection == null)
                throw new ArgumentNullException("oldCollection", "oldCollection is null.");

            Added = newCollection.Except(oldCollection, comparer);
            Removed = oldCollection.Except(newCollection, comparer);
        }
        /// <summary>
        ///		Determines the diferences between the current collection(newCollection) and the oldCollection.
        /// </summary>
        /// <typeparam name="T">
        ///		The type of elements in the list.
        ///	</typeparam>
        /// <param name="newCollection">
        ///		The newer collection. (Usually the collection you want to delete/insert/update the database collection to.)
        /// </param>
        /// <param name="oldCollection">
        ///		The older collection. (Usually the collection from database.)
        /// </param>
        /// <param name="IdComparer">
        ///		The System.Collections.Generic.IEqualityComparer&lt;T&gt; implementation to use
        ///     when comparing the ID of the values in the set, or null to use the default 
        ///     System.Collections.Generic.EqualityComparer&lt;T&gt; implementation for the set type.
        ///     (Both GetHashCode and Equals methods should only compare ID(s) of T)
        /// </param>
        /// <param name="FullComparer">
        ///		The System.Collections.Generic.IEqualityComparer&lt;T&gt; implementation to use
        ///     when comparing values in the set, or null to use the default 
        ///     System.Collections.Generic.EqualityComparer&lt;T&gt; implementation for the set type.
        ///     (Both GetHashCode method should only compare Id(s) of T, Equals method should compare all values of T)
        /// </param>
        /// <param name="Added">
        ///		Elements that have been added from to the new collection.
        /// </param>
        /// <param name="Removed">
        ///		Elements that have been removed from the old collection.
        /// </param>
        /// <param name="Updated">
        ///		Elements that have been updated from the old collection.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        ///		If either collection is null.
        /// </exception>
        public static void GetDiferences<T>(this IEnumerable<T> newCollection, IEnumerable<T> oldCollection, IEqualityComparer<T> IdComparer, IEqualityComparer<T> FullComparer, out IEnumerable<T> Added, out IEnumerable<T> Removed, out IEnumerable<T> Updated)
        {
            GetDiferences(newCollection, oldCollection, IdComparer, out Added, out Removed);

            Updated = newCollection.Except(Added, IdComparer)
                                   .Except(oldCollection, FullComparer);
        }

        /// <summary>
        /// Sorts a list of type T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="Collection">
        /// Collection of type T to return.
        /// </typeparam>
        /// <param name="list">
        /// The list to sort.
        /// </param>
        /// <param name="sortExpression">
        /// The name of the property on the parameter object to sort by.
        /// </param>
        /// <returns>
        /// The sorted list of type Collection.
        /// </returns>
        public static Collection Sort<T, Collection>(this IEnumerable<T> list, string sortExpression) where Collection : List<T>, new()
        {
            return Sort<T, Collection>(list, sortExpression, null);
        }
        /// <summary>
        ///		Sorts a list of type T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="Collection">
        ///		Collection of type T to return.
        /// </typeparam>
        /// <param name="list">
        ///		The list to sort.
        ///	</param>
        /// <param name="sortExpression">
        ///		The name of the property on the parameter object to sort by.
        /// </param>
        /// <param name="comparer">
        ///		Custom comparer to use.
        /// </param>
        /// <returns>
        ///		The sorted list of type Collection.
        ///	</returns>
        public static Collection Sort<T, Collection>(this IEnumerable<T> list, string sortExpression, IComparer<object> comparer) where Collection : List<T>, new()
        {
            if (list == null)
                return null;

            Collection collection = new Collection();
            if (list.FirstOrDefault() == null)
                return collection;
            if (String.IsNullOrEmpty(sortExpression))
            {
                if (list is Collection)
                    return list as Collection;

                collection.AddRange(list);
                return collection;
            }

            IOrderedQueryable<T> enumerable = null;
            IEnumerable<string> sortExprssns = iSharpRegEx.CommaSplitPattern
                                                         .Split(sortExpression)
                                                         .Where(exp => !String.IsNullOrEmpty(exp));
            string firstExpr = sortExprssns.FirstOrDefault();
            if (firstExpr != null)
            {
                SortDirection sortDirection = SortDirection.Ascending;
                Expression<Func<T, object>> lambda;

                if (GetParameters<T>(ref firstExpr, out sortDirection, out lambda))
                    if (comparer == null)
                        enumerable = (sortDirection == SortDirection.Ascending ?
                                      list.AsQueryable<T>().OrderBy<T, object>(lambda) :
                                      list.AsQueryable<T>().OrderByDescending<T, object>(lambda));
                    else
                        enumerable = (sortDirection == SortDirection.Ascending ?
                                      list.AsQueryable<T>().OrderBy<T, object>(lambda, comparer) :
                                      list.AsQueryable<T>().OrderByDescending<T, object>(lambda, comparer));
            }

            foreach (string item in sortExprssns.Skip(1))
            {
                string expr = item;
                SortDirection sortDirection = SortDirection.Ascending;
                Expression<Func<T, object>> lambda;

                if (GetParameters<T>(ref expr, out sortDirection, out lambda))
                    if (comparer == null)
                        enumerable = (sortDirection == SortDirection.Ascending ?
                                      enumerable.ThenBy<T, object>(lambda) :
                                      enumerable.ThenByDescending<T, object>(lambda));
                    else
                        enumerable = (sortDirection == SortDirection.Ascending ?
                                      enumerable.ThenBy<T, object>(lambda, comparer) :
                                      enumerable.ThenByDescending<T, object>(lambda, comparer));
            }
            collection.AddRange(enumerable);
            return collection;
        }

        /// <summary>
        ///		Indicates whether the specified IEnumerable&lt;T&gt; object is null or empty.
        /// </summary>
        /// <returns>
        ///		true if the IEnumerable&lt;T&gt; is null or an empty; otherwise, false.
        /// </returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> list)
        {
            return (list == null || list.FirstOrDefault() == null);
        }

        /// <summary>
        /// This list extension takes two delegate methods
        /// one to determine the match and one to determine inner collection type.
        /// Based on those inputs, it returns List of the item T finding them recursively.
        /// </summary>
        /// <typeparam name="T">Item type to look for.</typeparam>
        /// <param name="collection">Item Collection to look into.</param>
        /// <param name="match">Delegate to match.</param>
        /// <param name="InnerCollectionFunc">Delegate to Inner Items.</param>
        /// <returns>List of T found in Collection recursively.(including InnerCollection)</returns>
        public static List<T> FindItems<T>(this IEnumerable<T> collection, Predicate<T> match, Func<T, IEnumerable<T>> InnerCollectionFunc)
        {
            if (InnerCollectionFunc == null)
                throw new ArgumentNullException("InnerCollectionFunc", "InnerCollectionFunc is null.");
            if (collection == null)
                throw new ArgumentNullException("collection", "collection is null.");
            if (match == null)
                throw new ArgumentNullException("match", "match is null.");

            List<T> Items = new List<T>();
            Queue<T> queue = new Queue<T>();
            foreach (T item in collection)
                queue.Enqueue(item);

            while (queue.Count > 0)
            {
                T childCtrl = queue.Dequeue();
                if (match(childCtrl))
                    Items.Add(childCtrl);

                IEnumerable<T> InnerCollection = InnerCollectionFunc(childCtrl);
                if (InnerCollection != null)
                    foreach (T item in InnerCollection)
                        queue.Enqueue(item);
            }
            return Items;
        }

        #region Helper Functions
        internal static List<PropertyInfo> CreatePropertiesMapping<T>(IEnumerable<string> ProperiesToAdd)
        {
            Type t = typeof(T);
            List<PropertyInfo> mapping = new List<PropertyInfo>();

            if (ProperiesToAdd != null)
                foreach (string propertyName in ProperiesToAdd)
                {
                    PropertyInfo property = t.GetProperty(propertyName);
                    if (property != null)
                        mapping.Add(property);
                    else
                        throw new Exception(String.Format("{0} does not contain property '{1}'",
                                                            t.Name, propertyName));
                }
            if (mapping.Count == 0)
                mapping.AddRange(typeof(T).GetProperties());

            return mapping;
        }

        internal static DataTable CreateDataTable<T>(List<PropertyInfo> properties)
        {
            DataTable dt = new DataTable(typeof(T).Name);
            DataColumn dc = null;
            foreach (PropertyInfo pi in properties)
            {
                dc = new DataColumn();
                dc.ColumnName = pi.Name;
                dc.DataType = pi.PropertyType;
                dt.Columns.Add(dc);
            }
            return dt;
        }
        internal static void FillData<T>(List<PropertyInfo> properties, DataTable dt, T t)
        {
            DataRow dr = dt.NewRow();
            foreach (PropertyInfo pi in properties)
                dr[pi.Name] = pi.GetValue(t, null);
            dt.Rows.Add(dr);
        }

        internal static void FillCSVData(StringBuilder csv, IEnumerable<string> colNames)
        {
            bool PrependComma = false;
            foreach (string colName in colNames)
            {
                AddPropertyToCSV(csv, colName, PrependComma);
                PrependComma = true;
            }
            csv.Append(Environment.NewLine);
        }
        internal static void FillCSVData(StringBuilder csv, List<PropertyInfo> properties, Func<PropertyInfo, string> keySelector)
        {
            bool PrependComma = false;
            foreach (PropertyInfo pi in properties)
            {
                AddPropertyToCSV(csv, keySelector(pi), PrependComma);
                PrependComma = true;
            }
            csv.Append(Environment.NewLine);
        }
        internal static void FillCSVData<T>(StringBuilder csv, KeyPair<T>[] columns, Func<KeyPair<T>, string> keySelector)
        {
            bool PrependComma = false;
            foreach (KeyPair<T> p in columns)
            {
                AddPropertyToCSV(csv, keySelector(p), PrependComma);
                PrependComma = true;
            }
            csv.Append(Environment.NewLine);
        }
        internal static Regex QuoteOrComma = new Regex(@"["",]|\r?\n", RegexOptions.Compiled);
        internal static void AddPropertyToCSV(StringBuilder csv, string str, bool PrependComma)
        {
            if (PrependComma) csv.Append(",");

            bool SurroundWithQuotes = false;
            string s = QuoteOrComma.Replace(str, match =>
            {
                switch (match.Value)
                {
                    case "\n":
                    case "\r\n":
                        return " ";

                    case "\"":
                        SurroundWithQuotes = true;
                        return "\"\"";

                    case ",":
                        SurroundWithQuotes = true;
                        return match.Value;

                    default:
                        return match.Value;
                }
            });
            if (SurroundWithQuotes)
                csv.Append("\"").Append(s).Append("\"");
            else
                csv.Append(s);
        }

        internal static Regex m_descendingSort = new Regex(@"\s+DESC$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        internal static bool GetParameters<T>(ref string parameters, out SortDirection sortDirection, out Expression<Func<T, object>> lambda)
        {
            sortDirection = SortDirection.Ascending;
            lambda = null;

            if (m_descendingSort.IsMatch(parameters))
            {
                sortDirection = SortDirection.Descending;
                parameters = m_descendingSort.Replace(parameters, "");
            }

            GetParamter<T>(ref parameters, ref lambda);
            return true;
        }

        internal static Regex m_property = new Regex(@"(?<=(^|\.)\s*)[a-zA-Z_]\w*(?=\s*($|\.))", RegexOptions.Compiled);
        internal static void GetParamter<T>(ref string parameters, ref Expression<Func<T, object>> lambda)
        {
            MatchCollection properties = m_property.Matches(parameters);
            if (properties.Count > 0)
            {
                ParameterExpression prmExpression = Expression.Parameter(typeof(T), "root");
                MemberExpression keySelectExpr = Expression.Property(prmExpression, properties[0].Value);
                for (int i = 1; i < properties.Count; i++)
                    keySelectExpr = Expression.Property(keySelectExpr, properties[i].Value);

                UnaryExpression converted = Expression.Convert(keySelectExpr, typeof(object));
                lambda = Expression.Lambda<Func<T, object>>(converted, prmExpression);
            }
        }

        internal static void fillHash<T>(IEnumerable<T> collection, IEqualityComparer<T> cmprr, HashSet<T> hash, string errMsg)
        {
            foreach (var item in collection)
                if (!hash.Add(item))
                    throw new DiffException<T>(errMsg,
                        hash.First(itm => cmprr.GetHashCode(itm) == cmprr.GetHashCode(item)), item);
        }
        #endregion
    }
}
