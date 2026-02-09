using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour
{
    [Header("Inscribed")]
    //Prefab for instantiating apples
    public GameObject applePrefab;
    
    //Prefab for instantiating branches
    public GameObject branchPrefab;

    //Speed at which the apple tree moves
    public float baseSpeed = 10f;

    //Distance where the apple tree turns around
    public float leftAndRightEdge = 24f;

    // Chance that the apple tree will change directions
    public float changeDirChance = 0.1f;

    // Seconds between apple instantiations
    public float appleDropDelay = 1f;
    
    // Seconds between branch instantiations (much longer than apples)
    public float branchDropDelay = 10f;
    private bool gameOver = false;
    private float currentSpeed;
    private float lastBranchDropTime = -100f; // Track when last branch was dropped
    
    // Start is called before the first frame update
    void Start(){
        currentSpeed = baseSpeed;
        // Start dropping apples (offset to 2.5s so they don't align with branches at 3s, 13s, 23s...)
        Invoke("DropApple", 2.5f);
        // Start dropping branches (every 10 seconds starting at 3 seconds)
        Invoke("DropBranch", 3f);
    }

    void DropApple(){
        if (gameOver) return; // Don't drop apples if game is over
        
        // If a branch was just dropped (within last 1 second), skip this apple
        if (Time.time - lastBranchDropTime <= 1f)
        {
            // Reschedule this apple for later
            Invoke("DropApple", appleDropDelay);
            return;
        }
        
        GameObject apple = Instantiate<GameObject>(applePrefab);
        apple.transform.position = transform.position;
        
        Invoke("DropApple", appleDropDelay);
    }
    
    void DropBranch(){
        if (gameOver) return; // Don't drop branches if game is over
        
        lastBranchDropTime = Time.time; // Record when this branch was dropped
        
        GameObject branch = Instantiate<GameObject>(branchPrefab);
        branch.transform.position = transform.position;
        
        Invoke("DropBranch", branchDropDelay);
    }
    public void StopDropping()
    {
        gameOver = true;
        CancelInvoke("DropApple");
        CancelInvoke("DropBranch");
    }
    
    public void SetSpeedMultiplier(float multiplier)
    {
        currentSpeed = baseSpeed * multiplier;
    }

    // Update is called once per frame
    void Update()
    {
        // Basic movement
        Vector3 pos = transform.position; 
        pos.x += currentSpeed * Time.deltaTime;
        transform.position = pos;

        //Changing direction
        if (pos.x < -leftAndRightEdge){
            currentSpeed = Mathf.Abs(currentSpeed); //Move right
        }
        else if (pos.x > leftAndRightEdge){
            currentSpeed = -Mathf.Abs(currentSpeed); //Move left
        }
        // else if (Random.value < changeDirChance){
        //     currentSpeed *= -1; //Change direction
        // }
    }
    void FixedUpdate(){
        // Random direction changes are now timed-based
        if (Random.value < changeDirChance){
            currentSpeed *= -1; //Change direction
        }
    }
}

