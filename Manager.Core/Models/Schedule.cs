using System;
using System.Collections.Generic;
using Manager.Core.Exceptions;

namespace Manager.Core.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public string Location { get; set; }
        public User Creator { get; set; }
        public int CreatorId { get; set; }
        public ScheduleType Type { get; set; }
        public ScheduleStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ICollection<Attendee> Attendees { get; set; }

        public Schedule()
        {
            Attendees = new List<Attendee>();
        }

        public Schedule(string title, string description, DateTime timestart, DateTime timeEnd,
            string location, int creatorId, ScheduleType type, ScheduleStatus status)
        {
            SetTitle(title);
            SetDescription(description);
            SetTimeStart(timestart);
            SetTimeEnd(timeEnd);
            SetCreator(creatorId);
            SetLocation(location);
            Type = type;
            Status = status;
        }

        public void SetTitle(string title)
        {
            if (String.IsNullOrEmpty(title))
            {
                throw new DomainException(ErrorCodes.InvalidTitle,
                    "Title can not be empty.");
            }
            if (title.Length > 250)
            {
                throw new DomainException(ErrorCodes.InvalidTitle,
                    "Title can not be longer than 250 characters.");
            }

            Title = title;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetDescription(string description)
        {
            if (String.IsNullOrEmpty(description))
            {
                throw new DomainException(ErrorCodes.InvalidDescription,
                    "Description can not be empty.");
            }

            Description = description;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetTimeStart(DateTime timeStart)
        {
            if (timeStart == null)
            {
                throw new DomainException(ErrorCodes.InvalidTimeStart,
                    "TimeStart must have a value.");
            }
            if (timeStart < DateTime.UtcNow.AddDays(-7))
            {
                throw new DomainException(ErrorCodes.InvalidTimeStart,
                    "Can't add date start of task with more than 7 days later.");
            }

            TimeStart = timeStart;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetTimeEnd(DateTime timeEnd)
        {
            if (timeEnd == null)
            {
                throw new DomainException(ErrorCodes.InvalidTimeEnd,
                    "TimeEnd must have a value.");
            }
            if (timeEnd < TimeStart)
            {
                throw new DomainException(ErrorCodes.InvalidTimeEnd,
                    "TimeEnd must be greater than timeStart.");
            }

            TimeEnd = timeEnd;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetCreator(int creatorId)
        {
            CreatorId = creatorId;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetLocation(string location)
        {
            if (location.Length > 150)
            {
                throw new DomainException(ErrorCodes.InvalidLocation,
                    "Location field can not be longer than 150 characters.");
            }

            Location = location;
            UpdatedAt = DateTime.UtcNow;
        }
    }

    public enum ScheduleStatus
    {
        Valid = 1,
        Cancelled = 2
    }

    public enum ScheduleType 
    {
        Work = 1,
        Coffee = 2,
        Doctor = 3,
        Shopping = 4,
        Other = 5
    }
}