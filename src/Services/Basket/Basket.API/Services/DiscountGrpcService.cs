using Discount.Grpc.Protos;

namespace Basket.API.Services
{
    public class DiscountGrpcService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient discountProtoService;
        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountProtoService)
        {
            this.discountProtoService = discountProtoService;
        }

        public async Task<CouponModel> GetDiscount(string Productname)
        {
            var discountRequset = new GetDiscountRequest { Productname = Productname };

            return await discountProtoService.GetDiscountAsync(discountRequset);
        }
    }
}
