﻿using DotnetBaseKit.Components.Shared.Notifications;
using FluentValidation.Results;

namespace DotnetBaseKit.Components.Domain.Sql.Entities.Base
{
    public abstract class BaseEntity : Notifiable<Notification>, IBaseEntity
    {
        public BaseEntity()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }
        public Guid Id { get; protected set; }
        public DateTime CreatedAt { get; protected set; }

        public abstract void Validate();
    }
}
