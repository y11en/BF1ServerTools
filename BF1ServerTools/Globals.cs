namespace BF1ServerTools;

public static class Globals
{
    public static OrderBy OrderBy = OrderBy.Score;

    ///////////////////////////////////////////////////////
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
