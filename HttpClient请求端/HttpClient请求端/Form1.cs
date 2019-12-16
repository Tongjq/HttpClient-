using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HttpClient请求端
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            // Get请求
            HttpClient client = new HttpClient();
            string html = await client.GetStringAsync("http://www.github.com");
            textBox1.Text = html;
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            //表单格式请求， 报文体是“userName = admin & password = 123”这样的格式
            HttpClient client = new HttpClient();
            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            keyValues["userName"] = "admin";
            keyValues["password"] = "123";
            FormUrlEncodedContent content = new FormUrlEncodedContent(keyValues);
            var respMsg = await client.PostAsync("http://127.0.0.1:6666/Home/Login/", content);// 不要错误的调用 了 PutAsync，应该是 PostAsync
            string msgBody = await respMsg.Content.ReadAsStringAsync();
            MessageBox.Show(respMsg.StatusCode.ToString());
            MessageBox.Show(msgBody);
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            // 普通字符串做报文体
            string json = "{userName:'admin',password:'123'}";
            HttpClient client = new HttpClient();
            StringContent content = new StringContent(json);
            //contentype 必不可少
            content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            var respMsg = await client.PostAsync("http://127.0.0.1:6666/Home/Login2/", content);
            string msgBody = await respMsg.Content.ReadAsStringAsync();
            MessageBox.Show(respMsg.StatusCode.ToString());
            MessageBox.Show(msgBody);
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            //上传
            HttpClient client = new HttpClient();
            MultipartFormDataContent content = new MultipartFormDataContent();
            content.Headers.Add("UserName", "admin");
            content.Headers.Add("Password", "123");
            using (Stream stream = File.OpenRead(@"D:\temp\logo 透明.png"))
            {
                content.Add(new StreamContent(stream), "file", "logo.png");
                var respMsg = await client.PostAsync("http://127.0.0.1:6666/Home/Upload/", content);
                string msgBody = await respMsg.Content.ReadAsStringAsync();
                MessageBox.Show(respMsg.StatusCode.ToString());
                MessageBox.Show(msgBody);
            }
        }
    }
}
