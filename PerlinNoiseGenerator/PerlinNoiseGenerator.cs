using System.Drawing;
using System.Drawing.Imaging;

using DotnetNoise;

namespace PerlinNoiseGenerator;

public class PerlinNoiseGenerator : NoiseGenerator
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
   
}