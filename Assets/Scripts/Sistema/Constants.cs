
public static class Constants
{
    public static class RandomGeneration
    {
        // chance to connect rooms when that room has or 2 or more corridors already connected
        public const int ChanceAcceptBuild = 5;
        //chance connect rooms when stuck
        public const int ChanceConnectStuck = 50;

        //the adjescent directions and the far directions north, south, east and  west
        public static readonly Punt2d[] CloseDirections = { new Punt2d(0, 1), new Punt2d(0, -1), new Punt2d(1, 0), new Punt2d(-1, 0) };

        public static readonly Punt2d[] LighthouseInteriorLocations = { new Punt2d(-10, 0), new Punt2d(-20, 0), new Punt2d(-30, 0), new Punt2d(-40, 0) };

        //identifiers basic map elements  
        public const int EmptyRoomId = 1;
        public const int InitalRoomId = 2;
        public const int CorridorId = 1111;
        public const int VerticalCorridorId = 3;
        public const int HorizontalCorridorId = 4;
        //identifiers complex map elements
        public const int LighthouseRoomId = 10;
        public const int ExitRoomId = 11;
        public const int BlackMarketRoomId = 12;
        public const int NpcRoomId = 13;
        // identifiers enemy rooms
        public const int StandardEnemyRoomId = 20;
        public const int RedEnemyRoomId = 21;
        public const int BlueEnemyRoomId = 22;
        public const int YellowEnemyRoomId = 23;
        public const int AllEnemyRoomId = 24;
        public const int BossEnemyRoomId = 25;
        // number of enemy rooms by type
        public const int StandardEnemyRoomQuantity = 19;
        public const int RedEnemyRoomQuantity = 12;
        public const int BlueEnemyRoomQuantity = 10;
        public const int YellowEnemyRoomQuantity = 7;
        public const int AllEnemyRoomQuantity = 5;
        public const int BossEnemyRoomQuantity = 1;
    }

    public static class Items
    {
        public static BaseCaracterStats RedHeart = new BaseCaracterStats
        {
            EntityName = "Red Heart",
            EntityDescription = "A common enemy heart",
            RedHearts = 1
        };
        public static BaseCaracterStats BlueHeart = new BaseCaracterStats
        {
            EntityName = "Blue Heart",
            EntityDescription = "An uncommon enemy Heart",
            BlueHearts = 1
        };
        public static BaseCaracterStats YellowHeart = new BaseCaracterStats
        {
            EntityName = "Yellow Heart",
            EntityDescription = "A rare enemy heart",
            YellowHearts = 1
        };
        public static BaseCaracterStats OilBottle = new BaseCaracterStats
        {
            EntityName = "Oil for the guy",
            EntityDescription = "Some oil for you, don't die please",
            Life = 5
        };
        public static BaseCaracterStats RandomOilBottle = new BaseCaracterStats
        {
            EntityName = "Oil for the guy",
            EntityDescription = "Some oil for you, don't die please",
            Life = 10
        };
        public static BaseCaracterStats LilBear = new BaseCaracterStats
        {
            EntityName = "Bear for the crazy guy",
            EntityDescription = "A lil bear",
            OldTools = true
        };
    }

    public static class Protagonists
    {
        public static BaseProtagonistStats Po
        {
            get
            {
                return new BaseProtagonistStats
                {
                    EntityName = "Po",
                    EntityDescription = "Te mola po",
                    Attack = 2,
                    Life = 100,
                    MaxLife = 100,
                    BaseSpeed = 200f,
                    CurrentSpeed = 200f,
                    AttackCadence = 0.6f,
                    RedHearts = 0,
                    BlueHearts = 0,
                    YellowHearts = 0
                };
            }
        }
    }

    public static class Enemies
    {

        private static float _baseSpeed = 5.5f;


        public static EnemyStats StandardSlug
        {
            get
            {
                return new EnemyStats
                {
                    EntityName = "StandardSlug",
                    EntityDescription = "It's a slug",
                    Level = 0,
                    Attack = 2,
                    Life = 3,
                    MaxLife = 3,
                    BaseSpeed = _baseSpeed * 0.6f,
                    AgroSpeed = _baseSpeed * 1.1f,
                    CurrentSpeed = _baseSpeed,
                    Mass = 20
                };
            }
        }

        public static EnemyStats RedSlug
        {
            get
            {
                return new EnemyStats
                {
                    EntityName = "Red StandardSlug",
                    EntityDescription = "It's a red slug",
                    Level = 1,
                    Attack = 3,
                    Life = 8,
                    MaxLife = 8,
                    BaseSpeed = _baseSpeed * 0.7f,
                    AgroSpeed = _baseSpeed * 1.2f,
                    CurrentSpeed = _baseSpeed,
                    Mass = 25
                };
            }
        }

        public static EnemyStats BlueSlug
        {
            get
            {
                return new EnemyStats
                {
                    EntityName = "Blue StandardSlug",
                    EntityDescription = "It's a blue slug",
                    Level = 2,
                    Attack = 4,
                    Life = 12,
                    MaxLife = 12,
                    BaseSpeed = _baseSpeed * 0.8f,
                    AgroSpeed = _baseSpeed * 1.3f,
                    CurrentSpeed = _baseSpeed,
                    Mass = 30
                };
            }
        }

        public static EnemyStats YellowSlug
        {
            get
            {
                return new EnemyStats
                {
                    EntityName = "Yellow StandardSlug",
                    EntityDescription = "It's a yellow slug",
                    Level = 3,
                    Attack = 7,
                    Life = 30,
                    MaxLife = 30,
                    BaseSpeed = _baseSpeed * 1f,
                    AgroSpeed = _baseSpeed * 1.4f,
                    CurrentSpeed = _baseSpeed,
                    Mass = 35
                };
            }
        }

        public static EnemyStats Minotaur
        {
            get
            {
                return new EnemyStats
                {
                    EntityName = "Minotaur",
                    EntityDescription = "The Immortal Tartarus Guardian",
                    Level = 10,
                    Attack = 10,
                    Life = 666,
                    MaxLife = 666,
                    BaseSpeed = _baseSpeed * 1f,
                    AgroSpeed = _baseSpeed * 1.5f,
                    CurrentSpeed = _baseSpeed,
                    Mass = 200,
                    Immortal = true
                };
            }
        }

    }

    public static class Furnance
    {
        public const float RedHeartTimeToOilRate = 2f;
        public const float BlueHeartTimeToOilRate = 2f;
        public const float YellowHeartTimeToOilRate = 2f;

        public const int RedHeartProcessTime = 10;
        public const int BlueHeartProcessTime = 15;
        public const int YellowHeartProcessTime = 20;
    }

    public static class Prices
    {
        public static readonly int[] LighthousesActivation = { 10, 40, 60, 80 };
        public static readonly BaseCaracterStats PriceTools = new BaseCaracterStats { BlueHearts = -5, YellowHearts = -5};
        public static readonly BaseCaracterStats UpgradeOne = new BaseCaracterStats { Attack = 1, MaxLife = 50, RedHearts = -10 };
        public static readonly BaseCaracterStats UpgradeTwo = new BaseCaracterStats { BaseSpeed = 75, MaxLife = 50, BlueHearts = -7 };
        public static readonly BaseCaracterStats UpgradeThree = new BaseCaracterStats { Attack = 1, MaxLife = 150, YellowHearts = -5 };
        public static readonly BaseCaracterStats UpgradeFour = new BaseCaracterStats { Attack = 1, BaseSpeed = 75, MaxLife = 50, RedHearts = -5, BlueHearts = -5, YellowHearts = -5 };
    }

    public static class Colors
    {
        public static string RedHeart = "#D83E49";
        public static string BlueHeart = "#8BB4F5";
        public static string YellowHeart = "#E9FA7D";
    }

    public static class GuiTransitions
    {
        public const int HoleTransition = 0;
        public const int NormalTransition = 1;
        public const int In = 0;
        public const int Out = 1;
    }

    public static class Destructibles
    {
        public const int BarrelLife = 0;
    }
}
