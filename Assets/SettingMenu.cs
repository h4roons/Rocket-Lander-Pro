using UnityEngine;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    public Slider volumeSlider;
    public Button applyButton;
    public GameObject Optionmenu;
    public GameObject mainMenu;

    void Start()
    {
        // Load saved volume (if any)
        if (PlayerPrefs.HasKey("Volume"))
        {
            volumeSlider.value = PlayerPrefs.GetFloat("Volume");
            SetVolume(volumeSlider.value);
        }
        else
        {
            // Set default volume if it's not yet set
            volumeSlider.value = 1f;
            SetVolume(1f);
        }
    }

    public void SetVolume(float volume)
    {
        // Set volume
        AudioListener.volume = volume;

        // Save volume
        PlayerPrefs.SetFloat("Volume", volume);
    }

    public void ApplyChanges()
    {
        // Apply any other changes here if necessary
        Debug.Log("Changes applied!");
        Optionmenu.SetActive(false);
        mainMenu.SetActive(true);
    }
}
