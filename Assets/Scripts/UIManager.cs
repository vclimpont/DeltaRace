using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text textCurrentLevel = null;
    [SerializeField] private Text textRacePosition = null;
    [SerializeField] private Text textProgression = null;
    [SerializeField] private Text textSpeed = null;
    [SerializeField] private Text textScore = null;

    private LevelManager lmanager;

    void Start()
    {
        lmanager = LevelManager.Instance;

        textCurrentLevel.text = "LEVEL " + lmanager.CurrentLevel;
    }

    // Update is called once per frame
    void Update()
    {
        textRacePosition.text = lmanager.RacePosition + " / " + lmanager.RacersSize;
        textProgression.text = lmanager.Progression + " %";
        textSpeed.text = lmanager.Speed + " km/h";
        textScore.text = "" + lmanager.Score;
    }
}
