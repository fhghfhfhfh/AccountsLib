namespace AccountLib.Internal;

using BCrypt.Net;


public class Account
{
    private readonly int uuid;
    private readonly string userName;
    private string fullUserName;
    private string hashedUserPassword;

    public Account(int uuid, string UserName, string UserPassword)
    {
        this.uuid = uuid;
        this.userName = UserName;
        this.hashedUserPassword = BCrypt.EnhancedHashPassword(UserPassword);
        this.fullUserName = "";
    }
    public Account(int uuid, string UserName, string FullUserName, string UserPassword)
    {
        this.uuid = uuid;
        this.userName = UserName;
        this.hashedUserPassword = BCrypt.EnhancedHashPassword(UserPassword);
        this.fullUserName = FullUserName;
    }
    public int GetUUID()
    {
        return this.uuid;
    }
    public string GetUserName()
    {
        return this.userName;
    }
    public string GetFullUserName()
    {
        return this.fullUserName;
    }
    public string GetHashedUserPassword()
    {
        return this.hashedUserPassword;
    }
    public void SetFullUserName(string newFullUserName)
    {
        this.fullUserName = newFullUserName;
    }
    public void ChangePassword(string newPassword)
    {
        this.hashedUserPassword = BCrypt.EnhancedHashPassword(newPassword);
    }
    
}