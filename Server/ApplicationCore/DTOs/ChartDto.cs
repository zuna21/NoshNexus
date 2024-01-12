namespace ApplicationCore;

public class ChartDto
{

}

public class PieChartDto
{
    public ICollection<string> Labels { get; set; }
    public ICollection<long> Data { get; set; }
}

public class LineChartDto
{
    public ICollection<string> Labels { get; set; }
    public ICollection<int> Data { get; set; }
}