using System;
using System.Collections.Generic;

namespace asbEvent.Models;

public partial class Reward
{
    public long Id { get; set; }

    public string RewardName { get; set; } = null!;

    public string RewardDesc { get; set; } = null!;

    public string Eligibility { get; set; } = null!;
}
