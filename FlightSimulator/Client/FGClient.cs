using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.IO;

namespace FlightSimulator
{
    class FGClient
    {
        TcpClient client;
        NetworkStream stream;
        StreamWriter writer;
        bool isConnected = false;
        public bool Connect(string ip,int port)
        {
            try
            {
                client = new TcpClient(ip, port);
                stream = client.GetStream();
                writer = new StreamWriter(stream);
                isConnected = true;
                return true;
            }

            catch
            {
                return false;
            }
        }

        public void Send(string data)
        {
            if (isConnected) {
                writer.Write(data);
                writer.Flush();
            }
            else
            {
                Console.WriteLine("Error: client not connected.");
            }
        }
        public void Close()
        {
            client.Close();
            stream.Close();
            writer.Close();
            isConnected = false;
        }
    }
}
