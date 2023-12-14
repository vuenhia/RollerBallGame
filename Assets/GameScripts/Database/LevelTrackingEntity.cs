
namespace DataBank
{

    public class LevelTrackingEntity
    {
        public string levelName;
        public bool star1;
        public bool star2;
        public bool star3;
        public float bestTime;

        public LevelTrackingEntity()
        {
            this.levelName = "";
            this.star1 = false;
            this.star2 = false;
            this.star3 = false;
            this.bestTime = 0;
        }
        public LevelTrackingEntity(string levelName, bool star1, bool star2, bool star3, float bestTime)
        {
            this.levelName = levelName;
            this.star1 = star1;
            this.star2 = star2;
            this.star3 = star3;
            this.bestTime = bestTime;
        }
    }
}