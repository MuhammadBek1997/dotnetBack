using FudballManagement.Application.Attributes;
using System.ComponentModel.DataAnnotations;

namespace FudballManagement.Application.DTOs.Commons;
public class ChangePasswordDto
{
    [PhoneNumber]
    public string PhoneNumber { get; set; }
    [Required(ErrorMessage = "Current password is required")]
    public string OldPassword { get; set; }

    [Required(ErrorMessage = "New password is required")]
    [StrongPassword]
    public string NewPassword { get; set; }

    [Required(ErrorMessage = "Password confirmation is required")]
    [Compare("NewPassword", ErrorMessage = "Passwords do not match")]
    public string ConfirmNewPassword { get; set; }
}

