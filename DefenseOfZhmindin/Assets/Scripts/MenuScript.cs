using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject TradeMenu;
    private bool TradeMenuUp = false;

    //Pause Menu
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
    //Trade Menu
    public void TradeMenuPress()
    {
        if (TradeMenuUp == false)
        {
            TradeMenuUp = true;
            TradeMenu.SetActive(true);
        }
        else
        {
            TradeMenuUp = false;
            TradeMenu.SetActive(false);
        }
    }
}
