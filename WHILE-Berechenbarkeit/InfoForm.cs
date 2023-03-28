using System.Reflection;


namespace WHILE_Berechenbarkeit
{
    public partial class InfoForm : Form
    {
        public InfoForm()
        {
            InitializeComponent();
        }

        private void InfoForm_Load(object sender, EventArgs e)
        {
            string v = "WHILE-Berechenbarkeit: Ein While-Programm-Simulationsprogramm\nVersion: " + Assembly.GetEntryAssembly().GetName().Version.ToString() + ", 28.03.2023, Jonas Hülse\nEvtl. Kontakt";
            InfoTb.Text = v;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
