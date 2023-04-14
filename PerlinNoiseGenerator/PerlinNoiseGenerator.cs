using System.Drawing;
using System.Drawing.Imaging;

using DotnetNoise;

namespace PerlinNoiseGenerator;

public class PerlinNoiseGenerator
{
    private readonly int _mapHeight;
    private readonly int _mapWidth;

    public PerlinNoiseGenerator(int mapHeight, int mapWidth)
    {
        _mapHeight = mapHeight;
        _mapWidth = mapWidth;
    }

    public float[,] GenerateNoiseMap(int seed, float scale)
    {
        scale = (float)(scale > 0.0 ? scale : 0.0001);
        float[,] noiseMap = new float[_mapWidth, _mapHeight];
        FastNoise noise = new FastNoise(seed);
        
        for (int y = 0; y < _mapHeight; y++)
        {
            for (int x = 0; x < _mapWidth; x++)
            {
                float sampleX = x / scale;
                float sampleY = y / scale;
                noiseMap[y, x] = noise.GetPerlin(sampleX, sampleY);
            }
        }

        return noiseMap;
    }
    
    public Bitmap NoiseMapToImage(float[,] noiseMap)
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
                int grayValue = (int)(255 * (value - minValue) / (maxValue - minValue));
                Color pixelColor = Color.FromArgb(grayValue, grayValue, grayValue);
                bitmap.SetPixel(x, y, pixelColor);
            }
        }

        return bitmap;
    }
}