using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Monobehaviour script used to create Sim entities.
public class SimManager : MonoBehaviour
{
    public List<SimData> sims = new List<SimData>();
    public GameObject simPrefab;
    public Transform simsHolder;

    //On initialization, the SimManager will create Sim entities for each instance of SimData it has stored.
    void Start()
    {
        foreach (SimData simData in sims)
        {
            CreateSimEntity(simData);
        }
    }

    //Given data about a Sim, it creates the entity associated with that data
    public void CreateSimEntity(SimData simData)
    { 
        GameObject simObject = Instantiate(simPrefab, simsHolder);
        Sim sim = simObject.GetComponent<Sim>();
        sim.Initialize(simData);
    }

    public void example()
    {

        string data = "bob, 23, 4,4";
        string[] dataArray = data.Split(",");
        SimData sim = new SimData();
        sim.simName = dataArray[0];
        int.TryParse(dataArray[1], out sim.age);
    }

}
