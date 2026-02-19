using System.Collections;
using CRUDContacts.Entities;

namespace CRUDContacts.Core;

public class ContactCollection : IEnumerable
{
    private Contact[] items = new Contact[2];
    private int count = 0;

    public int Count => count;

    public void Add(Contact contact)
    {
        if (count == items.Length)
            Resize();

        items[count++] = contact;
    }

    public void RemoveAt(int index)
    {
        if (index < 0 || index >= count)
            return;

        for (int i = index; i < count - 1; i++)
            items[i] = items[i + 1];

        items[--count] = null;
    }

    public Contact GetAt(int index)
    {
        if (index < 0 || index >= count)
            return null;

        return items[index];
    }

    public void SetAt(int index, Contact contact)
    {
        if (index < 0 || index >= count)
            return;

        items[index] = contact;
    }

    private void Resize()
    {
        Contact[] newArr = new Contact[items.Length * 2];

        for (int i = 0; i < count; i++)
            newArr[i] = items[i];

        items = newArr;
    }

    public IEnumerator GetEnumerator()
    {
        return new ContactEnumerator(items, count);
    }

    public void SortDefault()
    {
        Array.Sort(items, 0, count);
    }

    public void SortWithComparer(IComparer comparer)
    {
        Array.Sort(items, 0, count, comparer);
    }
}
