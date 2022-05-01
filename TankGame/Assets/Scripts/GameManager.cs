using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI lastWaveText;
    public TextMeshProUGUI waveText;
    public Button restartButton;
    private SpawnManager spawnManager;
    private PlayerController playerController;
    public GameObject tittleScreen;
    public bool gameIsActive;

    // Start is called before the first frame update
    void Start()
    {
        gameIsActive = false;
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    public void StartGame()
    {
        Debug.Log("Start button was clicked");
        gameIsActive = true;
        tittleScreen.gameObject.SetActive(false);
        waveText.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Update is called once per frame
    void Update()
    {
        waveText.SetText("Wave: " + spawnManager.waveNumber);
        if (playerController.playerIsAlive == false)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        waveText.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(true);
        lastWaveText.SetText("Final wave survived: " + spawnManager.waveNumber);
        lastWaveText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        gameIsActive = false;
    }


}
