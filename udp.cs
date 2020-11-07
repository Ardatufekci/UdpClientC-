using System;
using System.Net;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;

namespace udp
{
 public partial class Form1 : Form
    {
    UdpClient receivingUdpClient = new UdpClient(PORT NUMBER);
    IAsyncResult ar_ = null;
    IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Parse("YOUR IP"), PORT NUMBER);
    
    public void Connect()
        {
            try
            {
                receivingUdpClient.Connect(RemoteIpEndPoint);
                Console.WriteLine("Establishing a connection to {0}", RemoteIpEndPoint.ToString());

                Byte[] message = Encoding.ASCII.GetBytes("start");
                receivingUdpClient.Send(message, message.Length);
                Console.WriteLine("Sent -> {0}", message[0].ToString());
                

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
         }
         public void Receive(IAsyncResult ar, Window pencere, Byte[] receiveBytes)
        {
            try
            {
                receiveBytes = Encoding.ASCII.GetBytes(Encoding.ASCII.GetString(receiveBytes) + Encoding.ASCII.GetString(receivingUdpClient.Receive(ref RemoteIpEndPoint)));
                string returnData = Encoding.ASCII.GetString(receiveBytes);
                
                List<string> res = returnData.Split().ToList();
                var ilkoge = receiveBytes[0];
                if (ilkoge !=0)
                {   
                    img = ImDecode(np.frombuffer(receiveBytes, np.uint8).ToByteArray());
                    //Cv2.ImShow("cam",img);

                    Console.WriteLine("This message {0} is received from {1}", returnData, RemoteIpEndPoint.ToString());
                    // Window.ShowImages(img);
                }
                else
                {
                    pencere.ShowImage(img);
                    receiveBytes = receivingUdpClient.Receive(ref RemoteIpEndPoint);
                    receivingUdpClient.EndReceive(ar_, ref RemoteIpEndPoint);
                    Receive(ar_,pencere, receiveBytes);
                }


                
                /*
                byte[] receivedByte = { };
                sock.Receive(receivedByte, SocketFlags.None);
                string data = Encoding.ASCII.GetString(receivedByte);
                */
                Console.WriteLine("This message {0} is received from {1}", returnData.ToString(), RemoteIpEndPoint.ToString());
                //receivingUdpClient.EndReceive(ar_, ref RemoteIpEndPoint);
            }
            catch (ArgumentNullException ane)
            {

                Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
            }

            catch (SocketException se)
            {

                Console.WriteLine("SocketException : {0}", se.ToString());
            }
            catch(OpenCVException cve)
            {
                Console.WriteLine("OpenCv Exception: {0}", cve.ToString());
            }
        }
        
        public Form1()
        {
            InitializeComponent();             
        }
        private void Form1_Load(object sender, EventArgs e)
        {
           
        }
    }



}
