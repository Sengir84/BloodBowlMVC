using System;
using System.Collections.Generic;

namespace BloodBowlMVC.Models;

public partial class Trainer
{
    public int TrainerIdPk { get; set; }

    public string? TrainerName { get; set; }

    public virtual ICollection<Team> Teams { get; set; } = new List<Team>();
}
