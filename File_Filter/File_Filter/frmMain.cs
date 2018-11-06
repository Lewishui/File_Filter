using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace File_Filter
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            string ZFCEPath = AppDomain.CurrentDomain.BaseDirectory + "Results\\";

            this.textBox1.Text = @"F:\mysteap\notepad\notepad";
            this.textBox2.Text = ZFCEPath + "少于6个字";
            this.textBox3.Text = ZFCEPath + "少于30个字";
            this.textBox4.Text = ZFCEPath + "特殊符号";
            this.textBox5.Text = ZFCEPath + "关键字";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                this.textBox1.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> Alist = GetBy_CategoryReportFileName(textBox1.Text);
                this.label6.Text = "";
                this.progressBar1.Visible = true;
                checkdata(Alist);
                this.label6.Text = "运行结束";
                MessageBox.Show("运行结束 ！");
                this.progressBar1.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误" + ex, "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

                throw;
            }

        }

        private void checkdata(List<string> Alist)
        {
            this.progressBar1.Maximum = Alist.Count;


            for (int i = 0; i < Alist.Count; i++)
            {
                bool iscontuine = false;
                this.progressBar1.Value = i;
                string Savename = Alist[i];
                string name = System.IO.Path.GetFileNameWithoutExtension(Alist[i]);

                this.label6.Text = name + "    " + i + "/" + Alist.Count;

                bool ischina = HasChineseTest(name);
                int isABC = Regex.Matches(name, "[a-zA-Z]").Count;



                //                4，
                //有特殊符号的
                //ˉˇ¨‘’々～‖∶”’‘｜〃〔〕《》「」『』．〖〗【【】（）〔〕｛｝≈≡≠＝≤≥＜＞≮≯∷±＋－×÷／∫∮∝∞∧∨∑∏∪∩∈∵∴⊥‖∠⌒⊙≌∽√°′〃＄￡￥‰％℃¤￠§№☆★○●◎◇◆□■△▲※→←↑↓〓＃＆＠＼＾＿
                //不包含（，。）标号重复2个以上的也放到一个文件夹里面 举例：！！！
                //例如：！！、~~~等；
                //包含过多重复文字;
                //纯数字、纯外文；
                string xt = "ˉ ˇ ¨ ‘’ 々 ～ ‖ ∶ ” ’ ‘ ｜ 〃 〔 〕 《 》 「 」 『 』 ． 〖 〗 【 【 】 （ ） 〔 〕 ｛ ｝ ≈ ≡ ≠ ＝ ≤ ≥ ＜ ＞ ≮ ≯ ∷ ± ＋ － × ÷ ／ ∫ ∮ ∝ ∞ ∧ ∨ ∑ ∏ ∪ ∩ ∈ ∵ ∴ ⊥ ‖ ∠ ⌒ ⊙ ≌ ∽ √ ° ′ 〃 ＄ ￡ ￥ ‰ ％ ℃ ¤ ￠ § № ☆ ★ ○ ● ◎ ◇ ◆ □ ■ △ ▲ ※ → ← ↑ ↓ 〓 ＃ ＆ ＠ ＼ ＾ ＿ ";
                //string[] astr = xt.ToArray();
                string[] temp3 = System.Text.RegularExpressions.Regex.Split(xt, " ");
                iscontuine = false;
                for (int j = 0; j < temp3.Length; j++)
                {
                    string dsds = temp3[j];
                    if (dsds != "")
                    {
                        string str2 = name.Replace(temp3[j], "");
                        int count = name.Length - str2.Length;
                        if (count > 1)
                        {
                            iscontuine = true;
                            File.Copy(textBox1.Text + "\\" + Savename, textBox4.Text + "\\" + Savename, true);
                            break;
                        }
                    }
                }
                if (iscontuine == true)
                    continue;
                //包含过多重复文字;
                //纯数字、纯外文；
                //string a = "abcdefababctr";
                //string b = "abc";
                //int c = SubstringCount(a, b); //这里的c就是b在a中出现的次数了

                #region MyRegion
                //char[] cc = name.ToCharArray();
                //for (int l = 0; l < cc.Length; l++)
                //{
                //    if (@cc[l].ToString() != ".")
                //    {
                //        string dss = cc[l].ToString();
                //        int replace = Regex.Matches(name, @cc[l].ToString()).Count;
                //        if (replace > 1)
                //        {
                //        }
                //        MatchCollection Matches = Regex.Matches(
                //                 name,
                //                 @cc[l].ToString(),
                //                 RegexOptions.IgnoreCase |         //忽略大小写
                //                 RegexOptions.ExplicitCapture |    //提高检索效率
                //                 RegexOptions.RightToLeft          //从左向右匹配字符串
                //             );

                //        foreach (Match NextMatch in Matches)
                //        {
                //            Console.Write("匹配的位置：{0,2} ", NextMatch.Index);
                //            Console.Write("匹配的内容：{0,2} ", NextMatch.Value);
                //            Console.Write("/n");
                //        }
                //    }

                //} 
                #endregion
                //name = "sss溜溜";
                //string pattern = "\\(\\w+\\)";
                //Match result = Regex.Match(name, pattern);
                //string rr = result.Value;

                #region 有以下关键字的，都放入一个文件夹内
                string key = "点赞,最牛,最强,我的天,惊呆了,草泥马,双击评论,666,上热门,小姨子,姐夫,出轨,隔壁老王,苞米地,抖胸,小三,偷情,嫖娼,车震,装逼,草泥马,特么的,撕逼,玛拉戈壁,爆菊,JB,呆逼,本屌,齐B短裙,法克鱿,丢你老母,达菲鸡,装13,逼格,蛋疼,傻逼,绿茶婊,你妈的,表砸,屌爆了,买了个婊,已撸,吉跋猫,妈蛋,逗比,我靠,碧莲,碧池,然并卵,日了狗,屁民,吃翔,XX狗,淫家,你妹,浮尸国,滚粗,TMD";

                string[] temp1 = System.Text.RegularExpressions.Regex.Split(key, ",");
                iscontuine = false;

                for (int j = 0; j < temp1.Length; j++)
                {
                    if (name.Contains(temp1[j]) && temp1[j] != "")
                    {
                        File.Copy(textBox1.Text + "\\" + Savename, textBox5.Text + "\\" + Savename, true);
                        iscontuine = true;
                        break;
                    }
                }
                if (iscontuine == true)
                    continue;

                #endregion

                #region MyRegion
                //1,文字字数少于6个字一个文件夹
                //2，文字字数少于30个字大于6个字的，一个文件夹（没有特殊符号的，没有重复符号的）
                //3，文字字数超过30个字的一个文件夹。
                string result = System.Text.RegularExpressions.Regex.Replace(name, @"[^0-9]+", "");
                     
                if (ischina == true && isABC <= 0)
                {
                    if (name.Length < 6)
                    {
                        File.Copy(textBox1.Text + "\\" + Savename, textBox2.Text + "\\" + Savename, true);
                        continue;
                    }
                    else if (name.Length >= 6 && name.Length < 30)
                    {
                        File.Copy(textBox1.Text + "\\" + Savename, textBox3.Text + "\\" + Savename, true);
                        continue;
                    }
                    else if (name.Length >= 30)
                    {
                        File.Copy(textBox1.Text + "\\" + Savename, textBox4.Text + "\\" + Savename, true);
                        continue;
                    }


                }
                #endregion

            }
        }
        public bool HasChineseTest(string text)
        {
            //string text = "是不是汉字，ABC,keleyi.com";
            char[] c = text.ToCharArray();
            bool ischina = false;

            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] >= 0x4e00 && c[i] <= 0x9fbb)
                {
                    ischina = true;

                }
                else
                {
                    //  ischina = false;
                }
            }
            return ischina;

        }
        static int SubstringCount(string str, string substring)
        {
            if (str.Contains(substring))
            {
                string strReplaced = str.Replace(substring, "");
                return (str.Length - strReplaced.Length) / substring.Length;
            }
            return 0;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                this.textBox2.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                this.textBox3.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                this.textBox4.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                this.textBox5.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();

        }
        public List<string> GetBy_CategoryReportFileName(string dirPath)
        {

            List<string> FileNameList = new List<string>();
            ArrayList list = new ArrayList();

            if (Directory.Exists(dirPath))
            {
                list.AddRange(Directory.GetFiles(dirPath));
            }
            if (list.Count > 0)
            {
                foreach (object item in list)
                {
                    if (!item.ToString().Contains("~$"))
                        FileNameList.Add(item.ToString().Replace(dirPath + "\\", ""));
                }
            }

            return FileNameList;
        }

    }
}
