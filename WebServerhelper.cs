﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Core;

namespace Http
{
    public class WebServerhelper
    {
      
        private Socket connectionSocket;

        public WebServerhelper(Socket connectinSocket)
        {
            this.connectionSocket = connectionSocket;
        }

        public static void Main1(string[] args)
        {

            string name = "localhost";
            IPAddress[] addrs = Dns.GetHostEntry(name).AddressList;

            Console.WriteLine(addrs[0]);
            Console.WriteLine(addrs[1]);

            //TcpListener welcomeSocket = new TcpListener(addrs[1], 6789);
            //IPAddress ip = IPAddress.Parse("127.0.0.1");
            //TcpListener serverSocket = new TcpListener(ip, 6789);

            TcpListener serverSocket = new TcpListener(6789);
            serverSocket.Start();

            while (true)
            {
                Socket connectionSocket = serverSocket.AcceptSocket();
                Console.WriteLine("Server activated now");
                WebServerhelper service = new WebServerhelper(connectionSocket);
                service.DoIt();

            }

            serverSocket.Stop();

        }

        private void DoIt()
        { 
            Stream ns = new NetworkStream(connectionSocket);
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true; // enable automatic flushing

            string message = sr.ReadLine();
            string answer;
            while (message != null && message != "")
            {
                Console.WriteLine("Client: " + message);
                answer = message.ToUpper();
                sw.WriteLine(answer);
                message = sr.ReadLine();

            }
            connectionSocket.Close();
        }
    }
}
