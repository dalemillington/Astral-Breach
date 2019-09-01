using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    // Configuration parameters
    [SerializeField] AudioClip breakSound;
    [SerializeField] int maxHits;
    [SerializeField] Sprite[] hitSprites;
    [SerializeField] GameObject[] blockImpactVFX;

    // Cached reference
    Level level;

    // State variables
    [SerializeField] int timesHit = 0; //TODO only serialized for debug purposes

    private void Start()
    {
        CountBreakableBlocks();
    }

    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<Level>();
        if (tag == "Breakable")
        {
            level.CountBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "Breakable")
        {
            HandleHit();
        }
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = timesHit - 1;
        GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        TriggerEchoesVFX();
    }

    private void HandleHit()
    {
        timesHit++;
        if (timesHit >= maxHits)
        {
            DestroyBlock();
        }
        else
        {
            ShowNextHitSprite();
        }
    }

    private void DestroyBlock()
    {
        PlayBlockDestroySFX();
        Destroy(gameObject);
        level.BlockDestroyed();
        TriggerEchoesVFX();
    }

    private void PlayBlockDestroySFX()
    {
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
        FindObjectOfType<GameSession>().AddToScore();
    }

    private void TriggerEchoesVFX()
    {
        int blockVFXIndex = timesHit - 1;
        GameObject echo = Instantiate(blockImpactVFX[blockVFXIndex], transform.position, transform.rotation);
        Destroy(echo, 1f);
    }
}
