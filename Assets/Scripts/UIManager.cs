using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Level Canvas")]
    [SerializeField] private GameObject goLevelCanvas;
    [SerializeField] private Text textCurrentLevel = null;
    [SerializeField] private Text textRacePosition = null;
    [SerializeField] private Text textProgression = null;
    [SerializeField] private Text textSpeed = null;
    [SerializeField] private Text textScore = null;

    [Header("Post Game Canvas")]
    [SerializeField] private GameObject goPostGameCanvas;
    [SerializeField] private Text textPGCurrentLevel = null;
    [SerializeField] private Text textPGRacePosition = null;
    [SerializeField] private Text textPGScore = null;
    [SerializeField] private Text textPGHighScore = null;

    private LevelManager lmanager;
    private bool raceHasEnded;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        lmanager = LevelManager.Instance;

        textCurrentLevel.text = "LEVEL " + lmanager.CurrentLevel;
    }

    // Update is called once per frame
    void Update()
    {
        if(raceHasEnded)
        {
            return;
        }

        if(lmanager.CurrentState == LevelManager.LevelState.InRace)
        {
            textRacePosition.text = lmanager.RacePosition + " / " + lmanager.RacersSize;
            textProgression.text = lmanager.Progression + " %";
            textSpeed.text = lmanager.Speed + " km/h";
            textScore.text = "" + lmanager.Score;
        }
        else
        {
            goLevelCanvas.SetActive(false);
            goPostGameCanvas.SetActive(true);

            textPGCurrentLevel.text = "LEVEL " + lmanager.CurrentLevel;
            textPGRacePosition.text = lmanager.RacePosition + " / " + lmanager.RacersSize;
            textPGScore.text = "" + lmanager.Score;
            textPGHighScore.text = "" + lmanager.HighScore;
            raceHasEnded = true;
        }

    }
}
