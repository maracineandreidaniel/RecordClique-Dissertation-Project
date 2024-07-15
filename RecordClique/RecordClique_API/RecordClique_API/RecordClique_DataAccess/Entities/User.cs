using System.ComponentModel.DataAnnotations;
using RecordClique.Models;

namespace RecordClique_DataAccess.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? ConfirmedPassword { get; set; }
        public string? Token { get; set; }
        public string? Role { get; set; }
        public string Email {  get; set; }
        public double MoneyBalance { get; set; }
        public string RefreshToken {  get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public string? ResetPasswordToken { get; set; }
        public DateTime? ResetPasswordExpiry { get; set; }
        public ICollection<Album>? Albums { get; set; }
    }
}
