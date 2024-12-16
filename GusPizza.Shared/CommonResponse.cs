namespace GusPizza.Shared;

public class CommonResponse<T>
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }

    public CommonResponse(int statusCode, string message, T data)
    {
        StatusCode = statusCode;
        Message = message;
        Data = data;
    }

    public static CommonResponse<T> commonResponse(int statusCode, string message, T data)
    {
        return new CommonResponse<T>(statusCode, message, data);
    }
}