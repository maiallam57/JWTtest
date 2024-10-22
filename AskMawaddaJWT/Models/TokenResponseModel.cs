namespace AskMawaddaJWT.Models;

public class TokenResponseModel
{ 
    public string Token { get; set; } = default!;
    public DateTime ExpiresOn { get; set; }
    public string UserEmail { get; set; } = default!;
    public string UserName { get; set; } = default!;
    public List<string> Roles { get; set; } = default!;
}
