namespace CRUDContacts.Core;
public interface IMatchable
{
    bool MatchesQuery(string query);
}