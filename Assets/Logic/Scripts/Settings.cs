using Assets.Scripts;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Assets.Logic.Scripts
{
    class Settings : MonoBehaviour
    {
        [SerializeField]
        private Slider VolumeAmbient;

        [SerializeField]
        private Slider VolumeSounds;

        [SerializeField]
        private Slider CursorSensitivity;

        [SerializeField]
        private AudioMixer AudioMixer;

        private void Start()
        {
            AudioMixer.GetFloat("Ambient", out float ambient);
            VolumeAmbient.value = ambient;
            AudioMixer.GetFloat("Sounds", out float sounds);
            VolumeSounds.value = sounds;
            CursorSensitivity.value = GameInfo.Instance.CursorSensitivity;
        }

        public void ChangeVolumeAmbient()
        {
            float volume = VolumeAmbient.value;
            AudioMixer.SetFloat("Ambient", Mathf.Log10(volume) * 30);
        }

        public void ChangeVolumeSounds()
        {
            float volume = VolumeSounds.value;
            AudioMixer.SetFloat("Sounds", Mathf.Log10(volume) * 30);
        }

        public void ChangeCursorSensitivity()
        {
            GameInfo.Instance.CursorSensitivity = CursorSensitivity.value;
        }
    }
}
