using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VkNet;
using VkNet.Model;
using ReqPar = VkNet.Model.RequestParams;
using System.IO;
using VkNet.Enums.Filters;

namespace Group_1
{
    public partial class Posts : Form
    {
        public Posts()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var vk = new VkApi();
            StreamReader sr = new StreamReader("C://Users/Dusha/Desktop/parametrs.txt");
            ulong appID = ulong.Parse(sr.ReadLine());                      
            string email = sr.ReadLine();
            string pass = sr.ReadLine();
            var OwnID = Convert.ToInt64(-204320459);
            var auth = new ApiAuthParams();
            Settings scope = Settings.All;
            auth.Login = email;
            auth.Password = pass;
            auth.ApplicationId = appID;
            auth.Settings = scope;
            try
            {
                vk.Authorize(auth);
            }
            catch (Exception z)
            {
                Console.WriteLine(z.Message);
            }
            vk.Wall.Post(new ReqPar.WallPostParams { OwnerId = OwnID, FromGroup = true,  Message = textBox1.Text });
            sr.Close();
        }

        private void Posts_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void Posts_Load(object sender, EventArgs e)
        {

        }
    }
}
