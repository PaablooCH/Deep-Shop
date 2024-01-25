using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TooltipUI : MonoBehaviour, IUI
{
    [SerializeField] TextMeshProUGUI _headerField;
    [SerializeField] TextMeshProUGUI _bodyField;

    [SerializeField] LayoutElement _layoutElement;

    [SerializeField] private int _charachterWrapLimit;

    [SerializeField] private Vector2 _offset = new(10f, -20f);

    private RectTransform _rectTransform;

    private int _numberChildrenStart;

    private void Awake()
    {
        _numberChildrenStart = transform.childCount;
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        MoveTooltipToMouse();
    }

    public void MoveTooltipToMouse()
    {
        Vector2 mousePosition = Input.mousePosition;
        if (mousePosition.x < Screen.width * 0.5f)
        {
            _rectTransform.pivot = new Vector2(0, 1);
        }
        else
        {
            _rectTransform.pivot = new Vector2(1, 1);
        }
        _rectTransform.position = mousePosition + _offset;
    }

    public void SetText(string body, string header = "")
    {
        if (string.IsNullOrEmpty(header))
        {
            _headerField.gameObject.SetActive(false);
        }
        else
        {
            _headerField.gameObject.SetActive(true);
            _headerField.text = header;
        }

        _bodyField.text = body;

        int headerLenght = _headerField.text.Length;
        int bodyLenght = _bodyField.text.Length;
        _layoutElement.enabled = headerLenght > _charachterWrapLimit || bodyLenght > _charachterWrapLimit;
    }

    public void AddGameObjects(GameObject[] gos)
    {
        foreach (GameObject go in gos)
        {
            GameObject copy = Instantiate(go, transform);
            copy.SetActive(true);
        }
    }   

    public void CleanGameObjectsAdded()
    {
        for (int i = _numberChildrenStart; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    public void OpenUI()
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
        CleanGameObjectsAdded();
        MoveTooltipToMouse();
    }

    public void Exit()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }
}
