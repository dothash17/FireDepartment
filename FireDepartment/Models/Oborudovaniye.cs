using System.ComponentModel;

namespace FireDepartment.Models;

public partial class Oborudovaniye
{
    [DisplayName("Идентификатор оборудования")]
    public int Id { get; set; }

    [DisplayName("Название")]
    public string Name { get; set; } = null!;

    [DisplayName("Тип")]
    public string Type { get; set; } = null!;

    [DisplayName("Дата последнего обслуживания")]
    public DateTime DateTimeOfService { get; set; }

    [DisplayName("Статус")]
    public bool Status { get; set; }

    public virtual ICollection<CallOborudovaniye> CallOborudovaniye { get; set; } = new List<CallOborudovaniye>();

    public virtual ICollection<Inventory> Inventory { get; set; } = new List<Inventory>();
}