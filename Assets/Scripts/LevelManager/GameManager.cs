using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [HideInInspector]
    public Field_Generation fieldScript;
    public Enemy_Manager enemyScript;

    public GameObject player;
    private GameObject instance_player;

    // [HideInInspector]
    public GameObject player_overlay;
    public Text scoreText;
    public Text levelText;
    public Text healthText;
    public Slider healthSlider;

    [HideInInspector]
    public float player_health;
    public float player_max_health;

    [HideInInspector]
    private int weapon;
    public Text magazine;
    public int max_mag;


    private int score = 0;
    private int level = 0;
    private int floor = 0;

    [HideInInspector]
    public int difficulty;
    public float scaling;
    public int dmg_add;

    Collider2D spawn_collider;

    void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        generateGame();
    }

    public int scale_stat(int stat){
        float temp_stat = stat * (Mathf.Pow(scaling, (floor * 10 + (level - 1))));
        return Mathf.RoundToInt(temp_stat);
    }

    private void difficulty_scaling(){
        if(difficulty == 0){
            scaling = 1.01f;
        } else if(difficulty == 1){
            scaling = 1.03f;
        } else if(difficulty == 2){
            scaling = 1.06f;
        } else if(difficulty == 3){
            scaling = 1.15f;
        }
    }

    private void player_regen(){
        if (level == 1 || level == 3 || level == 5 || level == 7 || level == 9){
            player_health += 25;
            if(player_health > player_max_health){
                player_health = player_max_health;
            }
        }
    }

    public void scale_player(){
        if (level == 3 || level == 6 || level == 9){
            if(weapon == 0){
                if(difficulty == 3){
                    dmg_add += 10;
                } else {
                    dmg_add += 3;
                }
            } else if (weapon == 1){
                if(difficulty == 3){
                    dmg_add += 3;
                } else {
                    dmg_add += 1;
                }
            } else if (weapon == 2){
                if(difficulty == 3){
                    dmg_add += 55;
                } else {
                    dmg_add += 15;
                }
            }
        }
    }

    public void calculateScore(int type){
        score += type + 1 * ((floor * 10 + level) * (difficulty + 1));
        updateScoreUI();
    }

    #region Starting Game
    private void generateGame(){
        Cursor.visible = false;
        fieldScript.rooms.Clear();
        difficulty_scaling();

        level++;
        scale_player();
        player_regen();
        InitGame();
        enemyScript.Start();

        player_overlay.SetActive(true);
        update_floor();
        updateLevelUI();
        updateScoreUI();
        Invoke("SpawnPlayer", 1f);
        Physics2D.IgnoreLayerCollision(8,9,true);
    }

    void Awake(){
        if (instance == null){
            instance = this;
        } else if (instance != this){
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        fieldScript = this.GetComponent<Field_Generation>();
        enemyScript = this.GetComponent<Enemy_Manager>();

        difficulty = Stats.stats_obj.difficulty;
        weapon = Stats.stats_obj.weapon;
    }


    private void SpawnPlayer(){
        instance_player = Instantiate(player,new Vector3(0,0,0),Quaternion.identity);
    }

    void InitGame(){
        fieldScript.SetupScene(level);
    }

    #endregion


    #region little_things
    public void update_floor(){
        if (level == 11){
            floor++;
            level = 1;
        }
    }

    public int get_weapon(){
        return weapon;
    }

    public int get_floor(){
        return floor;
    }

    public int get_level(){
        return level;
    }

    #endregion

    #region UI
    public void update_UI_Mag(int curr_mag){
        magazine.text = curr_mag.ToString() + " / " + max_mag.ToString(); 
    }

    public void updateUI(){
        healthSlider.value = CalculateHealthPercentDmg();
        healthText.text = Mathf.Ceil(player_health).ToString() + " / " + player_max_health.ToString();
    }

    public void updateLevelUI(){
        levelText.text = "Stage: " + (1 + floor).ToString() + " - " + level.ToString();
    }

    public void updateScoreUI(){
        scoreText.text = "Score: " + score.ToString();
    }

    private float CalculateHealthPercentDmg(){
        return (player_health / player_max_health);
    }

    #endregion

    // Upon game over or returning to menu disables majority components of the game so that they don't cause background errors as the game is finished
    public void disableGame(){
        Cursor.visible = true;
        Time.timeScale = 0f;
        GameObject.Find("Aim_Weapon").SetActive(false);
        GameObject.Find("Crosshair").SetActive(false);
        instance_player.GetComponent<PlayerMovement>().enabled = false;
        instance_player.GetComponent<Player_Aim>().enabled = false;
        instance.player_overlay.SetActive(false);

        Destroy(GameObject.Find("Enemies"));
        Destroy(instance_player);
    }

    public void GameOver(){
        disableGame();
        this.enabled = false;
    }
}
