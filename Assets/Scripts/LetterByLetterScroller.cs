using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LetterByLetterScroller : MonoBehaviour
{
    [Header("Text Settings")]
    public string inputText = "Hello World";

    [Header("Letter Scroll Settings")]
    public float scrollSpeed = 50f;     // Speed of scroll
    public float spacing = 50f;         // Fixed spacing between letters
    public bool scrollLeftToRight = true;

    [Header("References")]
    public TextMeshProUGUI letterPrefab;       // Prefab (can be disabled in inspector)

    private RectTransform canvasRect;
    private List<TextMeshProUGUI> activeLetters = new();
    private float timer;
    private int currentIndex;

    private void Start()
    {
        canvasRect = GetComponent<RectTransform>();

        if (canvasRect == null || letterPrefab == null)
        {
            Debug.LogError("Missing RectTransform or Letter Prefab.");
            return;
        }

        ClearLetters();
        currentIndex = inputText.Length - 1;
        timer = 0;
    }

    private void Update()
    {
        if (currentIndex >= 0)
        {
            timer += Time.deltaTime;
            if (timer >= spacing / scrollSpeed)
            {
                SpawnLetter(inputText[currentIndex]);
                currentIndex--;
                timer = 0;
            }
        }

        MoveLetters();
        CleanupLetters();

        if (activeLetters.Count == 0 && currentIndex < 0)
        {
            currentIndex = inputText.Length - 1;
        }
    }

    void SpawnLetter(char c)
    {
        TextMeshProUGUI letter = Instantiate(letterPrefab, transform);
        letter.text = c.ToString();
        letter.gameObject.SetActive(true); // Enable if prefab is disabled

        // Correct pivot and anchors
        letter.rectTransform.pivot = new Vector2(0, 0.5f);
        letter.rectTransform.anchorMin = new Vector2(0, 0.5f);
        letter.rectTransform.anchorMax = new Vector2(0, 0.5f);

        // Start position at LEFT or RIGHT edge of canvas
        float startX = scrollLeftToRight ? 0 : canvasRect.rect.width;
        letter.rectTransform.anchoredPosition = new Vector2(startX, 0);

        activeLetters.Add(letter);
    }

    void MoveLetters()
    {
        float direction = scrollLeftToRight ? 1f : -1f;

        foreach (var letter in activeLetters)
        {
            Vector2 pos = letter.rectTransform.anchoredPosition;
            pos.x += direction * scrollSpeed * Time.deltaTime;
            letter.rectTransform.anchoredPosition = pos;
        }
    }

    void CleanupLetters()
    {
        for (int i = activeLetters.Count - 1; i >= 0; i--)
        {
            var letter = activeLetters[i];
            float x = letter.rectTransform.anchoredPosition.x;

            bool offScreen = scrollLeftToRight
                ? x > canvasRect.rect.width - 3f
                : x < -letter.preferredWidth;

            if (offScreen)
            {
                Destroy(letter.gameObject);
                activeLetters.RemoveAt(i);
            }
        }
    }

    void ClearLetters()
    {
        foreach (var letter in activeLetters)
        {
            if (letter != null)
                Destroy(letter.gameObject);
        }
        activeLetters.Clear();
    }
}
