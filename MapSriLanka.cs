namespace EcoQuest;

public static class MapSriLanka
{

    static char[,] map = new char[,]
{
        { ' ', ' ', ' ', ' ', ' ', ' ', 'S', 'T', 'A', 'T', 'I', 'O', 'N', ' ', ' ', ' ', ' ', ' ', ' '},
        { ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#', '#', '#', '#', '#', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
        { ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
        { ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
        { ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
        { ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#', '#', '#', '#', '#', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
        { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
        { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
        { ' ', 'T', 'H', ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', ' ', 'B', 'E', 'A', 'C', 'H'},
        { '#', '#', '#', '#', '#', ' ', ' ', '#', '#', '#', '#', '#', ' ', ' ', '#', '#', '#', '#', '#'},
        { '#', ' ', ' ', ' ', '#', ' ', ' ', '#', ' ', ' ', ' ', '#', ' ', ' ', '#', ' ', ' ', ' ', '#'},
        { '#', ' ', ' ', ' ', '#', '#', '#', '#', ' ', ' ', ' ', '#', '#', '#', '#', ' ', ' ', ' ', '#'},
        { '#', ' ', ' ', ' ', '#', ' ', ' ', '#', ' ', ' ', ' ', '#', ' ', ' ', '#', ' ', ' ', ' ', '#'},
        { '#', '#', '#', '#', '#', ' ', ' ', '#', '#', '#', '#', '#', ' ', ' ', '#', '#', '#', '#', '#'},
        { ' ', ' ', ' ', ' ', ' ', ' ', ' ', 'P', 'O', 'R', 'T', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},

};


    public static void ShowMap(Room? currentRoom)
    {
        if (currentRoom.RoomName.Contains("Port"))
        {
            map[11, 9] = '*';
        }
        else if (currentRoom.RoomName.Contains("Town Hall"))
        {
            map[11, 2] = '*';
        }
        else if (currentRoom.RoomName.Contains("Beach"))
        {
            map[11, 16] = '*';
        }
        else
        {
            map[3, 9] = '*';
        }

        Console.WriteLine("\n|-------Map--------|");

        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                if(map[y, x] == '*')
                    Game.ColorWrite($"{map[y, x]}", ConsoleColor.DarkRed);
                else
                    Console.Write(map[y, x]);
            }
            Console.WriteLine();
        }

        Console.WriteLine("|------------------|\n\n");

        map[11, 9] = ' ';
        map[11, 2] = ' ';
        map[11, 16] = ' ';
        map[3, 9] = ' ';

    }
}