using WEB_API_Curency_Handler.Model;

namespace WEB_API_Curency_Handler.Services
{
    public interface IGetJsonService
    {
        public Task<IEnumerable<Valuta>> GetDataValutesAsync();
    }
}
