using System.ComponentModel.DataAnnotations;

namespace MessageQDemo.Models;

public class UserMessage
{
  [Required]
  [EmailAddress]
  public string From { get; set; } = string.Empty;

  [Required]
  [EmailAddress]
  public string To { get; set; } = string.Empty;

  [Required]
  public string Message { get; set; } = string.Empty;
}
