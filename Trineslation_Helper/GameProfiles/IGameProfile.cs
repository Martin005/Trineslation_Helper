using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml;

namespace Trineslation_Helper.GameProfiles
{
    public interface IGameProfile
    {
        string LuaStringsFolderLang { set; }
        string GettextMoFolderLang { set; }

        Dictionary<string, FileType> GameFolderStructure { get; }
        Dictionary<string, FileType> LauncherFolderStructure { get; }

        Task<XmlDocument> ConvertLuaStringsToXliffAsync(FileInfo luaInput);
    }
}
