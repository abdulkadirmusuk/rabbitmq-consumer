using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace RabbitMQ.Consumer
{
    static class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory
            {
                //Docker Container Port Dinleniyor..
                Uri = new Uri("amqp://guest:guest@localhost:5672")
            };
            using var connection = factory.CreateConnection(); //factory ile bağlantı oluşturuyorum
            using var channel = connection.CreateModel();//kanal oluşturdum


            //Bu aşağıdaki alan tek consumer için oluşturuldu. Çoklu consumer için Queue consumer methodune channel publish edildi
            /*
            channel.QueueDeclare("demo-queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);//kanal özelliklerini tanımladık
                                 ///////////////////////////////
            ///Yukarıdaki alanlar producer işi ile aynı işleri yapar. aynı factory ve channel a bağlanır.

            var consumer = new EventingBasicConsumer(channel);//Consumer channel a bağlanır
            consumer.Received += (sender, e) => {
                var body = e.Body.ToArray();//event içindeki Body nesnesi byte array olarak alınır
                var message = Encoding.UTF8.GetString(body);//mesaj byte array den string nesnesine çevirilir.
                Console.WriteLine(message);
            };
            channel.BasicConsume("demo-queue", true, consumer);//demo-queue isimli kanaldan, true: tüm gelenleri oku. consumer aracılığı ile
            Console.ReadLine();
            */

            //QueueConsumer.Consume(channel); //Çoklu consumer için kullanmıştık

            //Şimdi Exchange için consumer tanımını yani DirectExchangeConsumer methoduna kanalı gönderelim
            DirectExchangeConsumer.Consume(channel);
        }
    }
}
