using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Manager.Core.Models;
using Manager.Core.Repositories;
using Manager.Struct.Extensions;
using static Manager.Struct.Extensions.RandomExtensions;
using static Manager.Struct.Extensions.RefillerExtensions;
using NLog;
using Manager.Struct.EF;

namespace Manager.Struct.Services
{
    public class DataRefiller : IDataRefiller
    {
        private readonly IUserService _userService;
        private readonly IAccountService _accountService;
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IActivityRepository _activityRepository;
        private readonly IUnitOfWork _unitOfWork;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();


        public DataRefiller(IUserService userService, IAccountService accountService, 
        IScheduleRepository scheduleRepository, IActivityRepository activityRepository,
        IUnitOfWork unitOfWork)
        {
            _userService = userService;
            _accountService = accountService;
            _scheduleRepository = scheduleRepository;
            _activityRepository = activityRepository;
            _unitOfWork = unitOfWork;
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

            await _accountService.SignUpAsync(Guid.NewGuid(),"PiotrK", "PiotrKantorowicz", "piotr@gmail.com", "secret", "avatar_02.png", "Developer", "admin");
            Logger.Trace($"Adding user: 1");
            await _accountService.SignUpAsync(Guid.NewGuid(), "SandraS", "Sandra Sernik", "sandra@gmail.com" , "secret", "avatar_03.jpg", "Web Designer", "editor");
            Logger.Trace($"Adding user: 2");
            await _accountService.SignUpAsync(Guid.NewGuid(), "JanuszC", "Janusz Cieslak", "janusz@gmail.com", "secret", "avatar_03.jpg", "Quality Assurance", "editor");
            Logger.Trace($"Adding user: 3");
            await _accountService.SignUpAsync(Guid.NewGuid(), "Vorek", "Vorek Vox", "vortek@gmail.com", "secret", "avatar_04.jpg", "Developer", "user");
            Logger.Trace($"Adding user: 4");
            await _accountService.SignUpAsync(Guid.NewGuid(), "Artur", "Artut Kupczak", "artur@gmail.com", "secret", "avatar_04.jpg", "kasztan", "user");
            Logger.Trace($"Adding user: 8");
            await _accountService.SignUpAsync(Guid.NewGuid(), "Kamil", "Kamil Kuczkeos", "kamil@gmail.com", "secret", "avatar_01.png", "Web Designer", "user");
            Logger.Trace($"Adding user: 6.");  

            for (var i = 1; i <= 300; i++)
            {
                var r2 = CustomRandom(2);
                var r3 = CustomRandom(3);
                var r5 = CustomRandom(5);
                var r6 = CustomRandom(6);
                var hours = CustomRandom(24);

                var rndSchTitle = SchTitle();
                var rndTkTitle = TkTitle();
                var rndSchDescription = SchDescription();
                var rndTkDescription = TkDescription();
                var rndlocation = Location();
                var schtype = ChooseSchType();
                var actType = ChooseType();
                var priority = ChoosePriority();
                var status = ChooseStatus();
                var timeStart = DateTime.UtcNow.AddHours(hours);
                var timeEnd = DateTime.UtcNow.AddHours(hours);

                var schedule = new Schedule
                {
                    Title = rndSchTitle,
                    Description = rndSchDescription,
                    Location = rndlocation,
                    CreatorId = r6,
                    Status = status,
                    Type = schtype,
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
                    Status = status,
                    Type = actType,
                    Priority = priority,
                    TimeStart = timeStart,
                    TimeEnd = timeEnd                  
                };

                await _activityRepository.AddAsync(activity);              
                await _unitOfWork.SaveChangesAsync();

                Logger.Trace($"Adding Task{i}");
            }

            Logger.Trace("Data was initialized.");
        }
    }
}