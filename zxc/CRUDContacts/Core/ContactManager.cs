using CRUDContacts.Entities;
using System.Collections;
using CRUDContacts.Comparers;

namespace CRUDContacts.Core;

public class ContactManager
{
    protected Contact[] Contacts = new Contact[2];
    protected int count = 0;

    public bool AddContact(string name, string phoneNumber, DateTime? date = null)
    {
        if (ContactExists(phoneNumber))
        {
            Console.WriteLine("Контакт вже існує.");
            return false;
        }

        if (count == Contacts.Length)
        {
            Resize();
        }

        Contact contact = new Contact(name, phoneNumber);

        if (date != null)
        {
            contact.CreateTime = date.Value;
        }

        Contacts[count] = contact;
        count++;

        return true;
    }

    public void Search(string query)
    {
        bool found = false;

        for (int i = 0; i < count; i++)
        {
            if (Contacts[i].MatchesQuery(query))
            {
                Console.WriteLine($"Контакт знайдено:{Contacts[i]}");
                found = true;
            }
        }

        if (!found)
        {
            Console.WriteLine("Контакт не знайдено.");
        }
    }

    public void RemoveContact(string phoneNumber)
    {
        int indexToRemove = -1;

        for (int i = 0; i < count; i++)
        {
            if (Contacts[i].PhoneNumber == phoneNumber)
            {
                indexToRemove = i;
                break;
            }
        }

        if (indexToRemove == -1)
        {
            Console.WriteLine("Контакт не знайдено.");
            return;
        }

        for (int i = indexToRemove; i < count - 1; i++)
        {
            Contacts[i] = Contacts[i + 1];
        }

        count--;
        Contacts[count] = null;
    }

    public void Print()
    {
        for (int i = 0; i < count; i++)
        {
            Console.WriteLine($"{i + 1}. {Contacts[i]}");
        }
    }

    private bool ContactExists(string phoneNumber)
    {
        for (int i = 0; i < count; i++)
        {
            if (Contacts[i].PhoneNumber == phoneNumber)
            {
                return true;
            }
        }

        return false;
    }

    public void SortByName()
    {
        for (int i = 0; i < count - 1; i++)
        {
            for (int j = 0; j < count - i - 1; j++)
            {
                if (string.Compare(Contacts[j].Name, Contacts[j + 1].Name) > 0)
                {
                    Contact temp = Contacts[j];
                    Contacts[j] = Contacts[j + 1];
                    Contacts[j + 1] = temp;
                }
            }
        }

        Console.WriteLine("Відсортовано");
    }

    public void SaveToFile(string file)
    {
        using (StreamWriter writer = new StreamWriter(file))
        {
            for (int i = 0; i < count; i++)
            {
                writer.WriteLine(Contacts[i].Name + "-" +
                                 Contacts[i].PhoneNumber + " " +
                                 Contacts[i].CreateTime);
            }
        }
    }

    public void ReadFile(string fileName)
    {
        using (StreamReader reader = new StreamReader(fileName))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                try
                {
                    string[] first = line.Split('-', 2);

                    if (first.Length >= 2)
                    {
                        string name = first[0];
                        string rest = first[1];

                        string[] second = rest.Split(' ', 2);

                        if (second.Length >= 2)
                        {
                            string phone = second[0];
                            string dateString = second[1];

                            DateTime date = DateTime.Parse(dateString);
                            AddContact(name, phone, date);
                        }
                    }
                }
                catch
                {
                }
            }
        }
    }

    private void Resize()
    {
        int newSize = Contacts.Length * 2;
        Contact[] newArray = new Contact[newSize];

        for (int i = 0; i < count; i++)
        {
            newArray[i] = Contacts[i];
        }

        Contacts = newArray;
    }

    private ContactCollection? swapCollection = null;

    public ContactCollection GetSwapCollection()
    {
        if (swapCollection == null)
        {
            swapCollection = new ContactCollection();

            for (int i = 0; i < count; i++)
            {
                swapCollection.Add(Contacts[i]);
            }
        }

        return swapCollection;
    }

    public void SortDefaultSwap()
    {
        var col = GetSwapCollection();
        col.SortDefault();
        Console.WriteLine("Swap: Відсортовано за ім'ям (IComparable)");
    }

    public void SortPhoneSwap()
    {
        var col = GetSwapCollection();
        col.SortWithComparer(new ContactPhoneComparer());
        Console.WriteLine("Swap: Відсортовано за телефоном (IComparer)");
    }

    public void StatsSwap()
    {
        Console.WriteLine($"Кількість контактів: {count}");

        if (count == 0)
            return;

        DateTime min = Contacts[0].CreateTime;
        DateTime max = Contacts[0].CreateTime;

        for (int i = 1; i < count; i++)
        {
            if (Contacts[i].CreateTime < min)
                min = Contacts[i].CreateTime;

            if (Contacts[i].CreateTime > max)
                max = Contacts[i].CreateTime;
        }

        Console.WriteLine($"Найстаріший контакт: {min}");
        Console.WriteLine($"Найновіший контакт: {max}");
    }
}
