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
namespace Thread
{
    public partial class Form1 : Form
    {
        private AutoResetEvent AutoResetEvent   =new AutoResetEvent(false);
        private object lockTread=new object();
        public Form1()
        {
            InitializeComponent();
            TimerThread();

        }

        private void button2_Click(object sender, EventArgs e)
        {
         
            //System.Threading.Thread thread = new System.Threading.Thread(new ThreadStart(Dowork));
            // thread.Start();
            System.Threading.Thread thread = new System.Threading.Thread(new ParameterizedThreadStart(Dowork));
            //این دستور باعث می شود تا پروسه داخلی تموم بشود سپس دستور را ادامه می دهد
            //البته در اینجا معنی نمی دهد وقتی چند پروسه داخلی صدا زده میشه کاربرد داره
            AutoResetEvent.WaitOne();


            /*
             * دو نوع ترید داریم Backgroundو FORGROUND
             * به صورت پیش فرض فورگروند هستن
             * این نوع ترید حتی اگر ترید اصلی بسته شود در پس زمینه اجرا می ماند حتی اگر برنامه رو کلوز  کنی
             * 
             * Background:با بسته شدن ترید پدر بلافاصله پایان داده میشود           * 
             */
            //  thread.IsBackground = true; 


            thread.Start(new ThreadParams() { 
            End = 10000,
            Start=10,
            });
        }

        private void Dowork(object param)
        {
            var threadParams=(ThreadParams) param;
            /*
             * برای جلوگیری از رویداد concurancy  از دو دستور لاک یا اینتر استفاده میکنیم
             * 
             *   Monitor.Enter(lockTread);
             */

            //lock (lockTread)
            //{
                for (int i = threadParams.Start; i < threadParams.End; i++)
                {
                    Invoke(new Action(() =>
                    {

                        textBox1.Text = i.ToString();
                    }));
                }
               // Monitor.Exit(lockTread);
          //  }
          
            //اعلام پایان پروسه
            AutoResetEvent.Set();   
        }


        /*
         * 
         * 
         * یک تابع در دوره زمانی مشخص
         */
        private void TimerThread()
        {
            TimerCallback timerCallbac = new TimerCallback((state) =>
            {
                //Invoke(new Action(() =>
                //{

                    MessageBox.Show("run job!!!" + state);

               // }));
            });
            System.Threading.Timer timer = new System.Threading.Timer(timerCallbac, "aaa", 2000, 3000);


        }
        public class ThreadParams
        {
            public int Start { get; set; }
            public int End { get; set; }
        }
    }
}
