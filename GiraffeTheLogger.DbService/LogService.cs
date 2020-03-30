using System;
using System.Threading.Tasks;
using AutoMapper;
using GiraffeTheLogger.DbServiceContract;
using GiraffeTheLogger.Domain.Dtos;
using GiraffeTheLogger.Domain.Entities;
using Nest;

namespace GiraffeTheLogger.DbService
{
    public class LogService : ILogService
    {
        private readonly IElasticClient elasticClient;
        private readonly IMapper mapper;

        public LogService(IElasticClient elasticClient, IMapper mapper)
        {
            this.elasticClient = elasticClient;
            this.mapper = mapper;
        }

        public async Task<Guid> CreateAsync(MessageDto messageDto)
        {
            var logEntity = mapper.Map<LogEntity>(messageDto);
            
            await elasticClient.IndexDocumentAsync(logEntity);
            
            return logEntity.Id;
        }
    }
}