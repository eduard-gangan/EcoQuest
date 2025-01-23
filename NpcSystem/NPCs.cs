namespace EcoQuest;

public static class NPCs
{
    public static readonly NPC Garry = new("Garry", NpcReply.GARRY_GREETING1);
    public static readonly FancyNPC Lanka = new FancyNPC("Mayor Lanka", NpcReply.LANKA_GREETING, 500);
    public static readonly NPC Larry = new("Larry", NpcReply.LARRY_GREETING);

    public static readonly NPC Andrew = new("Andrew", NpcReply.ANDREW_GREETING);

    public static readonly NPC Captain = new("Captain Sylvia", NpcReply.CAPTAIN_GREETING);


}