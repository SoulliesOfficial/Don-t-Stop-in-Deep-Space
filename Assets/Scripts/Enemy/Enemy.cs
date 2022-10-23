using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;

    public Collider2D substancialArea, hitArea;

    public void Hurt(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public static void GenerateEnemy(string enemyName, SpaceRoom spaceRoom, Vector2 position)
    {
        GameObject enemy = GameManager.gameManager.enemyDictionary.GetValueOrDefault(enemyName);
        if (enemy != null)
        {
            LeanPool.Spawn(enemy, spaceRoom.roomCenter + position, Quaternion.identity);
        }
    }
}

[System.Serializable]
public class Enemy_Save
{
    public string enemyName;
    public Vector2 position;

    public Enemy_Save() { }

    public Enemy_Save(string enemyName, Vector2 position)
    {
        this.enemyName = enemyName;
        this.position = position;
    }
}