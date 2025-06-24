using Microsoft.EntityFrameworkCore;
using SME_API_HR.Entities;
using SME_API_HR.Repository;
using SME_API_HR.Services;


namespace SME_API_HR
{
    #region swaggwer 3.0
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // ✅ Register Database Context
            builder.Services.AddDbContext<HRAPIDBContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            // ✅ Add NSwag Swagger 2.0
            builder.Services.AddOpenApiDocument(config =>
            {
                config.DocumentName = "API_SME_HR_V1";
                config.Title = "API SME HR";
                config.Version = "v1";
                config.Description = "API documentation using Swagger 2.0";
                config.SchemaType = NJsonSchema.SchemaType.Swagger2;
            });


            builder.Services.AddScoped<IJobService, JobService>();
            builder.Services.AddScoped<IJobTitleRepository, JobTitleRepository>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<ITEmployeeProfileRepository, TEmployeeProfileRepository>();
            builder.Services.AddScoped<ITEmployeeProfileService, TEmployeeProfileService>();
            builder.Services.AddScoped<IMPositionRepository, MPositionRepository>();
            builder.Services.AddScoped<IMPositionService, MPositionService>();
            builder.Services.AddScoped<IMBusinessUnitRepository, MBusinessUnitRepository>();
            builder.Services.AddScoped<IBusinessUnitService, BusinessUnitService>();
            builder.Services.AddScoped<IMJobLevelRepository, MJobLevelRepository>();
            builder.Services.AddScoped<IMJobLevelService, MJobLevelService>();
            builder.Services.AddScoped<IApiInformationRepository, ApiInformationRepository>();
            builder.Services.AddScoped<ITEmployeeMovementRepository, TEmployeeMovementRepository>();
            builder.Services.AddScoped<ITEmployeeMovementService, TEmployeeMovementService>();
            builder.Services.AddScoped<ITOrganizationTreeRepository, TOrganizationTreeRepository>();
            builder.Services.AddScoped<ITOrganizationTreeService, TOrganizationTreeService>();
            builder.Services.AddScoped<IMEmployeeByIdRepository, MEmployeeByIdRepository>();
            builder.Services.AddScoped<IMEmployeeByIdService, MEmployeeByIdService>();
            builder.Services.AddScoped<ITEmployeeContractRepository, TEmployeeContractRepository>();
            builder.Services.AddScoped<ITEmployeeContractService, TEmployeeContractService>();


            builder.Services.AddScoped<ICallAPIService, CallAPIService>();
            builder.Services.AddHttpClient<CallAPIService>();
            builder.Services.AddSingleton<CallAPIService>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment()
                || app.Environment.IsProduction()
                )
            {
                app.UseOpenApi();     // ✅ Serve Swagger 2.0 JSON
                app.UseSwaggerUi3();  // ✅ Use Swagger UI 3
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
    #endregion swaggwer 3.0
}
