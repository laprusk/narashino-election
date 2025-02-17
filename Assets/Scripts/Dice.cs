using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dice : MonoBehaviour
{
    public int Value { get; private set; }
    public bool IsRolling { get; private set; }
    public bool IsStopped { get; private set; }
    [SerializeField]
    private TextMeshProUGUI valueText;
    [SerializeField]
    private float rollInterval = 0.2f;
    [SerializeField]
    private Sprite[] diceFaces;

    public AudioClip diceRollSound;
    public AudioClip diceStopSound;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        Value = 1;
        UpdateValueText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // void OnMouseDown()
    // {
    //     if (!IsRolling)
    //     {
    //         StartRolling();
    //     }
    //     else
    //     {
    //         StopRolling();
    //     }
    // }

    public void StartRolling()
    {
        if (!IsRolling)
        {
            Debug.Log("Start Rolling");
            IsRolling = true;
            IsStopped = false;
            StartCoroutine(Roll());
            
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = diceRollSound;
            audioSource.Play();
            audioSource.loop = true;
        }
    }

    public void StopRolling()
    {
        if (IsRolling)
        {
            Debug.Log("Stop Rolling");
            IsRolling = false; 
            audioSource.Stop();
            audioSource.loop = false;
            audioSource.clip = diceStopSound;
            audioSource.Play();
        }
    }

    private IEnumerator Roll()
    {
        while (IsRolling)
        {
            Value = Random.Range(1, 7);
            UpdateValueText();
            yield return new WaitForSeconds(rollInterval);
        }
        Value = Random.Range(1, 7);
        UpdateValueText();
        IsStopped = true;
        Debug.Log("Value: " + Value);
    }

    public void UpdateValueText()
    {
        valueText.text = Value.ToString();
        GetComponent<SpriteRenderer>().sprite = diceFaces[Value - 1];
    }
}
