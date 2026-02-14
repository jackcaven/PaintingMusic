using CanvasCaptureVLM.Classes.Logging;
using CanvasCaptureVLM.Interfaces.Repositories;

namespace CanvasCaptureVLM.Pages
{
    public partial class PromptDesigner : Form
    {
        private readonly IPromptRepository promptRepository;

        public PromptDesigner(IPromptRepository promptRepository)
        {
            this.promptRepository = promptRepository;
            InitializeComponent();
            SetUp();
        }

        private void SetUp()
        {
            InitialiseListBox();
            textBoxPromptName.Enabled = false;
            textBoxPrompt.Enabled = false;
            buttonSavePrompt.Enabled = false;
        }

        private void InitialiseListBox()
        {
            List<string> promptNames = [.. promptRepository.GetPromptNames()];

            listBoxPrompts.Items.Clear();

            foreach (string promptName in promptNames)
            {
                listBoxPrompts.Items.Add(promptName);
            }
        }

        private async void listBoxPrompts_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (listBoxPrompts.SelectedItem == null)
                    return;

                string selectedItem = listBoxPrompts.SelectedItem.ToString() ?? string.Empty;


                if (string.IsNullOrEmpty(selectedItem))
                    return;

                string prompt = await promptRepository.GetPrompt($"{selectedItem}.txt");

                textBoxPromptName.Text = selectedItem;
                textBoxPrompt.Text = prompt;

                textBoxPrompt.Enabled = selectedItem != "Default";
                textBoxPromptName.Enabled = selectedItem != "Default";

                buttonDeletePrompt.Enabled = selectedItem != "Default";
                buttonSavePrompt.Enabled = selectedItem != "Default";
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, nameof(PromptDesigner));
                MessageBox.Show("Error fetching prompt", "Prompt Repository Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void buttonAddPrompt_Click(object sender, EventArgs e)
        {
            try
            {
                textBoxPrompt.Text = string.Empty;
                textBoxPromptName.Text = string.Empty;

                textBoxPromptName.Enabled = true;
                textBoxPrompt.Enabled = true;
                buttonSavePrompt.Enabled = true;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, nameof(PromptDesigner));
                MessageBox.Show("Error initializing adding prompt", "Prompt Repository Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void buttonSavePrompt_Click(object sender, EventArgs e)
        {
            try
            {
                string name = textBoxPromptName.Text;
                string prompt = textBoxPrompt.Text;

                if (listBoxPrompts.Items.Contains(name))
                {
                    // Edit
                    promptRepository.Update($"{name}.txt", prompt);
                    Log.Debug($"Updating prompt {name}, {prompt}", nameof(PromptDesigner));
                }
                else
                {
                    // Add
                    promptRepository.Add($"{name}.txt", prompt);
                    Log.Debug($"Adding prompt {name}, {prompt}", nameof(PromptDesigner));

                    listBoxPrompts.Items.Add(name);
                    listBoxPrompts.SelectedItem = name;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, nameof(PromptDesigner));
                MessageBox.Show("Error saving prompt", "Prompt Repository Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void buttonDeletePrompt_Click(object sender, EventArgs e)
        {
            try
            {

                if (listBoxPrompts.SelectedItem == null)
                    return;

                string selectedItem = listBoxPrompts.SelectedItem.ToString() ?? string.Empty;

                if (string.IsNullOrEmpty(selectedItem))
                    return;

                promptRepository.Remove($"{selectedItem}.txt");

                listBoxPrompts.Items.Remove(selectedItem);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, nameof(PromptDesigner));
                MessageBox.Show("Error deleting prompt", "Prompt Repository Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
