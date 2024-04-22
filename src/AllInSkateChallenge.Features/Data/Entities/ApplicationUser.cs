using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Identity;

namespace AllInSkateChallenge.Features.Data.Entities;

public class ApplicationUser : IdentityUser
{
    [PersonalData]
    public string SkaterName { get; set; }

    [PersonalData]
    public string ExternalProfileImage { get; set; }

    [PersonalData]
    public bool IsStravaAccount { get; set; }

    [PersonalData]
    public bool AcceptProgressNotifications { get; set; }

    [PersonalData]
    public bool HasPaid { get; set; }

    public List<SkateLogEntry> SkateLogEntries { get; set; }

    public List<StravaEvent> StravaEvents { get; set; }

    [PersonalData]
    public DateTime DateRegistered { get; set; }

    [PersonalData]
    public int Target { get; set; }

    [PersonalData]
    public int Team { get; set; }

    public string GetDisplaySkaterName()
    {
        if (string.IsNullOrWhiteSpace(SkaterName))
        {
            return "Private Skater";
        }

        if (IsStravaAccount && SkaterName.Contains("_"))
        {
            return SkaterName.Replace("_", " ");
        }

        return SkaterName;
    }
}