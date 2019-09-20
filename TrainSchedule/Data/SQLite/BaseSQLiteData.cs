using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TrainSchedule.Data.SQLite
{
    public delegate void ForEachProperty(PropertyInfo info, SQLiteRowAttribute rowInfo);
    //public class JsonExtensionDataAttribute : System.Attribute
    public abstract class BaseSQLiteData
    {
        [SQLiteRow("id",
            SQLiteDataType.Integer)]
        public int id { get; set; }
        protected string sqlSelect
        {
            get
            {
                if (selectParam.Length > 6)
                {
                    var temp = selectHead.ToString() + selectParam.ToString();
                    temp = temp.Remove(temp.Length - 4, 4);
                    return temp;
                }
                else
                    return selectHead.ToString();
            }
        }
        protected string sqlInsert
        {
            get => insertHead.ToString() + insertParam.ToString();
        }
        protected string sqlUpdate
        {
            get => updateHead.ToString() + updateParam.ToString();
        }
        protected string sqlDelete
        {
            get => $"{deleteHead}where id = {id}";
        }
        protected StringBuilder selectHead = new StringBuilder("select * from ");
        protected StringBuilder insertHead = new StringBuilder("insert into ");
        protected StringBuilder updateHead = new StringBuilder("update ");
        protected StringBuilder deleteHead = new StringBuilder("delete from ");
        protected StringBuilder selectParam = new StringBuilder("where ");
        protected StringBuilder updateParam = new StringBuilder();
        protected StringBuilder insertParam = new StringBuilder();
        protected readonly string connectionString;
        protected readonly SQLiteConnection connection;
        /// <summary>
        /// 记录了所有需要序列化的属性
        /// </summary>
        private List<PropertyInfo> infos = new List<PropertyInfo>();
        private bool _insertResult;
        protected BaseSQLiteData(bool insertResult) : this()
        {
            _insertResult = insertResult;
        }
        protected BaseSQLiteData()
        {
            //搜索类的特性
            var classInfo = this.GetType().GetCustomAttribute<SQLiteSerializationAttribute>();
            connectionString = "Data Source="+ AppDomain.CurrentDomain.BaseDirectory + "Data\\" + classInfo.dbName +".db";
            connection = new SQLiteConnection(connectionString);
            //为select语句附加头部
            selectHead.Append(classInfo.tabelName).Append(' ');
            insertHead.Append(classInfo.tabelName).Append(' ');
            updateHead.Append(classInfo.tabelName).Append(" set ");
            deleteHead.Append(classInfo.tabelName).Append(' ');
        }
        private void buildUpdate()
        {
            var downText = "where id = " + id;
            var upStringBuilder = new StringBuilder();
            this.ForEachProperty((info, rowInfo) =>
            {
                switch (rowInfo.type)
                {
                    case SQLiteDataType.Text:
                        upStringBuilder
                            .Append(info.Name).Append(" = '").Append(info.GetValue(this)).Append("',");
                        break;
                }
            });
            upStringBuilder.Remove(upStringBuilder.Length - 1, 1);
            updateParam = upStringBuilder;
            updateParam.Append(' ').Append(downText);
        }
        private void buildInsert()
        {
            insertParam = new StringBuilder();
            StringBuilder keys = new StringBuilder("id,");
            StringBuilder values = new StringBuilder("NULL,");
            this.ForEachProperty((info, rowInfo) =>
            {
                if (rowInfo.isInsert)
                {
                    keys.Append(rowInfo.rowName)
                    .Append(','); ;
                    var value = info.GetValue(this);
                    if (value == null)
                        value = "NULL";
                    switch (rowInfo.type)
                    {
                        case SQLiteDataType.Text:
                        case SQLiteDataType.Integer:
                            values.Append("'").Append(info.GetValue(this)).Append("'")
                            .Append(',');
                            break;
                            
                    }
                }
            });
            keys.Remove(keys.Length - 1, 1);
            values.Remove(values.Length - 1, 1);
            insertParam
                .Append('(').Append(keys).Append(')')
                .Append(' ').Append("values").Append(' ')
                .Append('(').Append(values).Append(')');

        }
        private void buildSelect()
        {
            selectParam = new StringBuilder("where ");
            this.ForEachProperty((info, rowInfo) =>
            {
                if(rowInfo.isSelectWhere && rowInfo.isSerializationFromDB)
                    appendSelectWhere(info,rowInfo);
            });
        }
        private void ForEachProperty(ForEachProperty e)
        {
            if (e == null) return;
            //搜索所有属性
            foreach (var p in this.GetType().GetProperties())
            {
                var attributes = p.GetCustomAttributes(typeof(SQLiteRowAttribute), false);
                if (attributes.Length == 1)
                {
                    var SQLiteRowInfo = attributes[0] as SQLiteRowAttribute;
                    e(p,SQLiteRowInfo);
                }
            }
        }
        /// <summary>
        /// 为基类的SelectWhere附加查询字串
        /// </summary>
        private void appendSelectWhere(PropertyInfo info,SQLiteRowAttribute rowInfo)
        {
            var value = info.GetValue(this);
            //先判断rowinfo
            if (!rowInfo.isSelectIgnoreNULL)
            {
                //如果该值为false，则参与空判断                   
                value = value == null ? "NULL" : value;
                selectParam
                    .Append(rowInfo.rowName)
                    .Append(" = ")
                    .Append("'").Append(value).Append("'")
                    .Append(" and ");
            }
            else
            {
                //如果Ignore不是false，则为空时不再参与where语句
                if(value != null)
                {
                    selectParam
                    .Append(rowInfo.rowName)
                    .Append(" = ")
                    .Append("'").Append(value).Append("'")
                    .Append(" and ");
                }
            }
        }
        /// <summary>
        /// 从数据库中提取所有符合条件的数据
        /// </summary>
        public virtual List<T> Select<T>() where T:BaseSQLiteData
        {
            var result = new List<T>();
            //构建select语句
            buildSelect();

            //读数据
            SQLiteCommand cmd = new SQLiteCommand(sqlSelect,connection);
            connection.Open();
            var ds = new DataSet();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
            
            adapter.Fill(ds);
            if (ds.Tables.Count != 1)
                throw new SQLiteException("tabel数量不等于1 ==>" + ds.Tables.Count);
            var table = ds.Tables[0];
            
            connection.Close();
            //序列化
            foreach (DataRow row in table.Rows)
            {
                var obj = Activator.CreateInstance(this.GetType(),true as object);
                this.ForEachProperty((info, rowInfo) =>
                {
                    if (rowInfo.isSelectWhere)
                    {
                        switch (rowInfo.type)
                        {
                            case SQLiteDataType.Text:
                                var t = row[rowInfo.rowName];
                                info.SetValue(obj, row[rowInfo.rowName]);
                                break;
                            case SQLiteDataType.Integer:
                                info.SetValue(obj, Convert.ToInt32(row[rowInfo.rowName]));
                                break;
                        }
                    }
                });
                if (obj != null)
                    result.Add(obj as T);
            }
            return result;

        }
        /// <summary>
        /// 向数据库中插入数据
        /// </summary>
        /// <returns></returns>
        public virtual int Insert()
        {
            buildInsert();

            SQLiteCommand cmd = new SQLiteCommand(sqlInsert, connection);
            connection.Open();
            var result = cmd.ExecuteNonQuery();
            connection.Close();
            return result;
        }
        /// <summary>
        /// 仅执行update语句而不做其他动作，注意此时当前对象为update之前的数据。
        /// </summary>
        /// <returns></returns>
        public virtual int Update()
        {
            if (!_insertResult) throw new SQLiteException("update语句需要先进行insert语句");
            buildUpdate();
            SQLiteCommand cmd = new SQLiteCommand(sqlUpdate, connection);
            connection.Open();
            var result = cmd.ExecuteNonQuery();
            connection.Close();
            return result;
        }
        /// <summary>
        /// 从数据库中移出这一条数据
        /// </summary>
        /// <returns></returns>
        public virtual void Delete()
        {
            if (!_insertResult) throw new SQLiteException("delete语句需要先进行insert语句");
            SQLiteCommand cmd = new SQLiteCommand(sqlDelete, connection);
            connection.Open();
            var result = cmd.ExecuteNonQuery();
            connection.Close();
        }
        public int GetDBRowCount()
        {
            var classInfo = this.GetType().GetCustomAttribute<SQLiteSerializationAttribute>();
            SQLiteCommand cmd = new SQLiteCommand($"select count(*) from {classInfo.tabelName}", connection);
            connection.Open();
            var ds = new DataSet();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);

            adapter.Fill(ds);
            connection.Close();
            return Convert.ToInt32(ds.Tables[0].Rows[0][0]); 
        }
    }
}
