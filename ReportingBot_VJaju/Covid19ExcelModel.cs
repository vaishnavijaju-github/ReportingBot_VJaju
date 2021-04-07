namespace ReportingBot_VJaju
{
    public class Covid19ExcelModel
    {
        public int Id { get; set; }
        public int? Age { get; set; }
        public string Gender { get; set; }
        public string HadPositive { get; set; }
        public string HadVaccine { get; set; }
        public string OneWordComment { get; set; }
    }
}
