using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ChangeText : MonoBehaviour
{
    [SerializeField] DoData _data;
    [SerializeField] Text _text;
    [SerializeField] string _newText1 = "Мы заменили тут текст";
    [SerializeField] string _newText2 = ". Мы дополнили тут текст этим сообщением";
    [SerializeField] string _newText3 = "Мы взломали тут текст";


    private void Start()
    {
        DOTween.Sequence()
        .Append(_text.DOText(_newText1, _data.Duration))
        .AppendInterval(_data.Delay)
        .Append(_text.DOText(_newText2, _data.Duration).SetRelative())
        .AppendInterval(_data.Delay)
        .Append(_text.DOText(_newText3, _data.Duration, true, ScrambleMode.All));
    }
}