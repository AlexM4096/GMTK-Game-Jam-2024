using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneRestarterFromUI : MonoBehaviour
{
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
