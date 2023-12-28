namespace ApplicationCore;

public class PagedList<T>
{
    public int TotalItems { get; set; } = 0;
    public ICollection<T> Result { get; set; }
}
