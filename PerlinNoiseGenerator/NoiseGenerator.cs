using System.Drawing;
using System.Drawing.Imaging;

namespace PerlinNoiseGenerator;

public class NoiseGenerator
{
    public Bitmap NoiseMapToImage(float[,] noiseMap, List<TerrainType> regions)
    {
        int width = noiseMap.GetLength(0);
        int height = noiseMap.GetLength(1);

        float minValue = float.MaxValue;
        float maxValue = float.MinValue;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float value = noiseMap[x, y];
                minValue = Math.Min(minValue, value);
                maxValue = Math.Max(maxValue, value);
            }
        }

        Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float value = noiseMap[x, y];
                //int grayValue = (int)(255 * (value - minValue) / (maxValue - minValue));
                //Color pixelColor = Color.FromArgb(grayValue, grayValue, grayValue);
                Color pixelColor = GetAssignedColor(value, regions, minValue, maxValue);
                bitmap.SetPixel(x, y, pixelColor);
            }
        }

        return bitmap;
    }

    private Color GetAssignedColor(float height, List<TerrainType> regions, float minValue, float maxValue)
    {
        foreach (TerrainType region in regions)
        {
            if (height < region.Height)
            {
                return region.Color;
            }
        }

        int grayValue = (int)(255 * (height - minValue) / (maxValue - minValue));
        return Color.FromArgb(grayValue, grayValue, grayValue);
    }
}