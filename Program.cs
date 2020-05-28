// #define UNITY
// #define SERVER
using System;
using System.Security.Cryptography;

namespace U3DTools
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = ClientSocket.Create("127.0.0.1",888);
            Console.ReadLine();
        }
    }

#if SERVER
    public class A
    {
        // public int value{get;set;}
    }

#endif
}
