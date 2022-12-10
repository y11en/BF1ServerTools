using BF1ServerTools.Data;

namespace BF1ServerTools;

public static class Globals
{
    /// <summary>
    /// 玩家列表排序规则
    /// </summary>
    public static OrderBy OrderBy = OrderBy.Score;

    ///////////////////////////////////////////////////////

    /// <summary>
    /// 当前服务器游戏Id
    /// </summary>
    public static string GameId = string.Empty;

    ///////////////////////////////////////////////////////

    /// <summary>
    /// 服务器管理员，PID
    /// </summary>
    public static List<long> ServerAdmins_PID = new();
    /// <summary>
    /// 服务器VIP
    /// </summary>
    public static List<long> ServerVIPs_PID = new();

    /// <summary>
    /// 自定义白名单玩家列表
    /// </summary>
    public static List<long> CustomWhites_PID = new();
    /// <summary>
    /// 自定义黑名单玩家列表
    /// </summary>
    public static List<long> CustomBlacks_PID = new();

    public static List<string> CustomWhites_Name = new();
    public static List<string> CustomBlacks_Name = new();

    /// <summary>
    /// 缓存玩家生涯数据
    /// </summary>
    public static List<LifePlayerData> LifePlayerCacheDatas = new();
}

public enum OrderBy
{
    Score,
    Rank,
    Clan,
    Name,
    SquadId,
    Kill,
    Dead,
    KD,
    KPM,
    LKD,
    LKPM,
    Kit2,
    Weapon
}
