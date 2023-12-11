var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.UseHttpsRedirection();

app.MapPost("/rpc", (JsonRpcRequest request) =>
{
    if (request.Method == "eth_getBlockNumber")
    {
        string mockBlockNumber = "0x" + new Random().Next(1000000, 9999999).ToString("X");

        return Results.Ok(new JsonRpcResponse
        {
            Id = request.Id,
            Result = mockBlockNumber
        });
    }

    return Results.BadRequest("Method not supported");
});


app.Run();

public class JsonRpcRequest
{
    public string Jsonrpc { get; set; } = "2.0";
    public string Method { get; set; }
    public object[] Params { get; set; }
    public int Id { get; set; }
}

public class JsonRpcResponse
{
    public string Jsonrpc { get; set; } = "2.0";
    public string Result { get; set; }
    public int Id { get; set; }
}
