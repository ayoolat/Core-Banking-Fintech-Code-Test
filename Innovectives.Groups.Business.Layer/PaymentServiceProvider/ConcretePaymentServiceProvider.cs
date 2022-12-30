using AutoMapper;
using Innovectives.Groups.Business.Layer.PaymentServiceProvider.Flutterwave;
using Innovectives.Groups.Business.Layer.PaymentServiceProviders.Interface;
using Innovectives.Groups.Business.Layer.PaymentServiceProviders.Paystack;
using Innovectives.Groups.Business.Layers.PaymentServiceProviders;
using Microsoft.Extensions.Configuration;

namespace Innovectives.Groups.Business.Layer.PaymentServiceProviders
{
    class ConcretePaymentServiceProvider : PaymentServiceFactory
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public ConcretePaymentServiceProvider(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }
        public override IPaymentProvider FactoryMethod(string type)
        {
            switch (type.ToLower())
            {
                case "paystack": return new PaystackProvider(_configuration, _mapper);
                case "flutterwave": return new flutterwaveProvider(_configuration, _mapper);
                default: return new flutterwaveProvider(_configuration, _mapper);
            }
        }
    }
}
