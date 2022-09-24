using Microsoft.AspNetCore.Identity;
using Sample.Domain.Entities;
using Sample.Persistence.Context;
using TaskEntity = Sample.Domain.Entities.Task;
using Task = System.Threading.Tasks.Task;

namespace Sample.API.DataSeed;

public static class DataSeed
{
    public static async Task Seed(SampleDbContext context, IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
        await SeedUser(userManager);
        await SeedTask(context);
    }

    private static async Task SeedUser(UserManager<User> userManager)
    {
        if (userManager.Users.Any())
        {
            return;
        }

        await userManager.CreateAsync(new User("Hussein", "HusseinnHM", "husseinnhm7@gmail.com"),"1234");
    } 
    
    private static async Task SeedTask(SampleDbContext context)
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