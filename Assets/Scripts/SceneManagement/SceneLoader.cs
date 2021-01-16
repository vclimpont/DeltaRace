using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; set; }

    void Awake()
    {
        Instance = this;
    }

    public void StartLevel()
    {
        SceneManager.LoadScene("DeltaRace");
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("PreGame");
    }
}
