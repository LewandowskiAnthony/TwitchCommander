using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net; //For our IPAddress
using System.Net.Sockets; //For our TcpClient
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows.Input;
using WindowsInput;

namespace TwitchMessagesCommander
{
    class Program
    {
        [DllImport("User32.dll")]
        static extern int SetForegroundWindow(IntPtr point);

        static void Main(string[] args)
        {
            //Process Start
            Console.WriteLine("Lancement du programme");
            // Process.Start("sameboy.exe D:\\ROMS\\PokemonR.gb");
            Process[] tousLesProcess = Process.GetProcesses();

            // Socket Listening
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 3000));
            socket.Listen(0);
            var client = socket.Accept();
            var buffer = Encoding.UTF8.GetBytes("Hello NODEJS");
            client.Send(buffer, 0, buffer.Length, 0);

            foreach (Process Processus in tousLesProcess)
            {
                //int iDProcess = Processus.Id;
                String name = Processus.ProcessName;
               // if (name == "sameboy")
              //  {
                    Console.WriteLine("Le processus {0} est lancé", name);
                    Process p = Process.GetProcessesByName(name).FirstOrDefault();
                    Console.WriteLine("On passe le processus {0} en main", name);
                    IntPtr h = p.MainWindowHandle;
                    Console.WriteLine("On envois l'appuie sur la touche entrée.");
                    InputSimulator sim = new InputSimulator();
                    while (true)
                    {
                        buffer = new byte[255];
                        int rec = client.Receive(buffer, 0, buffer.Length, 0);
                        Array.Resize(ref buffer, rec);
                        Console.WriteLine("Received : " + Encoding.UTF8.GetString(buffer));
                    }
             //   }
            }



            client.Close();
            socket.Close();
        }
    }
}
