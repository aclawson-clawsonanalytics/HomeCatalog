using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SQLite
{
	public interface ISQLiteConnection
	{
		string DatabasePath { get; }

		bool TimeExecution { get; set; }

		bool Trace { get; set; }

		bool StoreDateTimeAsTicks { get; }

		void EnableLoadExtension (int onoff);

		TimeSpan BusyTimeout { get; set; }

		IEnumerable<TableMapping> TableMappings { get ; }

		TableMapping GetMapping (Type type, CreateFlags createFlags = CreateFlags.None);

		TableMapping GetMapping<T> ();

		int DropTable<T> ();

		int CreateTable<T> (CreateFlags createFlags = CreateFlags.None);

		int CreateTable (Type ty, CreateFlags createFlags = CreateFlags.None);

		int CreateIndex (string indexName, string tableName, string columnName, bool unique = false);

		int CreateIndex (string tableName, string columnName, bool unique = false);

		void CreateIndex<T> (Expression<Func<T, object>> property, bool unique = false);

//		List<ColumnInfo> GetTableInfo (string tableName);

		SQLiteCommand CreateCommand (string cmdText, params object[] ps);

		int Execute (string query, params object[] args);

		T ExecuteScalar<T> (string query, params object[] args);

		List<T> Query<T> (string query, params object[] args) where T : new();

		IEnumerable<T> DeferredQuery<T> (string query, params object[] args) where T : new();

		List<object> Query (TableMapping map, string query, params object[] args);

		IEnumerable<object> DeferredQuery (TableMapping map, string query, params object[] args);

		TableQuery<T> Table<T> () where T : new();

		T Get<T> (object pk) where T : new();

		T Get<T> (Expression<Func<T, bool>> predicate) where T : new();

		T Find<T> (object pk) where T : new();

		object Find (object pk, TableMapping map);

		T Find<T> (Expression<Func<T, bool>> predicate) where T : new();

		bool IsInTransaction { get; }

		void BeginTransaction ();

		string SaveTransactionPoint ();

		void Rollback ();

		void RollbackTo (string savepoint);

		void Release (string savepoint);

		void Commit ();

		void RunInTransaction (Action action);

		int InsertAll (System.Collections.IEnumerable objects);

		int InsertAll (System.Collections.IEnumerable objects, string extra);

		int InsertAll (System.Collections.IEnumerable objects, Type objType);

		int Insert (object obj);

		int InsertOrReplace (object obj);

		int Insert (object obj, Type objType);

		int InsertOrReplace (object obj, Type objType);

		int Insert (object obj, string extra);

		int Insert (object obj, string extra, Type objType);

		int Update (object obj, Type objType);

		int UpdateAll (System.Collections.IEnumerable objects);

		int Delete (object objectToDelete);

		int Delete<T> (object primaryKey);

		int DeleteAll<T> ();

		void Close ();
	}
}

