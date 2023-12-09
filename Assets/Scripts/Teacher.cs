
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
    public int numQues;
    bool isAnswered = false;
    private void OnTriggerEnter(Collider other)
    {
        if(!isAnswered)
        {
            confirmPanel.OnResult = (result) =>
            {
                if (result == ConfirmResult.OK)
                {
                    isAnswered = true;
                    GameManager.Instance.QuestionController.StartAnswering(numQues);
                    missionListPanel.UnLockMission(id);
                }
            };
            confirmPanel.Display(quest);


        }
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
