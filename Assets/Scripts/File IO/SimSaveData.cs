using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SimSaveData
{
    public string simName;
    public int age;
    public List<SimData.Trait> traits; //we'll store it as the value, enums are also numbers
    public List<KeyValuePair<NeedData.NeedType, NeedData>> needsMap;
    public float[] simColor;

    /// <summary>
    /// Constructor for the fully-serializable save data file format
    /// </summary>
    /// <param name="simData">SimData object to be serialized</param>
    public SimSaveData(SimData simData)
    {
        simName = simData.simName;
        age = simData.age;
        traits = simData.traits;
        needsMap = new List<KeyValuePair<NeedData.NeedType, NeedData>>();
        foreach (NeedData.NeedType need in simData.needsMap.Keys)
        {
            KeyValuePair<NeedData.NeedType, NeedData> kvp = new KeyValuePair<NeedData.NeedType, NeedData>(need, simData.needsMap[need]);
            needsMap.Add(kvp);
        }
        simColor = new float[4];
        simColor[0] = simData.simColour.r;
        simColor[1] = simData.simColour.g;
        simColor[2] = simData.simColour.b;
        simColor[3] = simData.simColour.a;
    }

    /// <summary>
    /// Converts serializable SimSaveData object into a SimData object
    /// </summary>
    /// <returns>SimData object of the serialized data</returns>
    public SimData GetSimData()
    {
        SimData simData = new SimData();
        simData.LoadSim(simName, age, traits, needsMap, new Color(simColor[0], simColor[1], simColor[2], simColor[3]));

        return simData;
    }
}

