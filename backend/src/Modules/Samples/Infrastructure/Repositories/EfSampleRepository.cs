using Backend.Modules.Samples.Domain;
using Backend.Modules.Samples.Domain.Repositories;
using Backend.Shared.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Backend.Modules.Samples.Infrastructure.Repositories;

public class EfSampleRepository(AppDbContext context) : ISampleRepository
{
    public Task<Sample?> FindByIdAsync(Guid id)
    {
        return context.Samples.FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<IReadOnlyList<Sample>> FindByServiceOrderIdAsync(Guid serviceOrderId)
    {
        return await context.Samples
            .Where(s => s.ServiceOrderId == serviceOrderId)
            .OrderBy(s => s.CreatedAt)
            .ToListAsync();
    }

    public async Task SaveAsync(Sample sample)
    {
        var exists = await context.Samples.AnyAsync(s => s.Id == sample.Id);
        if (exists)
            context.Samples.Update(sample);
        else
            context.Samples.Add(sample);

        await context.SaveChangesAsync();
    }

    public async Task SaveManyAsync(IEnumerable<Sample> samples)
    {
        context.Samples.AddRange(samples);
        await context.SaveChangesAsync();
    }
}
