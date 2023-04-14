﻿using System.Drawing;
using System.Drawing.Imaging;

namespace PerlinNoiseGenerator;

public class MapGenerator
{
    private const float ScaleMin = 0.1f;
    private const float ScaleMax = 5.0f;

    private readonly Random _r;

    public MapGenerator()
    {
        _r = new Random();
    }

    public void GenerateImages(int width, int height, int amount, string filename)
    {
        Console.WriteLine("Generating worlds...");
        PerlinNoiseGenerator perlinNoiseGenerator = new PerlinNoiseGenerator(height, width);

        for (int i = 0; i < amount; i++)
        {
            float scale = _r.NextSingle() * (ScaleMax - ScaleMin) + ScaleMin;
            float[,] noiseMap = perlinNoiseGenerator.GenerateNoiseMap(_r.Next(), scale);
            Bitmap image = perlinNoiseGenerator.NoiseMapToImage(noiseMap);
            image.Save(filename + $"[{i}].bmp", ImageFormat.Bmp);
        }

        Console.WriteLine("All done!");
    }
}