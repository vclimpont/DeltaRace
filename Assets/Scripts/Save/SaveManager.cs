using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    public SaveState state { get; private set; }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
        Load();

        SceneLoader.Instance.LoadMenu();
    }

    public void Save()
    {
        PlayerPrefs.SetString("save", Serializer.Serialize(state));
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey("save"))
        {
            state = Serializer.Deserialize<SaveState>(PlayerPrefs.GetString("save"));
        }
        else
        {
            state = new SaveState();
            Save();
            Debug.Log("New save");
        }
    }
}
