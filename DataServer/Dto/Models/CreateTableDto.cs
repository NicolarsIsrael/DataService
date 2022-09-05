namespace DataServer.Dto.Models
{
    public class CreateTableDto
    {
        public List<CreateTableColumnDataSetDto> columns { get; set; }

        public string FormatColumns()
        {
            string formatedColumns = "";
            if (columns.Count == 0)
                return "";

            foreach (var column in columns)
            {
                // column name and datatype
                var columnDetails = $" {column.name} {column.dataType}";

                // primary key
                if (column.primaryKey)
                    columnDetails += " PRIMARY KEY";

                // unique
                if (column.unique)
                    columnDetails += " UNIQUE";
                else
                {
                    if (!string.IsNullOrEmpty(column.defaultValue))
                        columnDetails += $"default \'{column.defaultValue}\'";
                }

                columnDetails += " ,";
                formatedColumns += columnDetails;
            }

            return formatedColumns.TrimEnd(',');
        }
    }

    public class CreateTableColumnDataSetDto
    {
        public string name { get; set; }
        public string dataType { get; set; }
        public bool primaryKey { get; set; }
        public string defaultValue { get; set; }
        public bool unique { get; set; }
    }
}
