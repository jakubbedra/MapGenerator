namespace PerlinNoiseGenerator;

public class GaussianNoiseGenerator : NoiseGenerator
{
    private static readonly Random _random = new Random();

    private readonly int _mapHeight;
    private readonly int _mapWidth;

    public GaussianNoiseGenerator(int mapHeight, int mapWidth)
    {
        _mapHeight = mapHeight;
        _mapWidth = mapWidth;
    }

    private float Gaussian(int x, int y, float sigma)
    {
        float center = (_mapWidth - 1) / 2.0f;
        float dx = x - center;
        float dy = y - center;
        float exponent = -(dx * dx + dy * dy) / (2 * sigma * sigma);
        return (float)Math.Exp(exponent);
    }

    public float[,] GenerateNoiseMap(int points)
    {
        // todo: wylosować kilka takich pkt
        float sigma = _random.NextSingle()*10.0f;
        float[,] noiseMap = new float[_mapWidth, _mapHeight];
        for (int x = 0; x < _mapWidth; x++)
        for (int y = 0; y < _mapHeight; y++)
            noiseMap[x, y] = 0;
        
        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        for (int i = 0; i < points; i++)
        {
            // randomize center point
            float peakSigma = sigma / _random.NextSingle() * 3.0f + 1.5f;//.Range(1.5f, 3.0f);
            float peakValue = _random.NextSingle()*100 / (float)points;

            int x1 = _random.Next(0, _mapWidth);
            int y1 = _random.Next(0, _mapHeight);
            
            for (int x = 0; x < _mapWidth; x++)
            {
                for (int y = 0; y < _mapHeight; y++)
                {
                    float value = Gaussian(x+x1, y+y1, peakSigma);
                    noiseMap[x, y] += value * peakValue;
                    
                    if (noiseMap[x, y] > maxNoiseHeight)
                    {
                        maxNoiseHeight = noiseMap[x, y];
                    }
                    else if (noiseMap[x, y] < minNoiseHeight)
                    {
                        minNoiseHeight = noiseMap[x, y];
                    }
                }
            }
        }
        for (int y = 0; y < _mapHeight; y++)
        {
            for (int x = 0; x < _mapWidth; x++)
            {
                noiseMap[x,y] = (noiseMap[x, y] - minNoiseHeight) / (maxNoiseHeight - minNoiseHeight);
            }
        }
        return noiseMap;
    }

    private float[,] Map(float[,] map, float fromMin, float fromMax, float toMin, float toMax)
    {
        float[,] result = new float[_mapWidth, _mapHeight];
        float fromRange = fromMax - fromMin;
        float toRange = toMax - toMin;

        for (int x = 0; x < _mapWidth; x++)
        {
            for (int y = 0; y < _mapHeight; y++)
            {
                float value = map[x, y];
                value = (value - fromMin) / fromRange;
                value = value * toRange + toMin;
                result[x, y] = value;
            }
        }

        return result;
    }
}