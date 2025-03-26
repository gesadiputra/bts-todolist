namespace RecruitmentTest.Model.Checklist;

public class ChecklistGetResponse
{
    public int ChecklistId { get; set; }
    public string Name { get; set; }
    public string ColorCode { get; set; }
}

public class ChecklistItemGetResponse
{
    public int ChecklistId { get; set; }
    public int ItemId { get; set; }
    public string Name { get; set; }
    public bool IsChecked { get; set; }
}