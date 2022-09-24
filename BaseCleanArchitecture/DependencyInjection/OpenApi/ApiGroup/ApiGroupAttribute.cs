namespace BaseCleanArchitecture.DependencyInjection.OpenApi.ApiGroup;

public abstract class ApiGroupAttribute : Attribute 
{
    protected ApiGroupAttribute(params ApiGroupNames[] names)
    {
        GroupNames = names;
    }
    public ApiGroupNames[] GroupNames { get; set; }
}

