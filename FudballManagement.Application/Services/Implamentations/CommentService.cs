using AutoMapper;
using FudballManagement.Application.DTOs.Stadium.SadiumComment;
using FudballManagement.Application.Exceptions;
using FudballManagement.Application.Extensions;
using FudballManagement.Application.Services.Interfaces;
using FudballManagement.Domain.Entities.Stadiums;
using FudballManagement.Domain.Entities.Users;
using FudballManagement.Infrastructure.Repositories.Interfaces;

namespace FudballManagement.Application.Services.Implamentations
{
    public class CommentService : ICommentService
    {
        private readonly IGenericRepository<StadiumComment> _stadiumCommentsRepository;
        private readonly IGenericRepository<Customer> _customerRepository;
        private readonly IMapper _mapper;

        public CommentService(IGenericRepository<StadiumComment> genericRepository,IMapper mapper, IGenericRepository<Customer> genericRepository1)
        {
            _stadiumCommentsRepository = genericRepository;
            _mapper = mapper;
            _customerRepository = genericRepository1;
        }

        public async Task<StadiumCommentResponseDto> CreateAsync(StadiumComentCreateDto dto, CancellationToken token)
        {
            var customer = await _customerRepository.GetAsync(c => c.Id == dto.CustomerId);
            if (customer == null)
                throw new CustomException(404, "Customer not found");

            var comment = _mapper.Map<StadiumComment>(dto);
            comment.CreatedAt = DateTimeHelper.GetUtcPlus5Now(); // fix timestamp

            var created = await _stadiumCommentsRepository.CreateAsync(comment, token);
            if (created == null)
                throw new CustomException(500, "Failed to create comment");

            var response = _mapper.Map<StadiumCommentResponseDto>(created);
            response.FullName = dto.IsAnonymous ? null : $"{customer.FullName}"; // logic for anonymous

            return response;
        }


        public async Task<bool> DeleteAsync(long id, CancellationToken token)
        {
            var comment = await _stadiumCommentsRepository.GetAsync(c => c.Id == id);
            if (comment == null)
                throw new CustomException(404, "comment not found");

            var result = await _stadiumCommentsRepository.DeleteAsync(c => c.Id == comment.Id, token);

            if(!result)
                throw new CustomException(500,"Failed to delete comment");
            return true;
        }

        public async Task<IEnumerable<StadiumCommentResponseDto>> GetByStadiumIdAsync(long stadiumId)
        {
            var comments = await _stadiumCommentsRepository.GetAllAsync();
            var FiletedComents = comments.Where(c => c.StadiumId == stadiumId);
            if (!FiletedComents.Any())
                throw new CustomException(404,$"no comments found for Stadium Id {stadiumId}");

            return FiletedComents.Select(C => _mapper.Map<StadiumCommentResponseDto>(C));
        }

        public async Task<bool> UpdateAsync(long id, StadiumCommentUpdateDto dto, CancellationToken token)
        {
            var Comment = await _stadiumCommentsRepository.GetAsync(c => c.Id == id);

            if (Comment is null)
                throw new CustomException(404, "Comment not found");

            _mapper.Map(dto, Comment);
            var update = await _stadiumCommentsRepository.UpdateAsync(Comment, token);

            if (update is null)
                throw new CustomException(500, "Failed to update comment");
            return true;
        }
    }
}
