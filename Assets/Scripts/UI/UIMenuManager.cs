using UnityEngine;
using UnityEngine.UI;

public class UIMenuManager : MonoBehaviour
{
    public static UIMenuManager Instance { get; set; }

    [SerializeField] private Text textLevel = null;
    [SerializeField] private Text textHighscore = null;
    [SerializeField] private Toggle toggleLowSettings = null;

    private SaveManager sm;

    void Awake()
    {
        Instance = this;
        sm = SaveManager.Instance;

        textLevel.text = "RACE " + sm.state.MaxLevel;
        textHighscore.text = "" + sm.state.HighScore;
        toggleLowSettings.isOn = sm.state.LowSettings;
    }

    public void ChangeLowSettingsValue()
    {
        sm.state.LowSettings = toggleLowSettings.isOn;
        sm.Save();
    }
}
