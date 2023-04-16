using UnityEngine;
using UnityEngine.Events;

public class WinZoneController : MonoBehaviour
{
    public GameObject ant1;
    public GameObject ant2;
    public AntController antController1;
    public AntController antController2;
    public AudioSource myAudioSource;

    public float distanceThreshold = .2f;
    public UnityEvent foodStolenEvent;

    private void Start()
    {
        antController1 = ant1.GetComponent<AntController>();
        antController2 = ant2.GetComponent<AntController>();
    }

    private void Update()
    {
        if (antController1.alreadyGrabbed)
        {
            float distance = Vector3.Distance(this.transform.position, ant1.transform.position);

            if (distance <= distanceThreshold)
            {
                if(ant1.transform.Find("Attach").childCount > 0)
                {
                    Debug.Log("Fruit stolen, +1 point for the ants");
                    Debug.Log("Food was a: " + ant1.transform.Find("Attach").GetChild(0).name);
                    Destroy(ant1.transform.Find("Attach").GetChild(0).gameObject);
                    antController1.alreadyGrabbed = false;
                    foodStolenEvent.Invoke();
                    // Play sfx.
                    myAudioSource.time = 0;
                    myAudioSource.Play();
                }
            }
        }

        if (antController2.alreadyGrabbed)
        {
            float distance = Vector3.Distance(this.transform.position, ant2.transform.position);
            if (distance <= distanceThreshold)
            {
                if (ant2.transform.Find("Attach").childCount > 0)
                {
                    Debug.Log("Fruit stolen, +1 point for the ants");
                    Debug.Log("Food was a: " + ant2.transform.Find("Attach").GetChild(0).name);
                    Destroy(ant2.transform.Find("Attach").GetChild(0).gameObject);
                    antController2.alreadyGrabbed = false;
                    foodStolenEvent.Invoke();
                }
            }
        }
    }
}