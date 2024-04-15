
namespace InternetBanking.Core.Application.Helpers
{
    public static class ProductIdHelper
    {
        public static string GenerateId()
        {
            DateTime horaActual = DateTime.Now;

            string horaFormateada = horaActual.ToString("HHmmss");

            Random rand = new Random();
            int numeroAleatorio = rand.Next(100, 1000);

            string idUnico = horaFormateada + numeroAleatorio.ToString();

            return idUnico.Substring(0, 9);
        }
    }
}
