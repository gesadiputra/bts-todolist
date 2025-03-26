namespace RecruitmentTest.Db;

public class BaseEntity
{
    public int? CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;
    public int? UpdatedBy { get; set; } = null;
    public DateTime? UpdatedDate { get; set; } = null;
}
