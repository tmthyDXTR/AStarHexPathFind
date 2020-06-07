using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This script handles the game state, like player and enemy turn
///
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager current;

    public enum GameState
    {
        PlayerTurn,
        EnemyTurn,
    }

    [SerializeField]
    public GameState gameState;

    void Awake()
    {        
            current = this;        
    }


    public void EndTurn()
    {
        if (GameManager.current.gameState == GameState.PlayerTurn)
        {
            ResourceManager.EndBurn(1);
            GameManager.current.gameState = GameState.EnemyTurn;
            Talker.TypeThis("The darkness is slowly creeping in...");

            Debug.Log("GameState: End Player Turn");
        }
        else
        {
            GameManager.current.gameState = GameState.PlayerTurn;
            Debug.Log("GameState: End Enemy Turn");
            Talker.TypeThis("Your move");
        }

    }
}
