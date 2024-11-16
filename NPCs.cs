namespace EcoQuest;

public static class NPCs
{
    public static readonly NPC Garry = new("Garry", NpcReply.GARRY_GREETING);
    public static readonly FancyNPC Lanka = new FancyNPC("Mayor Lanka", NpcReply.LANKA_GREETING, 1);
    public static readonly NPC Larry = new("Larry", NpcReply.LARRY_GREETING);


}