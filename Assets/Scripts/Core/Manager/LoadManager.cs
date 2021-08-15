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
    public async Task LoadLoadingScreen(bool disablePlayer = true)
    {
        AsyncOpHelper opHelper = new AsyncOpHelper();
        await opHelper.CompleteAsyncOp(SceneManager.LoadSceneAsync(LoadSceneName, LoadSceneMode.Additive), tick);
        if (disablePlayer)
        {
            Core.CoreManager.Instance.SetPlayerActive(false);
        }        
        Core.CoreManager.Instance.camera.GetComponent<Camera>().enabled = false;
        //await Task.Delay(1000);
    }

    public async Task UnloadLoadingScreen(bool enablePlayer = true)
    {
        Core.CoreManager.Instance.camera.GetComponent<Camera>().enabled = true;
        AsyncOpHelper opHelper = new AsyncOpHelper();
        if (enablePlayer)
        {
            Core.CoreManager.Instance.SetPlayerActive(true);
        }
        await opHelper.CompleteAsyncOp(SceneManager.UnloadSceneAsync(LoadSceneName), tick);      
    }
}
