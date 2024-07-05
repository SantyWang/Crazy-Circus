using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{

    public GameObject cratePrefab;


    public GameObject playerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        var playerStart = GameObject.Find("PlayerStart");
        if (playerStart != null )
        {
            var player = Instantiate(playerPrefab);
            player.transform.localPosition = playerStart.transform.localPosition;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
