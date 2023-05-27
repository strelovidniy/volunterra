﻿using EntityFrameworkCore.RepositoryInfrastructure;

namespace VolunteerManager.Data.Entities;

public class ContactInfo: Entity, IEntity
{
    public string PhoneNumber { get; set; }
    public string City { get; set; }
    public string Region { get; set; }
    public string LinkedInUrl { get; set; }
    public User? Users { get; set; }
    public Organization? Organization { get; set; }
    public Guid? OrganizationId { get; set; }
    public Guid? UserId { get; set; }
}