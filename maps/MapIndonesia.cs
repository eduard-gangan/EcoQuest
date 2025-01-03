using Spectre.Console;

namespace EcoQuest;

public static class MapIndonesia
{

    static char[,] map = new char[,]
{
        { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
        { ' ', ' ', ' ', ' ', ' ', ' ', 'D', 'O', 'C', 'K', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
        { ' ', ' ', ' ', ' ', ' ', ' ', '#', '#', '#', '#', '#', ' ', ' ', ' ', ' ', ' ', 'N', ' ', ' '},
        { ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', '#', ' ', ' ', ' ', ' ', 'W', '+', 'E', ' '},
        { ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', 'S', ' ', ' '},
        { ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
        { ' ', ' ', ' ', ' ', ' ', ' ', '#', '#', '#', '#', '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
        { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
        { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
        { ' ', ' ', ' ', ' ', ' ', ' ', '#', '#', '#', '#', '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
        { ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
        { ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
        { ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
        { ' ', ' ', ' ', ' ', ' ', ' ', '#', '#', '#', '#', '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
        { ' ', ' ', ' ', ' ', ' ', ' ', 'P', 'O', 'R', 'T', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},

};
static char[,] submarine = new char[,]
{
        { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
        { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
        { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
        { ' ', ' ', ' ', ' ', ' ', ' ', ' ', 'S', 'U', 'B', 'M', 'A', 'R', 'I', 'N', 'E', ' ', ' ', ' '},
        { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
        { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
        { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '_', '_', ' ', ' ', '|', '_', '_', ' ', ' ', ' ', ' '},
        { ' ', ' ', ' ', ' ', ' ', ' ', '_', '_', 'L', ' ', 'L', '_', '|', 'L', ' ', 'L', '_', '_', ' '},
        { '.', '.', '.', '[', '+', '(', '_', '_', '_', '_', '_', '_', '_', '_', '_', '_', '_', '_', ')'},
        { ' ', ' ', ' ', ' ', ' ', ' ', ' ', 'C', '_', '_', '_', '_', '_', '_', '_', '_', '_', '/', ' '},
        { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
        { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
        { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
        { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
        { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},

};


    public static void ShowMap(Room? currentRoom)
    {
        if(currentRoom.RoomName.Contains("Sumbarine"))
        {
            Console.WriteLine("\n|-------Sea--------|");

        for (int y = 0; y < submarine.GetLength(0); y++)
        {
            for (int x = 0; x < submarine.GetLength(1); x++)
            {
                    Console.Write(submarine[y, x]);
            }
            Console.WriteLine();
        }

        Console.WriteLine("|------------------|\n\n");
        }

        else
        {
            if (currentRoom.RoomName.Contains("Port"))
            {
                map[11, 8] = '*';
             }
            else
            {
                map[4, 8] = '*';
            }

            Console.WriteLine("\n|-------Map--------|");

            for (int y = 0; y < map.GetLength(0); y++)
            {
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    if (map[y, x] == '*')
                        AnsiConsole.Markup($"[rapidblink red]{map[y, x]}[/]");
                    else
                        Console.Write(map[y, x]);
                }
                Console.WriteLine();
            }

            Console.WriteLine("|------------------|\n\n");

            map[11, 8] = ' ';
            map[4, 8] = ' ';
            
        }

    }

    
}
