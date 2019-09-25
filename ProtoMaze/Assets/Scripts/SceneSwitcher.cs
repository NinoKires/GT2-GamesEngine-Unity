using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void LoadByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    void OnGenNewMazeClicked()
    {
        SceneManager.LoadScene("gameTemp");
    }
}