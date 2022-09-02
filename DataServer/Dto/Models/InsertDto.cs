namespace DataServer.Dto.Models
{
    public class InsertDto
    {
        public List<InsertDataSet> DataSets { get; set; }

        /// <summary>
        /// Formats the comma separeted values into an appropriate sql values by appending quotes to every value.
        /// </summary>
        /// <returns></returns>
        public string [] FormatDataSets()
        {
            string columns = "";
            string values = "";

            foreach (var dataSet in DataSets)
            {
                columns += $" {dataSet.Column},";
                values += $" \'{dataSet.Value}\',";
            }

            return new string[] { columns.TrimEnd(','), values.TrimEnd(',') };
        }
    }

    public class InsertDataSet
    {
        public string Column { get; set; }
        public string Value { get; set; }
    }
}
