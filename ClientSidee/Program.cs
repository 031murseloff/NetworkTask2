using System.Net.Sockets;
using System.Net;
using System.Text;



Console.WriteLine("Client");
var client = new UdpClient();
var endPoint = new IPEndPoint(IPAddress.Parse("192.168.0.248"), 27001);
try
{

    while (true)
    {

        Console.Write("Path:");
        var path = Console.ReadLine();



        if (Path.GetExtension(path) == ".png" && !string.IsNullOrEmpty(path))
        {
            var bytes = new byte[1024];
            int bytesRead = 0;
            using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {


                do
                {

                    bytesRead = await fileStream.ReadAsync(bytes, 0, bytes.Length);

                    if (bytesRead > 0)
                    {

                        client.Send(bytes, bytesRead, endPoint);
                    }

                } while (bytesRead > 0);
                var bytes2 = Encoding.UTF8.GetBytes("END");
                client.Send(bytes2,endPoint);

            }
            Console.WriteLine("Foto gönderildi ");
        }
    }

}
catch (Exception ex)
{

    Console.WriteLine(ex.Message);
}

