using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mon.MonGeneration;

public class MonGenerateTest : MonoBehaviour
{
    DataReader dataReader = new DataReader();
    MonGenerator monGenerator = new MonGenerator();

    [SerializeField]
    BaseMon baseMon;

    [SerializeField]
    public GeneratedMon generatedMon;

    // Start is called before the first frame update
    void Start()
    {
        baseMon = dataReader.ParseData("1");
        Debug.Log(baseMon.name);
        generatedMon = monGenerator.TranslateMon(baseMon);      
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Regenerate");
            generatedMon = monGenerator.TranslateMon(baseMon);
        }
    }
}
