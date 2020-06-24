using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace RatinhoMouse
{
    public partial class Form1 : Form
    {

        [DllImport("kernel32.dll")]
        static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);

        [Flags]
        enum EXECUTION_STATE : uint
        {
            ES_AWAYMODE_REQUIRED = 0x00000040,
            ES_CONTINUOUS = 0x80000000,
            ES_DISPLAY_REQUIRED = 0x00000002,
            ES_SYSTEM_REQUIRED = 0x00000001
        }

        public Form1()
        {
            InitializeComponent();
        }

        bool isExecut = false;
        int countmove = 1;
        private void Button1_Click(object sender, EventArgs e)
        {
            button2.Enabled = isExecut;
            isExecut = !isExecut;

            if (!isExecut)
            {
                AtivaProtetor();
                Application.ExitThread();
                Application.Exit();
                return;
            }

            DesativaProtetor();

            var t = new System.Threading.Thread(() =>
            {
                while (isExecut)
                {
                    countmove++;
                    System.Threading.Thread.Sleep(new TimeSpan(0, 0, 5));
                    //Cursor = new Cursor(Cursor.Current.Handle);
                    if (countmove % 2 == 0)
                        Cursor.Position = new Point(Cursor.Position.X - 100, Cursor.Position.Y - 50);
                    else
                        Cursor.Position = new Point(Cursor.Position.X + 100, Cursor.Position.Y + 50);
                    //Cursor.Clip = new Rectangle(Location, this.Size);
                }
            });

            t.Start();

            button1.Text = "Parar";
        }

        public static void DesativaProtetor()
        {
            SetThreadExecutionState(
                EXECUTION_STATE.ES_DISPLAY_REQUIRED | EXECUTION_STATE.ES_CONTINUOUS);
        }

        public static void AtivaProtetor()
        {
            SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
