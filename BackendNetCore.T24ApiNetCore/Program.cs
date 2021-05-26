using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendNetCore.T24ApiNetCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        /*
         create or replace table clientes(
            id int not null AUTO_INCREMENT PRIMARY KEY,
            nombre varchar(250) default null,
            apellido varchar(250) default null,
            direccion varchar(250) default null,
            dni int default null,
            fecha date default null
    )
        create or replace table videos(
            id int not null AUTO_INCREMENT PRIMARY KEY,
            title varchar(250) default null,
            director varchar(250) default null,
            clienteId int default null,
             FOREIGN KEY (clienteId) REFERENCES clientes(id) on delete set null

    )
         */
    }
}
