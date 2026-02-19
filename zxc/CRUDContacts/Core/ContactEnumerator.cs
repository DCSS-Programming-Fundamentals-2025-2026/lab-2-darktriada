using System.Collections;
using CRUDContacts.Entities;

namespace CRUDContacts.Core;

public class ContactEnumerator : IEnumerator
{
    private Contact[] items;
    private int count;
    private int position = -1;

    public ContactEnumerator(Contact[] items, int count)
    {
        this.items = items;
        this.count = count;
    }

    public bool MoveNext()
    {
        position++;
        return position < count;
    }

    public void Reset()
    {
        position = -1;
    }

    public object Current => items[position];
}
