using AutoMapper;
using Discount.Grpc.Entities;
using Discount.Grpc.Protos;
using Discount.Grpc.Repositories;
using Grpc.Core;

namespace Discount.Grpc.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        public readonly IDiscountRepository _discountRepository;
        public readonly ILogger<DiscountService> _logger;
        public readonly IMapper _mapper;
        public DiscountService(IDiscountRepository discountRepository, ILogger<DiscountService> logger, IMapper mapper)
        {
            _discountRepository = discountRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _discountRepository.GetDiscount(request.ProductName);
            if(coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with product name = {request.ProductName} no have fount!"));
            }
            return _mapper.Map<CouponModel>(coupon);
        }
        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            await _discountRepository.CreateDiscount(_mapper.Map<Coupon>(request.Coupon));
            return _mapper.Map<CouponModel>(request.Coupon);
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            await _discountRepository.UpdateDiscount(_mapper.Map<Coupon>(request.Coupon));
            return _mapper.Map<CouponModel>(request.Coupon);
        }
        public override async Task<DeleteDiscountReponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var deleted = await _discountRepository.DeleteDiscount(request.ProductName);
            var respone = new DeleteDiscountReponse
            {

                Success = deleted
            };
            return respone;
        }
    }
}
