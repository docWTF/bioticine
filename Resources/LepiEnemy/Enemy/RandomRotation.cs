using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UIElements;

public class RandomRotation : MonoBehaviour
{
    private float randomRotation;
    private float randomSteering;

    public float steerInterval;
    public float steerIntensity;

    public float rotationInterval;
    public float rotationIntensity;


    private void Start()
    {
        this.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, Random.Range(0f, 360f)));
        StartCoroutine(RandomSteeringInInterval());
        StartCoroutine(RandomRotationInInterval());
    }
    // Update is called once per frame
    void Update()
    { 
        Vector3 rotation = new Vector3(0f, 0f, 360f);
        transform.Rotate(rotation * Time.deltaTime * (randomRotation + randomSteering));
    }

    IEnumerator RandomSteeringInInterval()
    {
        while (true)
        {
            randomSteering = Random.Range(-steerIntensity, steerIntensity);
            yield return new WaitForSeconds(0.5f);
            randomSteering = 0f;
            yield return new WaitForSeconds(steerInterval);
        }
    }

    IEnumerator RandomRotationInInterval()
    {
        while (true)
        {
            randomRotation = Random.Range(-rotationIntensity, rotationIntensity);
            yield return new WaitForSeconds(Random.Range(0.5f,1.5f));
            randomRotation = 0f;
            yield return new WaitForSeconds(rotationInterval);
        }
    }
}
