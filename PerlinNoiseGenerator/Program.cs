namespace PerlinNoiseGenerator;

static class Program
{
    static void Main(string[] args)
    {
        MapGenerator generator = new MapGenerator();
        //generator.GenerateImages(1400, 1400, 5, "test_map");
        generator.GenerateImagesGaussian(1400, 1400, 5, "test_map");
    }
}