using Discount.Shared.Entities;

namespace Discount.Grpc.Repositories
{
    public interface IDiscountRepository
    {
        Task<Coupon> GetCoupon(string productName);
        Task<bool> CreateCoupon(Coupon model);
        Task<bool> UpdateCoupon(Coupon model);
        Task<bool> DeleteCoupon(string productName);
    }
}
