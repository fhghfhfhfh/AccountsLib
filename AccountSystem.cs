namespace AccountLib;

using AccountLib.Internal;
using System.Text.Json;

public class AccountSystem
{
    private int lastUnavialiableUuid;
    private List<Account>? accounts;
    private string savePath;

    public AccountSystem(string SavePath)
    {
        this.savePath = SavePath;
        lastUnavialiableUuid = 0;
    }
    public Account CreateAccount(string UserName, string UserPassword)
    {
        int givingUUID = lastUnavialiableUuid + 1;
        lastUnavialiableUuid = givingUUID;
        Account account = new(givingUUID, UserName, UserPassword);
        accounts.Add(account);
        return account;
    }
    public Account CreateAccount(string UserName, string FullUserName, string UserPassword)
    {
        int givingUUID = lastUnavialiableUuid + 1;
        lastUnavialiableUuid = givingUUID;
        Account account = new(givingUUID, UserName, FullUserName, UserPassword);
        accounts.Add(account);
        return account;
    }
    public List<Account>? GetAccounts()
    {
        return this.accounts;
    }
    public Account? GetAccountByUUID(int uuid)
    {
        Account account = this.accounts[uuid];
        if (account != null)
        {
            return account;
        }
        else
        {
            return null;
        }
    }
    public Account? GetAccountByUserName(string userName)
    {
        foreach (Account account in this.accounts)
        {
            if (account.GetUserName() == userName)
            {
                return account;
            }
        }
        return null;
    }
    public void DeleteAccount(Account account)
    {
        this.accounts.Remove(account);
    }
    public void DeleteAllAccounts()
    {
        this.accounts.Clear();
    }
    public void SaveToFile()
    {
        if (this.accounts == null) return;

        var dtoList = this.accounts.Select(a => new AccountDto
        {
            Uuid = a.GetUUID(),
            UserName = a.GetUserName(),
            FullUserName = a.GetFullUserName(),
            HashedUserPassword = a.GetHashedUserPassword()
        }).ToList();

        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        string json = JsonSerializer.Serialize(dtoList, options);

        File.WriteAllText(this.savePath, json);
    }
    public void LoadFromFile()
    {
        if (!File.Exists(this.savePath))
        {
            this.accounts = new List<Account>();
            this.lastUnavialiableUuid = 0;
            return;
        }

        string json = File.ReadAllText(this.savePath);

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        List<AccountDto>? dtoList = JsonSerializer.Deserialize<List<AccountDto>>(json, options);

        if (dtoList == null)
        {
            this.accounts = new List<Account>();
            this.lastUnavialiableUuid = 0;
            return;
        }

        this.accounts = new List<Account>();
        int maxUuid = 0;

        foreach (var dto in dtoList)
        {
            var account = new Account(dto.Uuid, dto.UserName, dto.FullUserName, dto.HashedUserPassword);
            this.accounts.Add(account);

            if (dto.Uuid > maxUuid)
                maxUuid = dto.Uuid;
        }

        this.lastUnavialiableUuid = maxUuid;
    }

}