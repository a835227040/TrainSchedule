using System;

namespace TrainSchedule.Data.SQLite
{
    /// <summary>
    /// 指示序列化的默认值
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class SQLiteRowAttribute : Attribute
    {
        public string rowName { get => _rowName; }
        public bool isSerializationFromDB { get; set; } = true;
        /// <summary>
        /// 判断该属性是否参与Select语句的Where条件
        /// 如果在特性中设置了该属性段，则基类在查询时会将这个字段计入计算。
        /// 除非你指定了isSelectWhereIgnore属性
        /// <para>如果要参与空判断（where name=NULL），将<see cref="isSelectIgnoreNULL"/>的值设置为<see cref="false"/></para>
        /// </summary>
        public bool isSelectWhere { get; set; } = false;
        /// <summary>
        /// 判断该属性是否参与Select语句。
        /// 如果该值为false，且该属性的值为空，则以 "where name=NULL"的形式填入.
        /// </summary>
        public bool isSelectIgnoreNULL { get; set; } = true;
        /// <summary>
        /// 判断该属性是否参与Insert语句，如果该值为false，则不会参与Insert语句
        /// </summary>
        public bool isInsert { get; set; } = false;
        public SQLiteDataType type { get => _type; }
        private SQLiteDataType _type;
        private string _rowName;
        public SQLiteRowAttribute(string rowName,SQLiteDataType type)
        {
            _rowName = rowName;
            _type = type;
        }
    }
}
