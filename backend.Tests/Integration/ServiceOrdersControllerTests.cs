using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace Backend.Tests.Integration;

public class ServiceOrdersControllerTests : IClassFixture<ApiFactory>, IAsyncLifetime
{
    private readonly HttpClient _client;
    private readonly ApiFactory _factory;

    public ServiceOrdersControllerTests(ApiFactory factory)
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


    private async Task<Guid> CreatePatientAsync(string cpf = "52998224725")
    {
        var response = await _client.PostAsJsonAsync("/api/patients", new
        {
            name = "Maria Silva",
            cpf,
            birthDate = "1990-05-15"
        });
        var body = await response.Content.ReadFromJsonAsync<PatientResponse>();
        return body!.Id;
    }

    private static object OrderPayload(Guid patientId, object[]? items = null) => new
    {
        patientId,
        priority = "Normal",
        items = items ?? new[]
        {
            new { examCode = "HEM", examName = "Hemograma Completo", tubeType = "purple" }
        }
    };


    [Fact]
    public async Task Create_WithValidData_Returns201AndOrder()
    {
        var patientId = await CreatePatientAsync();

        var response = await _client.PostAsJsonAsync("/api/service-orders", OrderPayload(patientId));

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var body = await response.Content.ReadFromJsonAsync<ServiceOrderResponse>();
        Assert.NotNull(body);
        Assert.NotEqual(Guid.Empty, body.Id);
        Assert.Equal(patientId, body.PatientId);
        Assert.Equal("Waiting", body.Status);
        Assert.Equal("Normal", body.Priority);
        Assert.Single(body.Items);
    }

    [Fact]
    public async Task Create_WithUrgentPriority_Returns201WithCorrectPriority()
    {
        var patientId = await CreatePatientAsync();
        var payload = new
        {
            patientId,
            priority = "Urgent",
            items = new[] { new { examCode = "GLI", examName = "Glicose", tubeType = "yellow" } }
        };

        var response = await _client.PostAsJsonAsync("/api/service-orders", payload);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var body = await response.Content.ReadFromJsonAsync<ServiceOrderResponse>();
        Assert.Equal("Urgent", body!.Priority);
    }

    [Fact]
    public async Task Create_WhenPatientDoesNotExist_Returns404()
    {
        var response = await _client.PostAsJsonAsync(
            "/api/service-orders",
            OrderPayload(Guid.NewGuid()));

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Create_WithNoItems_Returns422()
    {
        var patientId = await CreatePatientAsync();

        var response = await _client.PostAsJsonAsync(
            "/api/service-orders",
            OrderPayload(patientId, []));

        Assert.Equal(HttpStatusCode.UnprocessableEntity, response.StatusCode);
    }


    [Fact]
    public async Task List_AfterCreatingOrder_ReturnsItInQueue()
    {
        var patientId = await CreatePatientAsync();
        await _client.PostAsJsonAsync("/api/service-orders", OrderPayload(patientId));

        var response = await _client.GetAsync("/api/service-orders?status=Waiting");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var orders = await response.Content.ReadFromJsonAsync<ServiceOrderResponse[]>();
        Assert.NotEmpty(orders!);
    }


    [Fact]
    public async Task CallNext_WhenQueueIsEmpty_Returns404()
    {
        var response = await _client.PostAsJsonAsync("/api/service-orders/call-next", new { });

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task CallNext_WhenOrderIsWaiting_SetsStatusToInProgress()
    {
        var patientId = await CreatePatientAsync();
        await _client.PostAsJsonAsync("/api/service-orders", OrderPayload(patientId));

        var response = await _client.PostAsJsonAsync("/api/service-orders/call-next", new { });

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var body = await response.Content.ReadFromJsonAsync<ServiceOrderResponse>();
        Assert.Equal("InProgress", body!.Status);
    }


    private record PatientResponse(Guid Id, string Name, string Cpf);

    private record ServiceOrderResponse(
        Guid Id,
        Guid PatientId,
        string Status,
        string Priority,
        ServiceOrderItemResponse[] Items);

    private record ServiceOrderItemResponse(Guid Id, string ExamCode, string TubeType);
}
