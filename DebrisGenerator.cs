using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DebrisGenerator : ObjectPool
{
    [SerializeField]
    private float flyDuration = .3f;
    [SerializeField]
    private float flyStrength = 1f;
    [SerializeField]
    private int debrisCount = 5;
    [SerializeField]
    private float audioVolume = 0.05f;
    [SerializeField]
    private AudioSource audioSource = null;

    public void SpawnDebris()
    {
        for (int i = 0; i < debrisCount; i++)
        {
            var debris = SpawnObject();
            MoveDebrisInRandomDirection(debris);
        }
    }

    private void MoveDebrisInRandomDirection(GameObject debris)
    {
        debris.transform.DOComplete();
        var randomDirection = Random.insideUnitCircle;
        randomDirection = randomDirection.y > 0 ? new Vector2(randomDirection.x, -randomDirection.y) : randomDirection;
        audioSource.volume = audioVolume;
        debris.transform.DOMove(((Vector2)transform.position + randomDirection) * flyStrength, flyDuration).OnComplete(() => audioSource.Play());
        debris.transform.DORotate(new Vector3(0, 0, Random.Range(0f, 360f)), flyDuration);
    }
}
