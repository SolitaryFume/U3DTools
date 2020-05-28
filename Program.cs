// #define UNITY
using System;
using System.Security.Cryptography;

namespace U3DTools
{
    class Program
    {
        static void Main(string[] args)
        {
            var str = "加密数据";
            var md5value = MD5Help.GetMD5(str);
            System.Console.WriteLine(md5value);
        }
    }

    public class A
    {
        // public int value{get;set;}
    }
}
