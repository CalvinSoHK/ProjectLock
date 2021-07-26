using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utility;

public class LoadManager : MonoBehaviour
{
    public static string LoadSceneName = "LoadingScreen";
    public const int tick = 100;
    public async Task LoadLoadingScreen()
    {
        Core.CoreManager.Instance.camera.GetComponent<Camera>().enabled = false;
        AsyncOpHelper opHelper = new AsyncOpHelper();
        await opHelper.CompleteAsyncOp(SceneManager.LoadSceneAsync(LoadSceneName), tick);
        await Task.Delay(1000);
    }

    public async Task UnloadLoadingScreen()
    {
        AsyncOpHelper opHelper = new AsyncOpHelper();
        await opHelper.CompleteAsyncOp(SceneManager.UnloadSceneAsync(LoadSceneName), tick);
        Core.CoreManager.Instance.camera.GetComponent<Camera>().enabled = true;
    }
}
