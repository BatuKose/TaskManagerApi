using Entites.Data_Transfer_object.JobHeader;
using Entites.Enums;
using Entites.Exceptions.CustomExceptions;
using Entites.Models;
using Repositories.Contracts;
using Repositories.EFCore;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entites.Enums.JobStatusEnum;

namespace Services
{
    public class JobHeaderManager : IJobHeaderService
    {
        private readonly IRepositoryManager _repositoryManager;

        public JobHeaderManager(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public  async Task<JobHeader> DeleteJobHeader(int id)
        {
            if (id < 0) throw new BadRequestException("Silinmesi istenilen iş başlık bilgileri boştur");
            var isVarmi= await _repositoryManager.JobHeaderRepository.FindJobHeaderAsync(id);
            if (isVarmi == null) throw new NotFoundException("İş başlık bilgileri bulunamadı kontrol ediniz");
            if (isVarmi.Status!=JobStatus.Bekleniyor) throw new BadRequestException("İşleme alınmış işi silemezsiniz");
           await _repositoryManager.JobHeaderRepository.DeleteHeaderJobAsync(isVarmi);
            return isVarmi;
        }

        public async Task<CreateJobHeaderDTO> InsertJobInsertJobHeader(CreateJobHeaderDTO jobHeaderDTO)
        {
            if (jobHeaderDTO is null) throw new NotFoundException("Başlık bilgileri bulunamadı");
            if (jobHeaderDTO.ManagerId<=0) throw new NotFoundException("Yönetici bilgileri bulunamadı");
            if (jobHeaderDTO.AssignedUserId<=0) throw new NotFoundException("çalışan bilgileri bulunamadı");
            bool ManagerExits = await _repositoryManager.JobHeaderRepository.FındAdminOrManagerWorkersAsync(jobHeaderDTO.ManagerId);
            if (!ManagerExits) throw new BadRequestException("Sadece yönetici rolündekiler iş açabilir");
            bool isActiveManager = await _repositoryManager.JobHeaderRepository.isUserActive(jobHeaderDTO.ManagerId);
            if (!isActiveManager) throw new BadRequestException("Yönetici rolündeki kullanıcı aktif değil.");
            bool isActiveWorker = await _repositoryManager.JobHeaderRepository.isUserActive(jobHeaderDTO.ManagerId);
            if (!isActiveWorker) throw new BadRequestException("çalışan rolündeki kullanıcı aktif değil.");
            bool WorkerExist = await _repositoryManager.JobHeaderRepository.FındWorkersAsync(jobHeaderDTO.AssignedUserId);
            if (!WorkerExist) throw new BadRequestException("yönetici rolündeki kullanıcılara iş açılamaz");
            if (jobHeaderDTO.ManagerId==jobHeaderDTO.AssignedUserId) throw new BadRequestException("İşi açanla karşılayan aynı personel olamaz");
            var jobHeader = new JobHeader()
            {
                Title=jobHeaderDTO.Title,
                ManagerId=jobHeaderDTO.ManagerId,
                AssignedUserId=jobHeaderDTO.AssignedUserId,
                Deadline=jobHeaderDTO.Deadline
                
            };
            
            await _repositoryManager.JobHeaderRepository.InsertJobHeader(jobHeader);

            return new CreateJobHeaderDTO
            {
                Title= jobHeader.Title,
                ManagerId=jobHeader.ManagerId,
                AssignedUserId=jobHeader.AssignedUserId,
                Deadline=jobHeader.Deadline
            };

        }

        public SelectJobHeaderDTO SelectJobHeader(int id)
        {
            if (id < 0) throw new BadRequestException("İş id bilgileri bulunamadı");
            var result= _repositoryManager.JobHeaderRepository.SelectJobHeader(id);
            return result;
        }
    }

}
