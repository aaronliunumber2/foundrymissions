using Newtonsoft.Json.Linq;
using StarbaseUGC.Foundry.Winforms.Models;
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
    public partial class MissionInfoForm : Form
    {
        string missionJson;

        public MissionInfoForm(string missionJson)
        {
            InitializeComponent();

            this.missionJson = missionJson;
        }

        private void MissionInfoForm_Load(object sender, EventArgs e)
        {
            var jobject = JObject.Parse(missionJson);
            var project = jobject["Project"];
            var restrictions = project["RestrictionProperties"];
            var mission = jobject["Mission"];
            var objectives = mission["Objectives"];
            var components = jobject["Components"];
            var dialogues = components.Where(o => o["Type"].ToString().Equals("DIALOG_TREE")).ToList();

            NameTextBox.Text = (string)project["PublicName"];
            AuthorTextBox.Text = (string)project["AccountName"];
            FactionTextBox.Text = (string)restrictions["Faction"];
            MinLevelTextBox.Text = restrictions["MinLevel"].ToString();
            DescriptionTextBox.Text = (string)project["Description"];

            foreach(var objective in objectives)
            {
                var obj = new Objective()
                {
                    Json = objective,
                };
                objectiveListBox.Items.Add(obj);
            }


            foreach(var dialogue in dialogues)
            {
                var obj = new Dialogue()
                {
                    Json = dialogue,
                };
                dialogueListBox.Items.Add(obj);
            }
        }

        private void dialogueListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var dialogue = (Dialogue)dialogueListBox.SelectedItem;

            var promptBody = dialogue.Json["PromptBody"].ToString();
            dialogueTextBox.Text = promptBody;

            foreach (var action in dialogue.Json["Action"])
            {
                var obj = new DialogueButton()
                {
                    Json = action,
                };

                buttonsListBox.Items.Add(obj);
            }
        }

        private void buttonsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var dialogue = (Dialogue)dialogueListBox.SelectedItem;
            var button = (DialogueButton)buttonsListBox.SelectedItem;
            var nextPromptId = button.Json["NextPromptID"].ToString();

            if (string.IsNullOrWhiteSpace(nextPromptId))
            {
                dialogueTextBox.Clear();
                buttonsListBox.Items.Clear();
            }
            else
            {
                //find the next prompt
                var nextPrompt = dialogue.Json["DialogPrompts"].FirstOrDefault(o => o["Number"].ToString().Equals(nextPromptId));
                dialogueTextBox.Text = button.Json["ActionName"].ToString() + Environment.NewLine + nextPrompt["PromptBody"].ToString();
                buttonsListBox.Items.Clear();
                foreach (var action in nextPrompt["Action"])
                {
                    var obj = new DialogueButton()
                    {
                        Json = action,
                    };

                    buttonsListBox.Items.Add(obj);
                }
            }

        }
    }
}
