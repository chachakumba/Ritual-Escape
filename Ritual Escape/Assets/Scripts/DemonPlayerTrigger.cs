using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonPlayerTrigger : MonoBehaviour
{
    Demon demon;
    Player player;
    bool searchingPlayer;
    private void Awake()
    {
        demon = GetComponentInParent<Demon>();
    }
    private void Start()
    {
        player = Manager.instance.player;
    }

    private void OnTriggerStay(Collider other)
    {
        RaycastHit hit = new RaycastHit();
        demon.eyes.LookAt(player.transform);
        //Physics.Raycast(demon.eyes.position, demon.eyes.forward, out hit, demon.foundRadius, demon.playerAndWallsLayer);
        Physics.Raycast(demon.eyes.position, player.transform.position - demon.eyes.position, out hit, demon.foundRadius, demon.playerAndWallsLayer);
        if(hit.collider!=null)
        if (hit.collider.CompareTag("Player"))
        {
            Debug.LogWarning("Found");
                demon.SetStatus(Demon.DemonState.found);
        }
    }

}
