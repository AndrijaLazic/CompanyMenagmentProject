using DAL;
using DOMAIN.Abstractions;
using DOMAIN.Models.DTR;
using DOMAIN.Shared;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class GlobalDataService
    {
        private readonly AppConfigClass _options;
        private FrontEndSettingsDTR? _frontEndSettingsDTR = null;
        private IServiceScopeFactory _serviceScopeFactory;

        public GlobalDataService(IOptions<AppConfigClass> options,  IServiceScopeFactory serviceScopeFactory)
        {
            _options = options.Value;
            _serviceScopeFactory= serviceScopeFactory;
        }

        public FrontEndSettingsDTR GetFrontEndSettingsDTR()
        {
            if (_frontEndSettingsDTR == null)
            {
                SetSettings();
            }
            return _frontEndSettingsDTR!;
        }

        private void SetSettings()
        {
            using IServiceScope scope = _serviceScopeFactory.CreateScope();

            var _userDataDB = scope
                .ServiceProvider
                .GetRequiredService<IUserDataDB>();

            var _workCalendarDB = scope
                .ServiceProvider
                .GetRequiredService<WorkCalendarDB>();


            _frontEndSettingsDTR = new FrontEndSettingsDTR();
            _frontEndSettingsDTR.userDTRs = new List<UserDTR>();
            var users = _userDataDB.GetAllUsers();
            for (int i = 0; i < users.Length; i++)
            {
                _frontEndSettingsDTR.userDTRs.Add(new UserDTR()
                {
                    Email = users[i].Email,
                    Id = users[i].Id,
                    Lastname = users[i].Lastname,
                    Name = users[i].Name,
                    PhoneNumber = users[i].PhoneNumber,
                    WorkerType = users[i].WorkerType,
                });
            }

            var workerTypes = _userDataDB.GetWorkerTypes();
            _frontEndSettingsDTR.workerTypesDTRs = new List<WorkerTypeDTR>();
            for (int i = 0; i < workerTypes.Length; i++)
            {
                _frontEndSettingsDTR.workerTypesDTRs.Add(new WorkerTypeDTR()
                {
                    Id = workerTypes[i].Id,
                    TypeName = workerTypes[i].TypeName,
                });
            }

            var shiftDTRs = _workCalendarDB.GetShiftTypes();
            _frontEndSettingsDTR.shiftDTRs = new List<ShiftTypeDTR>();
            for (int i = 0; i < shiftDTRs.Length; i++)
            {
                _frontEndSettingsDTR.shiftDTRs.Add(new ShiftTypeDTR()
                {
                    EndTime = shiftDTRs[i].EndTime,
                    ShiftNumber = shiftDTRs[i].ShiftNumber,
                    StartTime = shiftDTRs[i].StartTime,
                });
            }
        }
    }

}
