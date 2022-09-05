namespace DataServer.Dto.Models
{
    public class DeleteColumnDto
    {
        public string column { get; set; }
        public string FormatColumn()
        {
            return column;
        }
    }
}
