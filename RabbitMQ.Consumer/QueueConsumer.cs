using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ.Consumer
{
    public static class QueueConsumer
    {
        public static void Consume(IModel channel)
        {
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
            Console.WriteLine("Consumer Started...");
            Console.ReadLine();
        }
    }
}
