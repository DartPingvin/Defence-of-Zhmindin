using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    [SerializeField] private Button Start;
    void Awake()
    {
        Start.onClick.AddListener(StartScene);
    }
    void StartScene()
    {
        SceneManager.LoadScene(1);
    }
}
