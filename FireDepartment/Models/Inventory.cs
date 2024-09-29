using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace FireDepartment.Models;

public partial class Inventory
{
    [DisplayName("Идентификатор инвентаризации")]
    public int Id { get; set; }

    [DisplayName("Местонахождение")]
    public string Location { get; set; } = null!;

    [DisplayName("Состояние")]
    public bool State { get; set; }

    [DisplayName("Количество")]
    public int Quantity { get; set; }

    [DisplayName("Идентификатор оборудования")]
    public int OborudovaniyeId { get; set; }

    public virtual Oborudovaniye Oborudovaniye { get; set; } = null!;
}