namespace EmailSenderApi;

public class EmailDto
{
    public string From { get; set; } = string.Empty;
    public string To { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}
