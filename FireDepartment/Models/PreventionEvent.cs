using System.ComponentModel;

namespace FireDepartment.Models;

public partial class PreventionEvent
{
    [DisplayName("Идентификатор мероприятия")]
    public int Id { get; set; }

    [DisplayName("Название")]
    public string Name { get; set; } = null!;

    [DisplayName("Дата проведения")]
    public DateTime DateTime { get; set; }

    [DisplayName("Местоположение")]
    public string Location { get; set; } = null!;

    [DisplayName("Цель мероприятия")]
    public string Goal { get; set; } = null!;

    [DisplayName("Идентификатор сотрудника")]
    public int SotrudnikId { get; set; }

    public virtual Sotrudniki Sotrudnik { get; set; } = null!;
}