using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;


public class AgentsController : MonoBehaviour
{
    public GameObject agent1;
    public GameObject agent2;
    public GameingUIInventory gameing;
    private RiruAI agent1Script;
    private PlayerControllerAI agent2Script;
    public PlayerHealthAI healthAI;
    public CastSpellAI cast;
    void Start()
    {
        agent1Script = agent1.GetComponent<RiruAI>();
        agent2Script = agent2.GetComponent<PlayerControllerAI>();
    }

    void Update()
    {
        if (agent1Script.health <= 0 || healthAI.health <= 0)
        {
            ResetEnvironment();
        }
    }

    public void ResetEnvironment()
    {
        agent1Script.EndEpisode();
        agent2Script.EndEpisode();

        agent1Script.health = 500;
        gameing.HP = gameing.HP_MAX;
        healthAI.health = gameing.HP_MAX;
        agent2Script.health = gameing.HP_MAX;
        cast.ManaPoint = gameing.MP_MAX;


        agent1.transform.position = GetRandomStartPosition();
        agent2.transform.position = GetRandomStartPosition();
    }

    private Vector3 GetRandomStartPosition()
    {
        return new Vector3(Random.Range(-5.0f, 5.0f), 0, Random.Range(-5.0f, 5.0f));
    }
}
