using System.Windows.Forms.VisualStyles;
using System.Drawing;
using System.Reflection;
using Microsoft.VisualBasic.Devices;
using System.Threading.Tasks.Dataflow;

namespace kemia
{
    public partial class Form1 : Form
    {
        int pointer;
        int mozg;
        int lastindex;
        List<molekula> tmb;
        Label tt;
        int qa;
        public class elektron : PictureBox
        {
            public int elteresx;
            public int elteresy;
            public elektron()
            {
                this.Height = 10;
                this.Width = 10;
                this.BackColor = Color.Blue;
                this.Visible = true;
                this.elteresx = 20;
                this.elteresy = 60;
            }

        }
        public abstract class molekula : PictureBox
        {
            public int fajta;
            public int i;
            public int x;
            public int y;
            public int team;
            public int kotes;
            public int[] tarsak;
            public bool[] kotve;
            public molekula(int po, int pointes)
            {
                this.i = po;
                this.Height = 50;
                this.Width = 50;
                this.BackColor = Color.Gray;
                this.Visible = true;
                this.team = pointes;
            }
            public void csapatrend(int ru, int kuld, List<molekula> teve, int torzs)
            {
                this.team = ru;
                for (int ex = 0; ex < this.tarsak.Length; ex++)
                {
                    if (this.tarsak[ex]!=kuld && teve[this.tarsak[ex]].team!=ru && this.tarsak[ex] != torzs)
                    {
                        teve[this.tarsak[ex]].csapatrend(ru, this.i, teve, torzs);
                    }
                }
            }
            public void atrendez(int mozg, int xxx, int yyy, int ox, int oy, List<molekula> tmp)
            {
                int wx = ox - xxx;
                int wy = oy - yyy;
                for (int k = 0; k < tmp.Count; k++)
                {
                    if (tmp[mozg].team == tmp[k].team && k != mozg)
                    {
                        tmp[k].Location = new Point(tmp[k].Location.X + wx, tmp[k].Location.Y + wy);
                        tmp[k].reshape(tmp[k].Location.X + wx, tmp[k].Location.Y + wy);
                    }
                }
            }
            public abstract void reshape(int x, int y);
            public abstract void elektronmozg(List<int> a, bool temp);

            //public abstract void leoszt();
        }
        public class hidrogen : molekula
        {
            public elektron elso;
            public hidrogen(int po, int pointes) : base(po, pointes)
            {
                tarsak = new int[4];
                kotve = new bool[4];
                fajta = 1;
                for (int ik = 0; ik < 4; ik++)
                {
                    this.tarsak[ik] = 0;
                    this.kotve[ik] = false;
                }
                this.kotes = 1;
                this.BackColor = Color.Green;
                elso = new elektron();
                this.reshape(this.Location.X, this.Location.Y);

            }
            public override void reshape(int x, int y)
            {
                elso.Location = new Point(x+elso.elteresx, y + elso.elteresy);
                this.x = x + 25;
                this.y = y + 25;
            }
            public override void elektronmozg(List<int> a, bool temp)
            {
                if (temp)
                {
                    if (a[0] == 0)
                    {
                        elso.Location = new Point(this.Location.X + 20, this.Location.Y + 60);
                        elso.elteresx = 20;
                        elso.elteresy = 60;
                    }
                    else if (a[0] == 1)
                    {
                        elso.Location = new Point(this.Location.X - 20, this.Location.Y + 20);
                        elso.elteresx = -20;
                        elso.elteresy = 20;
                    }
                    else if (a[0] == 2)
                    {
                        elso.Location = new Point(this.Location.X + 20, this.Location.Y - 20);
                        elso.elteresx = 20;
                        elso.elteresy = -20;
                    }
                    else if (a[0] == 3)
                    {
                        elso.Location = new Point(this.Location.X + 60, this.Location.Y + 20);
                        elso.elteresx = 60;
                        elso.elteresy = 20;
                    }
                }
            }
        }
        public Form1()
        {
            InitializeComponent();
            tmb = new List<molekula>();
            qa = 0;
            pointer = -1;
            lastindex = -1;
            tt = new Label();
            this.Controls.Add(tt);
            tt.Location = new Point(40, 40);

        }
        public class szen : molekula
        {
            public int i;
            public elektron elso;
            public elektron masodik;
            public elektron harmadik;
            public elektron negyedik;
            public szen(int po, int pointes) : base(po, pointes)
            {
                fajta = 2;
                tarsak = new int[4];
                kotve = new bool[4];
                for (int ik = 0; ik < 4; ik++)
                {
                    this.tarsak[ik] = 0;
                    this.kotve[ik] = false;

                }
                this.kotes = 4;
                this.i = po;
                this.Height = 50;
                this.Width = 50;
                this.BackColor = Color.Gray;
                this.Visible = true;

                elso = new elektron();
                masodik = new elektron();
                harmadik = new elektron();
                negyedik = new elektron();
                this.reshape(this.Location.X, this.Location.Y);
            }
            public override void reshape(int x, int y)
            {
                this.x = x + 25;
                this.y = y + 25;
                elso.Location = new Point(x + 60, y + 20);
                masodik.Location = new Point(x + 20, y + 60);
                harmadik.Location = new Point(x - 20, y + 20);
                negyedik.Location = new Point(x + 20, y - 20);
            }

            public override void elektronmozg(List<int> a, bool temp)
            {

            }
        }
        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(0, 0);
        }

        private void cToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            szen alma = new szen(lastindex + 1, pointer+1);
            this.Controls.Add(alma.elso);
            this.Controls.Add(alma.masodik);
            this.Controls.Add(alma.harmadik);
            this.Controls.Add(alma.negyedik);
            this.Controls.Add(alma);
            this.tmb.Add(alma);
            pointer++;
            lastindex++;
            mozg = lastindex;
            timer1.Enabled = true;
            alma.Click += Alma_Click;
            alma.MouseDown += Alma_MouseDown;
        }
        private void Alma_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                szen alma = sender as szen;
                pointer++;
                mozg = alma.i;
                alma.team = pointer;
                for (int era = 0; era < alma.tarsak.Length; era++)
                {
                    for (int aa = 0; aa < 4; aa++)
                    {
                        if (tmb[alma.tarsak[era]].tarsak[aa] == alma.i)
                        {
                            tmb[alma.tarsak[era]].kotve[aa] = false;
                            tmb[alma.tarsak[era]].kotes++;
                        }
                    }
                }
                for (int era = 0; era < alma.tarsak.Length; era++)
                {
                    pointer++;
                    if (alma.kotve[era]==true)
                    {
                        tmb[alma.tarsak[era]].csapatrend(pointer, alma.i, tmb, alma.i);
                    }
                    tt.Text += tmb[alma.tarsak[era]].team;
                    alma.kotve[era] = false;
                }
                tt.Text = alma.team.ToString();
                alma.kotes = 4;
                qa = 8;
                timer1.Enabled = true;
                // visszaállít közepre 
            }
        }

        private void Alma_Click(object? sender, EventArgs e)
        {
            szen alma = sender as szen;
            mozg = alma.i;
            tt.Text = alma.team.ToString();
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (qa < 5)
            {
                List<int> temple = new List<int>();
                int mousex = MousePosition.X;
                int mousey = MousePosition.Y;
                int wx = mousex - tmb[mozg].Location.X;
                int wy = mousey - tmb[mozg].Location.Y;
                for (int k = 0; k < tmb.Count; k++)
                {
                    if (tmb[mozg].team == tmb[k].team && k != mozg)
                    {
                        tmb[k].Location = new Point(tmb[k].Location.X + wx, tmb[k].Location.Y + wy);
                        tmb[k].reshape(tmb[k].Location.X, tmb[k].Location.Y);
                    }
                }
                tmb[mozg].Location = new Point(mousex, mousey);
                tmb[mozg].reshape(tmb[mozg].Location.X, tmb[mozg].Location.Y);
                int x = tmb[mozg].Location.X + 25;
                int y = tmb[mozg].Location.Y + 25;
                int ti = 0;
                while (ti < tmb.Count)
                {
                    if ((tmb[ti].x - x) * (tmb[ti].x - x) + (tmb[ti].y - y) * (tmb[ti].y - y) < 10000 && ti != mozg && tmb[ti].team != tmb[mozg].team && tmb[ti].kotes > 0)
                    {
                        tmb[ti].kotes--;
                        tmb[mozg].kotes--;
                        int dx = tmb[mozg].Location.X;
                        int dy = tmb[mozg].Location.Y;
                        if (tmb[ti].x < x && tmb[ti].y + 70 > y && tmb[ti].y - 70 < y && tmb[mozg].kotve[0] == false && tmb[ti].kotve[3] == false) // jobbrol csatlakozik
                        {
                            tmb[mozg].Location = new Point(tmb[ti].Location.X + 80, tmb[ti].Location.Y);
                            tmb[mozg].reshape(tmb[mozg].Location.X, tmb[mozg].Location.Y);
                            tmb[mozg].atrendez(mozg, dx, dy, mousex, mousey, tmb);
                            for (int er = 0; er < tmb.Count; er++)
                            {
                                if (tmb[er].team == tmb[mozg].team && mozg != er)
                                {
                                    tmb[er].team = tmb[ti].team;
                                }
                            }
                            tmb[mozg].team = tmb[ti].team;
                            tmb[mozg].kotve[1] = true;
                            tmb[ti].kotve[3] = true;
                            tmb[mozg].tarsak[1] = ti;
                            tmb[ti].tarsak[3] = mozg;
                            temple.Add(1);
                        }
                        else if (tmb[ti].x > x && tmb[ti].y + 70 > y && tmb[ti].y - 70 < y && tmb[mozg].kotve[3] == false && tmb[ti].kotve[1] == false) // balrol csatlakozik
                        {
                            tmb[mozg].Location = new Point(tmb[ti].Location.X + -80, tmb[ti].Location.Y);
                            tmb[mozg].reshape(tmb[mozg].Location.X, tmb[mozg].Location.Y);
                            tmb[mozg].atrendez(mozg, dx, dy, mousex, mousey, tmb);
                            for (int er = 0; er < tmb.Count; er++)
                            {
                                if (tmb[er].team == tmb[mozg].team && mozg != er)
                                {
                                    tmb[er].team = tmb[ti].team;
                                }
                            }
                            tmb[mozg].team = tmb[ti].team;
                            tmb[mozg].kotve[3] = true;
                            tmb[ti].kotve[1] = true;
                            tmb[mozg].tarsak[3] = ti;
                            tmb[ti].tarsak[1] = mozg;
                            temple.Add(3);
                        }
                        else if (tmb[ti].y > y && tmb[ti].kotve[2] == false && tmb[mozg].kotve[0] == false) // a mozg van lejjebb
                        {
                            tmb[mozg].Location = new Point(tmb[ti].Location.X, tmb[ti].Location.Y - 80);
                            tmb[mozg].reshape(tmb[mozg].Location.X, tmb[mozg].Location.Y);
                            tmb[mozg].atrendez(mozg, dx, dy, mousex, mousey, tmb);
                            for (int er = 0; er < tmb.Count; er++)
                            {
                                if (tmb[er].team == tmb[mozg].team && mozg != er)
                                {
                                    tmb[er].team = tmb[ti].team;
                                }
                            }
                            tmb[mozg].team = tmb[ti].team;
                            tmb[mozg].kotve[0] = true;
                            tmb[ti].kotve[2] = true;
                            tmb[mozg].tarsak[0] = ti;
                            tmb[ti].tarsak[2] = mozg;
                            temple.Add(0);
                        }
                        else if (tmb[ti].kotve[0] == false && tmb[mozg].kotve[2] == false) // alulra csatlakozik
                        {
                            tmb[mozg].Location = new Point(tmb[ti].Location.X, tmb[ti].Location.Y + 80);
                            tmb[mozg].reshape(tmb[mozg].Location.X, tmb[mozg].Location.Y);
                            tmb[mozg].atrendez(mozg, dx, dy, mousex, mousey, tmb);
                            for (int er = 0; er < tmb.Count; er++)
                            {
                                if (tmb[er].team == tmb[mozg].team && mozg != er)
                                {
                                    tmb[er].team = tmb[ti].team;
                                }
                            }
                            tmb[mozg].team = tmb[ti].team;
                            tmb[mozg].kotve[2] = true;
                            tmb[ti].kotve[0] = true;
                            tmb[mozg].tarsak[2] = ti;
                            tmb[ti].tarsak[0] = mozg;
                            temple.Add(2);
                        }
                        timer1.Enabled = false;
                    }
                    ti++;
                }
                if (temple.Count > 0)
                {
                    tmb[mozg].elektronmozg(temple);
                }
        
                tt.Text = tmb[mozg].team.ToString();
            }
            else
            {
                qa--;
            }
            
        }
        // kor alaku molekulak
        // ahol kettos kotes van az kettoskoteskent jelentjen meg.
        // molekulak mentese

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            timer1.Enabled = false;
        }

        private void hToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hidrogen alma = new hidrogen(lastindex + 1, pointer+1);
            this.Controls.Add(alma.elso);
            this.Controls.Add(alma);
            this.tmb.Add(alma);
            lastindex++;
            pointer++;
            mozg = lastindex;
            timer1.Enabled = true;
            alma.Click += Alma_Click1;
            alma.MouseDown += Alma_MouseDown1;
        }

        private void Alma_MouseDown1(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                hidrogen alma = sender as hidrogen;
                pointer++;
                mozg = alma.i;
                alma.team = pointer;
                for (int era = 0; era < alma.tarsak.Length; era++)
                {
                    for (int aa = 0; aa < tmb[alma.tarsak[era]].tarsak.Length; aa++)
                    {
                        if (tmb[alma.tarsak[era]].tarsak[aa] == alma.i)
                        {
                            tmb[alma.tarsak[era]].tarsak[aa] = 0;
                            tmb[alma.tarsak[era]].kotes++;
                        }
                    }
                }
                
                for (int era = 0; era < alma.tarsak.Length; era++)
                {
                    pointer++;
                    if (alma.kotve[era] == true)
                    {
                        tmb[alma.tarsak[era]].csapatrend(pointer, alma.i, tmb, alma.i);
                    }
                    tt.Text += tmb[alma.tarsak[era]].team;
                    alma.kotve[era] = false;
                }
                alma.kotes = 4;
                timer1.Enabled = true;
                // itt meg le kéne szedni azt is akik rajta vannak.
            }
        }

        private void Alma_Click1(object? sender, EventArgs e)
        {
            hidrogen alma = sender as hidrogen;
            mozg = alma.i;
            timer1.Enabled = true;
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < tmb.Count; i++)
            {
                this.Controls.Remove(tmb[i]);
            }
            pointer = -1;
            tmb.Clear();

        }
    }
}
//tervezesi mintak
//commitolas