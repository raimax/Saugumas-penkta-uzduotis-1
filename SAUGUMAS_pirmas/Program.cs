using System.Security.Cryptography;
using System.Text;

namespace SAUGUMAS_pirmas
{
    internal class Program
    {
        public const string IP_ADDRESS = "127.0.0.1";
        public const int PORT = 1111;

        public static void Main(string[] args)
        {
            Console.WriteLine("Enter text: ");
            string userInput = Console.ReadLine();

            ASCIIEncoding ByteConverter = new ASCIIEncoding();

            byte[] originalData = ByteConverter.GetBytes(userInput);
            byte[] signedData;

            RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();
            RSAParameters key = RSAalg.ExportParameters(true);
            signedData = RsaSignature.HashAndSignBytes(originalData, key);

            SerializedData serializedData = new(originalData, signedData, key);

            byte[] data = Serializer.Serialize(serializedData);



            Client client = new(IP_ADDRESS, PORT);
            client.SendData(data);
            client.WaitForResponse();
        }
    }
}