using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(CircleCollider2D))]
public class VisitPlaceQuest : QuestSegment
{
#pragma warning disable 0414
    [SerializeField, ReadOnly] private string remember = "Position the Gameobject's position where you want the quest to occur";
#pragma warning restore 0414

    [SerializeField] private Sprite _placeToVisitSprite;

    void Start()
    {
        UpdateTooltip();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FinishQuestSegment();
        }
    }

    protected override void UpdateTooltip()
    {
        Image itemImage = _questTooltipPrefab.GetComponentInChildren<Image>();
        itemImage.sprite = _placeToVisitSprite;

        TextMeshProUGUI craftText = _questTooltipPrefab.GetComponentInChildren<TextMeshProUGUI>();
        craftText.text = "Visit this place.";

        GameEventsMediator.instance.questEvents.UpdateQuestTooltip(_questId, _questTooltipPrefab);
    }

    protected override void LoadSegmentState(string segmentState)
    {
        // Not necessary
    }

    protected override void SaveSegmentState()
    {
        // Not necessary
    }
}
