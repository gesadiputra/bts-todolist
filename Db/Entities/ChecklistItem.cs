namespace RecruitmentTest.Db.Entities;

public class ChecklistItem : BaseEntity
{
    public int Id { get; set; }
    public int ChecklistId { get; set; }
    public Checklist Checklist { get; set; }
    public string Name { get; set; }
    public bool IsChecked { get; set; }
}
