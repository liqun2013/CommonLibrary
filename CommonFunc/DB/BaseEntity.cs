using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CommonLibrary.DB
{
	public abstract class BaseEntity
	{
		public object GetValue(string propertyName)
		{
			var properties = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
			var p = properties.FirstOrDefault(x => x.Name.Equals(propertyName, StringComparison.OrdinalIgnoreCase));
			if (p != null)
				return p.GetValue(this, null);
			else
				return null;
		}
		public void SetValue(string propertyName, object v)
		{
			var properties = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
			var p = properties.FirstOrDefault(x => x.Name.Equals(propertyName, StringComparison.OrdinalIgnoreCase));
			if (p != null)
				p.SetValue(this, v, null);
		}
		/// <summary>
		/// 生成insert语句
		/// </summary>
		/// <param name="insertColNames">要insert的字段
		/// 为空时默认全部字段，否则只insert传进来的字段</param>
		/// <returns>insert sql语句</returns>
		public string GenerateInsertSql(string[] insertColNames)
		{
			var result = string.Empty;
			var t = GetType();
			var properties = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);
			List<string> insertCols = new List<string>();
			List<string> insertVals = new List<string>();
			foreach (var p in properties)
			{
				var attrs = p.GetCustomAttributes(false);
				var attrColName = getColName(attrs);
				var colName = string.IsNullOrWhiteSpace(attrColName) ? p.Name : attrColName;

				if (insertColNames == null || insertColNames?.Any(x => x.Equals(colName, StringComparison.OrdinalIgnoreCase)) == true)
				{
					var v = p.GetValue(this, null);
					if (v != null)
					{
						if (CheckPropertyNeedQuotes(p))
							insertVals.Add("'" + (p.GetValue(this, null)?.ToString() ?? string.Empty) + "'");
						else
							insertVals.Add(v.ToString());
						insertCols.Add(colName);
					}
				}
			}

			if (insertCols.Any() && insertVals.Any())
				result = string.Format("insert into {0} ({1}) values ({2})", t.Name, string.Join(",", insertCols), string.Join(",", insertVals));

			return result;
		}
		/// <summary>
		/// 生成insert语句,插入全部字段
		/// </summary>
		/// <returns>insert sql语句</returns>
		public string GenerateInsertSql()
		{
			return GenerateInsertSql(null);
		}

		/// <summary>
		/// 生成update语句
		/// </summary>
		/// <typeparam name="T">该实体存放准备更新的字段</typeparam>
		/// <param name="objCondition">该实体存放update的条件字段</param>
		/// <param name="toUpdateColNames">更新的数据库表的字段名</param>
		/// <param name="conditionColNames">条件字段名</param>
		/// <returns></returns>
		public string GenerateUpdateSql<T>(T objCondition, string[] toUpdateColNames, string[] conditionColNames) where T : BaseEntity
		{
			var result = string.Empty;
			var t = GetType();
			var properties = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);
			List<string> updateSet = new List<string>();
			List<string> updateWhere = new List<string>();
			foreach (PropertyInfo p in properties)
			{
				var attrs = p.GetCustomAttributes(false);
				var attrColName = getColName(attrs);
				string colName = string.IsNullOrWhiteSpace(attrColName) ? p.Name : attrColName;
				bool quotesRequired = CheckPropertyNeedQuotes(p);

				object vContition = p.GetValue(objCondition, null);
				object vToUpdate = p.GetValue(this, null);

				if (conditionColNames.Any(x => x.Equals(colName, StringComparison.OrdinalIgnoreCase)))
				{
					if (vContition != null)
						updateWhere.Add(string.Format("{0}={1}", colName, (quotesRequired ? "'" + vContition.ToString() + "'" : vContition.ToString())));
					else
						updateWhere.Add($"{colName} is null");
				}//条件字段
				else if (toUpdateColNames.Any(x => x.Equals(colName, StringComparison.OrdinalIgnoreCase)))
				{
					var nullabletype = typeof(Nullable<>).Name;
					string pdefaultVal = string.Empty;
					if (p.PropertyType.Name.Equals(nullabletype, StringComparison.OrdinalIgnoreCase))
					{
						var gtyp = Nullable.GetUnderlyingType(p.PropertyType);
						pdefaultVal = (p.PropertyType.IsValueType ? Activator.CreateInstance(gtyp).ToString() : "NULL");
					}
					else
					{
						pdefaultVal = (p.PropertyType.IsValueType ? Activator.CreateInstance(p.PropertyType).ToString() : "NULL");
					}
					var setVal = vToUpdate != null ?
						(quotesRequired ? "'" + vToUpdate.ToString() + "'" : vToUpdate.ToString())
						: (quotesRequired && !pdefaultVal.Equals("NULL", StringComparison.OrdinalIgnoreCase) ? "'" + pdefaultVal + "'" : pdefaultVal);
					updateSet.Add(string.Format("{0}={1}", colName, setVal));
				}//更新字段
			}

			if (updateSet.Any())
				result = string.Format("update {0} set {1} where {2}", t.Name,
					string.Join(",", updateSet),
					(updateWhere.Any() ? string.Join(" and ", updateWhere) : "1=1"));

			return result;
		}
		/// <summary>
		/// 根据属性信息提供的字段类型，判断生成sql语句时是否要加单引号
		/// 如string类型需要加单引号
		/// </summary>
		/// <param name="p">字段属性信息</param>
		/// <returns></returns>
		private bool CheckPropertyNeedQuotes(PropertyInfo p)
		{
			if (p == null)
				throw new ArgumentNullException("p");

			var nullabletype = typeof(Nullable<>).Name;
			string typName = p.PropertyType.Name;
			string typFullName = p.PropertyType.FullName;

			if ((typName.Equals(nullabletype, StringComparison.OrdinalIgnoreCase) &&
	(!typFullName.Contains("String") && !typFullName.Contains("Boolean") && !typFullName.Contains("Char") && !typFullName.Contains("DateTime")))
	||
	(!typName.Equals(nullabletype, StringComparison.OrdinalIgnoreCase) &&
	!typName.Equals("String", StringComparison.OrdinalIgnoreCase) && !typName.Equals("Boolean", StringComparison.OrdinalIgnoreCase) && !typName.Equals("Char", StringComparison.OrdinalIgnoreCase) && !typName.Equals("DateTime", StringComparison.OrdinalIgnoreCase)))
				return false;
			else
				return true;
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
	}
}
