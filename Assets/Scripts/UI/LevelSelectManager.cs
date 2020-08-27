using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class LevelSelectManager : MonoBehaviour
{
    public GameObject levelButton;
    public GameObject levelGrid;

    // Start is called before the first frame update
    void Start()
    {
        foreach(var level in GetLevelSceneNames())
        {
            CreateLevelButton(level);
        }

    }

    List<string> GetLevelSceneNames()
    {
        int sceneCount = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;
        List<string> levels = new List<string>();

        for(int i = 0; i < sceneCount; i++)
        {
            string sceneName = Path.GetFileNameWithoutExtension(UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(i));
            if(sceneName.StartsWith("LVL"))
            {
                levels.Add(sceneName);
            }
        }

        return levels;
    }

    void CreateLevelButton(string level)
    {
        GameObject newLevelButton = Instantiate(levelButton);
        newLevelButton.transform.SetParent(levelGrid.transform);
        newLevelButton.transform.localScale = new Vector3(1, 1, 1);
        LevelButtonController newLevelButtonController = newLevelButton.GetComponent<LevelButtonController>();
        newLevelButtonController.levelNo = int.Parse(level.Substring(3));
        newLevelButtonController.levelText = newLevelButton.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
