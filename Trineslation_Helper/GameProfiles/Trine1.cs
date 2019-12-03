using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using Trineslation_Helper.Parsers;

namespace Trineslation_Helper.GameProfiles
{
    public class Trine1 : IGameProfile
    {
        private string LuaStringsLang = "en";
        private string GettextMoLang = "de";

        public string LuaStringsFolderLang { set => LuaStringsLang = value; }
        public string GettextMoFolderLang { set => GettextMoLang = value; }

        public Dictionary<string, FileType> GameFolderStructure => new Dictionary<string, FileType>
        {
            { $@"data\locale\audio\{LuaStringsLang}\audiotiming.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\controller.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\extras.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\extras_pc.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\misc.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\systemmessages.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\hud\character_change_request.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\hud\collection_messages_window.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\hud\experience_window.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\hud\misc.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\hud\picksecretwindow.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\hud\touchscreen.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\hud\tutorial_tooltips.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\hud\weapon_window.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\menu\audiovolume.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\menu\controllerdisconnectedpopup.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\menu\dialog.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\menu\difficulty.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\menu\disconnectedpopup.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\menu\dropdownpopup.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\menu\gameovermenu.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\menu\ingameoptionsmenu.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\menu\inventory.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\menu\joininfopopup.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\menu\listview.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\menu\loadingwindow.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\menu\messageboxpopup.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\menu\mission.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\menu\nomoreplayerspopup.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\menu\pausegame.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\menu\pausemenu.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\menu\previewmenu.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\menu\saveinfopopup.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\menu\trophies.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\menu\trophypopup.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\menu\upgrade.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\menu\mainmenu\achievementsmenu.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\menu\mainmenu\audiovolumemenu.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\menu\mainmenu\campaignselectmenu.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\menu\mainmenu\common.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\menu\mainmenu\conceptartunlocksmenu.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\menu\mainmenu\controlsettings.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\menu\mainmenu\controlsmenu.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\menu\mainmenu\controlsmenupc.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\menu\mainmenu\creditsdata.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\menu\mainmenu\creditsmenu.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\menu\mainmenu\creditsviewevenmenu.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\menu\mainmenu\creditsviewoddmenu.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\menu\mainmenu\demosplashmenu.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\menu\mainmenu\developermenu.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\menu\mainmenu\dlcsplashmenu.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\menu\mainmenu\helpandoptionsmenu.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\menu\mainmenu\hostgamemenu.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\menu\mainmenu\howtoplaymenu.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\menu\mainmenu\joinonlinegamemenu.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\menu\mainmenu\languagesettingsmenu.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\menu\mainmenu\leaderboardsmenu.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\menu\mainmenu\main.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\menu\mainmenu\mainmenu.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\menu\mainmenu\mainmenupre.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\menu\mainmenu\microphonesettingsmenu.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\menu\mainmenu\multiplayermenu.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\menu\mainmenu\networksettingsmenu.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\menu\mainmenu\playmenu.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\menu\mainmenu\poemunlocksmenu.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\menu\mainmenu\profileselectmenu.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\menu\mainmenu\profilesettingsmenu.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\menu\mainmenu\settingsmenu.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\menu\mainmenu\singleplayermenu.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\menu\mainmenu\singleplayerstartdifficultymenu.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\menu\mainmenu\supportedcontrollersmenu.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\menu\mainmenu\trophiesandextrasmenu.lua", FileType.LuaStrings },
            { $@"data\locale\gui\{LuaStringsLang}\menu\mainmenu\visualsettingsmenu.lua", FileType.LuaStrings },
            { $@"data\locale\subtitles\{LuaStringsLang}\locales.lua", FileType.LuaStrings },
            { @"data\script\misc\locale_settings.lua", FileType.LuaLocaleSettings },
            { @"data\script\misc\mod_settings.lua", FileType.LuaModSettings }
        };

        public Dictionary<string, FileType> LauncherFolderStructure => new Dictionary<string, FileType>
        {
            { $@"data\launcher\locale\{GettextMoLang}\game_launcher.mo", FileType.GettextMo },
            { @"data\launcher\locale\launcher_base_locales.fbll", FileType.Fbll }
        };

        public async Task<XmlDocument> ConvertLuaStringsToXliffAsync(FileInfo luaInput)
        {
            Regex getId = new Regex(@"^[^\s.]*");
            Regex getSource = new Regex("(?<=\\[\\[).*(?=\\]\\])|(?<=\").*(?=\")");
            Regex getSourceFallback = new Regex(@"(?<=(\=\s))[^\s.]*");

            XliffParser xliff = new XliffParser("en-US", luaInput.Name);
            List<TranslationUnit> transUnits = new List<TranslationUnit>();

            using (StreamReader stream = luaInput.OpenText())
            {
                while (stream.EndOfStream == false)
                {
                    string line = await Task.Run(() => stream.ReadLineAsync());

                    if (string.IsNullOrEmpty(line) == false && line.StartsWith("--") == false)
                    {
                        if (getId.Matches(line).Count > 0)
                        {
                            if (line.EndsWith("[["))
                            {
                                string id = getId.Match(line).Value;
                                line = await Task.Run(() => stream.ReadLineAsync());

                                StringBuilder sb = new StringBuilder();
                                while (line.StartsWith("]]") == false)
                                {
                                    if (line.EndsWith("]]"))
                                    {
                                        sb.AppendLine(line.TrimEnd(']'));
                                        break;
                                    }
                                    sb.AppendLine(line);
                                    line = await Task.Run(() => stream.ReadLineAsync());
                                }
                                string source = sb.ToString();

                                transUnits.Add(new TranslationUnit
                                {
                                    Identifier = id,
                                    Source = source
                                });
                            }
                            else if (getSource.IsMatch(line) == false)
                            {
                                transUnits.Add(new TranslationUnit
                                {
                                    Identifier = getId.Match(line).Value,
                                    Source = getSourceFallback.Match(line).Value
                                });
                            }
                            else
                            {
                                if (getId.IsMatch(line) && getSource.IsMatch(line))
                                {
                                    transUnits.Add(new TranslationUnit
                                    {
                                        Identifier = getId.Match(line).Value,
                                        Source = getSource.Match(line).Value
                                    });
                                }
                            }
                        }
                    }
                }
            }

            return xliff.CreateXliffDocument(transUnits);
        }
    }
}
