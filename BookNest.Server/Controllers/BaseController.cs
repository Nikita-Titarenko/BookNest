using System.Security.Claims;
using BookNest.Server.Responses;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace BookNest.Server.Controllers
{
    public class BaseController : ControllerBase
    {
        protected static ErrorResponse MapErrors(IReadOnlyList<IError> errors)
        {
            return new ErrorResponse
            {
                Errors = errors.Select(e => new ErrorResponseListItem
                {
                    Message = e.Message,
                    Code = e.Metadata.TryGetValue("Code", out var code) ? (int)code : null,
                    Field = e.Metadata.TryGetValue("Field", out var field) ? (string)field : string.Empty
                }).ToArray()
            };
        }

        // Authorize attribute guarantees value
        protected int GetUserId()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            return int.Parse(userId);
        }
    }
}