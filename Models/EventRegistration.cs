using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace asbEvent.Models;

public partial class EventRegistration
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    public long AttendeeId { get; set; }

    public long EventId { get; set; }

    public DateTime EventDate { get; set; }

    public DateTime RegistrationTime { get; set; }

    public DateTime? AttendedTime { get; set; }

    public string Qrcode { get; set; } = null!;

    public virtual Attendee Attendee { get; set; } = null!;

    public virtual Event Event { get; set; } = null!;
}
