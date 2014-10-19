using MuliThreadDatabaseOperator;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace MultiThreadDatabaseOperator
{
    class Program
    {
        static void Run()
        {
            DatabaseConnectionManager dcb = new DatabaseConnectionManager(new IPAddress(0x0100007F),"root","t7ggpxfy","bank");
            
                dcb.Open();
                //dcb.Close();
            
        }
        
        static void Main(string[] args)
        {
            Thread[] threads = new Thread[10];

            Run();
            Console.ReadKey();
            for(int i =0;i<threads.Length;i++)
            {
                threads[i] = new Thread(new ThreadStart(Run));
            }
            foreach (Thread t in threads)
            {
                t.Start();
                //t.Join();
            }
            Console.ReadKey();
        }
    }
}
