namespace Orders.Api.Endpoints.Orders.Services
{
    public class CardProcessor
    {

        public async Task<string> ProcessCardAsync(string customerName, CancellationToken token)
        {
            // write code to do this later
            await Task.Delay(1000);
            return "33333999"; // confirmation number
        }
    }
}
