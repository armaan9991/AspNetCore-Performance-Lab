namespace Api.Controllers.Settings
{
    public class AppSettings
    {
        public int PageSize { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public bool EnableCaching { get; set; }
    }
}