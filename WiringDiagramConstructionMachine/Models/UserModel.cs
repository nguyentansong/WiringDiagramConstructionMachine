using System;
namespace WiringDiagramConstructionMachine.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string Phone { get; set; }
        public bool? IsPaid { get; set; }
        public int? Role { get; set; }
        public bool? IsTrial { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? ExtendDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public DateTime? CreateDate { get; set; }

        public string UserRole
        {
            get
            {
                if (Role == 0)
                {
                    return "Admin";
                }
                else if (IsTrial == true)
                {
                    return "Dùng thử";
                }
                else
                {
                    return "Đã thanh toán";
                }
            }
        }

        public bool IsAdmin
        {
            get
            {
                if (Role == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
