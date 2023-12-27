
using TMPro;
using UnityEngine;

public class Teacher : MonoBehaviour
{
    public int id;
    public string teacher = "Teacher";
    public string quest = "Xác nhận trả lời câu hỏi ?";
    public ArrowDirection arrowDirection;
    public MissionListPanel missionListPanel;
    public GameObject teacherNameObj;
    public Transform player;
    public ConfirmPanel confirmPanel;
    public Chat chat;
    public int numQues;
    public ChatPanel chatPanel;
    bool isAnswered = false;
    private void OnTriggerEnter(Collider other)
    {
        if(!isAnswered)
        {
            chatPanel.ShowPanel(chat, () =>
            {
                isAnswered = true;
                GameManager.Instance.QuestionController.StartAnswering(numQues);
                missionListPanel.UnLockMission(id);
            });
        }
    }

    private void OnTriggerExit(Collider other)
    {
        chatPanel.HidePanel();
    }
    private void Awake()
    {
        teacherNameObj.GetComponentInChildren<TextMeshProUGUI>().text = teacher;
    }
    public void SetNumQues(int numQues)
    {
        this.numQues = numQues; 
    }
    private void Update()
    {
        Vector3 direction = player.position - teacherNameObj.transform.position;
        direction.Normalize();
        Quaternion rotation = Quaternion.LookRotation(direction);
        teacherNameObj.transform.rotation = Quaternion.Euler(0, rotation.eulerAngles.y, 0);
    }

}
