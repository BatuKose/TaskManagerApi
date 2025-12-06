using Entites.Data_Transfer_object.JobDetail;
using Entites.Enums;
using Entites.Exceptions.CustomExceptions;
using Entites.Models;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class JobDetailManager: IJobDetailService
    {
        private readonly IRepositoryManager _repositoryManager;

        public JobDetailManager(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<InsertJobDetailDTO> InsertJobDetailAsync(InsertJobDetailDTO detailDTO)
        {
            var user = await _repositoryManager.UserRepository.UserExistsAsync(detailDTO.userId);
            if (user==false) throw new NotFoundException("Kullanıcı bulunamadı");
            var header = await _repositoryManager.JobHeaderRepository.SelectJobHeaderById(detailDTO.HeaderId);
            if (header is null) throw new NotFoundException("İş bulunamadı");
            if (header.Status==JobStatusEnum.JobStatus.Bekleniyor) throw new BadRequestException("İşi karşılamadan detay ekleyemezsin");
            if (header.Status==JobStatusEnum.JobStatus.Done) throw new BadRequestException("İşleme kapatılmış işi düzenleyemezsin");
            if (header.AssignedUserId!=detailDTO.userId) throw new BadRequestException("Başkasının üzerindeki işe müdahale edilemez");
            var Insert = new JobDetail()
            {
                Detail=detailDTO.Detail,
                HeaderId=detailDTO.HeaderId,
                userId=detailDTO.userId,
                jobDetayStatus=detailDTO.jobDetayStatus
            };
          await  _repositoryManager.JobDetailRepository.InsertJobDetailAsync(Insert);
            var retunDto = new InsertJobDetailDTO()
            {
                Detail=Insert.Detail,
                HeaderId=Insert.HeaderId,
                userId= Insert.userId,
                jobDetayStatus=Insert.jobDetayStatus
            };
            return retunDto;
        }
    }
}
