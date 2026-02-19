using System.Collections;
using CRUDContacts.Entities;

namespace CRUDContacts.Comparers;

public class ContactPhoneComparer : IComparer
{
    public int Compare(object x, object y)
    {
        Contact c1 = x as Contact;
        Contact c2 = y as Contact;

        return string.Compare(c1.PhoneNumber, c2.PhoneNumber);
    }
}
