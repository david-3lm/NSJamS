using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void ChangeToScene(int i)
    {
        SceneManager.LoadSceneAsync(i);
    }
}
