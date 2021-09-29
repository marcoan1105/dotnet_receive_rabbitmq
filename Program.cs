using System;
using System.Text;
using System.Threading;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ReceiveRabbitMq
{
    public class Program
    {
        static void Main(string[] args)
        {

            var RBHOST = Environment.GetEnvironmentVariable("RBHOST");
            var RBUSER = Environment.GetEnvironmentVariable("RBUSER");
            var RBPASS = Environment.GetEnvironmentVariable("RBPASS");
            var RBQUEUE = Environment.GetEnvironmentVariable("RBQUEUE");


            RBHOST = RBHOST != null ? RBHOST : "localhost";
            RBUSER = RBUSER != null ? RBUSER : "admin";
            RBPASS = RBPASS != null ? RBPASS : "123456";
            RBQUEUE = RBQUEUE != null ? RBQUEUE : "guid";

            using(var rabbit = new Rabbit(
                hostname: RBHOST,
                user: RBUSER,
                password: RBPASS
            )){

                rabbit.QueueDeclare(RBQUEUE);    
                   
                rabbit.CreateBasicConsumer(RBQUEUE, ReceivedFunction);

                Console.WriteLine(" Pressione qualquer tecla para finalizar");
                Console.ReadLine();
            }                   
        }

        protected static void ReceivedFunction(object model, BasicDeliverEventArgs ea){
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            Console.WriteLine(" [x] Recebido {0}", message);
            Thread.Sleep(50);
        }
    }
}
