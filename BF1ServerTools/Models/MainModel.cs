using CommunityToolkit.Mvvm.ComponentModel;

namespace BF1ServerTools.Models;

public partial class MainModel : ObservableObject
{
    /// <summary>
    /// 程序运行时间
    /// </summary>
    [ObservableProperty]
    private string appRunTime;

    /// <summary>
    /// 头像
    /// </summary>
    [ObservableProperty]
    private string avatar;

    /// <summary>
    /// 显示名称
    /// </summary>
    [ObservableProperty]
    private string displayName;

    /// <summary>
    /// 数字Id
    /// </summary>
    [ObservableProperty]
    private long personaId;
}
