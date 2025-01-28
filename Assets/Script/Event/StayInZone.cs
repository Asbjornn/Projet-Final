using TMPro;
using Unity.Cinemachine;
using UnityEngine;

public class StayInZone : MonoBehaviour
{
    public float timeInZone;
    public float timeForWin;
    public GameObject secondCircle;
    public bool inZone;
    public TextMeshProUGUI textTimer;
    public EventManager eventManager;
    public TextMeshProUGUI textZone;

    float elapsedTime = 0f;


    void Start()
    {
        eventManager = GameObject.Find("EventManager").GetComponent<EventManager>();

        timeInZone = timeForWin;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeInZone <= 0)
        {
            eventManager.VictoryEvent();
            Destroy(transform.parent.gameObject);
        }

        if(inZone)
        {
            Timer();
            SizeCircle();
        }
        else
        {
            return;
        }
    }

    public void Timer()
    {
        timeInZone -= Time.deltaTime;

        int minutes = Mathf.FloorToInt(timeInZone / 60);
        int secondes = Mathf.FloorToInt(timeInZone % 60);
        textTimer.text = string.Format("{0:00}:{1:00}", minutes, secondes);
    }

    public void SizeCircle()
    {
        elapsedTime += Time.deltaTime;
        float t = elapsedTime / timeForWin;
        secondCircle.transform.localScale = Vector3.Lerp(new Vector3(0,0,0), new Vector3(1,1,1), t);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inZone = true;
            textZone.enabled = false;
            textTimer.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inZone = false;
            textZone.enabled = true;
            textTimer.enabled = false;
        }
    }
}
