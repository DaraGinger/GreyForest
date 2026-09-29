using Assets.Enumerables;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Logic.Scripts;
using TMPro;

namespace Assets.Scripts
{
    public class Menu : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text DifficultyText;

        [SerializeField]
        private Button RightArrow;

        [SerializeField]
        private Button LeftArrow;

        [SerializeField]
        private TMP_Text EndText;

        [SerializeField]
        private GameObject MainMenu;

        [SerializeField]
        private GameObject Bindings;

        [SerializeField]
        private GameObject Options;

        [SerializeField]
        private GameObject EndMenu;

        void Start()
        {
            GameInfo.Instance.DifficultyType = DifficultyType.Easy;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
          
            if (GameInfo.Instance.IsGameOver)
            {
                ActivateEndMenu();
            }

            GameInfo.Instance.Clean();
        }

        public void LeftArrowClick()
        {
            int difficulty = (int)GameInfo.Instance.DifficultyType-1;

            DifficultyType difficultyType = (DifficultyType)difficulty;

            DifficultyText.text = difficultyType.ToString();

            if (difficultyType == DifficultyType.Easy)
            {
                RightArrow.interactable = true;
                LeftArrow.interactable = false;
            }
            else
            {
                RightArrow.interactable = true;
            }

            GameInfo.Instance.DifficultyType = difficultyType;
        }

        public void RightArrowClick()
        {
            int difficulty = (int)GameInfo.Instance.DifficultyType+1;

            DifficultyType difficultyType = (DifficultyType)difficulty;

            DifficultyText.text = difficultyType.ToString();

            if (difficultyType == DifficultyType.Hard)
            {
                RightArrow.interactable = false;
                LeftArrow.interactable = true;
            }
            else
            {
                LeftArrow.interactable = true;
            }

            GameInfo.Instance.DifficultyType = difficultyType;
        }

        public void PLayButtonClick()
        {
            GameInfo.Instance.UpdateGameInformation();

            SceneManager.LoadScene("Scenes/Forest");
        }

        public void QuitButtonClick()
        {
            Application.Quit();
        }

        public void MenuButtonFromEndMenuClick()
        {
            EndMenu.SetActive(false);
            MainMenu.SetActive(true);
        }

        public void BackToOptionsClick()
        {
            Bindings.SetActive(false);
            Options.SetActive(true);
        }

        public void MenuButtonFromOptionsMenuClick()
        {
            Options.SetActive(false);
            MainMenu.SetActive(true);
        }

        public void ActivateEndMenu()
        {
            EndMenu.SetActive(true);
            MainMenu.SetActive(false);
            EndText.text = EndTextManager.Instance.Text;
            EndText.color = EndTextManager.Instance.Color;
        }

        public void BindingsButtonClick()
        {
            Bindings.SetActive(true);
            Options.SetActive(false);
        }

        public void OptionsButtonClick()
        {
            Options.SetActive(true);
            MainMenu.SetActive(false);
        }
    }
}
