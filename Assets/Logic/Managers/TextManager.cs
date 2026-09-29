using TMPro;

namespace Assets.Logic.Scripts
{
    public class TextManager
    {
        private TextManager() { }
        public TMP_Text CounterText;

        public TMP_Text PressC;

        private static TextManager Instance = new TextManager();

        public static TextManager TextManagerInstance => Instance;
    }
}
