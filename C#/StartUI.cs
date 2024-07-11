using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUI : MonoBehaviour
{
    // Start is called before the first frame update
    
    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void ListClick()
    {
        Debug.Log("Button绑定function的点击事件！");
    }

    public void StartCal()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void Return()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
