using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Manager.Core.Models;
using Manager.Core.Repositories;
using Manager.Struct.Extensions;
using static Manager.Struct.Extensions.RandomExtensions;
using NLog;

namespace Manager.Struct.Services
{
    public class DataRefiller : IDataRefiller
    {
        private readonly IUserService _userService;
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IActivityRepository _activityRepository;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();


        public DataRefiller(IUserService userService, IScheduleRepository scheduleRepository,
            IActivityRepository activityRepository)
        {
            _userService = userService;
            _scheduleRepository = scheduleRepository;
            _activityRepository = activityRepository;
        }

        public async Task SeedAsync()
        {
            var users = await _userService.GetAllAsync();

            if (users.Any())
            {
                Logger.Trace("Data was already Initialized.");

                return;
            }
          
            Logger.Trace("Initializing data ...");

            var user1 = await _userService.RegisterAsync("PiotrK", "piotr@gmail.com", "PiotrKantorowicz", "secret", "avatar_02.png", "admin", "Developer");
            Logger.Trace($"Adding user: '{user1}'.");
            var user2 = await _userService.RegisterAsync("SandraS", "sandra@gmail.com" , "Sandra Sernik", "secret", "avatar_03.jpg", "editor", "Web Designer");
            Logger.Trace($"Adding user: '{user2}'.");
            var user3 = await _userService.RegisterAsync("JanuszC", "janusz@gmail.com", "Janusz Cieslak", "secret", "avatar_03.jpg", "editor", "Quality Assurance");
            Logger.Trace($"Adding user: '{user3}'.");
            var user4 = await _userService.RegisterAsync("Vorek", "vortek@gmail.com", "Vorek Vox", "secret", "avatar_04.jpg", "user", "Developer");
            Logger.Trace($"Adding user: '{user4}'.");
            var user5 = await _userService.RegisterAsync("Artur", "artur@gmail.com", "Artut Kupczak", "secret", "avatar_04.jpg", "user", "kasztan");
            Logger.Trace($"Adding user: '{user5}'.");
            var user6 = await _userService.RegisterAsync("Kamil", "kamil@gmail.com", "Kamil Kuczkeos", "secret", "avatar_01.png", "contractor", "Web Designer");
            Logger.Trace($"Adding user: '{user6}'.");

            for (var i = 1; i <= 300; i++)
            {
                var r2 = CustomRandom(2);
                var r3 = CustomRandom(3);
                var r5 = CustomRandom(5);
                var r6 = CustomRandom(6);
                var hours = CustomRandom(24);

                var rndSchTitle = RefillerExtensions.SchTitle();
                var rndTkTitle = RefillerExtensions.TkTitle();
                var rndSchDescription = RefillerExtensions.SchDescription();
                var rndTkDescription = RefillerExtensions.TkDescription();
                var rndlocation = RefillerExtensions.Location();
                var schEnum = (ScheduleType)r5;
                var schsEnum = (ScheduleStatus)r2;
                var tkEnum = (ActivityType)r5;
                var tkpEnum = (ActivityPriority)r3;
                var tksEnum = (ActivityStatus)r2;
                var timeStart = DateTime.UtcNow.AddHours(hours);
                var timeEnd = DateTime.UtcNow.AddHours(hours);

                var schedule = new Schedule
                {
                    Title = rndSchTitle,
                    Description = rndSchDescription,
                    Location = rndlocation,
                    CreatorId = r6,
                    Status = schsEnum,
                    Type = schEnum,
                    TimeStart = timeStart,
                    TimeEnd = timeEnd,
                    Attendees = new List<Attendee>
                    {
                        new Attendee { ScheduleId = CustomRandom(6), UserId = CustomRandom(6) },
                        new Attendee { ScheduleId = CustomRandom(6), UserId = CustomRandom(6) },
                        new Attendee { ScheduleId = CustomRandom(6), UserId = CustomRandom(6) }
                    }
                };

                await _scheduleRepository.AddAsync(schedule);

                Logger.Trace($"Adding schedule{i}");

                var activity = new Activity()
                {
                    Title = rndTkTitle,
                    Description = rndTkDescription,
                    Location = rndlocation,
                    CreatorId = r6,
                    Status = tksEnum,
                    Type = tkEnum,
                    Priority = tkpEnum,
                    TimeStart = timeStart,
                    TimeEnd = timeEnd                  
                };

                await _activityRepository.AddAsync(activity);

                Logger.Trace($"Adding Task{i}");
            }

            Logger.Trace("Data was initialized.");
        }
    }
}