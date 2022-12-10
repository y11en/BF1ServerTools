namespace BF1ServerTools.IMG.Img;

public static class ClassesImg
{
    public static Dictionary<string, string> ClassesDict = new();
    public static Dictionary<string, string> Classes2Dict = new();

    static ClassesImg()
    {
        InitDict();
    }

    private static void InitDict()
    {
        ClassesDict.Add("Scout.png", @"\BF1ServerTools.IMG;component\Assets\Images\Classes\Scout.png");
        ClassesDict.Add("Support.png", @"\BF1ServerTools.IMG;component\Assets\Images\Classes\Support.png");
        ClassesDict.Add("Assault.png", @"\BF1ServerTools.IMG;component\Assets\Images\Classes\Assault.png");
        ClassesDict.Add("Medic.png", @"\BF1ServerTools.IMG;component\Assets\Images\Classes\Medic.png");
        ClassesDict.Add("Cavalry.png", @"\BF1ServerTools.IMG;component\Assets\Images\Classes\Cavalry.png");
        ClassesDict.Add("Tanker.png", @"\BF1ServerTools.IMG;component\Assets\Images\Classes\Tanker.png");
        ClassesDict.Add("Pilot.png", @"\BF1ServerTools.IMG;component\Assets\Images\Classes\Pilot.png");

        Classes2Dict.Add("Scout.png", @"\BF1ServerTools.IMG;component\Assets\Images\Classes2\Scout.png");
        Classes2Dict.Add("Support.png", @"\BF1ServerTools.IMG;component\Assets\Images\Classes2\Support.png");
        Classes2Dict.Add("Assault.png", @"\BF1ServerTools.IMG;component\Assets\Images\Classes2\Assault.png");
        Classes2Dict.Add("Medic.png", @"\BF1ServerTools.IMG;component\Assets\Images\Classes2\Medic.png");
        Classes2Dict.Add("Cavalry.png", @"\BF1ServerTools.IMG;component\Assets\Images\Classes2\Cavalry.png");
        Classes2Dict.Add("Tanker.png", @"\BF1ServerTools.IMG;component\Assets\Images\Classes2\Tanker.png");
        Classes2Dict.Add("Pilot.png", @"\BF1ServerTools.IMG;component\Assets\Images\Classes2\Pilot.png");
    }
}
