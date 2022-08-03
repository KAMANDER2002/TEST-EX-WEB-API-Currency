namespace WEB_API_Curency_Handler.Model
{
    public class DailyCurrency
    {
        public DateTime Date { get; set; }
        public string PreviousURL { get; set; }
        public ValutaCountry Valute { get; set; }
    }
}
