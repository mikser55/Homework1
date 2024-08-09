using System.Security.Cryptography;
using System.Text;

namespace ConsoleApp1
{
    internal class PaymentSystems
    {
        private static void Main(string[] args)
        {
            Order order = new(1, 12000);

            Console.WriteLine(new PaymentSystem1().GetPayingLink(order));
            Console.WriteLine(new PaymentSystem2().GetPayingLink(order));
            Console.WriteLine(new PaymentSystem3().GetPayingLink(order));
        }
    }

    internal class Order(int id, int amount)
    {
        public int Id { get; } = id;
        public int Amount { get; } = amount;
    }

    internal interface IPaymentSystem
    {
        string GetPayingLink(Order order);
    }

    internal class PaymentSystem1 : IPaymentSystem
    {
        public string GetPayingLink(Order order)
        {
            string hash = GetMD5Hash(order.Id.ToString());
            return $"pay.system1.ru/order?amount={order.Amount}RUB&hash={hash}";
        }

        private string GetMD5Hash(string input)
        {
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = MD5.HashData(inputBytes);
            return string.Concat(hashBytes.Select(b => b.ToString("x2")));
        }
    }

    internal class PaymentSystem2 : IPaymentSystem
    {
        public string GetPayingLink(Order order)
        {
            string hash = GetMD5Hash($"{order.Id}{order.Amount}");
            return $"order.system2.ru/pay?hash={hash}";
        }

        private string GetMD5Hash(string input)
        {
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = MD5.HashData(inputBytes);
            return string.Concat(hashBytes.Select(b => b.ToString("x2")));
        }
    }

    internal class PaymentSystem3 : IPaymentSystem
    {
        private readonly string secretKey = Guid.NewGuid().ToString();

        public string GetPayingLink(Order order)
        {
            string hash = GetSHA1Hash($"{order.Amount}{order.Id}{secretKey}");
            return $"system3.com/pay?amount={order.Amount}&curency=RUB&hash={hash}";
        }

        private string GetSHA1Hash(string input)
        {
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = SHA1.HashData(inputBytes);
            return string.Concat(hashBytes.Select(b => b.ToString("x2")));
        }
    }
}