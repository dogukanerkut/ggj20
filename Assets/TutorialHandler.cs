using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TutorialHandler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Animator[] _animators;
    [SerializeField] private AttackManager _attackManager;
    int _currentIndex = 0;
    void Start()
    {
        _animators[_currentIndex].SetTrigger("TransitionIn");
    }
    private bool _isTutorialFinished = false;
    // Update is called once per frame
    void Update()
    {
        if (_isTutorialFinished)
        {
            if (Input.GetMouseButtonDown(0))
            {
                gameObject.SetActive(false);
                Time.timeScale = 1;
                _attackManager.StartAttack();
            }
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {

            _animators[_currentIndex++].SetTrigger("TransitionOut");
            _animators[_currentIndex].SetTrigger("TransitionIn");
            if (_currentIndex >= _animators.Length - 1)
            {
                _isTutorialFinished = true;
            }
        }
        Time.timeScale = 0;

    }
}
