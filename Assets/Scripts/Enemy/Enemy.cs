using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //audio
    public AudioClip deathSound;
    public AudioSource enemyAudioSource;
    

    public float health;

    public Vector2 spawnPosition;

    public Collider2D substancialArea, hitArea;

    public SpaceRoom room;

    public virtual void Hurt(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            room.enemies.Remove(this);
            if(room.enemies.Count == 0)
            {
                for(int i = 0; i < room.wormholes.Count; i++)
                {
                    room.wormholes[i].GetComponent<SpriteRenderer>().color = Color.white;
                    room.wormholes[i].GetComponent<BoxCollider2D>().enabled = true;
                }
            }
            //add death sound effect
            Destroy(gameObject);
        }
    }

    public static Enemy GenerateEnemy(string enemyName, SpaceRoom spaceRoom, Vector2 position)
    {
        GameObject enemy = GameManager.gameManager.enemyDictionary.GetValueOrDefault(enemyName);
        if (enemy != null)
        {
            Enemy e = LeanPool.Spawn(enemy, spaceRoom.roomCenter + position, Quaternion.identity).GetComponent<Enemy>();
            e.spawnPosition = spaceRoom.roomCenter + position;
            e.room = spaceRoom;
            return e;
        }
        return null;
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