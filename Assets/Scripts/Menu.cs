using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Button[] Levels;
    void Start()
    {
        
    }
  

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void DeleteKeys()
    {
        PlayerPrefs.DeleteAll();
    }
}
