using System.Collections;
using UnityEngine;

public class Counter : MonoBehaviour
{
    private float _delay = 0.5f;
    private int _count = 0;
    private bool _isCounterOn = false;

    private Coroutine _coroutine;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            ToggleCounterValue();

            if (_isCounterOn)
                _coroutine = StartCoroutine(RunCounter());
            else
                StopCoroutine(_coroutine);
        }
    }

    private IEnumerator RunCounter()
    {
        bool isWork = true;

        WaitForSeconds wait = new WaitForSeconds(_delay);

        while (isWork)
        {
            _count++;
            Debug.Log(_count);

            yield return wait;
        }
    }

    private void ToggleCounterValue()
    {
        _isCounterOn = !_isCounterOn;
    }
}