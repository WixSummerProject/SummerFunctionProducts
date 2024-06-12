using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Data.Contexts;

public class BloggingContextFactory : IDesignTimeDbContextFactory<ProductContext>
{
    public ProductContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ProductContext>();
        optionsBuilder.UseSqlServer("Data Source=wixsummerproject.database.windows.net;Initial Catalog=wix_summer_project;Persist Security Info=True;User ID=wixsummerproject;Password=Bytmig123!;Trust Server Certificate=True");

        return new ProductContext(optionsBuilder.Options);
    }
}