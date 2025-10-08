using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject Box1;
    public GameObject Box2;
    public GameObject Box3;

    private void Start()
    {
        mainMenuUI.SetActive(false);
    }
    public void ShowHideMainMenu()
    {
        mainMenuUI.SetActive(!mainMenuUI.activeInHierarchy);

        //Making sure the shapes all start invisible; you could do this bit in the inspector
        //If you're tweening the shapes out again, they'll be at zero after the first use ;-)
        mainMenuUI.transform.localScale = Vector3.zero;
        Box1.transform.localScale = Vector3.zero;
        Box2.transform.localScale = Vector3.zero;
        Box3.transform.localScale = Vector3.zero;

        //This method is a bit icky...it gets out of hand if you want to chain a lot of things
        //mainMenuUI.transform.DOScale(Vector3.one, 2).OnComplete(() =>
        //{
        //    Box1.transform.DOScale(Vector3.one, 2);
        //});

        ////this method is better:

        ////super-wierd ! no "new" keyword in here !!
        Sequence sequence = DOTween.Sequence();
        ////add all your tweens to the sequence without an .OnComplete
        sequence.Append(mainMenuUI.transform.DOScale(Vector3.one, 2).SetEase(Ease.OutElastic));
        sequence.Append(Box1.transform.DOScale(Vector3.one, 1).SetEase(Ease.OutBounce));
        Image menuImage = mainMenuUI.GetComponent<Image>();
        sequence.Append(menuImage.DOFade(0, 1));
        sequence.Append(Box2.transform.DOScale(Vector3.one, 2));
        sequence.Append(Box3.transform.DOScale(Vector3.one, 2));

        //now do the sequence with the .OnComplete:
        sequence.OnComplete(() =>
        {
            //this one happens after the whole sequence is done
            mainMenuUI.transform.DOShakePosition(1, 5);

            //you could just do nothing on complete though by having an empty function here
            //(there isn't a "ok just run the sequence now" thing, you have to use OnComplete, lols)
        });

    }
}
