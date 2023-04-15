using System.Drawing;
using System.Drawing.Imaging;

namespace PerlinNoiseGenerator;

public class GaussianNoiseGenerator
{
    private static readonly Random _random = new Random();
    
    private readonly int _mapHeight;
    private readonly int _mapWidth;

    public GaussianNoiseGenerator(int mapHeight, int mapWidth)
    {
        _mapHeight = mapHeight;
        _mapWidth = mapWidth;
    }

    public float[,] GenerateNoiseMap(int seed, float scale, float maxRadius, int numGaussians)
    {
        scale = (float)(scale > 0.0 ? scale : 0.0001);
        float[,] noiseMap = new float[_mapWidth, _mapHeight];
        
        // Calculate the standard deviation of the Gaussian functions
        float sigma = maxRadius / 3.0f;

        // Calculate the amplitude of the Gaussian functions
        float amplitude = 1.0f / (float)numGaussians;

        // Loop over each pixel in the noise map
        for (int y = 0; y < _mapHeight; y++)
        {
            for (int x = 0; x < _mapWidth; x++)
            {
                // Calculate the value of the pixel as a sum of Gaussian functions
                float value = 0.0f;
                for (int i = 0; i < numGaussians; i++)
                {
                    // Choose a random center point for the Gaussian function
                    float centerX = (float)_random.NextDouble() * _mapHeight;
                    float centerY = (float)_random.NextDouble() * _mapHeight;

                    // Calculate the distance to the center point
                    float distance = (float)Math.Sqrt(Math.Pow(x - centerX, 2) + Math.Pow(y - centerY, 2));

                    // Calculate the value of the Gaussian function at the distance
                    float gaussian = amplitude * (float)Math.Exp(-(distance * distance) / (2 * sigma * sigma));

                    // Add the value of the Gaussian function to the pixel value
                    value += gaussian;
                }

                // Store the pixel value in the noise map array
                noiseMap[x, y] = value;
            }
        }

        // Normalize the values in the noise map to a range of 0 to 1
        float minValue = float.MaxValue;
        float maxValue = float.MinValue;
        for (int y = 0; y < _mapHeight; y++)
        {
            for (int x = 0; x < _mapWidth; x++)
            {
                float value = noiseMap[x, y];
                if (value < minValue)
                    minValue = value;
                if (value > maxValue)
                    maxValue = value;
            }
        }
        float range = maxValue - minValue;
        for (int y = 0; y < _mapHeight; y++)
        {
            for (int x = 0; x < _mapWidth; x++)
            {
                noiseMap[x, y] = (noiseMap[x, y] - minValue) / range;
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