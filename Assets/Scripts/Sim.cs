using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

//Monobehaviour script that manages the behaviour of a Sim entity in the simulation.
public class Sim : MonoBehaviour
{
    //Text element used to show the name of the Sim
    public TMP_Text nameText;

    //Text element used to show the traits of the Sim
    public TMP_Text traitsText;

    //Prefab of the needs visual used for visualizing the needs of the Sim
    public GameObject needVisualPrefab;

    //Transform used to hold the need visuals that are instantiated
    public Transform needVisualsHolder;

    //Transform use to hold the emote visuals
    public Emote emote;

    //The visualization of the body - used for updating what the Sim looks like
    public SpriteRenderer bodyVisualRenderer;

    //How fast the Sim can move
    public float speed;

    //How often the Sim changes direction
    public float moveDirectionChangeFrequency;

    //The time used to keep track of when the Sim should change directions
    public float timeSinceLastDirectionChange;

    public float interactionRadius;

    [SerializeField]
    private float arrivalDistance;

    //The data used to initialize and manage the behaviour of the Sim
    private SimData simData;

    //A dictionary used to manage the visuals of the need as the character updates over time.
    private Dictionary<NeedData.NeedType, NeedVisual> needVisualMap = new Dictionary<NeedData.NeedType, NeedVisual>();

    private List<Transform> waypoints = new List<Transform>();
    private int currentWaypointIndex = 0;


    //Takes in data and initializes the properties of this particular Sim entity.
    public void Initialize(SimData inSimData, List<Transform> inWaypoints)
    {
        
        simData = inSimData;
        waypoints = inWaypoints;
        bodyVisualRenderer.color = simData.simColour;
        nameText.text = simData.simName;
        nameText.color = simData.simColour;

        string traitsValue = "";
        foreach(SimData.Trait trait in simData.traits)
        {
            traitsValue += trait.ToString() + ", ";
        }
        traitsText.text = traitsValue;

        needVisualMap.Clear();

        //Iterates over each of the needs for the sim
        foreach (KeyValuePair<NeedData.NeedType, NeedData> need in simData.needsMap)
        {
            GameObject needObject = Instantiate(needVisualPrefab, needVisualsHolder);
            NeedVisual needVisual = needObject.GetComponent<NeedVisual>();
            if(need.Value.initialValue > 0f && need.Value.decrementValue < 0f)
            {
                //making sure that the need data is initialized before using it
                if(!need.Value.IsInitialized)
                {
                    need.Value.Initialize();
                }

                //Initializes the need and adds it to the visual map so that it can be easily accessed later
                needVisual.Initialize(need);
                needVisualMap.Add(need.Key, needVisual);
            }
        }
    }


    private void Update()
    {
        //Move the sim in the direction that it's currently moving in
        transform.position += (waypoints[currentWaypointIndex].position - transform.position).normalized * speed * Time.deltaTime;

        //Iterate over each of the needs
        foreach (NeedData need in simData.needsMap.Values)
        {
            //Decrement the value of the need
            need.currentValue += need.decrementValue * Time.deltaTime;

            //Handling for the character's hunger
            bool needRequiresExtraChecks = need.needType == NeedData.NeedType.hunger || need.needType == NeedData.NeedType.social;
            if (needRequiresExtraChecks)
            {
                if(!CheckNeed(need))
                {
                    continue;
                }
            }
        }

        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(transform.position, interactionRadius);
        foreach(Collider2D detectedObject in detectedObjects)
        {
            IInteractable interactable = detectedObject.GetComponent<IInteractable>();
            if(interactable != null)
            {
                interactable.Interact(this);
            }
        }


        //If the frequency threshold is reached then the timer resets and the direction of movement is changed.
        if (Vector3.Distance(waypoints[currentWaypointIndex].position, transform.position) < arrivalDistance)
        {
            timeSinceLastDirectionChange += Time.deltaTime;
            if (timeSinceLastDirectionChange > moveDirectionChangeFrequency)
            {
                currentWaypointIndex++;
                if(currentWaypointIndex >= waypoints.Count)
                {
                    currentWaypointIndex = 0;
                }
                timeSinceLastDirectionChange = 0f;
            }
        }

    }

    private bool CheckNeed(NeedData need)
    {
        //If the current hunger is less than 0 then the Sim should be destroyed
        if (need.currentValue <= 0)
        {
            
            //Destroy(gameObject);
            return false;
        }
        bool needShouldShowWarning = need.hasWarning && need.currentValue <= SimManager.NeedWarningThreshold && needVisualMap.ContainsKey(need.needType);

        if (needShouldShowWarning)
        {
            needVisualMap[need.needType].ShowWarning();
        }
        else
        {
            needVisualMap[need.needType].HideWarning();
        }
        
        return true;
    }

    public bool IsNeedLow(NeedData.NeedType needType)
    {
        if(simData.needsMap.ContainsKey(needType))
        {
            return simData.needsMap[needType].currentValue < SimManager.NeedWarningThreshold;
        }
        return false;
    }

    public void IncreaseNeed(NeedData.NeedType needType, float increment)
    {
        if (simData.needsMap.ContainsKey(needType))
        {
            simData.needsMap[needType].IncreaseValue(increment);
        }
    }

    private void OnMouseDown()
    {
        //Get a default need value: if nothing else required, Sims just wanna have fun ;-)
        float mostNeedValue = 0;
        NeedData.NeedType mostNeed = NeedData.NeedType.fun;

        //iterate through the needs map to check what is most lacking for the sim right now
        foreach (KeyValuePair<NeedData.NeedType, NeedData> n in simData.needsMap)
        {
            if (n.Value.currentValue > mostNeedValue)
            {
                mostNeedValue = n.Value.currentValue;
                mostNeed = n.Key;
            }
        }

        //Show Sim's current top need
        emote.ShowEmote(mostNeed);
    }
}
