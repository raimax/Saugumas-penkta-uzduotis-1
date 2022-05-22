using System.Net.Sockets;

namespace SAUGUMAS_pirmas
{
    public class Client
    {
        private readonly TcpClient _client;
        private readonly NetworkStream _stream;

        public Client(string server, int port)
        {
            try
            {
                _client = new TcpClient(server, port);
                _stream = _client.GetStream();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in {nameof(Client)}: " + ex.Message);
            }
        }

        public void WaitForResponse()
        {
            try
            {
                // Receive the TcpServer.response.
                byte[] data = new byte[256];
                string responseData = string.Empty;
                int bytes = _stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                Console.WriteLine("Received from app 2: {0}", responseData);

                Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in {nameof(WaitForResponse)}: " + ex.Message);
            }

            Console.WriteLine("\n Press Enter key to exit...");
            Console.Read();
        }

        public void SendData(byte[] data)
        {
            try
            {
                // Send the message to the connected TcpServer.
                _stream.Write(data, 0, data.Length);

                Console.WriteLine("Data sent to app 2");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in {nameof(SendData)}: " + ex.Message);
            }
        }

        private void Close()
        {
            // Close everything.
            _stream.Close();
            _client.Close();
        }
    }
}
