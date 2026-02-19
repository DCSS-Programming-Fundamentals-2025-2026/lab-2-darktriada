using System.Text;
using CRUDContacts;

class Program
{
    public static void Main()
    {
        Console.OutputEncoding = Encoding.Unicode;
        Console.InputEncoding = Encoding.Unicode;
        Menu runner = new Menu();
        runner.Run();
    }
}