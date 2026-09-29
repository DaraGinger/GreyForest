using Assets.Scripts;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Logic.Scripts
{
    public class BadEnding: MonoBehaviour
    {
        private readonly string endText = "Failed";

        [SerializeField]
        private GameObject cameraHolder;

        [SerializeField]
        private GameObject jumpscareCamera;

        [SerializeField]
        public GameObject Flashlight;

        public float duration;

        public float magnitude;

        private Animator animator;

        private AudioSource audioSource;

        private bool IsAnimationEnd;

        private bool IsCoroutineEnd;

        private bool IsShakingEnd;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        void Update()
        {
            if (IsAnimationEnd)
            {
                StartCoroutine(Coroutine());
                if (IsCoroutineEnd)
                {
                    jumpscareCamera.transform.localPosition = new Vector3(0, 0, 0);
                    StartCoroutine(Shake());
                }
            }

            if (IsShakingEnd)
            {
                EndTextManager.Instance.Text = endText;
                EndTextManager.Instance.Color = Color.red;
                GameInfo.Instance.IsGameOver = true;
                SceneManager.LoadScene("Scenes/Menu");
            }
        }

        public void OnTriggerEnter(Collider other)
        {
            var parent = transform.parent.gameObject;
            animator = parent.GetComponent<Animator>();

            Camera.main.gameObject.SetActive(false);
            jumpscareCamera.SetActive(true);
            Flashlight.SetActive(true);

            animator.SetTrigger("Jumpscare");

            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

            audioSource.PlayOneShot(audioSource.clip);
            IsAnimationEnd = true;
        }

        private IEnumerator Shake()
        {
            Vector3 vector = jumpscareCamera.transform.localPosition;

            float elapsed = 0.0f;

            while (elapsed < duration)
            {
                float x = Random.Range(-1f, 1f) * magnitude;
                float z = Random.Range(-1f, 1f) * magnitude;

                jumpscareCamera.transform.localPosition = new Vector3(x, vector.y, z);

                elapsed += Time.deltaTime;

                yield return null;
            }

            jumpscareCamera.transform.localPosition = vector;
            IsShakingEnd = true;
        }

        private IEnumerator Coroutine()
        {
            yield return new WaitForSeconds(0.3f);
            IsCoroutineEnd = true;
        }
    }
}
