using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSpawn : MonoBehaviour
{
    [SerializeField] private GameObject ghost;
    [SerializeField] private List<GameObject> ghosts;

    public const string NUM_SPAWNS_KEY = "NUM_SPAWNS_KEY";

    // Start is called before the first frame update
    void Start()
    {
        this.ghost.SetActive(false);
        EventBroadcaster.Instance.AddObserver(EventNames.S12_Events.ON_SPAWN_THEA_BUTTON_CLICKED, this.OnSpawnEvent);
        EventBroadcaster.Instance.AddObserver(EventNames.S12_Events.ON_CLEAR_BUTTON_CLICKED, this.OnClearEvent);
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.S12_Events.ON_SPAWN_THEA_BUTTON_CLICKED);
    }

    private void OnSpawnEvent(Parameters parameters)
    {
        int nSpawns = parameters.GetIntExtra(NUM_SPAWNS_KEY, 1);

        for (int i = 0; i < nSpawns; i++)
        {
            GameObject copy = GameObject.Instantiate(this.ghost, this.transform.parent);
            copy.SetActive(true);
            this.ghosts.Add(copy);
        }
    }

    private void OnClearEvent()
    {
        for (int i = 0; i < this.ghosts.Count; i++)
        {
            GameObject.Destroy(this.ghosts[i]);
        }
        this.ghosts.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
