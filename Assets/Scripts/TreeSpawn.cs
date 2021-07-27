using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawn : MonoBehaviour
{
    [SerializeField] private GameObject tree;
    [SerializeField] private List<GameObject> treeList;

    public const string NUM_SPAWNS_KEY = "NUM_SPAWNS_KEY";

    // Start is called before the first frame update
    void Start()
    {
        this.tree.SetActive(false);
        EventBroadcaster.Instance.AddObserver(EventNames.S12_Events.ON_SPAWN_MICA_BUTTON_CLICKED, this.OnSpawnEvent);
        EventBroadcaster.Instance.AddObserver(EventNames.S12_Events.ON_CLEAR_BUTTON_CLICKED, this.OnClearEvent);
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.S12_Events.ON_SPAWN_BUTTON_CLICKED);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnSpawnEvent(Parameters parameters)
    {
        int numberSpawns = parameters.GetIntExtra(NUM_SPAWNS_KEY, 1);

        for (int i = 0; i < numberSpawns; i++)
        {
            GameObject copy = GameObject.Instantiate(tree, this.transform);
            copy.SetActive(true);
            this.treeList.Add(copy);
        }

    }
    private void OnClearEvent()
    {
        for (int i = 0; i < this.treeList.Count; i++)
        {
            GameObject.Destroy(this.treeList[i]);
        }
        this.treeList.Clear();
    }
}
