using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace CommonLibrary.DB
{
	public abstract class DataAccessProvider
	{
		private static log4net.ILog Logger = log4net.LogManager.GetLogger("dbsqllogger");
		protected DbConnection connection;
		protected string connectionString;
		protected DBAccessType dbAccessType;
		protected DbTransaction dbTransaction;

		protected void InitConnection()
		{
			if (string.IsNullOrWhiteSpace(connectionString))
				throw new Exception("connection string is empty");

			if (dbAccessType == DBAccessType.OleDb)
				connection = new OleDbConnection(connectionString);
			else if (dbAccessType == DBAccessType.Oracle)
				connection = new OracleConnection(connectionString);
			else if (dbAccessType == DBAccessType.SQLServer)
				connection = new SqlConnection(connectionString);
			else
				throw new NotImplementedException();
		}

		protected bool OpenConnection()
		{
			bool result = false;

			try
			{
				if (connection == null)
					InitConnection();

				if (connection.State == ConnectionState.Open)
				{
					result = true;
				}
				if (connection.State == ConnectionState.Closed)
				{
					connection.Open();
					result = true;
				}
				else if (connection.State == ConnectionState.Broken)
				{
					connection.Dispose();
					InitConnection();
					connection.Open();
					result = true;
				}
			}
			catch (DbException dbExc)
			{
				Logger.Error("Error in OpenConnection", dbExc);
			}
			catch (Exception exc)
			{
				Logger.Error("Error in OpenConnection", exc);
			}
			finally
			{
			}
			return result;
		}
		protected void CloseConnection()
		{
			if (connection != null)
			{
				if (connection.State == ConnectionState.Open)
					connection.Close();

				if (connection.State != ConnectionState.Closed)
					connection.Dispose();

				connection = null;
			}
		}
		protected void ResetConnection(string connString)
		{
			if (!string.IsNullOrWhiteSpace(connString))
			{
				CloseConnection();
				connectionString = connString;
			}
			else
				throw new ArgumentException("connection string is empty", "connString");
		}

		/// <summary>
		/// 执行sql语句，返回受影响的行数
		/// </summary>
		/// <param name="sql">sql语句</param>
		/// <returns>返回受影响的行数</returns>
		public int ExecuteSQL(string sql)
		{
			return ExecuteSQL(sql, CommandType.Text);
		}
		/// <summary>
		/// 执行sql语句或存储过程，返回受影响的行数
		/// </summary>
		/// <param name="sql">sql语句或存储过程</param>
		/// <param name="cmdType">命令类型，sql语句或存储过程</param>
		/// <param name="parameters">参数数组，可为空</param>
		/// <returns>返回受影响的行数</returns>
		public int ExecuteSQL(string sql, CommandType cmdType, params DbParameter[] parameters)
		{
			int result = 0;

			DbCommand dbCommand = null;
			try
			{
				if (OpenConnection())
				{
					dbCommand = connection.CreateCommand();
					dbCommand.CommandText = sql;
					dbCommand.CommandType = cmdType;
					dbCommand.CommandTimeout = 60;
					if (parameters?.Any() == true)
						foreach (var p in parameters)
							dbCommand.Parameters.Add(p);

					result = dbCommand.ExecuteNonQuery();
				}
				else
				{
					Logger.Error("Open connection failed");
				}
			}
			catch (DbException dbExc)
			{
				Logger.Error("Error in ExecuteSQL", dbExc);
			}
			catch (Exception exc)
			{
				Logger.Error("Error in ExecuteSQL", exc);
			}
			finally
			{
				dbCommand?.Dispose();
				//CloseConnection();

				Logger.Info(sql);
			}

			return result;
		}

		/// <summary>
		/// 执行select sql语句，返回List
		/// </summary>
		/// <typeparam name="T">对应实体</typeparam>
		/// <param name="sql">查询语句</param>
		/// <returns>List列表</returns>
		public List<T> ReadList<T>(string sql) where T : BaseEntity, new()
		{
			List<T> result = new List<T>();

			DbDataReader reader = null;

			DbCommand dbCommand = null;
			try
			{
				if (OpenConnection())
				{
					dbCommand = connection.CreateCommand();
					dbCommand.CommandText = sql;
					dbCommand.CommandTimeout = 60;

					reader = dbCommand.ExecuteReader();

					PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
					while (reader.Read())
					{
						result.Add(ReadEntity<T>(reader, properties));
					}
				}
				else
				{
					Logger.Error("Open connection failed");
				}
			}
			catch (ArgumentNullException aexc)
			{
				Logger.Error("Error in ReadList", aexc);
			}
			catch (FormatException fexc)
			{
				Logger.Error("Error in ReadList", fexc);
			}
			catch (DbException dbExc)
			{
				Logger.Error("Error in ReadList", dbExc);
			}
			catch (Exception exc)
			{
				Logger.Error("Error in ReadList", exc);
			}
			finally
			{
				dbCommand?.Dispose();

				Logger.Info(sql);
			}

			return result;
		}
		private T ReadEntity<T>(DbDataReader r, PropertyInfo[] properties) where T : BaseEntity, new()
		{
			T result = new T();

			object val = null;
			for (var i = 0; i < r.FieldCount; i++)
			{
				var name = r.GetName(i);
				var p = GetProperty(properties, name);
				val = r[i];
				if (p != null && val != null && !string.IsNullOrWhiteSpace(val.ToString()))
				{
					if (p.PropertyType.IsAssignableFrom(r[i].GetType()))
						p.SetValue(result, r[i], null);
					else
					{
						Type ptype = p.PropertyType;
						if (ptype.IsGenericType && ptype.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
							ptype = Nullable.GetUnderlyingType(ptype);

						SetPropertyValue(r[i].ToString(), result, p, ptype);
					}
				}
			}

			return result;
		}
		public List<string> ReadList(string sql)
		{
			List<string> result = new List<string>();

			DbDataReader reader = null;
			DbCommand dbCommand = null;
			try
			{
				if (OpenConnection())
				{
					dbCommand = connection.CreateCommand();
					dbCommand.CommandText = sql;
					dbCommand.CommandTimeout = 60;

					reader = dbCommand.ExecuteReader();

					while (reader.Read())
					{
						if (reader[0] != DBNull.Value && !string.IsNullOrEmpty(reader[0].ToString()) && !result.Contains(reader[0].ToString()))
							result.Add(reader[0].ToString());
					}
				}
				else
					Logger.Error("Open connection failed");
			}
			catch (DbException dbExc)
			{
				Logger.Error("Error in ReadList", dbExc);
			}
			catch (Exception exc)
			{
				Logger.Error("Error in ReadList", exc);
			}
			finally
			{
				dbCommand?.Dispose();

				Logger.Info(sql);
			}

			return result;
		}

		/// <summary>
		/// 根据实体更新对应的数据库表
		/// </summary>
		/// <param name="toUpdate">存放更新的字段</param>
		/// <param name="condition">存放更新条件字段</param>
		/// <param name="toUpdateColNames">存放更新的数据库表的字段名(sql 更新语句 set的字段名)</param>
		/// <param name="conditionColNames">存放更新条件的字段名(sql 更新语句 where后面的字段名)</param>
		/// <returns></returns>
		public bool Update(BaseEntity toUpdate, BaseEntity condition, string[] toUpdateColNames, string[] conditionColNames)
		{
			var sql = toUpdate.GenerateUpdateSql(condition, toUpdateColNames, conditionColNames);
			return ExecuteSQL(sql) > 0;
		}

		public object ExecuteScalar(string sql, CommandType cmdType, params DbParameter[] parameters)
		{
			object result = null;
			DbCommand dbCommand = null;
			try
			{
				if (OpenConnection())
				{
					dbCommand = connection.CreateCommand();
					dbCommand.CommandTimeout = 60;
					dbCommand.CommandText = sql;
					dbCommand.CommandType = cmdType;
					if (parameters?.Any() == true)
						foreach (var p in parameters)
							dbCommand.Parameters.Add(p);

					result = dbCommand.ExecuteScalar();
				}
				else
				{
					Logger.Error("Open connection failed");
				}
			}
			catch (DbException dbExc)
			{
				Logger.Error("Error in ExecuteScalar", dbExc);
			}
			catch (Exception exc)
			{
				Logger.Error("Error in ExecuteScalar", exc);
			}
			finally
			{
				dbCommand?.Dispose();
				//CloseConnection();

				Logger.Info(sql);
			}

			return result;
		}

		public string ExecuteScalarString(string sql, CommandType cmdType, params DbParameter[] parameters)
		{
			object obj = ExecuteScalar(sql, cmdType, parameters);
			return obj.ToString(true, string.Empty);
		}
		public int? ExecuteScalarInt(string sql, CommandType cmdType, params DbParameter[] parameters)
		{
			object obj = ExecuteScalar(sql, cmdType, parameters);
			return obj.ToNullableInt32(true);
		}
		public DateTime? ExecuteScalarDateTime(string sql, CommandType cmdType, params DbParameter[] parameters)
		{
			object obj = ExecuteScalar(sql, cmdType, parameters);
			return obj.ToNullableDateTime(true);
		}
		private PropertyInfo GetProperty(PropertyInfo[] properties, string fieldName)
		{
			foreach (var p in properties)
			{
				var attrs = p.GetCustomAttributes(false);
				var attrColName = getColName(attrs);
				var colName = string.IsNullOrEmpty(attrColName) ? p.Name : attrColName;

				if (colName.Equals(fieldName, StringComparison.OrdinalIgnoreCase))
					return p;
			}
			return null;
		}
		private string getColName(object[] attrs)
		{
			if (attrs != null)
				foreach (var itm in attrs)
				{
					DbEntityAttribute dbEntityAttribute = itm as DbEntityAttribute;
					if (dbEntityAttribute != null)
						return dbEntityAttribute.ColName;
				}

			return string.Empty;
		}
		private void SetPropertyValue<T>(string v, T result, PropertyInfo p, Type ptype) where T : new()
		{
			switch (ptype.Name.ToLower())
			{
				case "string":
					{
						p.SetValue(result, v, null);
					}
					break;
				case "datetime":
					{
						p.SetValue(result, v.ToDateTime(false), null);
					}
					break;
				case "int32":
					{
						p.SetValue(result, v.ToInt32(false), null);
					}
					break;
				case "uint32":
					{
						p.SetValue(result, v.ToUInt32(false), null);
					}
					break;
				case "int16":
					{
						p.SetValue(result, v.ToInt16(false), null);
					}
					break;
				case "uint16":
					{
						p.SetValue(result, v.ToUInt16(false), null);
					}
					break;
				case "int64":
					{
						p.SetValue(result, v.ToInt64(false), null);
					}
					break;
				case "uint64":
					{
						p.SetValue(result, v.ToUInt64(false), null);
					}
					break;
				case "boolean":
					{
						p.SetValue(result, v.ToBoolean(false), null);
					}
					break;
				case "double":
					{
						p.SetValue(result, v.ToDouble(false), null);
					}
					break;
				case "decimal":
					{
						p.SetValue(result, v.ToDecimal(false), null);
					}
					break;
				case "float":
					{
						p.SetValue(result, v.ToFloat(false), null);
					}
					break;
				case "char":
					{
						p.SetValue(result, v.ToChar(false), null);
					}
					break;
			}
		}

		public void BeginTransaction()
		{
			if (connection.State == ConnectionState.Open)
			{
				try
				{
					dbTransaction = connection.BeginTransaction();
				}
				catch (SqlException exc)
				{
					Logger.Error("Error in BeginTransaction", exc);
				}
				catch (DbException exc)
				{
					Logger.Error("Error in BeginTransaction", exc);
				}
				catch (Exception exc)
				{
					Logger.Error("Error in BeginTransaction", exc);
				}
			}
		}

		public void CommitTransaction()
		{
			if (connection.State == ConnectionState.Open && dbTransaction != null)
			{
				try
				{
					dbTransaction.Commit();
				}
				catch (SqlException exc)
				{
					Logger.Error("Error in BeginTransaction", exc);
				}
				catch (DbException exc)
				{
					Logger.Error("Error in BeginTransaction", exc);
				}
				catch (Exception exc)
				{
					Logger.Error("Error in BeginTransaction", exc);
				}
			}
		}
		public void RollBackTransaction()
		{
			if (connection.State == ConnectionState.Open && dbTransaction != null)
			{
				try
				{
					dbTransaction.Rollback();
				}
				catch (SqlException exc)
				{
					Logger.Error("Error in BeginTransaction", exc);
				}
				catch (DbException exc)
				{
					Logger.Error("Error in BeginTransaction", exc);
				}
				catch (Exception exc)
				{
					Logger.Error("Error in BeginTransaction", exc);
				}
			}
		}
		public void Dispose()
		{
			if (connection != null)
			{
				if (connection.State == ConnectionState.Open)
					connection.Close();
				connection.Dispose();
				connection = null;
			}
		}
	}
	public enum DBAccessType
	{
		OleDb,
		Oracle,
		SQLServer
	}
}
