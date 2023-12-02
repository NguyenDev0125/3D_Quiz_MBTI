
using TMPro;
using UnityEngine;

public class Teacher : MonoBehaviour
{
    public string teacher = "Teacher";
    public ArrowDirection arrowDirection;
    public MissionListPanel missionListPanel;
    public GameObject teacherNameObj;
    public Transform player;
    public int numQues;
    bool isAnswered = false;
    private void OnTriggerEnter(Collider other)
    {
        if(!isAnswered)
        {
            isAnswered = true;
            GameManager.Instance.QuestionController.StartAnswering(numQues);
            arrowDirection.SetNextTarget();
            missionListPanel.UnLockMission();
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
