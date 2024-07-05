using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject playerPrefab2;
    [HideInInspector]
    public PlayerController player;
    [HideInInspector]
    public PlayerController player2;

    // Start is called before the first frame update
    void Awake()
    {
        var playerStart1 = GameObject.Find("PlayerStart1");
        if (playerStart1 != null )
        {
            this.player = Instantiate(playerPrefab).GetComponent<PlayerController>();
            this.player.transform.localPosition = playerStart1.transform.localPosition;
            this.player.transform.rotation = playerStart1.transform.rotation;
        }
        var playerStart2 = GameObject.Find("PlayerStart2");
        if (playerStart2 != null)
        {
            this.player2 = Instantiate(playerPrefab2).GetComponent<PlayerController>();
            this.player2.transform.localPosition = playerStart2.transform.localPosition;
            this.player2.transform.rotation = playerStart2.transform.rotation;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
