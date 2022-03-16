using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    private static LoadingScreen instance;

    private static bool isLoadScene = false;

    private Animator animator;
    private AsyncOperation loadingOperation;


    public static void SwitchScene(string nameScene)
    {
        // �������� �������� ������������ ����
        instance.animator.SetTrigger("isStart");

        // ��������� ����� �����
        instance.loadingOperation = SceneManager.LoadSceneAsync(nameScene);

        // ����� �������� Unity ����� ������������� �� ����� ����� ��� ����������
        instance.loadingOperation.allowSceneActivation = false;
    }

    void Start()
    {
        instance = this;

        animator = GetComponent<Animator>();

        // ��������� ��� ����� �����������
        if (isLoadScene)
        {
            // ��������� ������������� �������� ��� ������� �����
            animator.SetTrigger("isEnd");
        }
    }

    public void OnAnimationEnd() // ��� ���������� Unity �������� �����. ��������� �� Event �� ��������
    {
        isLoadScene = true;
        loadingOperation.allowSceneActivation = true;
    }
}
