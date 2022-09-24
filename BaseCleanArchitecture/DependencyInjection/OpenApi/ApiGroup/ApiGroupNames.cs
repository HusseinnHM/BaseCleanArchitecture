namespace BaseCleanArchitecture.DependencyInjection.OpenApi.ApiGroup;

public enum ApiGroupNames
{
    [GroupInfo(title: "All", description: "", version: "v1")]
    All = 0,

    [GroupInfo(title: "Dashboard", description: "", version: "v1")]
    Dashboard = 1,

    [GroupInfo(title: "Mobile", description: "", version: "v1")]
    Mobile = 2,
}