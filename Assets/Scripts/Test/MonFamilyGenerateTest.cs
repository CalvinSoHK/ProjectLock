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

    // Start is called before the first frame update
    void Start()
    {
        //Load key data
        Utility.JsonUtility<KeysJSON> jsonLoader = new Utility.JsonUtility<KeysJSON>();
        keyObj = jsonLoader.LoadJSON("MonData/keyData");

        //Generates all mons in that keyObj
        monGenerator.GenerateMonsByKey(keyObj);

        display.generator = monGenerator;
        //display.ResetID();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Load key data
            Utility.JsonUtility<KeysJSON> jsonLoader = new Utility.JsonUtility<KeysJSON>();
            keyObj = jsonLoader.LoadJSON("MonData/keyData");

            //Generates all mons in that keyObj
            monGenerator.GenerateMonsByKey(keyObj);

            display.ResetID();

            //Save all generated mons
            monGenerator.monDex.SaveDex();
        }
    }


}
