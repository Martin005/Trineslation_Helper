using System.Collections.Generic;
using System.IO;
using System.Linq;
using Trineslation_Helper.GameProfiles;
using Trineslation_Helper.Properties;

namespace Trineslation_Helper.Processors
{
    public class FolderProcessor
    {
        public static Dictionary<string, FileType> GetFilesPaths(IGameProfile gameProfile, DirectoryInfo folder, TextType typeOfTexts, Direction direction)
        {
            Dictionary<string, FileType> filePaths = new Dictionary<string, FileType>();

            string inputFolderPath = $@"{folder.FullName}\";


            Dictionary<string, FileType> tempPaths = new Dictionary<string, FileType>();

            if (typeOfTexts == TextType.GameText)
            {
                if (direction == Direction.Input)
                {
                    tempPaths = gameProfile.GameFolderStructure;
                }
                else if (direction == Direction.Output)
                {
                    gameProfile.LuaStringsFolderLang = Settings.Default.TargetTerritoryFolder;
                    foreach (var file in gameProfile.GameFolderStructure.Where(i => i.Value == FileType.LuaStrings))
                    {
                        string tempFilePath = $@"{folder.FullName}\{Path.GetDirectoryName(file.Key)}\{Path.GetFileNameWithoutExtension(file.Key)}.xliff";
                        tempPaths.Add(tempFilePath, file.Value);
                    }
                    return tempPaths;
                }
            }
            else if (typeOfTexts == TextType.LauncherText)
            {
                tempPaths = gameProfile.LauncherFolderStructure;
            }

            foreach (var path in tempPaths)
            {
                filePaths.Add($"{inputFolderPath}{path.Key}", path.Value);
            }

            return filePaths;
        }
    }
}
