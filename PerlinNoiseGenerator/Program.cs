namespace PerlinNoiseGenerator;

static class Program
{
    static void Main(string[] args)
    {
        MapGenerator generator = new MapGenerator();
        generator.GenerateImages(1000, 1000, 5, "test_map");
    }
}