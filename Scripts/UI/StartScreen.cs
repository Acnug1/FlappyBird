using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartScreen : Screen
{
    public event UnityAction PlayButtonClick;

    public override void Close()
    {
        CanvasGroup.alpha = 0; // скрываем стартовый экран
        Button.interactable = false; // отключаем интерактивность кнопки
    }

    public override void Open()
    {
        CanvasGroup.alpha = 1; // отображаем стартовый экран
        Button.interactable = true; // включаем интерактивность кнопки
    }

    protected override void OnButtonClick() // вызываем обрабочик события при нажатии на кнопку
    {
        PlayButtonClick?.Invoke();
    }
}
