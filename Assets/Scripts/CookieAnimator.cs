using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookieAnimator : MonoBehaviour
{
    public enum State
    { Hello , Sad, Happy }
    [SerializeField] Sprite Hello;
    [SerializeField] Sprite Sad;
    [SerializeField] Sprite Happy;
    private Image image;
    private void Awake()
    {
        image = GetComponent<Image>();
    }
    public void SetState(State state)
    {
        switch (state)
        {
            case State.Hello:
                image.sprite = Hello;
                break;
            case State.Sad:
                image.sprite = Sad;
                break;
            case State.Happy:
                image.sprite = Happy;
                break;
            default:
                break;
        }
    }
}
