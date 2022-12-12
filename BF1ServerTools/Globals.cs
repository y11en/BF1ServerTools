using BF1ServerTools.Data;

namespace BF1ServerTools;

public static class Globals
{
    /// <summary>
    /// 玩家列表排序规则
    /// </summary>
    public static OrderBy OrderBy = OrderBy.Score;

    /// <summary>
    /// 是否使用模式1
    /// </summary>
    public static bool IsUseMode1 = true;

    ///////////////////////////////////////////////////////

    /// <summary>
    /// 模式1 玩家Avatar
    /// </summary>
    public static string Avatar1 = string.Empty;
    /// <summary>
    /// 模式2 玩家Avatar
    /// </summary>
    public static string Avatar2 = string.Empty;
    /// <summary>
    /// 玩家Avatar
    /// </summary>
    public static string Avatar
    {
        get
        {
            return IsUseMode1 ? Avatar1 : Avatar2;
        }
    }

    /// <summary>
    /// 模式1 玩家DisplayName
    /// </summary>
    public static string DisplayName1 = string.Empty;
    /// <summary>
    /// 模式2 玩家DisplayName
    /// </summary>
    public static string DisplayName2 = string.Empty;
    /// <summary>
    /// 全局玩家DisplayName
    /// </summary>
    public static string DisplayName
    {
        get
        {
            return IsUseMode1 ? DisplayName1 : DisplayName2;
        }
    }

    /// <summary>
    /// 模式1 玩家PersonaId
    /// </summary>
    public static long PersonaId1 = 0;
    /// <summary>
    /// 模式2 玩家PersonaId
    /// </summary>
    public static long PersonaId2 = 0;
    /// <summary>
    /// 全局PersonaId
    /// </summary>
    public static long PersonaId
    {
        get
        {
            return IsUseMode1 ? PersonaId1 : PersonaId2;
        }
    }

    ///////////////////////////////////////////////////////

    /// <summary>
    /// 玩家Remid
    /// </summary>
    public static string Remid = string.Empty;
    /// <summary>
    /// 玩家Sid
    /// </summary>
    public static string Sid = string.Empty;
    /// <summary>
    /// 玩家登录令牌，有效期4小时
    /// </summary>
    public static string AccessToken = string.Empty;

    /// <summary>
    /// 模式1 玩家SessionId
    /// </summary>
    public static string SessionId1 = string.Empty;
    /// <summary>
    /// 模式2 玩家SessionId
    /// </summary>
    public static string SessionId2 = string.Empty;
    /// <summary>
    /// 全局玩家SessionId
    /// </summary>
    public static string SessionId
    {
        get
        {
            return IsUseMode1 ? SessionId1 : SessionId2;
        }
    }

    /// <summary>
    /// 当前服务器游戏Id
    /// </summary>
    public static long GameId = 0;
    /// <summary>
    /// 当前服务器Id
    /// </summary>
    public static int ServerId = 0;
    /// <summary>
    /// 当前服务器游戏Guid
    /// </summary>
    public static string PersistedGameId = string.Empty;

    ///////////////////////////////////////////////////////

    /// <summary>
    /// 判断当前玩家是否为管理员
    /// </summary>
    /// <returns></returns>
    public static bool LoginPlayerIsAdmin
    {
        get
        {
            if (IsUseMode1)
                return ServerAdmins_PID.Contains(PersonaId1);
            else
                return ServerAdmins_PID.Contains(PersonaId2);
        }
    }

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
