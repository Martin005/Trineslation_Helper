namespace Trineslation_Helper
{
    public enum FileType
    {
        LuaStrings,
        LuaLocaleSettings,
        LuaModSettings,
        GettextMo,
        Fbll
    }

    public enum DialogType
    {
        ExtractGameInput,
        ExtractGameOutput,
        PackGameInput,
        PackGameOutput,
        ExtractLauncherInput,
        ExtractLauncherOutput,
        PackLauncherInput,
        PackLauncherOutput
    }

    public enum TextType
    {
        GameText,
        LauncherText
    }

    public enum TaskType
    {
        Extract,
        Pack
    }

    public enum Direction
    {
        Input,
        Output
    }
}
