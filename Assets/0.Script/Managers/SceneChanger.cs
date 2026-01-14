using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void OnLoadScene(int sceneNumber) => SceneManager.LoadScene(sceneNumber);
}