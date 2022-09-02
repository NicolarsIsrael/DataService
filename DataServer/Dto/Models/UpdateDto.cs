namespace DataServer.Dto.Models
{
    public class UpdateDto
    {
        public string Id { get; set; }
        public List<UpdateDataSet> DataSets { get; set; }

        /// <summary>
        /// Format list of data sets into executable sql codes
        /// </summary>
        /// <returns></returns>
        public string FormatDataSets()
        {
            string updatingColumns = "";
            foreach (var dataSet in DataSets)
            {
                updatingColumns += $" {dataSet.Column} = \'{dataSet.Value}\',";
            }
            return updatingColumns.TrimEnd(',');
        }
    }

    public class UpdateDataSet
    {
        public string Column { get; set; }
        public string Value { get; set; }
    }
}
