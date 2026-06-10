using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogBox : MonoBehaviour
{
    public TextMeshProUGUI leftName;
    public TextMeshProUGUI rightName;
    public TextMeshProUGUI text;
    public Button nextBtn;
    private void Start()
    {
        nextBtn.onClick.AddListener(DialogController.instance.DisplayNextSentence);
        gameObject.SetActive(false);
    }
}
