using Assets.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Logic.Scripts
{
    public class GoodEnding: MonoBehaviour
    {
        private readonly string endText = "Win";
        public void OnTriggerExit(Collider other)
        {
            EndTextManager.Instance.Text = endText;
            EndTextManager.Instance.Color = Color.green;
            GameInfo.Instance.IsGameOver = true;
            SceneManager.LoadScene("Scenes/Menu");
        }
    }
}
