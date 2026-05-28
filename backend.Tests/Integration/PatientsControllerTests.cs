using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace Backend.Tests.Integration;

public class PatientsControllerTests : IClassFixture<ApiFactory>, IAsyncLifetime
{
    private readonly HttpClient _client;
    private readonly ApiFactory _factory;

    public PatientsControllerTests(ApiFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    public async Task InitializeAsync()
    {
        await _factory.MigrateAsync();
        await _factory.ResetDatabaseAsync();
    }

    public Task DisposeAsync() => Task.CompletedTask;

    private static object ValidPayload(string cpf = "52998224725") => new
    {
        name = "Maria Silva",
        cpf,
        birthDate = "1990-05-15",
        phone = "11999990000"
    };

    [Fact]
    public async Task Register_WithValidData_Returns201AndPatient()
    {
        var response = await _client.PostAsJsonAsync("/api/patients", ValidPayload());

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var body = await response.Content.ReadFromJsonAsync<PatientResponse>();
        Assert.NotNull(body);
        Assert.NotEqual(Guid.Empty, body.Id);
        Assert.Equal("Maria Silva", body.Name);
        Assert.Equal("52998224725", body.Cpf);
    }

    [Fact]
    public async Task Register_WithSameCpf_IsIdempotentAndReturns200()
    {
        await _client.PostAsJsonAsync("/api/patients", ValidPayload());

        var response = await _client.PostAsJsonAsync("/api/patients", ValidPayload());

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var body = await response.Content.ReadFromJsonAsync<PatientResponse>();
        Assert.NotNull(body);
        Assert.Equal("52998224725", body.Cpf);
    }

    [Fact]
    public async Task Register_WithEmptyName_Returns422()
    {
        var payload = new { name = "", cpf = "52998224725", birthDate = "1990-05-15" };

        var response = await _client.PostAsJsonAsync("/api/patients", payload);

        Assert.Equal(HttpStatusCode.UnprocessableEntity, response.StatusCode);
    }

    [Fact]
    public async Task Register_WithCpfWrongLength_Returns422()
    {
        var payload = new { name = "João", cpf = "123456789", birthDate = "1990-05-15" };

        var response = await _client.PostAsJsonAsync("/api/patients", payload);

        Assert.Equal(HttpStatusCode.UnprocessableEntity, response.StatusCode);
    }

    [Fact]
    public async Task Register_WithFutureBirthDate_Returns422()
    {
        var payload = new
        {
            name = "João",
            cpf = "52998224725",
            birthDate = DateTime.UtcNow.AddYears(1).ToString("yyyy-MM-dd")
        };

        var response = await _client.PostAsJsonAsync("/api/patients", payload);

        Assert.Equal(HttpStatusCode.UnprocessableEntity, response.StatusCode);
    }

    [Fact]
    public async Task FindByCpf_WhenPatientExists_Returns200()
    {
        await _client.PostAsJsonAsync("/api/patients", ValidPayload());

        var response = await _client.GetAsync("/api/patients?cpf=52998224725");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task FindByCpf_WhenPatientDoesNotExist_Returns404()
    {
        var response = await _client.GetAsync("/api/patients?cpf=11122233396");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    private record PatientResponse(Guid Id, string Name, string Cpf);
}
