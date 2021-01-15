// This is a static class, can be accessed through anywhere

// Player stats will be global so its easy to access through scenes
public static class PlayerStats
{
    private static int HP = 100;
    private static int MP = 100;
    private static int strength = 5;
    private static int defense = 0;
    private static int speed = 10;
    private static int level = 1;
    private static float stamina = 100;

    private static int currentHP = 100;
    private static int currentMP = 100;
    private static int currentExp = 0;
    private static int nextLevel;
    private static float currentStamina = 100;

    private static bool block = false;
    private static bool hasRifle = false;
    private static bool advantage = false;

    // Get and Set functions
    public static int Health
    {
        get {return HP;}
        set { HP = value;}
    }

    public static int Magic
    {
        get { return MP; }
        set { MP = value; }
    }

    public static int Power
    {
        get { return strength; }
        set { strength = value; }
    }

    public static int Defense
    {
        get { return defense; }
        set { defense = value; }
    }

    public static int Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    public static int CurrentHealth
    {
        get { return currentHP; }
        set { currentHP = value; }
    }

    public static int CurrentMagic
    {
        get { return currentMP; }
        set { currentMP = value; }
    }

    public static bool Blocking
    {
        get { return block; }
        set { block = value; }
    }

    public static bool HasRifle
    {
        get { return hasRifle; }
        set { hasRifle = value; }
    }

    public static int Level
    {
        get { return level; }
        set { level = value; }
    }

    public static int CurrentExp
    {
        get { return currentExp; }
        set { currentExp = value; }
    }

    public static int NextLevel
    {
        get { return level * 10; }
        set { nextLevel = value; }
    }

    public static float Stamina
    {
        get { return stamina; }
        set { stamina = value; }
    }
    public static float CurrentStamina
    {
        get { return currentStamina; }
        set { currentStamina = value; }
    }

    public static bool HasAdvantage
    {
        get { return advantage; }
        set { advantage = value; }
    }
}
