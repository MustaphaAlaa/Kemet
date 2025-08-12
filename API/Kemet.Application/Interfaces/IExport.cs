namespace Entities.Models.Interfaces.Helpers;

public interface IExport
{
    Task<byte[]> Export(List<int> orderIds);
}
