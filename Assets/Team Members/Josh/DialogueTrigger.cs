using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool canInteract = false;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    [SerializeField] string tagFilter;

    private void OnTriggerEnter(Collider other)
    {
        if (!string.IsNullOrEmpty(tagFilter) && !other.gameObject.CompareTag(tagFilter)) return;
        canInteract = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!string.IsNullOrEmpty(tagFilter) && !other.gameObject.CompareTag(tagFilter)) return;
        canInteract = false;
    }


}
