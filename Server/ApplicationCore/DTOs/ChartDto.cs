namespace ApplicationCore;

public class ChartDto
{

}

public class PieChartDto
{
    public ICollection<string> Labels { get; set; }
    public ICollection<long> Data { get; set; }
}