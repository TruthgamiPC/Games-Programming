using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject gameOverUI;
    public GameObject winGameUI;

    void Update()
    {
        if(GameManager.instance.enabled){
            if (Input.GetKeyDown(KeyCode.Escape)){
                Cursor.visible = !Cursor.visible;
                if (GameIsPaused){
                    Resume();
                } else {
                    Pause();
                }
            }
        } else {
            gameOver_UI();
        }
    }

    public void winGame(){
        GameIsPaused = true;
        winGameUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void continue_Game(){
        GameIsPaused = false;
        winGameUI.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void gameOver_UI(){
        GameIsPaused = true;
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
    }
    

    public void Pause(){
        GameIsPaused = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume(){
        Cursor.visible = false;
        GameIsPaused = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void LoadMenu(){
        if(GameManager.instance.enabled){
            GameManager.instance.disableGame();
        }
        GameIsPaused = false;
        Destroy(GameObject.Find("Map"));
        Destroy(GameObject.FindWithTag("GameManager"));
        SceneManager.LoadScene(0);
    }

    public void QuitGame(){
        Application.Quit();
    }
}
