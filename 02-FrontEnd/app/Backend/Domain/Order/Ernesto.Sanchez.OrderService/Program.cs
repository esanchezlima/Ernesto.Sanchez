using Ernesto.Sanchez.OrderService.Application.Extensions;
using Ernesto.Sanchez.OrderService.Infrastructure.Extensions;
using Ernesto.Sanchez.OrderService.Infrastructure.Http.EndpointHandlers;
using Ernesto.Sanchez.OrderService.Infrastructure.Http.Extensions;
using Ernesto.Sanchez.OrderService.Infrastructure.Persistence.Extensions;
using Ernesto.Sanchez.OrderService.Infrastructure.Persistence.Extension;
using Ernesto.Sanchez.OrderService.Domain.Extensions;

var builder = WebApplication.CreateBuilder(args);
string connectionString = builder.Configuration.GetConnectionString("OrderConnectionString");


builder.Services.AddApplication();
builder.Services.AddDomain();
builder.Services.AddInfrastructure(opt =>opt.ConnectionString = connectionString) ;
var app = builder.Build();
//Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}
app.UseAuthentication();
app.UseAuthorization();
//app.UseHttpsRedirection();
app.RegisterEndpoints();
app.UseSeedData();
app.Run();
 