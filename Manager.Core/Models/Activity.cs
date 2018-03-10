using System;
using Manager.Core.Exceptions;

namespace Manager.Core.Models
{
    public class Activity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public string Location { get; set; }
        public User Creator { get; set; }
        public int CreatorId { get; set; }
        public ActivityType Type { get; set; }
        public ActivityPriority Priority { get; set; }
        public ActivityStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Activity()
        {
        }

        public Activity(string title, string description, DateTime timestart, DateTime timeEnd, 
            string location, int creatorId, int type, int priority, int status)
        {
            SetTitle(title);
            SetDescription(description);
            SetTimeStart(timestart);
            SetTimeEnd(timeEnd);
            SetCreator(creatorId);
            SetLocation(location);
            SetType(type);
            SetPriority(priority);
            SetStatus(status);
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
            if (String.IsNullOrEmpty(location))
            {
                throw new DomainException(ErrorCodes.InvalidLocation,
                    "Location can not be empty.");
            }
            if (location.Length > 150)
            {
                throw new DomainException(ErrorCodes.InvalidLocation,
                    "Location field can not be longer than 150 characters.");
            }

            Location = location;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetType(int type)
        {
            Type = (ActivityType)type;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetStatus(int status)
        {
            Type = (ActivityType)status;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetPriority(int priority)
        {
            Type = (ActivityType)priority;
            UpdatedAt = DateTime.UtcNow;
        }
    }

    public enum ActivityPriority : byte
    {
        High = 1,
        Medium = 2,
        Low = 3
    }

    public enum ActivityStatus : byte
    {
        ToMake = 1,
        Done = 2
    }

    public enum ActivityType : byte
    {
        Work = 1,
        Programming = 2,
        Private = 3,
        Other = 4
    }
}