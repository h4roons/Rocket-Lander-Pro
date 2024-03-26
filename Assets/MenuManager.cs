using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject optionsMenu;
    public bool isOptionOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("easy gap");
        Debug.Log("Scene loaded");
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void OptionsMenu()
    {
        if (!isOptionOpen)
        {
            MainMenu.SetActive(false);
            optionsMenu.SetActive(true);
        }
    }
}
