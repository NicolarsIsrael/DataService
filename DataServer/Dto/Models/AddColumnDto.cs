namespace DataServer.Dto.Models
{
    public class AddColumnDto
    {
        public string name { get; set; }
        public string dataType { get; set; }
        public string defaultValue { get; set; }
        public string FormatColumn()
        {
            // column name and datatype
            var formatedColumn = $"{name} {dataType}";

            // default value
            if (!string.IsNullOrEmpty(defaultValue))
                formatedColumn += $"default \'{defaultValue}\'";

            return formatedColumn;
        }
    }

}
