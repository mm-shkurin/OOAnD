using Game.Interfaces;

namespace Game.Models;

public class PrefixTreeAuthorizer : IAuthorizer
{
    private static readonly StringComparer KeyComparer = StringComparer.Ordinal;

    private readonly Dictionary<string, Dictionary<string, HashSet<string>>> _tree =
        new(KeyComparer);

    public void Grant(string userId, string objectId, string action)
    {
        if (!_tree.TryGetValue(userId, out var objects))
        {
            objects = new Dictionary<string, HashSet<string>>(KeyComparer);
            _tree[userId] = objects;
        }

        if (!objects.TryGetValue(objectId, out var actions))
        {
            actions = new HashSet<string>(KeyComparer);
            objects[objectId] = actions;
        }

        actions.Add(action);
    }

    public void Revoke(string userId, string objectId, string action)
    {
        if (!_tree.TryGetValue(userId, out var objects))
            return;

        if (!objects.TryGetValue(objectId, out var actions))
            return;

        actions.Remove(action);

        if (actions.Count == 0)
            objects.Remove(objectId);

        if (objects.Count == 0)
            _tree.Remove(userId);
    }

    public bool CheckPermission(string userId, string objectId, string action)
    {
        return _tree.TryGetValue(userId, out var objects)
            && objects.TryGetValue(objectId, out var actions)
            && actions.Contains(action);
    }
}
