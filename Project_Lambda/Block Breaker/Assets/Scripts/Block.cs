using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    //Config
    [SerializeField] AudioClip[] breakSounds;
    [SerializeField] GameObject sparksEffect;
    [SerializeField] Sprite[] hitSprites;

    //Cached Reference
    GameStatus gameStatus;
    Level level;

    // State Variables
    [SerializeField] int timesHit; // Only Serialized for Debugging

    private void Start()
    {
        gameStatus = FindObjectOfType<GameStatus>();
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

    private void HandleHit()
    {
        timesHit++;
        int maxHits = hitSprites.Length + 1;

        if (timesHit >= maxHits)
        {
            DestroyBlock();
        }
        else
        {
            ShowNextHitSprites();
        }
    }

    private void ShowNextHitSprites()
    {
        int spriteIndex = timesHit - 1;
        if (hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else
        {
            Debug.LogError("Block Sprite is missing from array" + gameObject.name);
        }
    }

    private void DestroyBlock()
    {
        PlayBlockDestroySFX();
        level.BlockDestroyed();
        gameStatus.IncreasePlayerScore();
        TriggerParticleEffect();
        Destroy(gameObject);
    }

    private void PlayBlockDestroySFX()
    {
        AudioClip clip = breakSounds[UnityEngine.Random.Range(0, breakSounds.Length)];
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, 0.05f);
    }

    private void TriggerParticleEffect()
    {
        GameObject sparkles = Instantiate(sparksEffect, transform.position, transform.rotation);
        Destroy(sparkles, 2f);
    }
}