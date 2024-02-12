namespace PlutoCast.Desktop.Helpers;

public static class MathHelper
{
    public static double Lerp(int x1, int x2, double progress)
    {
        return x1 * (1 - progress) + x2 * progress;
    }
}