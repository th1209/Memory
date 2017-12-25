using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{
    public void ToGameScene()
    {
        SceneManager.LoadScene("Main");
    }

    private void ChangeCpuLevelToggle()
    {
        CpuLevel cpuLevel = GameObject.Find("/CpuLevel").GetComponent<CpuLevel>();
        var levelAndToggleName = new Dictionary<int, string>(){
            {0, "Easy"},
            {1, "Normal"},
            {2, "Hard"},
        };
        string targetName = levelAndToggleName[cpuLevel.GetLevel()];

        GameObject[] toggleObjs = GameObject.FindGameObjectsWithTag("CpuLevelToggle");
        foreach (var toggleObj in toggleObjs)
        {
            if (toggleObj.name == targetName)
            {
                toggleObj.GetComponent<Toggle>().isOn = true;
            }
        }
    }

    void Start()
    {
        ChangeCpuLevelToggle();
    }
}
