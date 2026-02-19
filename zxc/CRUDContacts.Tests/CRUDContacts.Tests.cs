using CRUDContacts.Entities;
using CRUDContacts.Core;
namespace CRUDContacts.Tests;

public class TestableContactManager : ContactManager
{
    public Contact[] ExposedContacts
    {
        get
        {
            return Contacts;
        }
    }
    public int ExposedCount
    {
        get
        {
            return count;
        }
    }
}

public class ContactManagerTests
{
    private const string TestFileName = "test_contacts.txt";

    [Test]
    public void AddAndRemove()
    {
        var manager = new TestableContactManager();
        manager.AddContact("Andriy", "111");
        manager.AddContact("Bohdan", "222");
        manager.AddContact("Vasyl", "333");

        manager.RemoveContact("222");

        Assert.AreEqual(2, manager.ExposedCount);
        Assert.AreEqual("Vasyl", manager.ExposedContacts[1].Name);
        Assert.IsNull(manager.ExposedContacts[2]);
    }

    [Test]
    public void ResizeTest()
    {
        var manager = new TestableContactManager();
        manager.AddContact("User1", "001");
        manager.AddContact("User2", "002");

        manager.AddContact("User3", "003");

        Assert.AreEqual(3, manager.ExposedCount);
        Assert.IsTrue(manager.ExposedContacts.Length > 2);
        Assert.AreEqual("User3", manager.ExposedContacts[2].Name);
    }

    [Test]
    public void DuplicateTest()
    {
        var manager = new TestableContactManager();
        manager.AddContact("Existing", "12345");

        bool result = manager.AddContact("Duplicate", "12345");

        Assert.IsFalse(result);
        Assert.AreEqual(1, manager.ExposedCount);
    }

    [Test]
    public void SaveAndReadFileTest()
    {
        var managerSource = new TestableContactManager();
        managerSource.AddContact("Saver", "999-99-99");
        managerSource.AddContact("Loader", "888-88-88");

        var managerDest = new TestableContactManager();

        managerSource.SaveToFile(TestFileName);
        managerDest.ReadFile(TestFileName);

        Assert.AreEqual(2, managerDest.ExposedCount);
        Assert.AreEqual("Saver", managerDest.ExposedContacts[0].Name);
        Assert.AreEqual("Loader", managerDest.ExposedContacts[1].Name);
    }

    [Test]
    public void SortByNameTest()
    {
        var manager = new TestableContactManager();

        manager.AddContact("Zara", "111");
        manager.AddContact("Adam", "222");
        manager.AddContact("Ben", "333");

        manager.SortByName();

        Assert.AreEqual("Adam", manager.ExposedContacts[0].Name);
        Assert.AreEqual("Ben", manager.ExposedContacts[1].Name);
        Assert.AreEqual("Zara", manager.ExposedContacts[2].Name);
    }
}