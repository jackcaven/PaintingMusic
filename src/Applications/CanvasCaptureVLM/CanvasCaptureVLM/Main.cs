using CanvasCaptureVLM.Classes.Helper;
using CanvasCaptureVLM.Classes.Settings;
using CanvasCaptureVLM.Classes.VlmClients.Models;
using CanvasCaptureVLM.Pages;

namespace CanvasCaptureVLM
{
    public partial class FormMain : Form
    {
        private const int SideBarClosedWidth = 70;

        private int SideBarOpenWidth { get => buttonPerformance.Width; }
        private bool SidebarExpand = true;

        internal SettingsService SettingsService = new();

        internal Dictionary<string, Form> Pages = [];
        internal About AboutPage;
        internal Performance PerformancePage;
        internal DevSettings DevSettingsPage;

        public FormMain()
        {
            InitializeComponent();

            PerformancePage = new(SettingsService);
            DevSettingsPage = new(SettingsService);
            AboutPage = new();

            Pages.Add(nameof(PerformancePage), PerformancePage);
            Pages.Add(nameof(DevSettingsPage), DevSettingsPage);
            Pages.Add(nameof(AboutPage), AboutPage);

            ShowPage(nameof(PerformancePage));

            VlmComponentHelper.OnVlmDataReceived += UpdateTokenUsage;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }

        #region Side Panel Transition
        private void timerSideBarTransition_Tick(object sender, EventArgs e)
        {
            if (SidebarExpand)
            {
                if (flowLayoutPanelSideBar.Width <= SideBarClosedWidth)
                {
                    SidebarExpand = false;
                    timerSideBarTransition.Stop();
                    flowLayoutPanelSideBar.Width = SideBarClosedWidth;
                }
                else
                {
                    flowLayoutPanelSideBar.Width -= 10;
                }
            }
            else
            {
                flowLayoutPanelSideBar.Width += 10;

                if (flowLayoutPanelSideBar.Width >= SideBarOpenWidth)
                {
                    SidebarExpand = true;
                    timerSideBarTransition.Stop();
                    flowLayoutPanelSideBar.Width = SideBarOpenWidth;
                }
            }
        }

        private void pictureBoxHamburgerBtn_Click(object sender, EventArgs e)
        {
            timerSideBarTransition.Start();
        }
        #endregion

        #region Page Navigation
        private void ShowPage(string key)
        {
            this.ActiveMdiChild?.Hide();

            if (Pages.TryGetValue(key, out Form? value))
            {
                value.MdiParent = this;
                value.Dock = DockStyle.Fill;
                value.Show();
            }
        }
        private void buttonAbout_Click(object sender, EventArgs e)
        {
            ShowPage(nameof(AboutPage));
        }

        private void buttonPerformance_Click(object sender, EventArgs e)
        {
            ShowPage(nameof(PerformancePage));
        }
        #endregion

        private void buttonDevSettings_Click(object sender, EventArgs e)
        {
            ShowPage(nameof(DevSettingsPage));
        }

        private void UpdateTokenUsage(VlmResult result)
        {
            int tokenCount = int.Parse(labelTokenUsage.Text);

            tokenCount += result.Usage.TotalTokens;

            labelTokenUsage.Invoke(() =>
            {
                labelTokenUsage.Text = tokenCount.ToString();
            });
        }
    };
}