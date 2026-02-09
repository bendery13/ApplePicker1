using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{
    private ScoreCounter scoreCounter;
    // Start is called before the first frame update
    void Start(){
        
        // Find a GameObject named ScoreCounter
        GameObject scoreGO = GameObject.Find("ScoreCounter");

        // Get the ScoreCounter(Script) component of that GameObject
        scoreCounter = scoreGO.GetComponent<ScoreCounter>();
    }

    // Update is called once per frame
    void Update(){
        // Get the current screen position of the mouse from Input
        Vector3 mousePos2D = Input.mousePosition;

        // The Camera's z position sets how far to push the mouse into 3D
        mousePos2D.z = -Camera.main.transform.position.z;

        // Convert the point from 2D screen space into 3D game world space
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        // Move the x position of the basket to the x position of the mouse
        Vector3 pos = this.transform.position;
        pos.x = mousePos3D.x;
        this.transform.position = pos;
    }

    void OnCollisionEnter(Collision coll){
        // Find out what hit this basket
        GameObject collidedWith = coll.gameObject;
        if (collidedWith.CompareTag("Apple")){
            Destroy(collidedWith);
            // Increase the score
            scoreCounter.score += 100;
            HighScore.TRY_SET_HIGH_SCORE(scoreCounter.score);
        }
        else if (collidedWith.CompareTag("Branch")){
            // Branch caught! End the game
            //Destroy(collidedWith); // Destroy the branch
            //ApplePicker apScript = Camera.main.GetComponent<ApplePicker>();
            //apScript.AppleDestroyed(); // Trigger game over (lose a basket)
            //apScript.appleTree.StopDropping();

            // New: Show game over screen and pause time
            ApplePicker apScript = Camera.main.GetComponent<ApplePicker>();
            apScript.GameOverScreen.Setup(scoreCounter.score);
            Time.timeScale = 0f;
        }
    }
}
