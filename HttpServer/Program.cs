﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server("http://localhost:9050/");
            server.Start();
        }
    }
}
