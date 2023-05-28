using Microsoft.EntityFrameworkCore.Design;

public class ScaffoldingDesignTimeServices : IDesignTimeServices
{
    public void ConfigureDesignTimeServices(IServiceCollection services)
    {
        var options = ReverseEngineerOptions.DbContextAndEntities;
        services.AddHandlebarsScaffolding(options);
    }
}