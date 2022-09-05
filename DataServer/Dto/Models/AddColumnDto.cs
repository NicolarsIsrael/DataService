namespace DataServer.Dto.Models
{
    public class AddColumnDto
    {
        public AddColumnDataSet column { get; set; }
        public string FormatColumn()
        {
            // column name and datatype
            var formatedColumn = $"{column.name} {column.dataType}";

            // default value
            if (!string.IsNullOrEmpty(column.defaultValue))
                formatedColumn += $"default \'{column.defaultValue}\'";

            return formatedColumn;
        }
    }

    public class AddColumnDataSet
    {
        public string name { get; set; }
        public string dataType { get; set; }
        public string defaultValue { get; set; }
        
    }

}
