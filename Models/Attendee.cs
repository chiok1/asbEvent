using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace asbEvent.Models;

public partial class Attendee
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    public string Email { get; set; } = null!;

    public string? Fname { get; set; }

    public string Lname { get; set; } = null!;

    public string? Company { get; set; }

    public virtual ICollection<EventRegistration> EventRegistrations { get; set; } = new List<EventRegistration>();
}
