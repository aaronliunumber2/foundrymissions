using StarbaseUGC.Foundry.Engine.Serializers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StarbaseUGC.Foundry.Winforms
{
    public partial class MainScreen : Form
    {
        public MainScreen()
        {
            InitializeComponent();
        }

        private void ImportButton_Click(object sender, EventArgs e)
        {
            try
            {
                var fileName = ImportFileTextBox.Text;
                var mission = FoundryMissionSerializer.ImportMission(fileName);
                var json = FoundryMissionSerializer.ExportMissionToJson(mission);
                OutputTextBox.Text = json;

                var missionInfoForm = new MissionInfoForm(json);
                missionInfoForm.Show();
            }
            catch(Exception ex)
            {
                var frmError = new ErrorForm();
                frmError.ErrorMessage = ex.ToString();
                frmError.Show();
            }
        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ImportFileTextBox.Text = openFileDialog1.FileName;
            }
        }
    }
}
