using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class CustomerBehaviour : MonoBehaviour
{
    [SerializeField]
    private DialogTrigger DialogTrigger;
    [SerializeField]
    private GameObject counterPoint;
    [SerializeField]
    private GameObject StartingPoint;
    [SerializeField]
    private Customer[] Customerclass;
    [SerializeField]
    private QuestDetailViewer questDetail;
    private bool facingRight = true;
    private Animator animator;
    private Button dialogBtn;
    public bool spawned;
    private bool isClicked;
    private Customer currentCustomer;

    private void Start()
    {
        spawned = false;
        dialogBtn = DialogTrigger.gameObject.GetComponent<Button>();
    }

    public void Show()
    {
        gameObject.transform.position = StartingPoint.transform.position;
        RandomSprite();
        MoveToCounter();
    }

    private void RandomSprite()
    {
        int randomIndex = Random.Range(0, Customerclass.Length);
        if (Customerclass.Length > 0)
        {
            var customer = Customerclass[randomIndex];
            Instantiate(customer.CustomerSprite, this.transform);
            animator = transform.GetChild(0).GetComponent<Animator>();
            Debug.Log(customer.CustomerSprite.name);
            currentCustomer  = customer;
        }
        else
        {
            Debug.LogWarning("Array is empty.");
        }
    }
    public void Clicked(bool clicked)
    {
        isClicked = clicked;
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale = new Vector3(theScale.x * -1, 1, 1);
        transform.localScale = theScale;
    }

    public void MoveToCounter()
    {
        Flip();
        animator.SetBool("Walking", true);
        //GetComponent<SpriteRenderer>().DOFade(1,1);
        transform.DOMove(counterPoint.transform.position, 5).SetEase(Ease.Linear).OnComplete(RequestWait);
    }

    private void RequestWait()
    {
        animator.SetBool("Walking", false);
        Flip();
        DialogTrigger.gameObject.SetActive(true);
        dialogBtn.onClick.RemoveAllListeners();
        dialogBtn.onClick.AddListener( ()=> {
            isClicked = true;
            DialogTrigger.currentCustomer = currentCustomer;
            DialogTrigger.TriggerDialogue();
        });
        // DialogTrigger.TriggerDialogue();
        StartCoroutine(WaitForAction());
    }

    IEnumerator WaitForAction()
    {
        float randomValue = Random.Range(3.0f, 5.0f);
        Debug.Log("Random Value timer for customer: " + randomValue);
        yield return new WaitForSeconds(randomValue);
        if (isClicked)
        {
            yield return new WaitUntil( () => questDetail.IsStarted || questDetail.IsCanceled);
            Debug.Log("[BUG] HI");
            ExitCounter();            
        }
        else
        {
            ExitCounter();
        }
    }

    public void ExitCounter()
    {
        DialogTrigger.gameObject.SetActive(false);
        animator.SetBool("Walking", true);
        transform.DOMove(StartingPoint.transform.position, 5).SetEase(Ease.Linear).OnComplete(OnExitComplete);
        //GetComponent<SpriteRenderer>().DOFade(0, 4);
        isClicked = false;
    }

    private void OnExitComplete()
    {
        spawned = false;
        currentCustomer = null;
        Destroy(this.transform.GetChild(0).gameObject);
    }
}

[System.Serializable]
public class Customer
{
    public GameObject CustomerSprite;
    public Sprite CustomerImg;
    public QuestSO[] Quests;
}