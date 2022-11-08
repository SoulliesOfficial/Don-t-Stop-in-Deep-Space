using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //audio
    public AudioSource enemyAudioSource;
    // public AudioSource playerAudioSource;
    public AudioClip e_deathSound;
    public AudioClip e_hitSound;
    public AudioClip e_shootSound;
    public AudioClip e_dashSound;
    public bool playedSound = true;

    
    

    public float health;

    public Vector2 spawnPosition;

    public Collider2D substancialArea, hitArea;

    public SpaceRoom room;

    public virtual void Hurt(float damage)
    {
        enemyAudioSource.PlayOneShot(e_hitSound);
        health -= damage;

        if (health <= 0)
        {
            //add death sound effect
            GameObject playerAudio = GameObject.FindWithTag("Player");
            playerAudio.GetComponent<AudioSource>().PlayOneShot(e_deathSound);


            room.enemies.Remove(this);
            room.enemies.RemoveAll(x => x == null);

            bool close = true;
            foreach (Enemy e in room.enemies)
            {
                if (e.GetComponent<Enemy>() != null && e.GetComponent<EnemySpawnPoint>() == null)
                {
                    close = false;
                }
            }

            if (close)
            {
                foreach (Enemy e in room.enemies)
                {
                    room.enemies.Remove(e);
                    Destroy(e);
                }
            }

            if (room.enemies.Count == 0)
            {
                for(int i = 0; i < room.wormholes.Count; i++)
                {
                    room.wormholes[i].GetComponent<SpriteRenderer>().color = Color.white;
                    room.wormholes[i].GetComponent<BoxCollider2D>().enabled = true;
                }
            }


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