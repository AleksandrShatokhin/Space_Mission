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
        // запустим анимацию загрузочного окна
        instance.animator.SetTrigger("isStart");

        // запускаем новую сцену
        instance.loadingOperation = SceneManager.LoadSceneAsync(nameScene);

        // также запретим Unity сразу переключаться на сцену когда она загрузится
        instance.loadingOperation.allowSceneActivation = false;
    }

    void Start()
    {
        instance = this;

        animator = GetComponent<Animator>();

        // проверяем что сцена загрузилась
        if (isLoadScene)
        {
            // запускаем сворачивающую анимацию при запуске сцены
            animator.SetTrigger("isEnd");
        }
    }

    public void OnAnimationEnd() // для разрешения Unity раскрыть сцену. Использую по Event на анимации
    {
        isLoadScene = true;
        loadingOperation.allowSceneActivation = true;
    }
}
