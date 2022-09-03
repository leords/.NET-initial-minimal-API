using MiniAPI.Data;
using MiniAPI.Models;
using MiniAPI.ViewModels;

var builder = WebApplication.CreateBuilder(args);

//AddDbContext -> garante que teremos apenas uma conexão por requisição.
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();


app.MapGet("v1/todos", (AppDbContext context) =>
    {
        var todo = context.Todos.ToList();
        return Results.Ok(todo);
    }
).Produces<Todo>(); //Produces libera a produção no swagger, passando o Schema de <Todo>

//obs: é possivel ter rotas iguais, porém com verbos diferentes!
//obs: função anonima pós a virgula \/, lembra arrow function
app.MapPost("v1/todos", (
    AppDbContext context,
    CreateTodoViewModel model
    ) =>
{

    var todo = model.MapTo();
    if (!model.IsValid)
        return Results.BadRequest(model.Notifications);

    context.Todos.Add(todo);
    context.SaveChanges();

    return Results.Created($"/v1/todos;{todo.Id}", todo);

});

app.Run();
