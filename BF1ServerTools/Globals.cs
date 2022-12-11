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
    /// 玩家Avatar
    /// </summary>
    public static string Avatar = string.Empty;
    /// <summary>
    /// 玩家DisplayName
    /// </summary>
    public static string DisplayName = string.Empty;
    /// <summary>
    /// 玩家PersonaId
    /// </summary>
    public static long PersonaId = 0;
    /// <summary>
    /// 玩家Remid
    /// </summary>
    public static string Remid = string.Empty;
    /// <summary>
    /// 玩家Sid
    /// </summary>
    public static string Sid = string.Empty;
    /// <summary>
    /// 玩家SessionId
    /// </summary>
    public static string SessionId = string.Empty;
    /// <summary>
    /// 玩家登录令牌，有效期4小时
    /// </summary>
    public static string AccessToken = string.Empty;

    /// <summary>
    /// 当前服务器游戏Id
    /// </summary>
    public static long GameId = 0;

    /// <summary>
    /// 判断当前玩家是否为管理员
    /// </summary>
    /// <returns></returns>
    public static bool LoginPlayerIsAdmin
    {
        get
        {
            return ServerAdmins_PID.Contains(PersonaId);
        }
    }

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

    ///////////////////////////////////////////////////////

    /// <summary>
    /// 是否自动踢出违规玩家
    /// </summary>
    public static bool AutoKickBreakRulePlayer = false;
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
    LTime,
    Kit2,
    Weapon
}
