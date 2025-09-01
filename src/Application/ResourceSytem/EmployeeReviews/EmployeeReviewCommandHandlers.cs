using DbApp.Domain.Entities.ResourceSystem;
using DbApp.Domain.Interfaces.ResourceSystem;
using MediatR;

namespace DbApp.Application.ResourceSystem.EmployeeReviews
{
    public class CreateEmployeeReviewCommandHandler : IRequestHandler<CreateEmployeeReviewCommand, int>
    {
        private readonly IEmployeeReviewRepository _employeeReviewRepository;

        public CreateEmployeeReviewCommandHandler(IEmployeeReviewRepository employeeReviewRepository)
        {
            _employeeReviewRepository = employeeReviewRepository;
        }

        public async Task<int> Handle(CreateEmployeeReviewCommand request, CancellationToken cancellationToken)
        {
            var review = new EmployeeReview
            {
                EmployeeId = request.EmployeeId,
                Period = request.Period,
                Score = request.Score,
                EvaluationLevel = request.EvaluationLevel,
                EvaluatorId = request.EvaluatorId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            return await _employeeReviewRepository.CreateAsync(review);
        }
    }

    public class UpdateEmployeeReviewCommandHandler : IRequestHandler<UpdateEmployeeReviewCommand>
    {
        private readonly IEmployeeReviewRepository _employeeReviewRepository;

        public UpdateEmployeeReviewCommandHandler(IEmployeeReviewRepository employeeReviewRepository)
        {
            _employeeReviewRepository = employeeReviewRepository;
        }

        public async Task Handle(UpdateEmployeeReviewCommand request, CancellationToken cancellationToken)
        {
            var review = await _employeeReviewRepository.GetByIdAsync(request.ReviewId);
            if (review == null)
            {
                throw new InvalidOperationException($"未找到ID为 {request.ReviewId} 的员工绩效记录");
            }

            review.Score = request.Score;
            review.EvaluationLevel = request.EvaluationLevel;
            review.EvaluatorId = request.EvaluatorId;
            review.UpdatedAt = DateTime.UtcNow;

            await _employeeReviewRepository.UpdateAsync(review);
        }
    }

    public class DeleteEmployeeReviewCommandHandler : IRequestHandler<DeleteEmployeeReviewCommand>
    {
        private readonly IEmployeeReviewRepository _employeeReviewRepository;

        public DeleteEmployeeReviewCommandHandler(IEmployeeReviewRepository employeeReviewRepository)
        {
            _employeeReviewRepository = employeeReviewRepository;
        }

        public async Task Handle(DeleteEmployeeReviewCommand request, CancellationToken cancellationToken)
        {
            var review = await _employeeReviewRepository.GetByIdAsync(request.ReviewId);
            if (review == null)
            {
                throw new InvalidOperationException($"未找到ID为 {request.ReviewId} 的员工绩效记录");
            }

            await _employeeReviewRepository.DeleteAsync(review);
        }
    }
}
