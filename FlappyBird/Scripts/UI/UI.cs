using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Bird _bird;

    private string _nullScore = "0";

    private void OnEnable()
    {
        _bird.ScoreChanged += ChangeScore;
    }

    private void OnDisable()
    {
        _bird.ScoreChanged -= ChangeScore;    
    }

    private void Start()
    {
        _text.text = _nullScore;
    }

    private void ChangeScore()
    {
        _text.text = _bird.Score.ToString();
    }
}