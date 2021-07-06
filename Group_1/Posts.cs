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
            var getFollowers = vk.Groups.GetMembers(new ReqPar.GroupsGetMembersParams()
            { GroupId = "204320459", Fields = VkNet.Enums.Filters.UsersFields.FirstNameAbl });
            foreach (User user in getFollowers)
            {
                listBox1.Items.Add(Encoding.UTF8.GetString(Encoding.Default.GetBytes($"{user.FirstName} {user.LastName}")));
                listBox2.Items.Add(user.Id);
            }
            for (int i = 0; i < listBox2.Items.Count; i++)
            {
                System.Threading.Thread.Sleep(10);
                var get = vk.Wall.Get(new ReqPar.WallGetParams { OwnerId = Convert.ToInt32(listBox2.Items[i]), Extended = true, Count = 1, });
                vk.Wall.Post(new ReqPar.WallPostParams { OwnerId = OwnID, FromGroup = true, Attachments});
                //vk.Wall.Repost(new ReqPar. get.WallPosts[0].ToString(), textBox1.Text, -1*OwnID, true);Message =  get.WallPosts[0].ToString()
            }
            //vk.Wall.Post(new ReqPar.WallPostParams { OwnerId = OwnID, FromGroup = true,  Message = "Мастер" }); Convert.ToInt32(listBox2.Items[i])
            sr.Close();
        }

        private void Posts_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
    }
}
