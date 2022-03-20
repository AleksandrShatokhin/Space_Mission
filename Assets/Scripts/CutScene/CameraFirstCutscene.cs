using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFirstCutscene : MonoBehaviour
{
    [SerializeField] private GameObject cutSceneCamera;

    [SerializeField] private Vector3 position_1;
    private Quaternion rotation_1;

    [SerializeField] private Vector3 position_2;
    private Quaternion rotation_2;

    [SerializeField] private Vector3 position_3;
    private Quaternion rotation_3;

    void Start()
    {
        rotation_1 = Quaternion.Euler(25, -35, 0);
        rotation_2 = Quaternion.Euler(25, -110, 0);
        rotation_3 = Quaternion.Euler(0, -150, 0);
    }

    public void PositionCamera_1()
    {
        cutSceneCamera.transform.position = position_1;
        cutSceneCamera.transform.rotation = rotation_1;
    }

    public void PositionCamera_2()
    {
        cutSceneCamera.transform.position = position_2;
        cutSceneCamera.transform.rotation = rotation_2;
    }

    public void PositionCamera_3()
    {
        cutSceneCamera.transform.position = position_3;
        cutSceneCamera.transform.rotation = rotation_3;
    }

    IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(1.0f);

        LoadingScreen.SwitchScene("Level1");
    }

    public void StartLevel_1()
    {
        StartCoroutine(LoadLevel());
    }
}
