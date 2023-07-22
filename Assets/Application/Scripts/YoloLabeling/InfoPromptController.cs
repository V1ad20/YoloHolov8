using TMPro;
using UnityEngine;

namespace YoloHolo.YoloLabeling
{
    public class InfoPromptController : MonoBehaviour
    {
        [SerializeField] private TextMeshPro textMesh;

        [SerializeField] private GameObject contentParent;

        public GameObject InfoPane;
        public string InfoText;


        public void SetText(string text)
        {
            textMesh.text = text;
        }

        private void Start()
        {
            var lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.SetPosition(0, contentParent.transform.position);
            lineRenderer.SetPosition(1, transform.position);
        }

        public void ShowInfo()
        {
            InfoPane.SetActive(true);
            GameObject title = InfoPane.transform.GetChild(0).gameObject;
            title.GetComponent<TextMeshPro>().text = InfoText;
        }




    }
}