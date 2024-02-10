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

    private string _dialogueId;
    private Queue<Dialogue> _dialogues = new();

    private bool _endTalking = false;
    private bool _isTyping = false;

    private Coroutine _typeCoroutine;
    private Dialogue _d;

    private const string HTML_ALPHA = "<color=#00000000>";
    private const float MAX_TYPE_TIME = 0.1f;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void OpenUI()
    {
        UIManager.instance.ActiveUI(UIs.DIALOGUE);
    }

    public void Exit()
    {
        UIManager.instance.FreeUI();
    }

    public void NextDialogue(DialogueTextSO dialogueText)
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

        // Check if we have more dialogues
        if (_dialogues.Count == 0)
        {
            _endTalking = true;
        }
    }

    private void StartConversation(DialogueTextSO dialogueText)
    {
        // Open UI
        if (!gameObject.activeSelf)
        {
            OpenUI();
        }

        // Get dialogue ID
        _dialogueId = dialogueText.Id;

        // Update speaker name
        _speakerName.text = dialogueText.SpeakerName;

        // Enqueue dialogues
        foreach (Dialogue dialogue in dialogueText.Dialogues)
        {
            _dialogues.Enqueue(dialogue);
        }
    }

    public void EndConversation()
    {
        // Clear pending dialogues
        _dialogues.Clear();

        // Set endTalking
        _endTalking = false;

        // Propagate event
        GameEventsMediator.instance.dialogueEvents.FinishDialogue(_dialogueId);

        // Exit UI
        if (gameObject.activeSelf)
        {
            Exit();
        }
    }

    private IEnumerator TypeDialogue(Dialogue dialogueText)
    {
        _isTyping = true;

        _portrait.sprite = _d.sprite;

        _dialogueContent.text = "";

        string originalText = dialogueText.text;
        int alphaIndex = 0;

        for (int i = 0; i < dialogueText.text.Length; i++)
        {
            alphaIndex++;
            _dialogueContent.text = originalText;

            // Insert an ALPHA = 0 character to the left. This hides the rest of the text and simulates a real conversation
            _dialogueContent.text = _dialogueContent.text.Insert(alphaIndex, HTML_ALPHA);

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
