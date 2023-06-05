
public class StaticClass
{
    public static string[] cars = { "first", "jiga", "carvet" };
    public static string[] wheels = { "wheel1", "wheel2" };
    public static string[] colors = { "black", "darkGrey", "grey", "white", "lightBlue", "blue", "darkBlue", "lightGreen", "green", "darkGreen", "yellow", "pink", "purple", "red", "orange" };
    public static int[] carsColorElements = { 0, 2, 2 };

    public static string loadState = "menu";
    public static string lastTRTry = "dsadas";
    public static string gameMode = "timeRace";
    public static int place = 1;

    public static float GenVolume { get; set; } = 0.1f;
    public static float SoundsVolume { get; set; } = 1;
    public static float MusicVolume { get; set; } = 1;
    public static int CarsInfo { get; set; }
    public static int WheelsInfo { get; set; }
    public static int ColorInfo { get; set; }
}