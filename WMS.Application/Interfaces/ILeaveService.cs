using WMS.Application.DTOs.Leave;

namespace WMS.Application.Interfaces;

public interface ILeaveService
{
    Task<IEnumerable<LeaveResponseDto>> GetAllAsync();

    Task<LeaveResponseDto?> GetByIdAsync(int id);

    Task<LeaveResponseDto> CreateAsync(CreateLeaveDto dto);

    Task UpdateAsync(int id, UpdateLeaveDto dto);

    Task DeleteAsync(int id);

    // NEW
    Task ApproveAsync(int leaveId, int approvedBy);

    Task RejectAsync(int leaveId, int approvedBy);

    Task CancelAsync(int leaveId);
}