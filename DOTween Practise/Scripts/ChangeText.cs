using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ChangeText : MonoBehaviour
{
    [SerializeField] Text _text1;
    [SerializeField] Text _text2;
    [SerializeField] Text _text3;
    [SerializeField] private float _duration;
    [SerializeField] string _newText1 = "Мы заменили тут текст";
    [SerializeField] string _newText2 = ". Мы дополнили тут текст этим сообщением";
    [SerializeField] string _newText3 = "Мы взломали тут текст";


    private void Start()
    {
        _text1.DOText(_newText1, _duration);
        _text2.DOText(_newText2, _duration).SetRelative();
        _text3.DOText(_newText3, _duration, true, ScrambleMode.All);
    }
}