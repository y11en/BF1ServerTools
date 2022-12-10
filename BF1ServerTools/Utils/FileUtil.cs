namespace BF1ServerTools.Utils;

public static class FileUtil
{
    /// <summary>
    /// 我的文档目录路径
    /// </summary>
    public static readonly string MyDocuments_Path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

    /// <summary>
    /// 默认配置文件路径
    /// </summary>
    public static readonly string Default_Path = MyDocuments_Path + @"\BF1ServerTools";

    public static readonly string D_Cache_Path = Default_Path + @"\Cache";
    public static readonly string D_Config_Path = Default_Path + @"\Config";
    public static readonly string D_Data_Path = Default_Path + @"\Data";
    public static readonly string D_Log_Path = Default_Path + @"\Log";
    public static readonly string D_Robot_Path = Default_Path + @"\Robot";

    /// <summary>
    /// 主程序资源路径
    /// </summary>
    public const string Resource_Path = "BF1ServerTools.Features.Files.";
}
