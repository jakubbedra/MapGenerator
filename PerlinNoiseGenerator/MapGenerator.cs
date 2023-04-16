using System.Drawing;
using System.Drawing.Imaging;

namespace PerlinNoiseGenerator;

public class MapGenerator
{
    private const float ScaleMin = 0.1f;
    private const float ScaleMax = 5.0f;
    private const float RadiusMin = 1.0f;
    private const float RadiusMax = 50.0f;

    private readonly Random _r;

    private List<TerrainType> _regions;

    public MapGenerator()
    {
        _r = new Random();
        _regions = new List<TerrainType>();
    }

    public void InitDefaultRegions()
    {
        _regions.Add(new TerrainType()
        {
            Name = "deep water",
            Height = 0.1f,
            Color = Color.DarkBlue
        });_regions.Add(new TerrainType()
        {
            Name = "water1",
            Height = 0.2f,
            Color = Color.MediumBlue
        });
        _regions.Add(new TerrainType()
        {
            Name = "water",
            Height = 0.3f,
            Color = Color.Blue
        });
        _regions.Add(new TerrainType()
        {
            Name = "water",
            Height = 0.3f,
            Color = Color.RoyalBlue
        });
        _regions.Add(new TerrainType()
        {
            Name = "water",
            Height = 0.36f,
            Color = Color.Khaki
        });
        _regions.Add(new TerrainType()
        {
            Name = "land1",
            Height = 0.5f,
            Color = Color.Lime
        });
        _regions.Add(new TerrainType()
        {
            Name = "land2",
            Height = 0.7f,
            Color = Color.ForestGreen
        });
        _regions.Add(new TerrainType()
        {
            Name = "land2",
            Height = 0.76f,
            Color = Color.Green
        });
        _regions.Add(new TerrainType()
        {
            Name = "mountains",
            Height = 0.8f,
            Color = Color.LightSlateGray
        });
        _regions.Add(new TerrainType()
        {
            Name = "mountains2",
            Height = 0.86f,
            Color = Color.DimGray
        });
        _regions.Add(new TerrainType()
        {
            Name = "mountains2",
            Height = 0.94f,
            Color = Color.DarkSlateGray
        });
        _regions.Add(new TerrainType()
        {
            Name = "mountains3",
            Height = 1.0f,
            Color = Color.White
        });
    }

    public void GenerateImagesPerlin(int width, int height, int amount, string filename, int octaves = 4, float persistance = 0.5f, float lacunarity = 2.0f)
    {
        Console.WriteLine("Generating worlds...");
        PerlinNoiseGenerator perlinNoiseGenerator = new PerlinNoiseGenerator(height, width);

        for (int i = 0; i < amount; i++)
        {
            float scale = _r.NextSingle() * (ScaleMax - ScaleMin) + ScaleMin;
            float[,] noiseMap =
                perlinNoiseGenerator.GenerateNoiseMap(_r.Next(), scale, octaves, persistance, lacunarity);
            Bitmap image = perlinNoiseGenerator.NoiseMapToImage(noiseMap, _regions);
            image.Save(filename + $"[{i}].bmp", ImageFormat.Bmp);
        }

        Console.WriteLine("All done!");
    }

    public void GenerateImagesGaussian(int width, int height, int amount, string filename)
    {
        Console.WriteLine("Generating worlds...");
        GaussianNoiseGenerator gaussianNoiseGenerator = new GaussianNoiseGenerator(height, width);

        for (int i = 0; i < amount; i++)
        {
            float radius = _r.NextSingle() * (RadiusMax - RadiusMin) + RadiusMin;
            float scale = _r.NextSingle() * (ScaleMax - ScaleMin) + ScaleMin;
            float[,] noiseMap = gaussianNoiseGenerator.GenerateNoiseMap(_r.Next(10, 24));
            Bitmap image = gaussianNoiseGenerator.NoiseMapToImage(noiseMap, _regions);
            image.Save(filename + $"[{i}].bmp", ImageFormat.Bmp);
        }

        Console.WriteLine("All done!");
    }
}