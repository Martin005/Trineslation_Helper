using MetroFramework.Controls;
using MetroFramework.Forms;
using System;
using System.IO;
using System.Threading.Tasks;
using Trineslation_Helper.GameProfiles;
using Trineslation_Helper.Processors;
using Trineslation_Helper.Properties;

namespace Trineslation_Helper
{
    public partial class MainForm : MetroForm
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private DirectoryInfo ExtractGameInputFolder;
        private DirectoryInfo ExtractGameOutputFolder;
        private DirectoryInfo PackGameInputFolder;
        private DirectoryInfo PackGameOutputFolder;
        private DirectoryInfo ExtractLauncherInputFolder;
        private DirectoryInfo ExtractLauncherOutputFolder;
        private DirectoryInfo PackLauncherInputFolder;
        private DirectoryInfo PackLauncherOutputFolder;

        private void JumpToTheEndOfTextBox(MetroTextBox textBox)
        {
            textBox.Focus();
            textBox.Select(textBox.Text.Length, 0);
        }

        private void SanitizeFolderPath(MetroTextBox textBox)
        {
            if (textBox.Text.EndsWith(@"\") == false)
            {
                textBox.Text += @"\";
            }
        }

        private void PopulateTextBox(MetroTextBox textBox, DirectoryInfo folder)
        {
            if (folder != null)
            {
                textBox.Text = folder.FullName;
                SanitizeFolderPath(textBox);
                JumpToTheEndOfTextBox(textBox);
            }
        }

        private void PopulateFolderPath(ref DirectoryInfo folder, MetroTextBox textBox)
        {
            if (folder == null || $@"{folder.FullName}\" != textBox.Text)
            {
                SanitizeFolderPath(textBox);
                folder = new DirectoryInfo(textBox.Text);
            }
        }

        private IGameProfile GetGameProfile()
        {
            return Settings.Default.GetGameProfile();
        }

        private void ExtractGameInputButton_Click(object sender, EventArgs e)
        {
            ExtractGameInputFolder = Dialogs.ShowFolderDialog(DialogType.ExtractGameInput);
            PopulateTextBox(ExtractGameInputTextBox, ExtractGameInputFolder);
        }

        private void ExtractGameOutputButton_Click(object sender, EventArgs e)
        {
            ExtractGameOutputFolder = Dialogs.ShowFolderDialog(DialogType.ExtractGameOutput);
            PopulateTextBox(ExtractGameOutputTextBox, ExtractGameOutputFolder);
        }

        private async void ExtractGameTextButton_Click(object sender, EventArgs e)
        {
            PopulateFolderPath(ref ExtractGameInputFolder, ExtractGameInputTextBox);
            PopulateFolderPath(ref ExtractGameOutputFolder, ExtractGameOutputTextBox);

            await Task.Run(() => Converter.Convert(GetGameProfile(), ExtractGameInputFolder, ExtractGameOutputFolder, TextType.GameText, TaskType.Extract));
        }

        private async void PackGameTextButton_Click(object sender, EventArgs e)
        {
            PopulateFolderPath(ref PackGameInputFolder, PackGameInputTextBox);
            PopulateFolderPath(ref PackGameOutputFolder, PackGameOutputTextBox);

            await Task.Run(() => Converter.Convert(GetGameProfile(), PackGameInputFolder, PackGameOutputFolder, TextType.GameText, TaskType.Pack));
        }

        private async void ExtractLauncherTextButton_Click(object sender, EventArgs e)
        {
            PopulateFolderPath(ref ExtractLauncherInputFolder, ExtractLauncherInputTextBox);
            PopulateFolderPath(ref ExtractLauncherOutputFolder, ExtractLauncherOutputTextBox);

            await Task.Run(() => Converter.Convert(GetGameProfile(), ExtractLauncherInputFolder, ExtractLauncherOutputFolder, TextType.LauncherText, TaskType.Extract));
        }

        private async void PackLauncherTextButton_Click(object sender, EventArgs e)
        {
            PopulateFolderPath(ref PackLauncherInputFolder, PackLauncherInputTextBox);
            PopulateFolderPath(ref PackLauncherOutputFolder, PackLauncherOutputTextBox);

            await Task.Run(() => Converter.Convert(GetGameProfile(), PackLauncherInputFolder, PackLauncherOutputFolder, TextType.LauncherText, TaskType.Pack));
        }

        private void MetroComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            MetroComboBox comboBox = (MetroComboBox)sender;
            if (comboBox.SelectedItem != null)
            {
                Settings.Default.Game = comboBox.SelectedItem.ToString();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ExtractGameInputTextBox.WaterMark = Resources.ExtractGameInput;
            ExtractGameOutputTextBox.WaterMark = Resources.ExtractGameOutput;
            PackGameInputTextBox.WaterMark = Resources.PackGameInput;
            PackGameOutputTextBox.WaterMark = Resources.PackGameOutput;
            ExtractLauncherInputTextBox.WaterMark = Resources.ExtractLauncherInput;
            ExtractLauncherOutputTextBox.WaterMark = Resources.ExtractLauncherOutput;
            PackLauncherInputTextBox.WaterMark = Resources.PackLauncherInput;
            PackLauncherOutputTextBox.WaterMark = Resources.PackLauncherOutput;
            TargetLangTypeFolderTextBox.WaterMark = "Language Type";
            TargetTerritoryFolderTextBox.WaterMark = "Territory";
            TargetLangLabelTextBox.WaterMark = "Language Label";
            GamePickerComboBox.PromptText = Resources.PickGameFromListPrompt;

            ExtractGameInputButton.Text = Resources.SelectInputFolder;
            ExtractGameOutputButton.Text = Resources.SelectOutputFolder;
            PackGameInputButton.Text = Resources.SelectInputFolder;
            PackGameOutputButton.Text = Resources.SelectOutputFolder;
            ExtractLauncherInputButton.Text = Resources.SelectInputFolder;
            ExtractLauncherOutputButton.Text = Resources.SelectOutputFolder;
            PackLauncherInputButton.Text = Resources.SelectInputFolder;
            PackLauncherOutputButton.Text = Resources.SelectOutputFolder;
        }

        private void TargetLanguageLabel_Click(object sender, EventArgs e)
        {
            string url = "https://datahub.io/core/language-codes#resource-ietf-language-tags";
            System.Diagnostics.Process.Start(url);
        }

        private void TargetLangTypeFolderTextBox_TextChanged(object sender, EventArgs e)
        {
            MetroTextBox textBox = (MetroTextBox)sender;
            Settings.Default.TargetLangTypeFolder = textBox.Text;
        }

        private void TargetTerritoryFolderTextBox_TextChanged(object sender, EventArgs e)
        {
            MetroTextBox textBox = (MetroTextBox)sender;
            Settings.Default.TargetTerritoryFolder = textBox.Text;
        }

        private void TargetLangLabelTextBox_TextChanged(object sender, EventArgs e)
        {
            MetroTextBox textBox = (MetroTextBox)sender;
            Settings.Default.TargetLangLabel = textBox.Text;
        }
    }
}
