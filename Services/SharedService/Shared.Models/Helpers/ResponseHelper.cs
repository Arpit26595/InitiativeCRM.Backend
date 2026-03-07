using System.Net;

namespace Shared.Models.Helpers
{
    public class ResponseHelper
    {
        public static ResponseModel CreateSuccessResponse(object payload = null, string id = null, string message = null)
        {
            return new ResponseModel()
            {
                data = payload ?? "",
                message = message,
                status = "success",
                statuscode = (int)HttpStatusCode.OK
            };
        }

        public static ResponseModel CreateErrorResponse(HttpStatusCode statusCode, Exception exception)
        {
            return new ResponseModel()
            {
                data = null,
                message = exception.Message,
                status = "error",
                statuscode = GetStatusCode(exception)
            };
        }

        private static int GetStatusCode(Exception exception)
        {
            switch (exception.GetType().Name)
            {
                case nameof(UnauthorizedAccessException):
                    return (int)HttpStatusCode.Unauthorized;
                case nameof(ArgumentException):
                    return (int)HttpStatusCode.BadRequest;
                default:
                    return (int)HttpStatusCode.InternalServerError;
            }
        }
    }
    public class ResponseModel
    {
        public int statuscode { get; set; }
#pragma warning disable CS8618 // Non-nullable property 'status' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
        public string status { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'status' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning disable CS8618 // Non-nullable property 'data' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
        public object data { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'data' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning disable CS8618 // Non-nullable property 'message' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
        public string message { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'message' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
    }

}
