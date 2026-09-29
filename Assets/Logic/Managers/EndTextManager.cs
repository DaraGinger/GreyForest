using UnityEngine;

namespace Assets.Logic.Scripts
{
    class EndTextManager
    {
        private EndTextManager() { }

        public string Text;

        public Color Color;

        private static EndTextManager _instance = new EndTextManager();

        public static EndTextManager Instance => _instance;
    }
}
