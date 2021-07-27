using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISystem : MonoBehaviour
{
    public const string NUM_SPAWNS_KEY = "NUM_SPAWNS_KEY";

    [SerializeField] private InputField spawnCountInput;
    [SerializeField] private Button micaButton;
    [SerializeField] private Button theaButton;
    [SerializeField] private Button debbieButton;
    [SerializeField] private Button clearButton;
    private int spawnCount;

    void Start()
    {
        spawnCount = 0;
        spawnCountInput.onEndEdit.AddListener(SetCount);

        micaButton.onClick.AddListener(delegate{SpawnBroadcast(EventNames.S12_Events.ON_SPAWN_BUTTON_CLICKED);});
        micaButton.onClick.AddListener(delegate{SpawnBroadcast(EventNames.S12_Events.ON_SPAWN_MICA_BUTTON_CLICKED);});

        theaButton.onClick.AddListener(delegate{SpawnBroadcast(EventNames.S12_Events.ON_SPAWN_BUTTON_CLICKED);});
        theaButton.onClick.AddListener(delegate{SpawnBroadcast(EventNames.S12_Events.ON_SPAWN_THEA_BUTTON_CLICKED);});

        debbieButton.onClick.AddListener(delegate{SpawnBroadcast(EventNames.S12_Events.ON_SPAWN_DEBBIE_BUTTON_CLICKED);});

        clearButton.onClick.AddListener(ClearBroadcast);
    }

    /**
        Parses text in input field and assigns it to spawnCount as int
        Params: string count - text from input field
    */
    private void SetCount(string count)
    {
        try
        {
            int numVal = Int32.Parse(count);
            this.spawnCount = numVal;
            Debug.Log("Count reassigned to: " + numVal);
        }
        catch (FormatException)
        {
            this.spawnCount = 0;
            Debug.Log("Cannot parse: " + count + ", count reset to 0");
        }
    }

    /**
        Broadcast template for spawn buttons, to be delegated as listener
        Params: string eventKey - event key to broadcast
    */
    private void SpawnBroadcast(string eventKey)
    {
        Parameters spawnParams = new Parameters();

        spawnParams.PutExtra(NUM_SPAWNS_KEY, spawnCount);

        EventBroadcaster.Instance.PostEvent(eventKey, spawnParams);

        Debug.Log("Broadcasting: " + eventKey);
    }

    /**
        Broadcasts clear event
    */
    private void ClearBroadcast()
    {
        string eventKey = EventNames.S12_Events.ON_CLEAR_BUTTON_CLICKED;
        EventBroadcaster.Instance.PostEvent(eventKey);

        Debug.Log("Broadcasting: " + eventKey);
    }
}
