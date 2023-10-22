using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskUI : MonoBehaviour
{
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI dateText;
    public Image checkboxImage;

    public Sprite doneSprite;
    public Sprite notDoneSprite;

    [HideInInspector]
    public Task task;

    public void SetTask(Task task)
    {
        this.task = task;

        descriptionText.text = task.Description;
        dateText.text = task.Date;
        checkboxImage.sprite = task.Finished ? doneSprite : notDoneSprite;

        //if(task.Finished) checkboxImage.sprite = doneSprite;
        //else checkboxImage.sprite = notDoneSprite;
    }

    public void ChangeStatus()
    {
        task.Finished = !task.Finished;

        checkboxImage.sprite = task.Finished ? doneSprite : notDoneSprite;

        if (checkboxImage.sprite == doneSprite)
        {
            Invoke("DeleteTask", 5f);
        }
    }

    public void DeleteTask()
    {
        Destroy(gameObject); 
    }
}
