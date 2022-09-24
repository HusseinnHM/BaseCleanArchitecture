namespace BaseCleanArchitecture.DependencyInjection.OpenApi.ApiGroup;

public sealed class GroupInfoAttribute : Attribute
{
    public GroupInfoAttribute(string title, string description,string version)
    {
        Title = title;
        Version = version;
        Description = description;
    }
        
    public string Title { get; set; }
    public string Version { get; set; }
    public string Description { get; set; }
}