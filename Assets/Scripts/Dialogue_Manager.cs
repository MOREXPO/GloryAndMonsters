﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Dialogue_Manager : MonoBehaviour
{
    public Dialogue dialogue;

    Queue<string> sentences;

    public GameObject dialoguePanel;

    public TextMeshProUGUI displayText;

    public GameManager gameManager;
    bool isWithin;

    string activeSentence;
    public float typingSpeed;

    AudioSource myAudio;

    public AudioClip speakSound;
    public bool isHistory;

    private void Awake()
    {
        isWithin = false;
    }
    void Start()
    {
        sentences = new Queue<string>();
        myAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isWithin&&!isHistory) {
            if (Input.GetKeyDown(KeyCode.Return)&&displayText.text.Replace(gameManager.Nombre,"{nombre}")==activeSentence)
            {
                DisplayNextSentence();
            }
        }    
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player")) {
            dialoguePanel.SetActive(true);
            isWithin = true;
            StartDialogue();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isWithin = false;
            dialoguePanel.SetActive(false);
            StopAllCoroutines();
        }
    }

    void StartDialogue() {
        sentences.Clear();
        foreach (string sentence in dialogue.sentenceList) {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence() {
        if (sentences.Count<=0) {
            displayText.text = activeSentence;
            return;
        }
        activeSentence = sentences.Dequeue();
        displayText.text = activeSentence;
        StopAllCoroutines();
        StartCoroutine(TypeTheSentence(activeSentence));
    }

    IEnumerator TypeTheSentence(string sentence) {
        displayText.text = "";
        sentence = sentence.Replace("{nombre}",gameManager.Nombre);
        foreach (char letter in sentence.ToCharArray()) {
            displayText.text += letter;
            myAudio.PlayOneShot(speakSound);
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public bool IsWithin() {
        return isWithin;
    }
}
