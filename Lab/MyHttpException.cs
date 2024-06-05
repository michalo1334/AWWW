namespace Lab;

[Serializable]
internal class MyHttpException : Exception
{
    public int Code { get; set; }

    public MyHttpException(int code, string? message = null, Exception? innerException = null) : base(message, innerException)
    {
        Code = code;
    }
}