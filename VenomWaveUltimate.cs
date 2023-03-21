using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VenomWaveUltimate : MonoBehaviour
    // This script handles the Venom wave Ultimate skill. It's connected to the Venom Wave Ultimate prefab and can be placed in the scene.
    // The wave begins at the end of the path (where enemies are moving towards) and follows the waypoint objects to the beginning of the path.
    // Enemies get one hit dmg and then a dot.
{
    [Tooltip("Waypoints of a path.")]
    public List<Transform> waypointsList;

    private int hitDMG = 5, dotDMG = 2; // Dmg values of the ultimate
    private float speed = 3.0f;
    private Vector2 target; // where is going next
    private Vector2 waveStart; // starting point of the wave, where the Wave object has been dragged to in the scene
    public bool activated; // This tracks if the ultimate is activated
    //private TrailRenderer trail; // This is needed to activate and deactivare the rendered so that it doesnt show when it's moved back to the starting point
    private int waypointNumber; // Every time a waypoint is passed this value gets increased
    private GameObject waypoints; // GameObject containing waypoint info
    public GameObject waveUpDown, waveLeftRight;
    private Component[] childSprites;

    void Start()
    {
        waveStart = transform.position; // setting the wavestart to the location where object have been set
        waypointNumber = 0; // starting from the first (0 index) waypoint
        waypoints = GameObject.Find("Waypoints");
        if (waypoints != null)
        {
            target = GetFirstWaypoint(0);
        }
        else
        {
            Debug.Log("ERROR: " + gameObject.name + " did not find Waypoints gameobject");
        }

        childSprites = GetComponentsInChildren<SpriteRenderer>();

        activated = false; // ultimate is not activated 
    }

    // Update is called once per frame
    void Update()
    {
        if (activated) // the boolean gets changed when ultimate button is pressed
        {
            Movement();
        }
    }

    public void ActivateVenomWave() // this function is called in leader guardian button pressing
    {
        activated = true;
    }

    void Movement()
    {
        float step = speed * Time.deltaTime;
        // move sprite towards the target location
        transform.position = Vector2.MoveTowards(transform.position, target, step);
        if (Vector2.Distance(transform.position, target) < 0.05f)
        { // Is close enough to the waypoint we ask for the next waypoint.
            target = GetNextWaypoint(waypointNumber);
            waypointNumber++;
        }

        Vector2 currentDirection = ((Vector2)transform.position - target).normalized;
        if ((Mathf.Abs(currentDirection.y) < Mathf.Abs(currentDirection.x)))
        {
            waveUpDown.SetActive(false);
            waveLeftRight.SetActive(true);
            if (transform.position.x < target.x)
            {
                Debug.Log("menee oikealle");
                //flippaa kaikki spritet
                foreach (SpriteRenderer sprite in childSprites)
                {
                    Debug.Log("flip X FALSE");
                    sprite.flipX = false;
                    sprite.flipY = false;
                }

            }
            else
            {
                Debug.Log("menee vasemmalle");
                //flippaa kaikki spritet
                foreach (SpriteRenderer sprite in childSprites)
                {
                    Debug.Log("flip X TRUE");
                    sprite.flipX = true;
                    sprite.flipY = false;
                }
            }

        }
        else
        {
            if (transform.position.y < target.y)
            {
                Debug.Log("menee ylöspäin");
                //flippaa kaikki spritet
                foreach (SpriteRenderer sprite in childSprites)
                {
                    Debug.Log("flip Y FALSE");
                    sprite.flipY = false;
                    sprite.flipX = false;
                }
            }
            else
            {
                Debug.Log("menee alaspäin");
                //flippaa kaikki spritet
                foreach (SpriteRenderer sprite in childSprites)
                {
                    Debug.Log("flip Y TRUE");
                    sprite.flipY = true;
                    sprite.flipX = false;
                }
            }

            waveUpDown.SetActive(true);
            waveLeftRight.SetActive(false);
        }
    }


    // Trail gets the position of the first waypoint for their pathfinding
    public Vector2 GetFirstWaypoint(int wayPointNumber)
    {
        if (waypointsList != null)
        {
            return waypointsList[0].position;
        }
        else
        {
            Debug.Log("ERROR: GetFirstWaypoint() had nothing to return");
            return new Vector2(0, 0);
        }

    }

    //Trail gets the position of the next waypoint for their pathfinding
    public Vector2 GetNextWaypoint(int currentWaypoint)
    {
        Debug.Log("current venom wave waypoint: " + currentWaypoint);
        if (currentWaypoint + 1 < waypointsList.Count)
        {
            Transform tempWaypoint = waypointsList[currentWaypoint + 1];
            
                Vector2 nextWaypoint = new Vector2(tempWaypoint.position.x, tempWaypoint.position.y);
                return nextWaypoint;
        }
        else // no more waypoints, the wave has gone through all
        {
            Debug.Log("Reached Last waypoint, calling for reset.");
            StartCoroutine(ResetVenomWave());
            return target;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyArmored" || collision.gameObject.tag == "EnemyMagical") // If the venom wave hits these tags
        {
            Transform t_enemy = collision.gameObject.transform;
            GameObject g_enemy = collision.gameObject;

            Debug.Log("One hit dmg on enemy: " + g_enemy);
            g_enemy.GetComponent<Attacker>().ChangeHealth(hitDMG);

            //target can have only one dot
            if (!g_enemy.GetComponent<Attacker>().dotted)
            {
                Debug.Log("dotted enemy: " + g_enemy);
                g_enemy.GetComponent<Attacker>().StartCoroutine(g_enemy.GetComponent<Attacker>().DamageOverTime(t_enemy, dotDMG, 2));
            }
        }

    }

    IEnumerator ResetVenomWave() // This coroutine deactivates the ultimate and returns it to its original position and resets the values so it can be activated again
    {
        Debug.Log("Resetting the venom wave.");
        activated = false;
        //gameObject.GetComponent<TrailRenderer>().emitting = false;
        
        yield return new WaitForSeconds(3f);
        transform.position = waveStart;
        waypointNumber = 0;
        target = GetFirstWaypoint(0);

    }
}
