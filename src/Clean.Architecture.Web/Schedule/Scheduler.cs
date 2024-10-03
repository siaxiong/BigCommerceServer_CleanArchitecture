using Clean.Architecture.UseCases.Abstractions;
using Clean.Architecture.UseCases.Abstractions.Respository;
using Quartz;

namespace Clean.Architecture.Web.Schedule;


public class MyJob : IJob
{
  IBigCommerceRepository _bigCommerceRepository;

  public MyJob(IBigCommerceRepository bigCommerceRepository)
  {
    _bigCommerceRepository = bigCommerceRepository;
  }
  
  async public Task Execute(IJobExecutionContext context)
  {
    await _bigCommerceRepository.UpdateAllCustomerCredits();
  }
}

