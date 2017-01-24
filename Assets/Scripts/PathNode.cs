using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode : MonoBehaviour
{
    public PathNode NextPathDot;
    public Enemy Enemy;
    public string text;

    private Enemy SpawnedEnemy;

    public PathNode GetNextNode()
    {
        return NextPathDot;
    }

    // Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (Enemy != null)
        {
            if (SpawnedEnemy == null)
            {
                SpawnedEnemy = Instantiate(Enemy, transform.position, transform.rotation) as Enemy;
                SpawnedEnemy.EnemyPath = this;
            }
            
        }
	}
}
