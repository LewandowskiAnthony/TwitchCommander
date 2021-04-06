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
using WindowsInput.Native;
using WindowsInput;

namespace TwitchMessagesCommander
{
    class Program
    {
        [DllImport("user32.dll")]
        static extern int SetForegroundWindow(IntPtr point);
        [STAThread]
        static void Main()
        {
            //Process Start
            Console.WriteLine("Lancement du programme");
            //"SRC\\ROMS\\PokemonR.gb"
            //Process.Start(".\\SRC\\sameboy.exe");
            Process[] tousLesProcess = Process.GetProcesses();
            String command;

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
               Console.WriteLine("Le processus {0} :", name);
                if (name == "bgb")
               {
                    Console.WriteLine("Le processus {0} est lancé", name);
                    Process p = Process.GetProcessesByName(name).FirstOrDefault();
                    Console.WriteLine("On passe le processus {0} en main", name);
                    IntPtr h = p.MainWindowHandle;
                    Console.WriteLine("On envois l'appuie sur la touche entrée.");
                    InputSimulator sim = new InputSimulator();

                    while (true)
                    {
                        SetForegroundWindow(h);
                        Thread.Sleep(2000);
                        buffer = new byte[255];
                        int rec = client.Receive(buffer, 0, buffer.Length, 0);
                        Array.Resize(ref buffer, rec);
                        command = Encoding.UTF8.GetString(buffer).ToLower();
                        SetForegroundWindow(h);
                        Console.WriteLine(command + " received");
                        switch (command)
                        {
                            case "a":
                                sim.Keyboard.KeyDown(VirtualKeyCode.VK_A);
                                Thread.Sleep(175);
                                sim.Keyboard.KeyUp(VirtualKeyCode.VK_A);
                                break;
                            case "b":
                                sim.Keyboard.KeyDown(VirtualKeyCode.VK_Z);
                                Thread.Sleep(175);
                                sim.Keyboard.KeyUp(VirtualKeyCode.VK_Z);
                                break;
                            case "start":
                                sim.Keyboard.KeyDown(VirtualKeyCode.RETURN);
                                Thread.Sleep(175);
                                sim.Keyboard.KeyUp(VirtualKeyCode.RETURN);
                                break;
                            case "select":
                                sim.Keyboard.KeyDown(VirtualKeyCode.SHIFT);
                                Thread.Sleep(175);
                                sim.Keyboard.KeyUp(VirtualKeyCode.SHIFT);
                                break;
                            case "up":
                                sim.Keyboard.KeyDown(VirtualKeyCode.UP);
                                Thread.Sleep(175);
                                sim.Keyboard.KeyUp(VirtualKeyCode.UP);
                                break;
                            case "down":
                                sim.Keyboard.KeyDown(VirtualKeyCode.DOWN);
                                Thread.Sleep(175);
                                sim.Keyboard.KeyUp(VirtualKeyCode.DOWN);
                                break;
                            case "left":
                                sim.Keyboard.KeyDown(VirtualKeyCode.LEFT); 
                                Thread.Sleep(175);
                                sim.Keyboard.KeyUp(VirtualKeyCode.LEFT);
                                break;
                            case "right":
                                sim.Keyboard.KeyDown(VirtualKeyCode.RIGHT);
                                Thread.Sleep(175);
                                sim.Keyboard.KeyUp(VirtualKeyCode.RIGHT);
                                break;
                            default:
                                Console.WriteLine("No command available");
                                break;
                        }
                    }
                }
            }
            Console.WriteLine("End reached ;)");


            client.Close();
            socket.Close();
        }

        static void GetCommands()
        {
            
        }
    }
}
