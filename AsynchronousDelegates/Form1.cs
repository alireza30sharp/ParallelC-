namespace AsynchronousDelegates
{
    public delegate void AsyncCallback();
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_Click(object sender, EventArgs e)
        {
            AsyncCallback callback = new AsyncCallback(StartJob);
            // callback.Invoke();
            callback.BeginInvoke(null, null);
        }
        private void StartJob()
        {
            for (int i = 0; i < 1000000; i++)
            {
                //چون ابزار ها در ترید دیگر ساخته شدن باید از invoke  استفاده کنیم
                //textBox1.Text += "Process Number " + i + Environment.NewLine;
                Invoke(new Action(() => {
                    textBox1.Text += "Process Number " + i + Environment.NewLine;
                }));
            }
        }
    }
}