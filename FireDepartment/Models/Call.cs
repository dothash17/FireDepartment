using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace FireDepartment.Models;

public partial class Call
{
    [DisplayName("Идентификатор вызова")]
    public int Id { get; set; }

    [DisplayName("Дата вызова")]
    public DateTime DateTimeCall { get; set; }

    [DisplayName("Местоположение")]
    public string Location { get; set; } = null!;

    [DisplayName("Категория")]
    public string Category { get; set; } = null!;

    [DisplayName("Описание")]
    public string Description { get; set; } = null!;

    [DisplayName("Идентификатор сотрудника")]
    public int SotrudnikId { get; set; }

    public virtual ICollection<CallOborudovaniye> CallOborudovaniye { get; set; } = new List<CallOborudovaniye>();
    public virtual Sotrudniki Sotrudnik { get; set; } = null!;
}