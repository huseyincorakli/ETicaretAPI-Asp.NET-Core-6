using MediatR;

namespace ETicaretAPI_V2.Application.Features.Queries.Order.GetOrderById
{
    public class GetOrderByIdQueryRequest:IRequest<GetOrderByIdQueryResponse>
    {
        public string Id { get; set; }
    }
}
