using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Emote : MonoBehaviour
{
    public Image emotionUI;
    public List<Emotion> emotionSprites;

    private void Start()
    {
        //you could set this in the inspector...doing it this way makes it easier to set up in the editor
        //since you can see how it will look, instead of it being scaled to zero
        emotionUI.transform.localScale = Vector3.zero;
    }
    public void ShowEmote(SimData.Need need)
    {
        Emotion e = emotionSprites.Find(x => x.name == need);

        //we don't need to check for null because we can assign "null" as the sprite to show no emotion ;-)
        emotionUI.sprite = e.sprite;

        //Punch scales from current value (zero, set here in start or in inspector) to the target value
        //other arguments: duration to tween over
        //vibrato: how much it oscillates about the target value,
        //default is 10 which looks CRAYYYYYZY for our lil sprite
        //elasticity: damping on the springyness
        emotionUI.transform.DOPunchScale(Vector3.one, 2, 1, 1).SetEase(Ease.Flash);
    }
}

[System.Serializable]
public class Emotion
{
    public SimData.Need name;
    public Sprite sprite;
}
