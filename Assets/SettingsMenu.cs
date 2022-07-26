using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Threading.Tasks;


public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Slider master;
    public Slider music;
    public Slider effects;

    private void Awake(){
        master.value = PlayerPrefs.GetFloat("MasterVolume");
        music.value = PlayerPrefs.GetFloat("MusicVolume");
        effects.value = PlayerPrefs.GetFloat("EffectsVolume");
        updateAudio();
        
    }

    async void updateAudio(){
        await Task.Delay(100);
        audioMixer.SetFloat("master_volume",master.value);
        audioMixer.SetFloat("music",music.value);
        audioMixer.SetFloat("effects", effects.value);
    }

    public void SetVolumeMaster(float volume){
        audioMixer.SetFloat("master_volume",volume);
        PlayerPrefs.SetFloat("MasterVolume",volume);
    }

    public void SetVolumeMusic(float volume){
        audioMixer.SetFloat("music",volume);
        PlayerPrefs.SetFloat("MusicVolume",volume);
    }

    public void SetVolumeEffects(float volume){
        audioMixer.SetFloat("effects",volume);
        PlayerPrefs.SetFloat("EffectsVolume",volume);
    }
}
