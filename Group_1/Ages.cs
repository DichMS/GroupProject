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

namespace Group_1
{
    public partial class Ages : Form
    {
        public Ages()
        {
            InitializeComponent();
        }

        private void Ages_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int k = 0;
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            var api_user = new VkApi();
            api_user.Authorize(new ApiAuthParams
            { AccessToken = Auth.getAuthForUsers() });
            var get_Friends = api_user.Friends.Get(new ReqPar.FriendsGetParams
            { Fields = VkNet.Enums.Filters.ProfileFields.All });
            foreach (User user in get_Friends)
            {
                listBox1.Items.Add(Encoding.UTF8.GetString(Encoding.Default.GetBytes($"{user.FirstName} {user.LastName}")));
                listBox2.Items.Add(Encoding.UTF8.GetString(Encoding.Default.GetBytes($"{user.BirthDate}")));
            }
            StreamWriter sw = new StreamWriter("C://Users/Dusha/Desktop/date.txt"); // даты
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                if (listBox2.Items[i].ToString().Length > 7)
                {
                    textBox1.Text += listBox2.Items[i] + "\r\n";
                    k += 1;
                }
            }
            sw.WriteLine(textBox1.Text);
            sw.Close();
            string line;
            int p = 0;
            int[] chis = new int[k];
            int[] mes = new int[k];
            int[] year = new int[k];
            StreamReader sr = new StreamReader("C://Users/Dusha/Desktop/date.txt");
            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                if (line.Length != 0)
                {
                    int point = 0;
                    string ch = "";
                    string ms = "";
                    string god = "";
                    for (int i = 0; i < line.Length; i++)
                    {
                        if (line[i] == '.')
                            point++;
                        else if (point == 0)
                            ch += line[i];
                        else if (point == 1)
                            ms += line[i];
                        else if (point == 2)
                            god += line[i];
                    }
                    chis[p] = Convert.ToInt32(ch);
                    mes[p] = Convert.ToInt32(ms);
                    year[p] = Convert.ToInt32(god);
                    p++;
                }
            }
            sr.Close();
            int tch = 0;
            int tms = 0;
            int tgod = 0;
            string currentDate = DateTime.Now.Date.ToString();
            int q = 0;
            for (int i = 0; i < currentDate.Length; i++)
            {
                if (currentDate[i] == '.')
                    q++;
                else if (q == 0)
                    tch = tch * 10 + Convert.ToInt32(currentDate[i].ToString());
                else if (q == 1)
                    tms = tms * 10 + Convert.ToInt32(currentDate[i].ToString());
                else if (q == 2)
                    tgod = tgod * 10 + Convert.ToInt32(currentDate[i].ToString());
                if (currentDate[i + 1] == ' ')
                    i += currentDate.Length;
            }
            decimal vz = 0;
            for (int i = 0; i < p; i++)
            {
                if (tms > mes[i])
                    year[i] = tgod - year[i];
                else if (tms < mes[i])
                    year[i] = tgod - year[i] - 1;
                else if (tms == mes[i])
                {
                    if (tch >= chis[i])
                        year[i] = tgod - year[i];
                    else
                        year[i] = tgod - year[i] - 1;
                }
                vz += year[i];
            }
            label1.Text = (vz / p).ToString();
        }

        private void Ages_FormClosed_1(object sender, FormClosedEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
    }
}