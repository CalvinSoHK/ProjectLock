using Core.AddressableSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddressablesTest : MonoBehaviour
{
    AddressablesManager manager;

    [SerializeField]
    string testPath;

    // Start is called before the first frame update
    void Start()
    {
        manager = Core.CoreManager.Instance.addressablesManager;
        AddressablesManager.LoadProgressEvent += LogProgress;
        TestLoad();
    }

    private async void TestLoad()
    {
        Debug.Log("Attempting load of path: " + testPath);
        Object obj = await manager.LoadAddressable<Object>(testPath, true);
        Debug.Log("Completed test load of object: " + obj.name);

        Debug.Log("Attempting release of addressable.");
        manager.ReleaseAddressable(testPath);
        Debug.Log("Completed release of addressable");
    }

    private void LogProgress(float progress)
    {
        Debug.Log("Load progress: " + progress);
    }
}
