using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace FacebookAPI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Get_Click(object sender, EventArgs e)
        {
            /*WebClient client = new WebClient();
            String URI = textBoxURL.Text;
            Stream data = client.OpenRead(URI);
            StreamReader reader = new StreamReader(data);
            String s = reader.ReadToEnd();
            data.Close();
            reader.Close();
            listBox1.Items.Add(s);*/

            try
            {
                String url = textBoxURL.Text;
                String s;
                WebRequest request = WebRequest.Create(url);
                WebResponse response = request.GetResponse();
                Stream data = response.GetResponseStream();
                StreamReader reader = new StreamReader(data);
                s = reader.ReadToEnd();
                listBox1.Items.Add(s);
                data.Close();
                reader.Close();
            }
            catch(Exception ex)
            {
                throw new System.Exception("error: " + ex.Message);
            }



        }

        private void Post_Click(object sender, EventArgs e)
        {
            String data = "{\"name\":\"test\", \"salary\":\"123\",\"age\":\"23\"}";
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            String URI = textBoxURL.Text;
            String s;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URI);
            request.ContentLength = dataBytes.Length;
            request.ContentType = "application/json";
            request.Method = "POST";

            using(Stream requestBody = request.GetRequestStream())
            {
                requestBody.Write(dataBytes, 0, dataBytes.Length);
            }

            using(HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using(Stream stream = response.GetResponseStream())
            using(StreamReader reader = new StreamReader(stream))
            {
                s = reader.ReadToEnd();
                listBox1.Items.Add(s);
            }

            
        }
    }
}
