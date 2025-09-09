using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Monobehaviour script used to create Sim entities.
public class SimManager : MonoBehaviour
{

    [Range(0, 10)]public float range;

    [Tooltip("ahhHH!")]
    public List<SimData> sims = new List<SimData>();
    [SerializeField] private GameObject simPrefab;
    public Transform simsHolder;

    [TextArea]
    public string bigtext;

    //On initialization, the SimManager will create Sim entities for each instance of SimData it has stored.
    void Start()
    {
        foreach (SimData simData in sims)
        {
            CreateSimEntity(simData);
        }
    }

    [ContextMenu("example")]
    public void SayHello()
    {
        print("hello");
    }
    //Given data about a Sim, it creates the entity associated with that data

    /// <summary>
    /// description of the function!!!!
    /// </summary>
    /// <param name="simData">this is a variable! wow!</param>
    public void CreateSimEntity(SimData simData)
    { 
        GameObject simObject = Instantiate(simPrefab, simsHolder);
        Sim sim = simObject.GetComponent<Sim>();
        sim.Initialize(simData);
    }

}
