namespace Lab.Models;

public class ErrorViewModel
{
    public string? RequestId { get; set; }

    public int Code { get; set; }

    public string? Message { get; set; }

    public string Title { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}
