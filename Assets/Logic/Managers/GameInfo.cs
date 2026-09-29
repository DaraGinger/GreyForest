using Assets.Enumerables;

namespace Assets.Scripts
{
    public class GameInfo
    {
        private GameInfo() { }

        public DifficultyType DifficultyType;

        public int CollectedToys;

        public int SumOfToys;

        public float EnemyWalkSpeed;

        public float EnemyRunSpeed;

        public float SightRange;

        public bool IsGameOver;

        public float PlayerSpeed = 0.05f;

        public float PlayerRunningSpeed = 0.1f;

        public float PlayerStaminaCapacity = 2.0f;

        public float CursorSensitivity = 200;

        private static GameInfo _instance = new GameInfo();

        public static GameInfo Instance => _instance;

        public void CollectToy()
        {
            if (CollectedToys < SumOfToys)
                CollectedToys++;
        }

        public void UpdateGameInformation()
        {
           switch (DifficultyType)
            {
                case DifficultyType.Easy:
                    {
                        SumOfToys = 8;
                        EnemyWalkSpeed = 2;
                        EnemyRunSpeed = 4;
                        SightRange = 10;
                        break;
                    }

                case DifficultyType.Medium:
                    {
                        SumOfToys = 12;
                        EnemyWalkSpeed = 3.5f;
                        EnemyRunSpeed = 4.5f;
                        SightRange = 15;
                        break;
                    }

                case DifficultyType.Hard:
                    {
                        SumOfToys = 20;
                        EnemyWalkSpeed = 5;
                        EnemyRunSpeed = 6;
                        SightRange = 17;
                        break;
                    }
            }
        }

        public void Clean()
        {
            SumOfToys = 8;
            EnemyWalkSpeed = 2;
            EnemyRunSpeed = 4;
            SightRange = 10;
            IsGameOver = false;
            CollectedToys = 0;
        }
    }
}
