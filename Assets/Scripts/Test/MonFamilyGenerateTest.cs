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
        //Load key data
        Utility.JsonUtility<KeysJSON> jsonLoader = new Utility.JsonUtility<KeysJSON>();
        keyObj = await jsonLoader.LoadJSON("MonData/keyData");

        //Generates all mons in that keyObj

        await monGenerator.GenerateMonsByKey(keyObj);

        await Core.CoreManager.Instance.dexManager.InitDex(monGenerator.monDex);

        display.generator = monGenerator;
        display.ResetID();
    }

    private void Start()
    {
        LoadMon();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Core.CoreManager.Instance.dexManager.DexReady)
        {
            display.displaying = true;
            LoadMon();

            //Save all generated mons
            monGenerator.monDex.SaveDex();
        }
    }


}
