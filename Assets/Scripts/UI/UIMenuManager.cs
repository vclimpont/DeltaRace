using UnityEngine;
using UnityEngine.UI;

public class UIMenuManager : MonoBehaviour
{
    public static UIMenuManager Instance { get; set; }

    [SerializeField] private Text textLevel = null;
    [SerializeField] private Text textHighscore = null;

    void Awake()
    {
        Instance = this;

        textLevel.text = "RACE " + SaveManager.Instance.state.MaxLevel;
        textHighscore.text = "" + SaveManager.Instance.state.HighScore;
    }


}
