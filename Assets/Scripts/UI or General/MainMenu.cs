using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject selectionPanel;
    public GameObject mainPanel;
    
    public GameObject button_start;

    public void StartGame(){
        Debug.Log("Moving to Scene 1");
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    // # |
    // # v

    public void OpenSelector(){
        mainPanel.SetActive(false);
        selectionPanel.SetActive(true);
    }

    public void CloseSelector(){
        mainPanel.SetActive(true);
        selectionPanel.SetActive(false);
        reset();
    }

    public void setDifficulty(int value){
        Stats.stats_obj.set_difficulty(value);
    }

    public void setWeapon(int value){
        Stats.stats_obj.setWeapon(value);
    }

    public void startRun(){
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    // #---------------------------------------------------
    public void OpenSettings(){
        settingsPanel.SetActive(true);
        mainPanel.SetActive(false);
    }

    public void CloseSettings(){
        mainPanel.SetActive(true);
        PlayerPrefs.Save();
        settingsPanel.SetActive(false);
    }

    public void test(){
        button_start.GetComponent<Button>().interactable = false;
    }

    private void reset(){
        Stats.stats_obj.weapon = 4;
        Stats.stats_obj.difficulty = 4;
        test();
    }


    void Update(){
        if(Stats.stats_obj.weapon != 4 && Stats.stats_obj.difficulty != 4){
            button_start.GetComponent<Button>().interactable = true;
        } 
    }

    void Awake(){
        test();
    }

    public void ExitGame(){
        // Debug.Log("Game shutting down.");
        Application.Quit();
    }
}
