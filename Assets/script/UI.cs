using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject GameOver;
    [Space]
    [SerializeField] private TextMeshProUGUI TimeText;
    [SerializeField] private TextMeshProUGUI KillCountText;
    

    public static UI Instance;

    private int KillCount;

    private void Awake()
    {
        Instance = this;
        Time.timeScale = 1.0f;
    }

    private void Update()
    {
        TimeText.text = Time.time.ToString("F2") + "s";
    }

    public void EnableGameOver()
    {
        Time.timeScale = .5f;
        GameOver.SetActive(true);
    }

    public void Restart()
    {
        int sceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
    }

    public void AddKillCount()
    {
        KillCount++;
        KillCountText.text = KillCount.ToString();
    }

}
