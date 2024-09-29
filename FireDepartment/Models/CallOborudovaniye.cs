using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace FireDepartment.Models;

public partial class CallOborudovaniye
{
    [DisplayName("Идентификатор вызова")]
    public int CallId { get; set; }

    [DisplayName("Идентификатор оборудования")]
    public int OborudovaniyeId { get; set; }

    public virtual Call Call { get; set; } = null!;

    public virtual Oborudovaniye Oborudovaniye { get; set; } = null!;
}