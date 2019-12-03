using Microsoft.WindowsAPICodePack.Dialogs;
using System.IO;
using Trineslation_Helper.Properties;

namespace Trineslation_Helper
{
    public static class Dialogs
    {
        public static DirectoryInfo ShowFolderDialog(DialogType type)
        {
            DirectoryInfo returningFolderPath = null;
            using (var folderSelectDialog = new CommonOpenFileDialog())
            {
                folderSelectDialog.RestoreDirectory = true;
                folderSelectDialog.IsFolderPicker = true;

                switch (type)
                {
                    case DialogType.ExtractGameInput:
                        folderSelectDialog.Title = Resources.ExtractGameInput;
                        break;
                    case DialogType.ExtractGameOutput:
                        folderSelectDialog.Title = Resources.ExtractGameOutput;
                        break;
                    case DialogType.PackGameInput:
                        folderSelectDialog.Title = Resources.PackGameInput;
                        break;
                    case DialogType.PackGameOutput:
                        folderSelectDialog.Title = Resources.PackGameOutput;
                        break;
                    case DialogType.ExtractLauncherInput:
                        folderSelectDialog.Title = Resources.ExtractLauncherInput;
                        break;
                    case DialogType.ExtractLauncherOutput:
                        folderSelectDialog.Title = Resources.ExtractLauncherOutput;
                        break;
                    case DialogType.PackLauncherInput:
                        folderSelectDialog.Title = Resources.PackLauncherInput;
                        break;
                    case DialogType.PackLauncherOutput:
                        folderSelectDialog.Title = Resources.PackLauncherOutput;
                        break;
                    default:
                        folderSelectDialog.Title = "Select a folder";
                        break;
                }

                if (folderSelectDialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    returningFolderPath = new DirectoryInfo(folderSelectDialog.FileName);
                }
            }
            return returningFolderPath;
        }
    }
}
