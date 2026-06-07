namespace Game.Interfaces;

public interface IAuthorizer
{
    public void Grant(string userId, string objectId, string action);

    public void Revoke(string userId, string objectId, string action);

    public bool CheckPermission(string userId, string objectId, string action);
}
