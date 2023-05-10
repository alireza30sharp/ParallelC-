using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
namespace AsynchronousDelegate
{
    public delegate void StartJobDelegate();
    public partial class Form1 : Form
    {
        public bool jobCanceled=false;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //callback.Invoke();
            jobCanceled=false;
            StartJobDelegate callback = new StartJobDelegate(StartJob);
            callback.BeginInvoke(null, null);
            //var sumDelegate =new Func<int, int, int>(sum);

            //var callBack=new AsyncCallback(asyn => {
            //    var final = sumDelegate.EndInvoke(asyn);
            //    MessageBox.Show("final" + final);
            //});
            //sumDelegate.BeginInvoke(10,3, callBack, null);

        }

        private void StartJob()
        {
            for (int i = 0; i < 1000000; i++)
            {
                //چون ابزار ها در ترید دیگر ساخته شدن باید از invoke  استفاده کنیم
                //textBox1.Text += "Process Number " + i + Environment.NewLine;
                if(jobCanceled)
                    Thread.CurrentThread.Abort();
                Invoke(new Action(() => {
                    textBox1.Text += "Process Number " + i + Environment.NewLine;
                }));
                Thread.Sleep(1000);
            }
        }

        private int sum(int a,int b)
        {
            Thread.Sleep(1000);

            return a + b;   
        }

        private void button2_Click(object sender, EventArgs e)
        {
            jobCanceled=true;
        }
    }
}
