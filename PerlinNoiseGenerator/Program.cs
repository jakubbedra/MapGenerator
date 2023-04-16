namespace PerlinNoiseGenerator;

static class Program
{
    static void Main(string[] args)
    {
        MapGenerator generator = new MapGenerator();
        generator.InitDefaultRegions();
        generator.GenerateImagesPerlin(1400, 1400, 5, "test_map_perlin");
        generator.GenerateImagesGaussian(1400, 1400, 5, "test_map_gaussian");
    }
}