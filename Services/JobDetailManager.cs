using Entites.Data_Transfer_object.JobDetail;
using Entites.Enums;
using Entites.Exceptions.CustomExceptions;
using Entites.Models;
using Entites.View;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
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

        public async Task<JobDetail> DeleteJobDetailAsync(int id)
        {
            if (id <= 0) throw new BadRequestException("gelen başlık id bilgisi sıfırdan büyük olmalı.");
            var jobDetail = await _repositoryManager.JobDetailRepository.GetJobDetailByIdAsync(id);
            if (jobDetail == null) throw new NotFoundException("İş detayı bulunamadı");
            var isJobDone = _repositoryManager.JobDetailRepository.IsJobDone(id);
            if (isJobDone is not null) throw new BadRequestException("İşlemi bitmiş iş silinemez");
            await _repositoryManager.JobDetailRepository.DeleteJobDetailAsync(jobDetail);
            return jobDetail;
        }

        public List<CezalıIslerView> GetCezalıİsler()
        {
            try
            {
                string newResult = "";
                var result = _repositoryManager.JobDetailRepository.GetCezalıİsler();  
                return result;      
            }
            catch (Exception)
            {

                throw new Exception("Bir şeyler ters gitti lütfen bir daha deneyin");
            }
           
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

        public IQueryable<JobDetayStatusWithHeaderDTO> SelectJobDetaiAllDetail(int id)
        {
            if (id<=0) throw new BadRequestException("İş başlık id bilgisi boş olamaz");
            bool HedarExist=_repositoryManager.JobDetailRepository.HeaderVarmi(id);
            if (HedarExist==false) throw new  BadRequestException("İş başlık bilgisi bulunamadı");
            bool DetailExist= _repositoryManager.JobDetailRepository.DetailVarmi(id);
            if (DetailExist==false) throw new BadRequestException("İş detay bilgisi bulunamadı");
            var result = _repositoryManager.JobDetailRepository.JobStatusWithHedaer(id);
            return result;
        }
        public async Task<List<JobDetayStatusWithHeaderDTO>>BütünİsleriGetirAsync(bool? active)
        {
            var isler = await _repositoryManager.JobDetailRepository.BütünisleriGetir(active);
            if (isler is null || !isler.Any()) throw new NotFoundException("Geçerli iş bulunamadı");
            return isler;
        }
    }
}
