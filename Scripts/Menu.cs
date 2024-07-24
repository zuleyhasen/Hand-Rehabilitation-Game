using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void Game1ButtonOnClick()
    {
        SceneManager.LoadScene("Scene1"); // Oyun sahnesine geçiþ
    }

    public void Game2ButtonOnClick()
    {
        SceneManager.LoadScene("Scene2"); 
    }

    public void Game3ButtonOnClick()
    {
        SceneManager.LoadScene("Scene3"); // Ayarlar sahnesine geçiþ
    }

    public void ExitButtonOnClick()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif

    }
    public void ROMButtonOnClick()
    {
        SceneManager.LoadScene("hand_rom"); // Ayarlar sahnesine geçiþ
    }
}
