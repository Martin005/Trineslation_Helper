using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Trineslation_Helper.GameProfiles;
using Trineslation_Helper.Parsers;

namespace Trineslation_Helper.Processors
{
    public class Converter
    {
        public static async Task Convert(IGameProfile gameProfile, DirectoryInfo inputFolder, DirectoryInfo outputFolder, TextType textType, TaskType taskType)
        {
            if (taskType == TaskType.Extract)
            {
                // Extract Game text from Editor
                if (textType == TextType.GameText)
                {
                    var inputFiles = await Task.Run(() => FolderProcessor.GetFilesPaths(gameProfile, inputFolder, textType, Direction.Input));
                    var outputFiles = await Task.Run(() => FolderProcessor.GetFilesPaths(gameProfile, outputFolder, textType, Direction.Output));

                    // Convert lua strings files to XLIFFs
                    int counter = 0;
                    foreach (var inputFile in inputFiles.Where(i => i.Value == FileType.LuaStrings))
                    {
                        FileInfo file = new FileInfo(inputFile.Key);
                        XmlDocument xliff = await gameProfile.ConvertLuaStringsToXliffAsync(file);

                        await Task.Run(() => XliffParser.Save(outputFiles.ElementAt(counter).Key, xliff));

                        counter++;
                    }
                }
                // Extract Launcher text from game
                else if (textType == TextType.LauncherText)
                {

                }
            }
            else if (taskType == TaskType.Pack)
            {
                // Pack Game text from Editor
                if (textType == TextType.GameText)
                {

                }
                // Pack Launcher text from Editor
                else if (textType == TextType.LauncherText)
                {

                }
            }
        }
    }
}
