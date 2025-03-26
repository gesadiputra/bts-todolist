namespace RecruitmentTest.Db.Entities;

public class Checklist : BaseEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ColorCode { get; set; }

    public ICollection<ChecklistItem> ChecklistItems { get; set; }
}
