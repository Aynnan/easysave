using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using EasySave.MVVM.Model;
using Newtonsoft.Json;

namespace EasySaveServer
{
    class Server
    {
        private Socket socket;
        private Socket _client;
        public Server()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Start();

        }

        public void Start()
        {
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1"); // Adresse IP du serveur
            int port = 1234; // Port sur lequel le serveur
            socket.Connect(new IPEndPoint(ipAddress, port)); // Connecte le socket au serveur
                                                             // 
            socket.Bind(new IPEndPoint(ipAddress, port));
            socket.Listen(1); // Limite le nombre de connexions simultanées à 1
            Console.WriteLine("En attente de connexion");
            _client = socket.Accept();
            Console.WriteLine("Client connecté : " + _client.RemoteEndPoint);
            SendHello();
            Thread threadRead = new Thread(ReadData);
            threadRead.Start();
         
        }

        private void ReadData()
        {
            while (true) {
                byte[] buffer = new byte[1024];
                string message = Encoding.ASCII.GetString(buffer);
                Action(message);
            }
        }


        private void Action(string message)
        {
            string action = message.Split(' ')[0]; 
            switch(action)
            {
                case "play":
                    Console.WriteLine("User want to play");
                    break;
                case "stop":
                    Console.WriteLine("User wants to stop");
                    break;
                case "deconnection":
                    _client.Close();
                    break;

            }
        }

       private void SendHello()
        {
            List<Work> works = new Works().getList();
            string json = "travaux " + JsonConvert.SerializeObject(works);
            byte[] buffer = Encoding.ASCII.GetBytes(json);
            _client.Send(buffer); 


        }

    }
}
