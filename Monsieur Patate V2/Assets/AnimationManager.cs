using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class AnimationManager : MonoBehaviour
{
    [SerializeField] private float delayTime = 0.4f;
    private float timeBetweenBlinks = 2f;
    [SerializeField] private float timeBetweenBlinksMin = 0.8f;
    [SerializeField] private float timeBetweenBlinksMax = 3f;
    [SerializeField] private float blinkDuration = 0.1f;

    [SerializeField] private GameObject eye1;
    [SerializeField] private GameObject eye2;
    

    private void Start() {
        StartCoroutine(Blinker());
        this.timeBetweenBlinks = Random.Range(this.timeBetweenBlinksMin, this.timeBetweenBlinksMax);
    }

    private IEnumerator Blinker() {
        while (true) {
            yield return new WaitForSeconds(this.timeBetweenBlinks);
            this.Blink();
            yield return new WaitForSeconds(blinkDuration);
            this.Blink();
            this.timeBetweenBlinks = Random.Range(this.timeBetweenBlinksMin, this.timeBetweenBlinksMax);
        }
    }

    private void Blink(bool isDelayed = false) {
        this.eye1.SetActive(!this.eye1.activeSelf);
        this.eye2.SetActive(!this.eye2.activeSelf);
    }
}
