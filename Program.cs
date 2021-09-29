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

            using(var rabbit = new Rabbit(
                hostname: RBHOST != null ? RBHOST : "localhost",
                user: RBUSER != null ? RBUSER : "admin",
                password: RBPASS != null ? RBPASS : "123456"
            )){

                rabbit.QueueDeclare("guid");    
                   
                rabbit.CreateBasicConsumer("guid", ReceivedFunction);

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
