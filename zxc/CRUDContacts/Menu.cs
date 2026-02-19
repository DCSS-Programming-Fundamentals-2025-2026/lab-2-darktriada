using CRUDContacts.Core;
namespace CRUDContacts;

public class Menu
{
    private ContactManager manager = new ContactManager();

    public void Run()
    {
        while (true)
        {
            Console.WriteLine("1. Додати контакт");
            Console.WriteLine("2. Знайти контакт");
            Console.WriteLine("3. Показати всі контакти");
            Console.WriteLine("4. Видалити контакт");
            Console.WriteLine("5. Сортувати за ім'ям");
            Console.WriteLine("6. Зберегти у файл");
            Console.WriteLine("7. Зчитати з файла");
            Console.WriteLine("0. Вихід");

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                AddContactUI();
            }
            else if (choice == "2")
            {
                SearchUI();
            }
            else if (choice == "3")
            {
                manager.Print();
            }
            else if (choice == "4")
            {
                RemoveContactUI();
            }
            else if (choice == "5")
            {
                manager.SortByName();
            }
            else if (choice == "6")
            {
                SaveToFileUI();
            }
            else if (choice == "7")
            {
                ReadFileUI();
            }
            else if (choice == "0")
            {
                break;
            }
        }
    }

    private void AddContactUI()
    {
        Console.Write("Введіть ім'я: ");
        string name = Console.ReadLine();
        Console.Write("Введіть телефон: ");
        string phone = Console.ReadLine();

        bool isAded = manager.AddContact(name, phone);
        if (isAded)
        {
            Console.WriteLine("Контакт успішно додано");
        }
        else
        {
            Console.WriteLine("Помилка: Контакт з таким номером вже існує.");
        }
    }

    private void SearchUI()
    {
        Console.Write("Введіть текст для пошуку: ");
        string query = Console.ReadLine();
        manager.Search(query);
    }

    private void RemoveContactUI()
    {
        Console.Write("Введіть номер телефону для видалення: ");
        string phone = Console.ReadLine();
        manager.RemoveContact(phone);
    }

    private void SaveToFileUI()
    {
        Console.Write("Введіть назву файлу (наприклад contacts.txt): ");
        string fileName = Console.ReadLine();

        try
        {
            manager.SaveToFile(fileName);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка при збереженні: {ex.Message}");
        }
    }

    private void ReadFileUI()
    {
        Console.Write("Введіть назву файлу .txt в папці: ");
        string fileName = Console.ReadLine();

        try
        {
            manager.ReadFile(fileName);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка при зчитуванні: {ex.Message}");
        }
    }
}