namespace CRUDContacts.Core;
public abstract class PersonRecord
{
    public string Name {get; set;}

    public PersonRecord(string name)
    {
        Name = name;
    }
}