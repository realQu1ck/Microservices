using Dapper;
using Discount.Shared.Entities;
using Npgsql;

namespace Discount.API.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration configuration;
        public DiscountRepository(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        public async Task<Coupon> GetCoupon(string productName)
        {
            using var connection = new NpgsqlConnection(
                configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>
                ("SELECT * FROM Coupon WHERE ProductName = @ProductName",
                new { ProductName = productName });
            if (coupon == null)
                return new Coupon
                {
                    ProductName = "No Discount",
                    Amount = 0,
                    Description = "No Discount"
                };
            return coupon;
        }
        public async Task<bool> CreateCoupon(Coupon model)
        {
            using var connection = new NpgsqlConnection(
                      configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var execstatus = await connection.ExecuteAsync
                ("INSERT INTO Coupon (ProductName, Description, Amount) VALUES (@ProductName, @Description, @Amount)",
                    new
                    {
                        ProductName = model.ProductName,
                        Description = model.Description,
                        Amount = model.Amount
                    });

            if (execstatus == 0)
                return false;
            return true;
        }
        public async Task<bool> UpdateCoupon(Coupon model)
        {
            using var connection = new NpgsqlConnection(
                      configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var execstatus = await connection.ExecuteAsync("UPDATE Coupon SET ProductName=@ProductName, Description=@Decription, Amount=@Amount WHERE Id=@Id",
                new
                {
                    Id = model.Id,
                    ProductName = model.ProductName,
                    Description = model.Description,
                    Amount = model.Amount
                });
            if (execstatus == 0)
                return false;
            return true;
        }

        public async Task<bool> DeleteCoupon(string productName)
        {
            using var connection = new NpgsqlConnection(
                      configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var execstatus = await connection.ExecuteAsync("DELETE FROM Coupon WHERE ProductName=@ProductName",
                new
                {
                    ProductName = productName,
                });

            if (execstatus == 0)
                return false;
            return true;
        }
    }
}
