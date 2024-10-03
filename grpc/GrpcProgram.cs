using grpc.Services;
using Microsoft.AspNetCore.Builder;

public class GrpcProgram
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		
		builder.Services.AddGrpc().AddJsonTranscoding();

		var app = builder.Build();


		app.MapGrpcService<GreeterService>();
		app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

		app.Run();
	}

	//************** Middleware **************
	public void ConfigureServices(IServiceCollection services)
	{
		services.AddGrpc();
		services.AddControllers();
	}

	public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
	{
		if (env.IsDevelopment())
			app.UseDeveloperExceptionPage();

		app.UseRouting();
		app.UseGrpcWeb(new GrpcWebOptions { DefaultEnabled = true });

		// configuration pour activer grpc-web

		app.UseEndpoints(endpoints =>
		{
			endpoints.MapControllers();
			endpoints.MapGrpcService<GreeterService>().EnableGrpcWeb();

		});
	}
}
