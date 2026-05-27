namespace Backend.Modules.Samples.Domain.Repositories;

public interface ISampleRepository
{
    Task<Sample?> FindByIdAsync(Guid id);
    Task<IReadOnlyList<Sample>> FindByServiceOrderIdAsync(Guid serviceOrderId);
    Task SaveAsync(Sample sample);
    Task SaveManyAsync(IEnumerable<Sample> samples);
}
