using System;
using log4net;
using log4net.Config;
using UKSSDC.Services.Data;

namespace UKSSDC
{
    public class PostcodePerimeters : IRecord
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(UnitOfWork));

        private readonly IUnitOfWork _unitOfWork;

        public PostcodePerimeters(IUnitOfWork unitOfWork)
        {
            XmlConfigurator.Configure();

            _unitOfWork = unitOfWork;
        }

        public bool Run()
        {
            throw new NotImplementedException();
        }
    }
}