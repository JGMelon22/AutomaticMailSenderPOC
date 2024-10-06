using System.ComponentModel.DataAnnotations;

namespace AutomaticMailSenderPOC.DTOs;

public record BasicEmailRequest(

    [Required(ErrorMessage = "An recipient name must be informed!")]
    string RecipientName,

    [Required(ErrorMessage = "An e-mail must be informed")]
    [EmailAddress(ErrorMessage = "Invalid e-mail priveded!")]
    string Email,

    [Required(ErrorMessage = "A subject must be set!")]
    string Subject,

    [Required(ErrorMessage = "A subject must be set!")]
    string body
);
