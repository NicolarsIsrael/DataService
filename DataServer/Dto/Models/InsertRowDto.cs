namespace DataServer.Dto.Models
{
    public class InsertRowDto
    {
        public List<InsertRowDataSet> dataSets { get; set; }

        /// <summary>
        /// Formats the datasets to an execuatable sql query
        /// </summary>
        /// <returns></returns>
        public string [] FormatDataSets()
        {
            string columns = "";
            string values = "";

            if (dataSets.Count == 0)
                return new string[] { columns, values };

            foreach (var dataSet in dataSets)
            {
                columns += $" {dataSet.column},";
                values += $" \'{dataSet.value}\',";
            }

            return new string[] { columns.TrimEnd(','), values.TrimEnd(',') };
        }
    }

    public class InsertRowDataSet
    {
        public string column { get; set; }
        public string value { get; set; }
    }
}
