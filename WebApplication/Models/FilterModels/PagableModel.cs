namespace WebApplication.Models.FilterModels
{
    public class PagableModel
    {
        public int Skip { get; set; } = 0;
        /// <summary>
        /// Default = 50
        /// </summary>
        public int Take { get; set; } = 50;
    }
}