using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace asbEvent.Models;

public partial class Event
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    public string EventName { get; set; } = null!;

    public string EventDesc { get; set; } = null!;

    public DateTime StartDateTime { get; set; }

    public DateTime EndDateTime { get; set; }

    public string Location { get; set; } = null!;

    public virtual ICollection<EventRegistration> EventRegistrations { get; set; } = new List<EventRegistration>();
}
