using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mon.MonGeneration;

public class MonParseTest : MonoBehaviour
{
    DataReader dataReader = new DataReader();

    // Start is called before the first frame update
    void Start()
    {
        ParseTest();
    }

    private async void ParseTest()
    {
        BaseMon mon = await dataReader.ParseData("01_dat");
        Debug.Log(mon.name);
    }
}
