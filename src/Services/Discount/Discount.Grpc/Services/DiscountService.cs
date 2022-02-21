using AutoMapper;
using Discount.Grpc.Protos;
using Discount.Grpc.Repositories;
using Discount.Shared.Entities;
using Grpc.Core;

namespace Discount.Grpc.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IDiscountRepository repository;
        private readonly ILogger<DiscountService> logger;
        private readonly IMapper mapper;

        public DiscountService(IDiscountRepository repository, ILogger<DiscountService> logger, IMapper mapper)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await repository.GetCoupon(request.Productname);
            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Dicount with product name = {request.Productname} is not found ."));
            }
            logger.LogInformation("Discount is retrieved for ProductName : {productName},  Amount : {amount}", coupon.ProductName, coupon.Amount);

            var couponModel = mapper.Map<CouponModel>(coupon);
            return couponModel;
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = mapper.Map<Coupon>(request.Coupon);

            await repository.CreateCoupon(coupon);
            logger.LogInformation("Discount is successfully created . ProductName : {ProductName}", coupon.ProductName);

            var couponModel = mapper.Map<CouponModel>(coupon);
            return couponModel;
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = mapper.Map<Coupon>(request.Coupon);

            await repository.UpdateCoupon(coupon);
            logger.LogInformation("Discount is successfully updated . ProductName : {ProductName}", coupon.ProductName);

            var couponModel = mapper.Map<CouponModel>(coupon);
            return couponModel;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var delete = await repository.DeleteCoupon(request.Productname);
            var response = new DeleteDiscountResponse
            {
                Success = delete
            };
            return response;
        }
    }
}
