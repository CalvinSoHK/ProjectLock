using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticPaths
{
    /// <summary>
    /// Path to save generated mons as jsons.
    /// Still has a folder after to denote generation.
    /// </summary>
    public const string SaveToGeneratedMonsPaths = "Assets/Resources/Generated";

    /// <summary>
    /// Path to load generated mons as jsons.
    /// Does not require Resources because it is called form Resources.Load
    /// </summary>
    public const string LoadFromGeneratedMonsPaths = "Generated";

    /// <summary>
    /// Path to save generated encounters to as jsons.
    /// </summary>
    public const string SaveToGeneratedEncountersPaths = "Assets/Resources/Encounters";

    /// <summary>
    /// Path to load generated encounters as jsons.
    /// </summary>
    public const string LoadFromGeneratedEncountersPaths = "Encounters";

    /// <summary>
    /// Path to load MonGenerationCount scriptable object.
    /// NOTE: Doesn't need Resources because it is assumed in Resources.Load
    /// </summary>
    public const string GenerationCount = "ScriptableObjects/MonGenerationCount";

    /// <summary>
    /// Path to load dialogue from
    /// </summary>
    public const string DialoguePath = "Dialogue";
}
