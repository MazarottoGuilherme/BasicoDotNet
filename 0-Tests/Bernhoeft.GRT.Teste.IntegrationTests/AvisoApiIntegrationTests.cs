using System.Text;
using System.Text.Json;
using Bernhoeft.GRT.Teste.Application.Responses.Commands.v1;
using Bernhoeft.GRT.Teste.Application.Responses.Queries;
using Bernhoeft.GRT.Teste.Application.Responses.Queries.v1;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Bernhoeft.GRT.Teste.IntegrationTests;

public class AvisoApiIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public class OperationResultWrapper<T>
    {
        public T Dados { get; set; }
    }

    public AvisoApiIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    // [Fact]
    // public async Task CreateAviso_Endpoint_Deve_Criar_Aviso()
    // {
    //     var request = new
    //     {
    //         Titulo = "Teste API",
    //         Mensagem = "Mensagem API"
    //     };
    //
    //     var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
    //
    //     var response = await _client.PostAsync("/api/v1/avisos", content);
    //
    //     response.EnsureSuccessStatusCode();
    //
    // var a = await response.Content.ReadAsStringAsync();
    //     var responseWrapper = JsonSerializer.Deserialize<OperationResultWrapper<CreateAvisoResponse>>(
    //         await response.Content.ReadAsStringAsync(),
    //         new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
    //     );
    //
    //     var responseDto = responseWrapper.Dados;
    //
    //     Assert.True(responseDto.Sucesso);
    //     Assert.Equal("Teste API", responseDto.Titulo);
    // }
    //


    private StringContent ToJson(object obj) =>
        new StringContent(JsonSerializer.Serialize(obj), Encoding.UTF8, "application/json");

    private async Task<CreateAvisoResponse> CreateAvisoHelper(object request)
    {
        var content = ToJson(request);
        var response = await _client.PostAsync("/api/v1/avisos", ToJson(request));

        response.EnsureSuccessStatusCode();

        var dto = JsonSerializer.Deserialize<OperationResultWrapper<CreateAvisoResponse>>(
            await response.Content.ReadAsStringAsync(),
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        ).Dados;

        return dto;
    }

    [Fact]
    public async Task CreateAviso_Deve_Criar_Aviso()
    {
        var titulo = "Teste API_" + Guid.NewGuid();
        var request = new { Titulo = titulo, Mensagem = "Mensagem API" };

        var dto = await CreateAvisoHelper(request);

        Assert.True(dto.Sucesso);
        Assert.Equal(titulo, dto.Titulo);
    }

    [Fact]
    public async Task UpdateAviso_Deve_Atualizar_Mensagem()
    {
        var titulo = "Original_" + Guid.NewGuid();
        var createRequest = new { Titulo = titulo, Mensagem = "Mensagem Original" };

        var createdDto=  await CreateAvisoHelper(createRequest);

        var newMessage = "Mensagem Editada";
        var editResponse = await _client.PutAsync($"/api/v1/avisos/{createdDto.Id}", ToJson(newMessage));

        editResponse.EnsureSuccessStatusCode();

        var editedDto = JsonSerializer.Deserialize<OperationResultWrapper<EditarAvisoResponse>>(
            await editResponse.Content.ReadAsStringAsync(),
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        ).Dados;

        Assert.Equal(newMessage, editedDto.Mensagem);
    }

    [Fact]
    public async Task DeleteAviso_Deve_Desativar_Aviso()
    {
        var titulo = "ParaDeletar_" + Guid.NewGuid();
        var createRequest = new { Titulo = titulo, Mensagem = "Mensagem" };

        var createdDto = await CreateAvisoHelper(createRequest);

        var deleteResponse = await _client.DeleteAsync($"/api/v1/avisos/{createdDto.Id}");

        deleteResponse.EnsureSuccessStatusCode();

        var dto = JsonSerializer.Deserialize<OperationResultWrapper<DeleteAvisoByIdResponse>>(
            await deleteResponse.Content.ReadAsStringAsync(),
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        ).Dados;

        Assert.True(dto.Sucesso);
    }

    [Fact]
    public async Task GetAvisos_Endpoint_Deve_Retornar_Lista()
    {
        var r1 = new { Titulo = "A1_" + Guid.NewGuid(), Mensagem = "M1" };
        var r2 = new { Titulo = "A2_" + Guid.NewGuid(), Mensagem = "M2" };

        await CreateAvisoHelper(r1);
        await CreateAvisoHelper(r2);

        var response = await _client.GetAsync("/api/v1/avisos");

        response.EnsureSuccessStatusCode();

        var list = JsonSerializer.Deserialize<OperationResultWrapper<IEnumerable<GetAvisosResponse>>>(
            await response.Content.ReadAsStringAsync(),
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        ).Dados;

        Assert.True(list.Count() >= 2);
    }

    [Fact]
    public async Task GetAvisosById_Endpoint_Deve_Retornar_Aviso()
    {
        var titulo = "A1_" + Guid.NewGuid();
        var createRequest = new { Titulo = titulo, Mensagem = "M1" };

        var createdDto =  await CreateAvisoHelper(createRequest);

        var response = await _client.GetAsync($"/api/v1/avisos/{createdDto.Id}");

        response.EnsureSuccessStatusCode();

        var dto = JsonSerializer.Deserialize<OperationResultWrapper<GetAvisoByIdResponse>>(
            await response.Content.ReadAsStringAsync(),
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        ).Dados;

        Assert.Equal(createdDto.Id, dto.Id);
        Assert.Equal(titulo, dto.Titulo);
        Assert.Equal("M1", dto.Mensagem);
    }
}