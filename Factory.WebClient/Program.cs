using Factory.Application.Commands.Contract;
using Factory.Application.Commands.Employee;
using Factory.Application.Commands.Organization;
using Factory.Application.Commands.Payment;
using Factory.Application.Queries.Contract;
using Factory.Application.Queries.Department;
using Factory.Application.Queries.Employee;
using Factory.Application.Queries.Organization;
using Factory.Application.Queries.Payment;
using Factory.Application.Validators.Contract;
using Factory.Application.Validators.Employee;
using Factory.Application.Validators.Organization;
using Factory.Application.Validators.Payment;
using Factory.Domain.Entities;

namespace Factory.WebClient;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        
        builder.Services.AddContracts();
        builder.Services.AddEmployees();
        builder.Services.AddOrganizations();
        builder.Services.AddPayments();
        builder.Services.AddDepartments();
        
        builder.Services.AddValidation();
        
        builder.Services.AddHttpContextAccessor();
        
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        
        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Employees}/{action=Index}/{id?}");

        app.Run();
    }
}
public static class ServiceCollectionExtensions {
    public static void AddContracts(this IServiceCollection services)
    {
        services.AddTransient<AddContractCommand>();
        services.AddTransient<UpdateContractCommand>();
        services.AddTransient<DeleteContractCommand>();
        services.AddTransient<ContractQuery>();
        services.AddTransient<ContractSingleQuery>();
        
        services.AddTransient<ContractDtoValidator>();
        services.AddTransient<UpdateContractDtoValidator>();
    }

    public static void AddOrganizations(this IServiceCollection services)
    {
        services.AddTransient<AddOrganizationCommand>();
        services.AddTransient<UpdateOrganizationCommand>();
        services.AddTransient<DeleteOrganizationCommand>();
        services.AddTransient<OrganizationQuery>();
        
        services.AddTransient<OrganizationDtoValidator>();
        services.AddTransient<UpdateOrganizationDtoValidator>();
    }

    public static void AddEmployees(this IServiceCollection services)
    {
        services.AddTransient<AddEmployeeCommand>();
        services.AddTransient<UpdateEmployeeCommand>();
        services.AddTransient<DeleteEmployeeCommand>();
        services.AddTransient<EmployeeQuery>();
        services.AddTransient<EmployeeSingleQuery>();
        
        services.AddTransient<EmployeeDtoValidator>();
        services.AddTransient<UpdateEmployeeDtoValidator>();
    }

    public static void AddPayments(this IServiceCollection services)
    {
        services.AddTransient<AddPaymentCommand>();
        services.AddTransient<UpdatePaymentCommand>();
        services.AddTransient<DeletePaymentCommand>();
        services.AddTransient<PaymentQuery>();
        services.AddTransient<PaymentSingleQuery>();
        
        services.AddTransient<PaymentDtoValidator>();
        services.AddTransient<UpdatePaymentDtoValidator>();
    }

    public static void AddDepartments(this IServiceCollection services)
    {
        services.AddTransient<DepartmentQuery>();
    }
}