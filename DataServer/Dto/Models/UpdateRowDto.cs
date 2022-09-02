namespace DataServer.Dto.Models
{
    public class UpdateRowDto
    {
        public string Id { get; set; }
        public List<UpdateRowDataSet> datasets { get; set; }

        /// <summary>
        /// Format list of data sets into executable sql codes
        /// </summary>
        /// <returns></returns>
        public string FormatDataSets()
        {
            string updatingColumns = "";
            if (datasets.Count == 0)
                return "";

            foreach (var dataSet in datasets)
            {
                updatingColumns += $" {dataSet.column} = \'{dataSet.value}\',";
            }
            return updatingColumns.TrimEnd(',');
        }
    }

    public class UpdateRowDataSet
    {
        public string column { get; set; }
        public string value { get; set; }
    }
}
