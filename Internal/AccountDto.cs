namespace AccountLib.Internal;

public class AccountDto
{
    public int Uuid { get; set; }
    public string UserName { get; set; }
    public string FullUserName { get; set; }
    public string HashedUserPassword { get; set; }
}
