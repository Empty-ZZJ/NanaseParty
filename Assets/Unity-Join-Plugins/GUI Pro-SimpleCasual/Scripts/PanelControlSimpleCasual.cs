using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace LayerLab.SimpleCasual
{
    public class PanelControlSimpleCasual: MonoBehaviour
    {
        private int page;
        private bool isReady;
        [SerializeField] private List<GameObject> panelLight = new List<GameObject>();
        [SerializeField] private List<GameObject> panelDark = new List<GameObject>();
        private TextMeshProUGUI textTitle;
        
        [SerializeField] private Transform panelTransformLight;
        [SerializeField] private Transform panelTransformDark;
        
        
        [SerializeField] private Button buttonPrev;
        [SerializeField] private Button buttonNext;

        
        
        private void Start()
        {
            textTitle = transform.GetComponentInChildren<TextMeshProUGUI>();
            buttonPrev.onClick.AddListener(Click_Prev);
            buttonNext.onClick.AddListener(Click_Next);

            foreach (Transform t in panelTransformLight)
            {
                panelLight.Add(t.gameObject);
                t.gameObject.SetActive(false);
            }
            
            foreach (Transform t in panelTransformDark)
            {
                panelDark.Add(t.gameObject);
                t.gameObject.SetActive(false);
            }

            panelLight[page].SetActive(true);
            panelDark[page].SetActive(true);
            
            isReady = true;

            CheckControl();
            SetMode();
        }

        void Update()
        {
            // if (panels.Count <= 0 || !isReady) return;
            if (!isReady) return;

            if (Input.GetKeyDown(KeyCode.LeftArrow))
                Click_Prev();
            else if (Input.GetKeyDown(KeyCode.RightArrow))
                Click_Next();
        }

        private bool isDarakMode = false;
        //Click_Prev
        public void Click_Prev()
        {
            if (page <= 0 || !isReady) return;

            panelLight[page].SetActive(false);
            panelDark[page].SetActive(false);
            page -= 1;
            
            panelLight[page].SetActive(true);
            panelDark[page].SetActive(true);

            if (!isDarakMode)
            {
                textTitle.text = panelLight[page].name;
            }
            else
            {
                textTitle.text = panelDark[page].name;
            }
            
            CheckControl();
        }

        //Click_Next
        public void Click_Next()
        {
            if (page >= panelLight.Count - 1) return;

            
            panelLight[page].SetActive(false);
            panelDark[page].SetActive(false);
            page += 1;
            
            panelLight[page].SetActive(true);
            panelDark[page].SetActive(true);
            CheckControl();
        }

        void SetArrowActive()
        {
            buttonPrev.gameObject.SetActive(page > 0);
            buttonNext.gameObject.SetActive(page < panelLight.Count - 1);
        }

        //SetTitle, SetArrow Active
        private void CheckControl()
        {
            if (!isDarakMode)
            {
                textTitle.text = panelLight[page].name.Replace("_", " ");    
            }
            else
            {
                textTitle.text = panelDark[page].name.Replace("_", " ");
            }
            
            SetArrowActive();
        }

        public void Click_Mode()
        {
            isDarakMode = !isDarakMode;
            SetMode();
		CheckControl();

        }

        
        void SetMode()
        {
            if (!isDarakMode)
            {
                panelTransformLight.gameObject.SetActive(true);
                panelTransformDark.gameObject.SetActive(false);
            }
            else
            {
                panelTransformLight.gameObject.SetActive(false);
                panelTransformDark.gameObject.SetActive(true);
            }
        }
        
    }
}
