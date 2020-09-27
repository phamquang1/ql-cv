using Abp.Dependency;
using Abp.Domain.Services;
using NCCTalentManagement.IoC;
using System;
using System.Collections.Generic;
using System.Text;

namespace NCCTalentManagement.DomainServices
{
    public class BaseDomainService : DomainService
    {
        public IWorkScope WorkScope { get; set; }
        public BaseDomainService()
        {
            this.WorkScope = IocManager.Instance.Resolve<IWorkScope>();
        }
    }
}
