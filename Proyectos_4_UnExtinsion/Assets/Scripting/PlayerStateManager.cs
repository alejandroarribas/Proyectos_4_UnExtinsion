using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStateManager : MonoBehaviour
{
    private Scene scene;
    // Start is called before the first frame update
    private void Start()
    {
        scene = SceneManager.GetActiveScene();
    }
    public void PlayerDies()
    {
        Application.LoadLevel(scene.name);
    }
}
