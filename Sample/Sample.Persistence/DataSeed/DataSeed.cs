using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Sample.Domain.Entities;
using Sample.Persistence.Context;

namespace Sample.Persistence.DataSeed;

public static class DataSeed
{
    public static async TaskThread Seed(SampleDbContext context, IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
        await SeedUser(userManager);
        await SeedTask(context);
    }

    private static async TaskThread SeedUser(UserManager<User> userManager)
    {
        if (userManager.Users.Any())
        {
            return;
        }

        await userManager.CreateAsync(new User("Hussein", "HusseinnHM", "husseinnhm7@gmail.com"),"1234");
    } 
    
    private static async TaskThread SeedTask(SampleDbContext context)
    {
        if (context.Tasks.Any())
        {
            return;
        }

        var task = new TaskEntity("test task", 10, context.Users.First().Id);
        task.AddToDo("test todo");
        context.Tasks.Add(task);
        await context.SaveChangesAsync();
    }

}