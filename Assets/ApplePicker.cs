using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ApplePicker : MonoBehaviour
{
    public GameOverScreen GameOverScreen;
    private ScoreCounter scoreCounter;
    private AppleTree appleTree;
    private Text roundText;

    
    [Header("Inscribed")]
    public GameObject basketPrefab;
    public int numBaskets = 4;
    public float basketBottomY = -14f;
    public float basketSpacingY = 2f;
    public List<GameObject> basketList;
    
    [Header("Round Settings")]
    public int[] roundThresholds = { 0, 2000, 5000, 10000 }; // Score thresholds for rounds 1-4
    public float[] speedMultipliers = { 1f, 1.5f, 2.5f, 3.5f }; // Speed multiplier for each round
    private int currentRound = 1;
    // Start is called before the first frame update
    void Start(){
        // Find and store reference to ScoreCounter
        GameObject scoreGO = GameObject.Find("ScoreCounter");
        scoreCounter = scoreGO.GetComponent<ScoreCounter>();
        
        // Find and store reference to AppleTree
        appleTree = FindObjectOfType<AppleTree>();
        
        // Find and store reference to Round Text
        GameObject roundGO = GameObject.Find("RoundText");
        if (roundGO != null)
        {
            roundText = roundGO.GetComponent<Text>();
        }
        
        basketList = new List<GameObject>();
        for (int i = 0; i < numBaskets; i++){
           GameObject tBasketGO = Instantiate<GameObject>(basketPrefab);
           Vector3 pos = Vector3.zero;
            pos.y = basketBottomY + (basketSpacingY * i); 
            tBasketGO.transform.position = pos;
            basketList.Add(tBasketGO);
        }
    }
    public void AppleDestroyed()
    {
        // Destroy all of the falling apples
        GameObject[] appleArray = GameObject.FindGameObjectsWithTag("Apple");
        foreach (GameObject tempGo in appleArray)
        {
            Destroy(tempGo);
        }
        // Remove a basket
        // Get the index of the last basket in basketList
        int basketIndex = basketList.Count - 1;
        // Get a reference to that basket GameObject
        GameObject basketGO = basketList[basketIndex];
        // Remove the basket from the list and destroy the GameObject
        basketList.RemoveAt(basketIndex);
        Destroy(basketGO);
        // If there are no more baskets, restart the game
        if (basketList.Count == 0)
        {
            appleTree.StopDropping();
            GameOverScreen.Setup(scoreCounter.score); //SceneManager.LoadScene("_Scene_0");
        }
    }
    
    int GetRoundFromScore(int score)
    {
        for (int i = roundThresholds.Length - 1; i >= 0; i--)
        {
            if (score >= roundThresholds[i])
            {
                return i + 1; // Rounds are 1-indexed
            }
        }
        return 1;
    }
    
    void UpdateRound()
    {
        int newRound = GetRoundFromScore(scoreCounter.score);
        
        if (newRound != currentRound)
        {
            currentRound = newRound;
            Debug.Log("Round changed to: " + currentRound);
            
            // Apply speed multiplier for the new round
            int roundIndex = currentRound - 1;
            appleTree.SetSpeedMultiplier(speedMultipliers[roundIndex]);
            
            // Update UI text
            if (roundText != null)
            {
                roundText.text = "Round " + currentRound;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRound();
    }
}
