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
namespace TaskProject
{
    public partial class Form1 : Form
    {

        System.Threading.CancellationTokenSource CancellationToken = new System.Threading.CancellationTokenSource();    
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)

        {
           progressBar1.Value = 0;
            Task task = new Task(calck);
            task.Start();

        }
        private void calck()
        {
            var numberes = Enumerable.Repeat(70, 100_000_000).ToArray();
            long sum = 0;
            object _lock = new object();
            var watch = System.Diagnostics.Stopwatch.StartNew();
          

            //foreach (var item in numberes)
            //{
            ////    index++;
            ////    update(index);
            //    sum += item;
            //}
            ParallelOptions  parallelOptions = new ParallelOptions();
            parallelOptions.MaxDegreeOfParallelism = 5;
            parallelOptions.CancellationToken = CancellationToken.Token;
            try
            {
                Parallel.ForEach(numberes, parallelOptions, num =>
                {
                    lock (_lock)
                    {
                      
                        sum += num;
                    }
                });
                MessageBox.Show(sum.ToString());
            }
            catch (OperationCanceledException ex)
            {

MessageBox.Show(ex.Message);
            }
          
          //  watch.Stop();
           // label1.Text = $"Loop 1 Execution Time: {watch.ElapsedMilliseconds} ms";
            MessageBox.Show(sum.ToString());
        }
        private void update(int num)
        {
            Invoke(new Action(() =>
            {

                progressBar1.Value = num;
            }
            ));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CancellationToken.Cancel();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var num = Enumerable.Repeat(70, 100_000_00);
            var res = num.AsParallel().WithCancellation(CancellationToken.Token).Sum();
            MessageBox.Show(res.ToString());    
        }
    }
}
