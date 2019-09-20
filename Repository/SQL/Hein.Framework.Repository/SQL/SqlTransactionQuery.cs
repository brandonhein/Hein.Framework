using System.Collections.Generic;

namespace Hein.Framework.Repository.SQL
{
    public class SqlTransactionQuery<T> : SqlCommandBase where T : IRepositoryItem
    {
        private SqlTransactionType _transactionType;
        private T _entity;

        public override string SQL
        {
            get
            {
                var sqlCommand =
                    string.Concat(this.BuildCommandStart(),
                                  this.BuildColumnsAndValues());

                return sqlCommand;
            }
        }

        public SqlTransactionQuery<T> Insert(T entity)
        {
            _transactionType = SqlTransactionType.Insert;
            _entity = entity;

            return this;
        }

        public SqlTransactionQuery<T> Update(T entity)
        {
            _transactionType = SqlTransactionType.Update;
            _entity = entity;

            return this;
        }

        public SqlTransactionQuery<T> Delete(T entity)
        {
            _transactionType = SqlTransactionType.Delete;
            _entity = entity;

            return this;
        }

        private string BuildCommandStart()
        {
            var command = "";

            var tableName = typeof(T).Name;
            var attrs = typeof(T).GetCustomAttributes(true);
            foreach (var attr in attrs)
            {
                TableNameAttribute name = attr as TableNameAttribute;
                if (name != null)
                {
                    tableName = name.Value;
                }
            }

            switch (_transactionType)
            {
                case SqlTransactionType.Insert:
                    return string.Concat(command, "INSERT INTO ", tableName);
                case SqlTransactionType.Update:
                    return string.Concat(command, "UPDATE ", tableName);
                case SqlTransactionType.Delete:
                    return string.Concat(command, "DELETE ", tableName);
            }

            return "";
        }

        private string BuildColumnsAndValues()
        {
            if (_transactionType == SqlTransactionType.Insert)
            {
                var columns = new List<string>();
                var values = new List<string>();
                foreach (var prop in typeof(T).GetProperties())
                {
                    bool isKeyProperty = false;
                    bool isExcludeProp = false;
                    var attributes = prop.GetCustomAttributes(true);
                    foreach (var attr in attributes)
                    {
                        KeyAttribute key = attr as KeyAttribute;
                        if (key != null)
                        {
                            isKeyProperty = true;
                            break;
                        }

                        ExcludeAttribute ex = attr as ExcludeAttribute;
                        if (ex != null)
                        {
                            isExcludeProp = true;
                            break;
                        }
                    }

                    if (!isKeyProperty && !isExcludeProp)
                    {
                        columns.Add(prop.Name);
                        values.Add(string.Concat("'", _entity.GetType().GetProperty(prop.Name).GetValue(_entity, null), "'"));
                    }
                }

                var columnString = string.Join(",", columns.ToArray());
                columnString = string.Concat("(", columnString, ")");

                var valueString = string.Join(",", values.ToArray());
                valueString = string.Concat("VALUES (", valueString, ")");

                var result = string.Concat(columnString, valueString, " SELECT SCOPE_IDENTITY()").Replace("''", "NULL");
                return result;
            }

            if (_transactionType == SqlTransactionType.Update)
            {
                var updateDictionary = new Dictionary<string, string>();
                string keyName = string.Empty;

                foreach (var prop in typeof(T).GetProperties())
                {
                    bool isKeyProperty = false;
                    bool isExcludeProp = false;
                    var attributes = prop.GetCustomAttributes(true);
                    foreach (var attr in attributes)
                    {
                        KeyAttribute key = attr as KeyAttribute;
                        if (key != null)
                        {
                            isKeyProperty = true;
                            break;
                        }

                        ExcludeAttribute ex = attr as ExcludeAttribute;
                        if (ex != null)
                        {
                            isExcludeProp = true;
                            break;
                        }
                    }

                    if (!isKeyProperty && !isExcludeProp)
                    {
                        updateDictionary.Add(
                            prop.Name,
                            string.Concat("'", _entity.GetType().GetProperty(prop.Name).GetValue(_entity, null), "'"));
                    }
                    else if (isKeyProperty)
                    {
                        keyName = prop.Name;
                    }
                }

                string setString = string.Empty;
                foreach (var item in updateDictionary)
                {
                    setString = string.Concat(setString,
                        string.Format(" {0} = {1},", item.Key, item.Value));
                }
                setString = setString.TrimEnd(',');

                return string.Format(" SET {0} WHERE {1} = {2}", setString, keyName, _entity.GetId()).Replace("''", "NULL");
            }

            if (_transactionType == SqlTransactionType.Delete)
            {
                string keyName = string.Empty;
                foreach (var prop in typeof(T).GetProperties())
                {
                    bool isKeyProperty = false;
                    var attributes = prop.GetCustomAttributes(true);
                    foreach (var attr in attributes)
                    {
                        KeyAttribute key = attr as KeyAttribute;
                        if (key != null)
                        {
                            isKeyProperty = true;
                            break;
                        }
                    }

                    if (isKeyProperty)
                    {
                        keyName = prop.Name;
                    }
                }

                return string.Format(" WHERE {0} = {1}", keyName, _entity.GetId());
            }

            return string.Empty;
        }
    }
}
