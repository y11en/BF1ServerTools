using Chinese;

namespace BF1ServerTools.Utils;

public static class ChsUtil
{
    /// <summary>
    /// 字符串简体转繁体
    /// </summary>
    /// <param name="strSimple"></param>
    /// <returns></returns>
    public static string ToTraditional(string strSimple)
    {
        return ChineseConverter.ToTraditional(strSimple);
    }

    /// <summary>
    /// 字符串繁体转简体
    /// </summary>
    /// <param name="strTraditional"></param>
    /// <returns></returns>
    public static string ToSimplified(string strTraditional)
    {
        return ChineseConverter.ToSimplified(strTraditional);
    }
}
