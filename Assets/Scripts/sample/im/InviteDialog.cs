using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InviteDialog : MonoBehaviour
{
    private Text titleText;

    private Text contentText;

    private Button agreeButton;

    private Button refuseButton;

    public event Action OnAgree;
    public event Action OnRefuse;

   

    // Start is called before the first frame update
    void Start()
    {
        titleText = gameObject.transform.Find("InviteTitleText").GetComponent<Text>();
        contentText = gameObject.transform.Find("InviteContentText").GetComponent<Text>();
        agreeButton = gameObject.transform.Find("AgreeButton").GetComponent<Button>();
        refuseButton = gameObject.transform.Find("RefuseButton").GetComponent<Button>();

        agreeButton.onClick.AddListener(AgreeAction);
        refuseButton.onClick.AddListener(RefuseAction);
    }

    private void AgreeAction()
    {
        gameObject.SetActive(false);
        OnAgree?.Invoke();
    }

    private void RefuseAction()
    {
        gameObject.SetActive(false);
        OnRefuse?.Invoke();
    }


    public void setTitle(string title)
    {
        if(titleText != null)
        {
            titleText.text = title;
        }
    }


    public void setContent(string content)
    {
        if (contentText != null)
        {
            contentText.text = content;
        }
    }


}
