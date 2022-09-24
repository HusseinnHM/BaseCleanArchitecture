using BaseCleanArchitecture.Domain.Exceptions;
using BaseCleanArchitecture.Domain.Exceptions.ValidationError;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BaseCleanArchitecture.Application.OperationResponse;

public static class OperationResponseExtensions
{
    public static JsonResult ToJsonResult(this OperationResponse operationResponse) =>
        operationResponse switch
        {
            { IsSuccess: true } =>
                new JsonResult(operationResponse) { StatusCode = (int)HttpStatusCode.Ok200 },
            { Error: not null } =>
                new JsonResult(operationResponse.Error.Message)
                    { StatusCode = (int)operationResponse.Error.StatusCode },
            { ValidationErrors.Count: > 0 } =>
                new JsonResult(operationResponse.ValidationErrors)
                    { StatusCode = (int)HttpStatusCode.Failed400 },
            _ => throw new ArgumentOutOfRangeException()
        };

    public static JsonResult ToJsonResult<TResponse>(this OperationResponse<TResponse> operationResponse)
    {
        return operationResponse switch
        {
            { IsSuccess: true } =>
                new JsonResult(operationResponse.Response) { StatusCode = (int)HttpStatusCode.Ok200 },
            { Error: not null } =>
                new JsonResult(operationResponse.Error.Message)
                    { StatusCode = (int)operationResponse.Error.StatusCode },
            { ValidationErrors.Count: > 0 } =>
                new JsonResult(operationResponse.ValidationErrors)
                    { StatusCode = (int)HttpStatusCode.Failed400 },
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public static async Task<JsonResult> ToJsonResultAsync<TResponse>(this Task<OperationResponse<TResponse>> operationResponseTask) =>
        (await operationResponseTask).ToJsonResult();

    public static async Task<JsonResult> ToJsonResultAsync(this Task<OperationResponse> operationResponseTask) =>
        (await operationResponseTask).ToJsonResult();

    public static OperationResponse ToOperationResponse(this IdentityResult identityResult)
    {
        var operationResponse = new OperationResponse();
        operationResponse.ValidationErrors.AddRange(
            identityResult.Errors.Select(e => new ValidationError(e.Code, e.Description)));
        return operationResponse;
    }

    public static OperationResponse<TResponse> ToOperationResponse<TResponse>(this IdentityResult identityResult)
    {
        var operationResponse = new OperationResponse<TResponse>();
        operationResponse.ValidationErrors.AddRange(
            identityResult.Errors.Select(e => new ValidationError(e.Code, e.Description)));
        return operationResponse;
    }

    public static OperationResponse<TResponse> ToOperationResponse<TResponse>(this TResponse response)
    {
        return response;
    }
}