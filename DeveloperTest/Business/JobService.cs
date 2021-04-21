using System.Linq;
using DeveloperTest.Business.Interfaces;
using DeveloperTest.Database;
using DeveloperTest.Database.Models;
using DeveloperTest.Models;

namespace DeveloperTest.Business
{
    public class JobService : IJobService
    {
        private readonly ApplicationDbContext context;

        public JobService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public JobModel[] GetJobs()
        {
            return context.Jobs.Select(x => new JobModel
            {
                JobId = x.JobId,
                Engineer = x.Engineer,
                When = x.When,
                Customer = x.Customer != null ? new CustomerModel
                {
                    CustomerId = x.Customer.CustomerId,
                    Type = x.Customer.Type,
                    Name = x.Customer.Name

                } : new CustomerModel
                {
                    CustomerId = 0,
                    Name = "Unknown",
                    Type = "Unknown"
                }
            }).ToArray();
        }

        public JobModel GetJob(int jobId)
        {
            return context.Jobs.Where(x => x.JobId == jobId).Select(x => new JobModel
            {
                JobId = x.JobId,
                Engineer = x.Engineer,
                When = x.When,
                Customer = x.Customer != null ? new CustomerModel
                {
                    CustomerId = x.Customer.CustomerId,
                    Type = x.Customer.Type,
                    Name = x.Customer.Name

                } : new CustomerModel
                {
                    CustomerId = 0,
                    Name = "Unknown",
                    Type = "Unknown"
                }
            }).SingleOrDefault();
        }

        public JobModel CreateJob(BaseJobModel model)
        {
            var customer = context.Customers.Find(model.Customer.CustomerId);
            var addedJob = context.Jobs.Add(new Job
            {
                Engineer = model.Engineer,
                When = model.When,
                Customer = customer
            });

            context.SaveChanges();

            return new JobModel
            {
                JobId = addedJob.Entity.JobId,
                Engineer = addedJob.Entity.Engineer,
                When = addedJob.Entity.When,
                Customer = new CustomerModel
                {
                    CustomerId = customer.CustomerId,
                    Type = customer.Type,
                    Name = customer.Name
                }
            };
        }
    }
}
