using CRUDContacts.Core;
namespace CRUDContacts.Entities;

public class Contact : PersonRecord, IMatchable
{
    public DateTime CreateTime { get; set; }
    public string PhoneNumber { get; set; }
    public Contact(string name, string phoneNumber) : base(name)
    {
        PhoneNumber = phoneNumber;
        CreateTime = DateTime.Now;
    }

    public bool MatchesQuery(string query)
    {
        if (query.Contains(PhoneNumber) || query.Contains(Name))
        {
            return true;
        }

        return false;
    }

    public override string ToString()
    {
        return $"Ім'я: {Name}, Телефон: {PhoneNumber}, Дата: {CreateTime}";
    }
}