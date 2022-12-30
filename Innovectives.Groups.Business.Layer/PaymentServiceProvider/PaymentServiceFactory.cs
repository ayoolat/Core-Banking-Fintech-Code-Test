using Innovectives.Groups.Business.Layer.PaymentServiceProviders.Interface;

namespace Innovectives.Groups.Business.Layers.PaymentServiceProviders
{
    abstract class PaymentServiceFactory
    {
        public abstract IPaymentProvider FactoryMethod(string type);
    }
}
