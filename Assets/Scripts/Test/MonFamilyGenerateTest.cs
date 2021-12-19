using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mon.MonGeneration;

/// <summary>
/// Tests generating a whole family
/// </summary>
public class MonFamilyGenerateTest : MonoBehaviour
{
    DataReader dataReader = new DataReader();
    MonGenerator monGenerator = new MonGenerator();

    [SerializeField]
    DisplayMonInfo display;

    [SerializeField]
    private GeneratedMon generatedMon;

    KeysJSON keyObj;

    private async void LoadMon()
    {
        //Generates all mons in that keyObj
        await monGenerator.GenerateMonsByKey();

        await Core.CoreManager.Instance.dexManager.LoadDex(monGenerator.monDex);

        display.generator = monGenerator;
        display.ResetID();
    }

    private void Start()
    {
        Core.CoreManager.Instance.randomManager.InitializeSeed();
        LoadMon();
        StartCoroutine(WaitForReady());
    }

    private IEnumerator WaitForReady()
    {
        while (!Core.CoreManager.Instance.dexManager.DexReady)
        {
            yield return new WaitForEndOfFrame();
        }
        display.displaying = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Core.CoreManager.Instance.dexManager.DexReady)
        {
           
            LoadMon();

            //Save all generated mons
            monGenerator.monDex.SaveDex();
        }
    }


}
