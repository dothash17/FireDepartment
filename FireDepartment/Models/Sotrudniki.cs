using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace FireDepartment.Models;

public partial class Sotrudniki
{
    [DisplayName("Идентификатор сотрудника")]
    public int Id { get; set; }

    [DisplayName("Имя")]
    public string FirstName { get; set; } = null!;

    [DisplayName("Фамилия")]
    public string LastName { get; set; } = null!;

    [DisplayName("Завние")]
    public string Rank { get; set; } = null!;

    [DisplayName("Специализация")]
    public string Specialization { get; set; } = null!;

    [DisplayName("Номер телефона")]
    public string PhoneNumber { get; set; } = null!;

    [DisplayName("Почта")]
    public string Mail { get; set; } = null!;

    [DisplayName("Дата приема")]
    public DateTime DateOfReceipt { get; set; }

    public virtual ICollection<Call> Calls { get; set; } = new List<Call>();

    public virtual ICollection<PreventionEvent> PreventionEvents { get; set; } = new List<PreventionEvent>();
}