using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

Console.WriteLine("Server");
var endPoint = new IPEndPoint(IPAddress.Parse("192.168.0.248"), 27001);
var server = new UdpClient(endPoint);
var remoteEp = new IPEndPoint(IPAddress.Any, 0);
while (true)
{


    var bytes = server.Receive(ref remoteEp);
    var directoryPath = $@"{Directory.GetCurrentDirectory()}\{remoteEp.Address}";
    Directory.CreateDirectory(directoryPath);
    var filePath = $@"{directoryPath}\{Guid.NewGuid()}.png";

    using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
    {
        Console.WriteLine("Server listen etmeye basladi...");


        while (true) { 
        await fileStream.WriteAsync(bytes, 0, bytes.Length);
    
            await fileStream.FlushAsync();
            bytes = server.Receive(ref remoteEp);
            if (Encoding.UTF8.GetString(bytes) == "END")
            {
                break;
            }


            Console.WriteLine($"file a  {bytes.Length} byte data yazildi");
        }
        Console.WriteLine("File data axini sonlandi");
       


    }
}
