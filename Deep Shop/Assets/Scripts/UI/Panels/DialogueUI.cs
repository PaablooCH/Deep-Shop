using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueUI : MonoBehaviour, IUI
{
    [SerializeField] private TextMeshProUGUI _speakerName;
    [SerializeField] private TextMeshProUGUI _dialogueContent;
    
    [SerializeField] private Image _portrait;
    
    [Range(0.1f, 10f)]
    [SerializeField] private float _typeSpeed = 10f;

    private Queue<DialogueText.Dialogue> _dialogues = new();

    private bool _endTalking = false;
    private bool _isTyping = false;

    private Coroutine _typeCoroutine;
    private DialogueText.Dialogue _d;

    private const string HTML_ALPHA = "<color=#00000000>";
    private const float MAX_TYPE_TIME = 0.1f;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void OpenUI()
    {
        CanvasManager.instance.ActiveUI(gameObject);
    }

    public void Exit()
    {
        CanvasManager.instance.FreeUI();
    }

    public void NextDialogue(DialogueText dialogueText)
    {
        // Nothing in the queue
        if (_dialogues.Count == 0)
        {
            if (!_endTalking)
            {
                StartConversation(dialogueText);
            }
            else if (_endTalking && !_isTyping)
            {
                EndConversation();
                return;
            }
        }

        // If not typing load a new dialog
        if (!_isTyping)
        {
            _d = _dialogues.Dequeue();
            _typeCoroutine = StartCoroutine(TypeDialogue(_d));
        }
        // If typing finish
        else
        {
            FinishConversationEarly();
        }

        // Check if we have dialogue
        if (_dialogues.Count == 0)
        {
            _endTalking = true;
        }
    }

    private void StartConversation(DialogueText dialogueText)
    {
        // Open UI
        if (!gameObject.activeSelf)
        {
            OpenUI();
        }

        // Update speaker name
        _speakerName.text = dialogueText.SpeakerName;

        // Enqueue dialogues
        foreach (DialogueText.Dialogue dialogue in dialogueText.Dialogues)
        {
            _dialogues.Enqueue(dialogue);
        }
    }

    private void EndConversation()
    {
        // Clear pending dialogues
        _dialogues.Clear();

        // Set endTalking
        _endTalking = false;

        // Exit UI
        if (gameObject.activeSelf)
        {
            Exit();
        }
    }

    private IEnumerator TypeDialogue(DialogueText.Dialogue dialogueText)
    {
        _isTyping = true;

        _portrait.sprite = _d.sprite;

        _dialogueContent.text = "";

        string originalText = dialogueText.text;
        string displayedText = "";
        int alphaIndex = 0;

        for (int i = 0; i < dialogueText.text.Length; i++)
        {
            alphaIndex++;
            _dialogueContent.text = originalText;

            displayedText = _dialogueContent.text.Insert(alphaIndex, HTML_ALPHA);
            _dialogueContent.text = displayedText;

            yield return new WaitForSecondsRealtime(MAX_TYPE_TIME / _typeSpeed);
        }

        _isTyping = false;
    }

    private void FinishConversationEarly()
    {
        // Stop coroutine
        StopCoroutine(_typeCoroutine);

        // Finish text
        _dialogueContent.text = _d.text;

        // Stop typing
        _isTyping = false;
    }
}
