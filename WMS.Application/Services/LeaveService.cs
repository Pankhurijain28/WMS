using AutoMapper;
using WMS.Application.DTOs.Leave;
using WMS.Application.Interfaces;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;

namespace WMS.Application.Services;

public class LeaveService : ILeaveService
{
    private readonly ILeaveRepository _repository;
    private readonly IMapper _mapper;

    public LeaveService(
        ILeaveRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<
        IEnumerable<LeaveResponseDto>>
        GetAllAsync()
    {
        var leaves =
            await _repository.GetAllAsync();

        return _mapper.Map<
            IEnumerable<LeaveResponseDto>>
            (leaves);
    }

    public async Task<LeaveResponseDto?>
        GetByIdAsync(int id)
    {
        var leave =
            await _repository.GetByIdAsync(id);

        if (leave == null)
            return null;

        return _mapper.Map<
            LeaveResponseDto>(leave);
    }

    public async Task<LeaveResponseDto>
        CreateAsync(CreateLeaveDto dto)
    {
        var leave =
            _mapper.Map<Leave>(dto);

        leave.AppliedOn = DateTime.UtcNow;

        leave.Status = "Pending";

        await _repository.AddAsync(leave);

        return _mapper.Map<
            LeaveResponseDto>(leave);
    }

    public async Task UpdateAsync(
        int id,
        UpdateLeaveDto dto)
    {
        var leave =
            await _repository.GetByIdAsync(id);

        if (leave == null)
            throw new Exception(
                "Leave not found");

        _mapper.Map(dto, leave);

        await _repository.UpdateAsync(leave);
    }
    public async Task ApproveAsync(
    int leaveId,
    int approvedBy)
    {
        var leave =
            await _repository.GetByIdAsync(leaveId);

        if (leave == null)
            throw new Exception("Leave not found");

        leave.Status = "Approved";

        leave.ApprovedBy = approvedBy;

        leave.ApprovedOn = DateTime.UtcNow;

        await _repository.UpdateAsync(leave);
    }

    public async Task RejectAsync(
        int leaveId,
        int approvedBy)
    {
        var leave =
            await _repository.GetByIdAsync(leaveId);

        if (leave == null)
            throw new Exception("Leave not found");

        leave.Status = "Rejected";

        leave.ApprovedBy = approvedBy;

        leave.ApprovedOn = DateTime.UtcNow;

        await _repository.UpdateAsync(leave);
    }

    public async Task CancelAsync(
        int leaveId)
    {
        var leave =
            await _repository.GetByIdAsync(leaveId);

        if (leave == null)
            throw new Exception("Leave not found");

        leave.Status = "Cancelled";

        await _repository.UpdateAsync(leave);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}