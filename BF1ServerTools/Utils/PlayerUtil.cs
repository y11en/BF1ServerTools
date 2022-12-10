namespace BF1ServerTools.Utils;

public static class PlayerUtil
{
    /// <summary>
    /// 线程锁
    /// </summary>
    private static readonly object Obj = new();

    /// <summary>
    /// 获取玩家游玩时间，返回分钟数或小时数
    /// </summary>
    /// <param name="second"></param>
    /// <returns></returns>
    public static string GetPlayTime(double second)
    {
        var ts = TimeSpan.FromSeconds(second);

        if (ts.TotalHours < 1)
        {
            return ts.TotalMinutes.ToString("0") + " 分钟";
        }

        return ts.TotalHours.ToString("0") + " 小时";
    }

    /// <summary>
    /// 计算玩家KD
    /// </summary>
    /// <param name="kill">玩家击杀数</param>
    /// <param name="dead">玩家死亡数</param>
    /// <returns>返回玩家KD（小数float）<returns>
    public static float GetPlayerKD(int kill, int dead)
    {
        if (kill == 0 && dead >= 0)
        {
            return 0.0f;
        }
        else if (kill > 0 && dead == 0)
        {
            return kill;
        }
        else if (kill > 0 && dead > 0)
        {
            return (float)kill / dead;
        }
        else
        {
            return (float)kill / dead;
        }
    }

    /// <summary>
    /// 计算玩家KPM
    /// </summary>
    /// <param name="kill"></param>
    /// <param name="minute"></param>
    /// <returns></returns>
    public static float GetPlayerKPM(int kill, float minute)
    {
        if (minute != 0.0f)
        {
            return kill / minute;
        }
        else
        {
            return 0.0f;
        }
    }

    /// <summary>
    /// 获取玩家KPM
    /// </summary>
    /// <param name="kill"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public static string GetPlayerKPM(float kill, float time)
    {
        if (time < 60)
        {
            return "0.00";
        }
        else
        {
            var minute = (int)(time / 60);
            return $"{kill / minute:0.00}";
        }
    }

    /// <summary>
    /// 计算百分比
    /// </summary>
    /// <param name="num1"></param>
    /// <param name="num2"></param>
    /// <returns></returns>
    public static string GetPlayerPercentage(float num1, float num2)
    {
        if (num2 != 0)
        {
            return $"{num1 / num2 * 100:0.00}%";
        }
        else
        {
            return "0%";
        }
    }

    /// <summary>
    /// 获取击杀星数
    /// </summary>
    /// <param name="kills"></param>
    /// <returns></returns>
    public static string GetKillStar(int kills)
    {
        if (kills < 100)
        {
            return "";
        }
        else
        {
            int count = kills / 100;

            return $"{count}";
        }
    }

    /// <summary>
    /// 小数类型的时间秒，转为mm:ss格式
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public static string SecondsToMMSS(float time)
    {
        var mm = time / 60;
        var ss = time % 60;

        return $"{mm:00}:{ss:00}";
    }

    /// <summary>
    /// 修正服务器得分数据
    /// </summary>
    /// <param name="score"></param>
    /// <returns></returns>
    public static int FixedServerScore(int score)
    {
        return score < 0 || score > 2000 ? 0 : score;
    }

    /// <summary>
    /// 修正服务器得分数据
    /// </summary>
    /// <param name="score"></param>
    /// <returns></returns>
    public static double FixedServerScore(double score)
    {
        return score < 0 || score > 125 ? 0 : score;
    }

    /// <summary>
    /// 获取玩家ID或队标
    /// </summary>
    /// <param name="originalName"></param>
    /// <param name="isClan"></param>
    /// <returns></returns>
    public static string GetPlayerTargetName(string originalName, bool isClan)
    {
        if (string.IsNullOrEmpty(originalName))
            return "";

        int index = originalName.IndexOf("]");
        string clan;
        string name;
        if (index != -1)
        {
            clan = originalName.Substring(1, index - 1);
            name = originalName.Substring(index + 1);
        }
        else
        {
            clan = "";
            name = originalName;
        }

        if (isClan)
            return clan;
        else
            return name;
    }

    /// <summary>
    /// 小数类型的时间秒，转为分钟
    /// </summary>
    /// <param name="second"></param>
    /// <returns></returns>
    public static int SecondsToMinute(float second)
    {
        if (second >= 0 && second <= 36000)
        {
            return (int)(second / 60);
        }
        else
        {
            return 0;
        }
    }

    /// <summary>
    /// 判断玩家是不是管理员、VIP或白名单
    /// </summary>
    /// <param name="personaId"></param>
    /// <param name="list"></param>
    /// <returns></returns>
    public static bool IsAdminVIPWhite(long personaId, List<long> list)
    {
        return list.IndexOf(personaId) != -1;
    }

    /// <summary>
    /// 获取生涯KD
    /// </summary>
    /// <param name="personaId"></param>
    /// <returns></returns>
    public static float GetLifeKD(long personaId)
    {
        lock (Obj)
        {
            var index = Globals.LifePlayerCacheDatas.FindIndex(item => item.PersonaId == personaId);
            if (index != -1)
            {
                return Globals.LifePlayerCacheDatas[index].KD;
            }

            return 0;
        }
    }

    /// <summary>
    /// 获取生涯KPM
    /// </summary>
    /// <param name="personaId"></param>
    /// <returns></returns>
    public static float GetLifeKPM(long personaId)
    {
        lock (Obj)
        {
            var index = Globals.LifePlayerCacheDatas.FindIndex(item => item.PersonaId == personaId);
            if (index != -1)
            {
                return Globals.LifePlayerCacheDatas[index].KPM;
            }

            return 0;
        }
    }
}
