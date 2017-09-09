using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetMQ;
using NetMQ.Sockets;
using System.Threading;
using System.IO;

namespace ScannerTestApp
{
    class Subscriber
    {
        String publisherIP = "158.69.193.253";
        String publisherPort = "5551";
        public void ThreadB()
        {
            try
            {
                //string topic = args[0] == "All" ? "" : args[0];
                //string topic = "";
                //Console.WriteLine("Subscriber started for Topic : {0}", _topic);

                using (var subSocket = new SubscriberSocket())
                {
                    subSocket.Options.ReceiveHighWatermark = 0;
                    subSocket.Connect("tcp://" + publisherIP + ":" + publisherPort);
                    subSocket.SubscribeToAnyTopic();

                    //Console.WriteLine("Subscriber socket connecting...");
                    while (true)
                    {
                        try
                        {
                            string messageReceived;

                            System.Threading.Thread.Sleep(5);

                            if (subSocket.TryReceiveFrameString(out messageReceived))
                            {
                                //Writing the messages received in a log file
                                double millis = (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds;
                                
                                File.AppendAllText("D:/FeedLogNew.txt", messageReceived + "," +(millis / 1000) + "\n");
                            }
                        }
                        catch (Exception e)
                        {

                        }
                    }
                }
            }
            catch (Exception e)
            {

            }
        }

    }

}
