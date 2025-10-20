using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MenuScript : MonoBehaviour
{
    public GameObject PauseMenu;

    public void ToPausePress()
    {
        PauseMenu.SetActive(true);
    }
    public void ToStartPress()
    {
        SceneManager.LoadScene(0);
    }
    public void ToRestartPress()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ToReturnPress()
    {
        PauseMenu.SetActive(false);
    }
}
